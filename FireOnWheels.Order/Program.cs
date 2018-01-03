using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireOnWheels.Order
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static async Task AsyncMain() {
            Console.Title = "FireOnWheels-Alex-OrderServiceHost";
            LogManager.Use<DefaultFactory>().Level(LogLevel.Info);

            var endpointConfiguration = new EndpointConfiguration("FireOnWheels.Order.Endpoint");
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            try
            {
                Console.WriteLine("Presione cualquier tecla para continuar");
                Console.ReadKey();

            }
            finally {
                await endpointInstance.Stop().ConfigureAwait(false);

            }
            


        }
    }
}
