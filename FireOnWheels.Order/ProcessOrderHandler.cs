using FireOnWheels.Messages;
using FireOnWheels.Order.Helper;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireOnWheels.Order
{
    public class ProcessOrderHandler : IHandleMessages<ProcessOrderCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProcessOrderHandler));
        public async Task Handle(ProcessOrderCommand message, IMessageHandlerContext context)
        {
            Logger.InfoFormat("Orden Recibida de Dirección: {0} a Dirección: {1}", message.AddressFrom, message.AddressTo);
            EmailSender.SendEmailToDispatch(message);
        }
    }
}
