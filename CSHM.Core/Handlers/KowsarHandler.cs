using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Kowsar;
using CSHM.Widget.Calendar;
using CSHM.Widget.Dapper;
using Dapper;
using Stimulsoft.System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Handlers
{
    public class KowsarHandler : IKowsarHandler
    {
        private readonly IDapperWidget _dapper;

        public KowsarHandler(IDapperWidget dapper)
        {
            _dapper = dapper;
        }

        public   MessageViewModel InsertToGood(GoodViewModel goodViewModel)
        {
            MessageViewModel result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@goodCode",  goodViewModel.GoodCode);
            parameters.Add("@goodName", goodViewModel.GoodName);
            parameters.Add("@goodTypeCode", goodViewModel.GoodTypeCode);
            parameters.Add("@maxSellPrice", goodViewModel.MaxSellPrice);
            parameters.Add("@minSellPrice", goodViewModel.MinSellPrice);
            parameters.Add("@groups", goodViewModel.Groups);
            parameters.Add("@stackAmount", goodViewModel.StackAmount);
            parameters.Add("@image", goodViewModel.Image);
            parameters.Add("@cachedBarCode", goodViewModel.CachedBarCode);
            parameters.Add("@active", goodViewModel.Active);
           var x =  _dapper.CallProcedure<int>("sp_insertToGood", parameters);

            if (x[0] > 0) 
            {
                result = new MessageViewModel
                {
                    Status = "Success",
                    Message = "کالا بدرستی افزوده شد"

                };
            }
            else
            {
                result = new MessageViewModel
                {
                    Status = "Failed",
                    Message = "این کد کالا در حال حاضر موجود است"
                };

            }

            return result;

        }




    }
}
