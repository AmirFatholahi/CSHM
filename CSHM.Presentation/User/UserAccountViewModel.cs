using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentations.User;

public class UserAccountViewModel
{
    public int ID { get; set; }

    public int AccountTypeID { get; set; }

    public string AccountTypeTitle { get; set; }

    public int BankID { get; set; }

    public string BankName { get; set; }

    public int UserID { get; set; }

    public string NID { get; set; }

    public string AccountNumber { get; set; }

    public string IBAN { get; set; }

    public string Title { get; set; }

    public string DisplayTitle { get; set; }

    public long BalanceAmount { get; set; }  // مانده حساب

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; }

  
}