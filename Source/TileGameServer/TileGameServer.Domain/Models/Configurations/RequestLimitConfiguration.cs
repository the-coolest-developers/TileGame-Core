using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileGameServer.Domain.Models.Configurations
{
    public class RequestLimitConfiguration
    {
        public int MaxRequestLimit { get; set; }
        public int MinRequestLimit { get; set; }
        public int Default { get; set; }
    }
}
