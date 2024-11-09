using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentations.User
{
    public class UserForbiddenTransactionTypeViewModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string FullName { get; set; }

        public int TransactionTypeID { get; set; }

        public string TransactionTypeTitle { get; set; }

        public bool IsActive { get; set; }
    }
}
