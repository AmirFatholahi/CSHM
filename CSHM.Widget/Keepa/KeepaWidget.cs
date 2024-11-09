using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSHM.Widget.Dapper;
using CSHM.Widget.Keepa;
using CSHM.Widget.Log;
using CSHM.Widget.Rest;
using CSHM.Widget.Method;
using CSHM.Presentation.Base;
using CSHM.Presentations.User;

namespace CSHM.Widget.Keepa
{
    public class KeepaWidget:IKeepaWidget
    {
        private readonly IRestWidget _rest;
        private readonly ILogWidget _log;
        private readonly IDapperWidget _dapper;



        public KeepaWidget(IRestWidget rest, ILogWidget log, IDapperWidget dapper)
        {
            _rest = rest;
            _log = log;
            _dapper = dapper;
        }



        public ResultViewModel<KeepaUserWalletViewModel> CreateWallet(UserViewModel entity)
        {
            ResultViewModel<KeepaUserWalletViewModel> result = new ResultViewModel<KeepaUserWalletViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/keepa/createWallet";
            try
            {
                var data = _rest.CallRestApi<UserViewModel, ResultViewModel<KeepaUserWalletViewModel>>(RestSharp.Method.Post, endPoint, entity, ServerName.HUB);
                result.Message = new MessageViewModel();
                result.Message = data.Message;
                result.Result = new KeepaUserWalletViewModel();
                result.Result = data.Result;
                result.List = new List<KeepaUserWalletViewModel>();
                result.List = data.List;
                return result;
            }
            catch (Exception ex)
            {
                errors.Add(new ErrorViewModel
                {
                    ErrorMessage = _log.GetExceptionMessage(ex),
                    ErrorCode = _log.GetExceptionHResult(ex)
                });
                result.Message = new MessageViewModel
                {
                    Status = "error",
                    Title = "error",
                    Message = "استثنای درونی ناشناخته در ارتباط با کیپا",
                    Errors = errors,
                    ID = -3,
                    Value = ""
                };
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0);
                return result;
            }
        }
    }
}
