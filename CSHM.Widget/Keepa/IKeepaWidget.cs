using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHM.Presentations.User;
using CSHM.Presentation.Base;
using CSHM.Widget.Keepa;


namespace CSHM.Widget.Keepa
{
    public interface IKeepaWidget
    {
        ResultViewModel<KeepaUserWalletViewModel> CreateWallet(UserViewModel entity);
    }
}
