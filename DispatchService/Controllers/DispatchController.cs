using DispatchService.Helper;
using DispatchService.Models;
using System.Web.Http;

namespace DispatchService.Controllers
{
    public class DispatchController : ApiController
    {
        public void Post(Order order)
        {
            EmailSender.SendEmailToDispatch(order);
        }
    }
}
