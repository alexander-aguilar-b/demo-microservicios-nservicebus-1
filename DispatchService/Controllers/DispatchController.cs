using DispatchService.Models;
using FireOnWheels.Messages;
using FireOnWheels.Order.Helper;
using NServiceBus;
using System.Threading.Tasks;
using System.Web.Http;

namespace DispatchService.Controllers
{
    public class DispatchController : ApiController
    {
        private readonly IEndpointInstance endpoint;

        public DispatchController(IEndpointInstance enpoint) {
            this.endpoint = endpoint;
        }

        public async Task Post(Models.Order order)
        {
            await endpoint.Send("FireOnWheels.Order.Endpoint", new ProcessOrderCommand
            {
                AddressFrom = order.AddressFrom,
                AddressTo = order.AddressTo,
                Price = order.Price,
                Weight = order.Weight
            }).ConfigureAwait(false);

            //EmailSender.SendEmailToDispatch(order);
        }
    }
}
