using CSHM.Widget.Convertor;
using CSHM.Widget.Log;
using CSHM.Widget.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CSHM.Presentation.Base;

namespace CSHM.Widget.Ticketum
{
    public class TicketumWidget : ITicketumWidget
    {
        private readonly ILogWidget _log;
        private readonly IRestWidget _rest;
        public TicketumWidget(ILogWidget log, IRestWidget rest)
        {
            _log = log;
            _rest = rest;

        }


        /// <summary>
        /// گرفتن اولویت ها
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<PriorityTypeViewModel> PriorityTypeSelectAll()
        {
            ResultViewModel<PriorityTypeViewModel> result = new ResultViewModel<PriorityTypeViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/priorityTypeSelectAll";
            try
            {
                result = _rest.CallRestApi<ResultViewModel<PriorityTypeViewModel>>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(PriorityTypeSelectAll), 0);
                return result;
            }
        }

        /// <summary>
        /// گرفتن نوع تیکت ها
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<TicketTypeViewModel> TicketTypeBySection(int sectionID)
        {
            ResultViewModel<TicketTypeViewModel> result = new ResultViewModel<TicketTypeViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/ticketTypeBySection/{sectionID}";
            try
            {
                result = _rest.CallRestApi<ResultViewModel<TicketTypeViewModel>>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(TicketTypeBySection), 0);
                return result;
            }
        }
        /// <summary>
        /// گرفتن بخش ها
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<SectionViewModel> UserSelectSectionsByCurrentUser()
        {
            ResultViewModel<SectionViewModel> result = new ResultViewModel<SectionViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/userSelectSectionsByCurrentUser";
            try
            {
                result = _rest.CallRestApi<ResultViewModel<SectionViewModel>>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(UserSelectSectionsByCurrentUser), 0);
                return result;
            }
        }


        /// <summary>
        /// ایجاد کاربر جدید
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MessageViewModel UserGenerate(TicketUserViewModel entity)
        {
            MessageViewModel result = new MessageViewModel();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            try
            {
              //  result = _rest.CallRestApi<TicketUserViewModel, MessageViewModel>(RestSharp.Method.Post, "api/sandbox/UserGenerate", entity, ServerName.Ticketum, true);
                return result;
            }
            catch (Exception ex)
            {
                errors.Add(new ErrorViewModel
                {
                    ErrorMessage = _log.GetExceptionMessage(ex),
                    ErrorCode = _log.GetExceptionHResult(ex)
                });
                result = new MessageViewModel
                {
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketSubmit), 0);
                return result;
            }
        }


        public MessageViewModel ManageTicketCloseWithComment(CommentViewModel entity)
        {
            MessageViewModel result = new MessageViewModel();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            try
            {
                result = _rest.CallRestApi<CommentViewModel, MessageViewModel>(RestSharp.Method.Post, "api/sandbox/manageTicketCloseWithComment", entity, ServerName.Ticketum, true);
                return result;
            }
            catch (Exception ex)
            {
                errors.Add(new ErrorViewModel
                {
                    ErrorMessage = _log.GetExceptionMessage(ex),
                    ErrorCode = _log.GetExceptionHResult(ex)
                });
                result = new MessageViewModel
                {
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketSubmit), 0);
                return result;
            }
        }


        /// <summary>
        /// پاسخ به یک تیکت
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MessageViewModel MangeTicketResponse(RequestTicketViewModel entity)
        {
            MessageViewModel result = new MessageViewModel();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            try
            {
                result = _rest.CallRestApi<RequestTicketViewModel, MessageViewModel>(RestSharp.Method.Post, "api/sandbox/manageTicketResponse", entity, ServerName.Ticketum, entity.files, true);
                return result;
            }
            catch (Exception ex)
            {
                errors.Add(new ErrorViewModel
                {
                    ErrorMessage = _log.GetExceptionMessage(ex),
                    ErrorCode = _log.GetExceptionHResult(ex)
                });
                result = new MessageViewModel
                {
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketSubmit), 0);
                return result;
            }
        }

        /// <summary>
        /// ثبت تیکت
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<ResponseTicketViewModel> MangeTicketSubmit(RequestTicketViewModel entity)
        {
            ResultViewModel<ResponseTicketViewModel> result = new ResultViewModel<ResponseTicketViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            try
            {
               result = _rest.CallRestApi<RequestTicketViewModel, ResultViewModel<ResponseTicketViewModel>>(RestSharp.Method.Post, "api/sandbox/manageTicketSubmit", entity, ServerName.Ticketum, entity.files, true);
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketSubmit), 0);
                return result;
            }
        }



        /// <summary>
        /// فراخوانی لیستی از تیکت ها
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<TicketViewModel> MangeTicketList(string username, int pageNumber, int pageSize)
        {
            ResultViewModel<TicketViewModel> result = new ResultViewModel<TicketViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/manageTicketList/{username}/{pageNumber}/{pageSize}";
            try
            {
                result = _rest.CallRestApi<ResultViewModel<TicketViewModel>>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);
                if (result.TotalCount == 0)
                {
                    result.List = new List<TicketViewModel>();
                }
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketList), 0);
                return result;
            }
        }


        /// <summary>
        /// فراخوانی لیستی از تیکت ها
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<TicketViewModel> MangeTicketList(FilterViewModel filter, string username, int pageNumber, int pageSize)
        {
            ResultViewModel<TicketViewModel> result = new ResultViewModel<TicketViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/manageTicketList/{username}/{pageNumber}/{pageSize}";
            try
            {
                result = _rest.CallRestApi<FilterViewModel, ResultViewModel<TicketViewModel>>(RestSharp.Method.Post, endPoint, filter, ServerName.Ticketum);
                if (result.TotalCount == 0)
                {
                    result.List = new List<TicketViewModel>();
                }
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketList), 0);
                return result;
            }
        }

        /// <summary>
        /// فراخوانی جزییات یک تیکت
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<TicketDetailViewModel> MangeTicketHistory(int ticketID, string username)
        {
            ResultViewModel<TicketDetailViewModel> result = new ResultViewModel<TicketDetailViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/manageTicketHistory/{ticketID}/{username}";
            try
            {
                result = _rest.CallRestApi<ResultViewModel<TicketDetailViewModel>>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketSubmit), 0);
                return result;
            }
        }




        /// <summary>
        /// بررسی وضعیت یک تیکت براساس کد رهگیری
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultViewModel<FollowUpViewModel> MangeTicketFollowUp(string ticketCode)
        {
            ResultViewModel<FollowUpViewModel> result = new ResultViewModel<FollowUpViewModel>();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            var endPoint = $"api/sandbox/manageTicketFollowUp/{ticketCode}";
            try
            {
                result = _rest.CallRestApi<ResultViewModel<FollowUpViewModel>>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);
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
                    Status = "Error",
                    Title = "Exception",
                    Message = "استثنای درونی ناشناخته در دریافت اطلاعات سند",
                    Errors = errors,
                    ID = -1,
                    Value = ""
                };
                _log.ExceptionLog(ex, nameof(MangeTicketFollowUp), 0);
                return result;
            }
        }




        /// <summary>
        /// دانلود فایل مربوط به جزییات یک تیکت
        /// </summary>
        /// <param name="documentFileID"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public FileContentResult ManageTicketDownload(int documentFileID, string username)
        {
            FileContentResult result;
            var endPoint = $"api/sandbox/manageTicketDownload/{documentFileID}/{username}";
            try
            {
                result = _rest.CallRestApi<FileContentResult>(RestSharp.Method.Get, endPoint, ServerName.Ticketum);

                return result;
            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, nameof(MangeTicketFollowUp), 0);
                return null;
            }
        }


    }
}
