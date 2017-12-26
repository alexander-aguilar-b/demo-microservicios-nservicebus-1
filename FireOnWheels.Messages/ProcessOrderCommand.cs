using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireOnWheels.Messages
{
    public class ProcessOrderCommand : ICommand
    {
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
    }
}
