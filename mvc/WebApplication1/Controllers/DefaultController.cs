using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IMessageSource _messageSource;
        private readonly IEventWriter _eventWriter;

        public DefaultController(IMessageSource messageSource, IEventWriter eventWriter)
        {
            _messageSource = messageSource;
            _eventWriter = eventWriter;
        }

        // Maps to http://localhost:61589/
        //cancelation tokens
        //react .js front end
        //facebook fixed data table.
        [Route]
        public ViewResult Index()
        {
            return View();
        }

        [Route("messages")]
        public async Task Messages()
        {
            Response.ContentType = "text/event-stream";
            Response.CacheControl = "no-cache";
            Response.Headers.Add("Connection", "keep-alive");
            Response.StatusCode = 200;
            await Response.FlushAsync();

            var queue = new BlockingCollection<string>(); //for producer/consumer pattern
            //producer adds items to the collection
            //consumer uses the getconsumingenuerable

            var liveConnection = Observable.Create<string>(
                    o => Response.ClientDisconnectedToken.Register(o.OnCompleted))
                .Finally(() => queue.CompleteAdding());

            var events = _messageSource.Messages();
            events.Connect();

            var subscription = events
                .TakeUntil(liveConnection)
                .SubscribeOn(TaskPoolScheduler.Default)
                // if you don't do this it won't be a non blocking subscribe and you will never hit the using code.
                .Subscribe(serverEvent => queue.Add(serverEvent));

            using (subscription)
            {
                var consumer = Task.Run(async () =>
                {
                    foreach (string message in queue.GetConsumingEnumerable())
                        //forever looping until complete adding is called.
                    {
                        var success = await _eventWriter.WriteEvent(message, Response).ConfigureAwait(false);
                        if (!success)
                        {
                            break;
                        }
                    }
                });
                await consumer.ConfigureAwait(false);
            }
        }
    }
}