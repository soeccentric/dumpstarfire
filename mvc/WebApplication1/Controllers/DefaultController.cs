using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive;
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
		[Route]
		public ViewResult Index()
		{
			return View();
		}

        [Route("messages")]
	    public async Task Messages()
	    {
	        Response.ContentType = "text/event-stream";
            var liveConnection = new TaskCompletionSource<bool>();
            var queue = new BlockingCollection<string>();

	        var events = _messageSource.Messages();
	        events.Connect();

	        var subscription = events
	            .TakeWhile(x => Response.IsClientConnected)
	            .Finally(() => queue.CompleteAdding())
	            .SubscribeOn(TaskPoolScheduler.Default)
	            .Subscribe(
	                serverEvent => queue.Add(serverEvent),
	                ex => liveConnection.TrySetException(ex),
	                () => liveConnection.TrySetResult(false)
	            );

	        using (subscription)
	        {
	            var consumer = Task.Run(async () =>
	            {
                    foreach (string message in queue.GetConsumingEnumerable())
                    {
                        var success = await _eventWriter.WriteEvent(message, Response).ConfigureAwait(false);
                        if (!success)
                        {
                            break;
                        }
                    }
                });
	            await consumer.ConfigureAwait(false);
	            await liveConnection.Task.ConfigureAwait(false);
	        }
	    }
	}
}