using CSHM.Presentation.Base;
using CSHM.Presentation.Kowsar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Handlers.Interfaces
{
    public interface IKowsarHandler
    {
        public MessageViewModel InsertToGood(List<GoodViewModel> goodViewModel);

        public MessageViewModel DeleteGood(List<GoodViewModel> goodViewModel);

        public MessageViewModel Common(List<CommonViewModel> commonViewModel);
    }
}
