using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Redis
{
    public class RedisConnectionStringViewModel
    {
        public string ConnectionStringMaster { get; set; }
        public string ConnectionStringUrl1 { get; set; }
        public string ConnectionStringUrl2 { get; set; }
        public int DatabaseNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
