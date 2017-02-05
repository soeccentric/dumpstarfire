using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Services
{
    public interface IEventWriter
    {
        Task<bool> WriteEvent(string message, HttpResponseBase response);
    }
}