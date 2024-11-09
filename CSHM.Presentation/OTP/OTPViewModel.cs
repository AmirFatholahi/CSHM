using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentation.OTP
{
    public class OTPViewModel
    {
        public int UserID { get; set; }

        public string WalletTitle { get; set; }

        public string Cellphone { get; set; }

        public string Username { get; set; }

        public string SecretKey { get; set; }

        public string OTP { get; set; }

        public string Action { get; set; }

        public string Content { get; set; }

        public long Amount { get; set; }

        public int MerchantID { get; set; }

        public string MerchantFullName { get; set; }

        public int TerminalID { get; set; }

        public string TerminalNumber { get; set; }

        public string ApiKey { get; set; }

        public DateTime LastSentDateTime { get; set; }


    }
}
