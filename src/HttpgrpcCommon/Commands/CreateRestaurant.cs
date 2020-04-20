using Httpgrpc.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace httpgrpc.common.Commands
{
    public class CreateRestaurant : ICommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
