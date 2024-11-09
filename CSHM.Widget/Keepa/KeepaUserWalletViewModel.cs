using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Keepa
{
    public class KeepaUserWalletViewModel
    {
        public bool IsAssigned { get; set; }

        public int AssignedID { get; set; }

        public int TypeID { get; set; }

        public string Title { get; set; }

        public string Identifier { get; set; }

        public bool IsRevolving { get; set; }

        public decimal MaximumCredit { get; set; }

        public decimal Credit { get; set; }
    }
}
