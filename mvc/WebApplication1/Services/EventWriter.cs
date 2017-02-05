using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Services
{
    public class EventWriter : IEventWriter
    {
        public async Task<bool> WriteEvent(string message, HttpResponseBase response)
        {
            //TODO: this needs to write out according to server sent events spec
            string output = $"data: {message}\n\n";
            var bytes = Encoding.UTF8.GetBytes(output);
            try
            {
                await response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                await response.FlushAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}