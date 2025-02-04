using Microsoft.AspNetCore.Hosting;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentation.Notification;
using CSHM.Core.Presentations.User;
using CSHM.Presentation.Resources;
using CSHM.Core.Services;
using CSHM.Core.Services.Interfaces;
using CSHM.Domain;
using CSHM.Widget.Calendar;
using CSHM.Widget.Dapper;
using CSHM.Widget.Email;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.Redis;
using CSHM.Widget.SMS;
using CSHM.Widget.Stream;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.OTP;

namespace CSHM.Core.Handlers;

public class NotificationHandler : INotificationHandler
{
    private readonly ILogWidget _log;
    private readonly IDapperWidget _dapper;
    private readonly INotificationDraftService _draftService;
    private readonly INotificationService _notificationService;
    private readonly INotificationOwnerService _ownerService;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ISMSWidget _sms;
    private readonly IEmailWidget _email;
    private readonly IUserService _userService;
    private readonly IRedisWidget _redis;

    public NotificationHandler(ILogWidget log, IDapperWidget dapper, INotificationDraftService draftService, INotificationService notificationService,
        INotificationOwnerService ownerService, IHostingEnvironment hostingEnvironment, ISMSWidget sms, IEmailWidget email, IUserService userService,IRedisWidget redisWidget)
    {
        _log = log;
        _dapper = dapper;
        _draftService = draftService;
        _notificationService = notificationService;
        _ownerService = ownerService;
        _hostingEnvironment = hostingEnvironment;
        _sms = sms;
        _email = email;
        _userService = userService;
        _redis = redisWidget;
    }


    /// <summary>
    /// دریافت پیام‌های یک کاربر
    /// </summary>
    /// <param name="activate">وضعیت نوتیفیکیشن اونر پیام</param>
    /// <param name="userID">شناسه مالک پیام</param>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public ResultViewModel<NotificationViewModel> SelectNotificationsByUser(bool? activate, int userID, string filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<NotificationViewModel>();
        try
        {
            Expression<Func<NotificationOwner, bool>> condition = x => x.UserID == userID
                                                                       && x.Notification.NotificationTypeID == 3
                                                                       && (string.IsNullOrWhiteSpace(filter) || x.SeenDateTime.Contains(filter));

            var items = _ownerService.GetAll(activate, condition, pageNumber, pageSize, o => o.ID, true);

            result.List = items.Select(x => new NotificationViewModel()
            {
                ID = x.NotificationID,
                MessageContent = x.Notification.MessageContent,
                NotificationOwnerID = x.ID,
                NotificationTypeID = x.Notification.NotificationTypeID,
                NotificationTypeTitle = x.Notification.NotificationType.Title,
                Path = x.Notification.Path,
                Status = x.Notification.Status,
                Subject = x.Notification.Subject,
                SeenDateTime = x.SeenDateTime,
                CreationDateTime = CalenderWidget.ToJalaliDateTime(x.CreationDateTime),
                CreatorID = x.Notification.CreatorID,
                CreatorName = x.Notification.Creator.FullName,
                IsActive = x.IsActive
            }).ToList();

            result.TotalCount = _ownerService.Count(activate, condition);

            result.Message = result.TotalCount > 0
                ? new MessageViewModel { Status = Statuses.Success }
                : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }


    /// <summary>
    /// دریافت سه پیام آخر خوانده نشده یک کاربر
    /// </summary>
    /// <param name="userID">شناسه مالک پیام</param>
    /// <returns></returns>
    public List<NotificationViewModel> SelectTopNotificationsByUser(int userID)
    {
        List<NotificationViewModel> result = new List<NotificationViewModel>();
        var items = _ownerService.GetAll(true, x => x.UserID == userID && x.Notification.NotificationTypeID == 3, 1, 3, o => o.ID, true);
        result = items.Select(x => new NotificationViewModel()
        {
            ID = x.NotificationID,
            MessageContent = x.Notification.MessageContent,
            NotificationOwnerID = x.ID,
            NotificationTypeID = x.Notification.NotificationTypeID,
            NotificationTypeTitle = x.Notification.NotificationType.Title,
            Path = x.Notification.Path,
            Status = x.Notification.Status,
            Subject = x.Notification.Subject,
            SeenDateTime = x.SeenDateTime,
            CreationDateTime = CalenderWidget.ToJalaliDateTime(x.CreationDateTime),
            CreatorID = x.Notification.CreatorID,
            CreatorName = x.Notification.Creator.FullName,
            IsActive = x.IsActive
        }).ToList();
        return result;
    }


    /// <summary>
    /// دریافت محتویات یک پیام
    /// </summary>
    /// <param name="notificationOwnerID">شناسه پیام</param>
    /// <returns></returns>

    public NotificationViewModel Select(int notificationOwnerID)
    {
        NotificationViewModel result;
        var item = _ownerService.GetByID(notificationOwnerID);
        if (item != null)
        {
            result = new NotificationViewModel()
            {
                ID = item.NotificationID,
                MessageContent = item.Notification.MessageContent,
                NotificationOwnerID = item.ID,
                NotificationTypeID = item.Notification.NotificationTypeID,
                NotificationTypeTitle = item.Notification.NotificationType.Title,
                Path = item.Notification.Path,
                Status = item.Notification.Status,
                Subject = item.Notification.Subject,
                SeenDateTime = item.SeenDateTime,
                CreationDateTime = CalenderWidget.ToJalaliDateTime(item.CreationDateTime),
                CreatorID = item.Notification.CreatorID,
                CreatorName = item.Notification.Creator.FullName,
                IsActive = item.IsActive
            };
            return result;
        }
        else
        {
            result = new NotificationViewModel()
            {
                MessageContent = "این پیام یافت نشد"
            };
            return result;
        }
    }



    /// <summary>
    /// ارسال پیام به یک کاربر از طریق پیش نویس
    /// </summary>
    /// <param name="draftID">شناسه پیش نویس</param>
    /// <param name="list">مقادیر پیش نویس</param>
    /// <param name="userID">شناسه دریافت کننده پیام</param>
    /// <param name="creatorID">شناسه ایجاد کننده پیام</param>
    public void SendNotification(int draftID, List<KeyValueViewModel> list, int userID, int creatorID)
    {
        var draft = _draftService.GetByID(draftID);
        if (draft != null)
        {
            string content = _draftService.ContentConstructor(draft.MessageBody, list);
            if (!string.IsNullOrWhiteSpace(content))
            {
                var resAdd = _notificationService.Add(new Notification()
                {
                    MessageContent = content,
                    NotificationTypeID = 3,
                    Path = draft.Path,
                    Status = draft.Status,
                    Subject = draft.Subject,
                    Attachments="",
                    IsActive = true
                }, creatorID);
                if (resAdd.Status == Statuses.Success)
                {
                    _ownerService.Add(new NotificationOwner()
                    {
                        NotificationID = resAdd.ID,
                        SeenDateTime = String.Empty,
                        UserID = userID,
                        IsActive = true
                    }, creatorID);
                }
                else
                {
                    //TODO: log error
                }
            }
        }
    }


    /// <summary>
    /// ایجاد پیام انبوه به کاربران از طریق پیش نویس
    /// </summary>
    /// <param name="draftID">شناسه پیش نویس</param>
    /// <param name="list">مقادیر پیش نویس</param>
    /// <param name="userIDs">لیست شناسه کاربران دریافت کننده پیام</param>
    /// <param name="creatorID">شناسه کاربر ایجاد کننده پیام</param>
    public void SendNotification(int draftID, List<KeyValueViewModel> list, List<int> userIDs, int creatorID)
    {
        var draft = _draftService.GetByID(draftID);
        if (draft != null)
        {
            string content = _draftService.ContentConstructor(draft.MessageBody, list);
            if (!string.IsNullOrWhiteSpace(content))
            {
                var resAdd = _notificationService.Add(new Notification()
                {
                    MessageContent = content,
                    NotificationTypeID = 3,
                    Path = draft.Path,
                    Status = draft.Status,
                    Subject = draft.Subject,
                    IsActive = true
                }, creatorID);
                if (resAdd.Status == Statuses.Success)
                {
                    foreach (var item in userIDs)
                    {
                        _ownerService.Create(new NotificationOwner()
                        {
                            NotificationID = resAdd.ID,
                            SeenDateTime = String.Empty,
                            UserID = item,
                            IsActive = true
                        }, creatorID);
                    }
                    _ownerService.Save();
                }
                else
                {
                    //TODO: log error
                }
            }
        }
    }


    /// <summary>
    /// ارسال پیام به کاربر بدون پیش نویس
    /// </summary>
    /// <param name="notification">پیام</param>
    /// <param name="userID">شناسه کاربر دریافت کننده پیام</param>
    /// <param name="creatorID">شناسه کاربر ارسال کننده پیام</param>
    public void SendNotification(Notification notification, int userID, int creatorID)
    {
        if (!string.IsNullOrWhiteSpace(notification.MessageContent))
        {
            var resAdd = _notificationService.Add(notification, creatorID);
            if (resAdd.Status == Statuses.Success)
            {
                _ownerService.Add(new NotificationOwner()
                {
                    NotificationID = resAdd.ID,
                    SeenDateTime = String.Empty,
                    UserID = userID,
                    IsActive = true
                }, creatorID);
            }
            else
            {
                //TODO: log error
            }
        }
    }


    /// <summary>
    /// ارسال پیام انبوه به کاربران بدون پیش نویس
    /// </summary>
    /// <param name="notification">پیام</param>
    /// <param name="userIDs">لیست شناسه کاربران دریافت کننده پیام</param>
    /// <param name="creatorID">شناسه کاربر ایجاد کننده پیام</param>
    public void SendNotification(Notification notification, List<int> userIDs, int creatorID)
    {
        if (!string.IsNullOrWhiteSpace(notification.MessageContent))
        {
            var resAdd = _notificationService.Add(notification, creatorID);
            if (resAdd.Status == Statuses.Success)
            {
                foreach (var item in userIDs)
                {
                    _ownerService.Create(new NotificationOwner()
                    {
                        NotificationID = resAdd.ID,
                        SeenDateTime = String.Empty,
                        UserID = item,
                        IsActive = true
                    }, creatorID);
                }
                _ownerService.Save();
            }
            else
            {
                //TODO: log error
            }
        }
    }


    /// <summary>
    /// ارسال کننده پیام ایمیل
    /// </summary>
    /// <param name="draftID">شناسه پیش نویس</param>
    /// <param name="to">گیرنده</param>
    /// <param name="list">لیست منغیرات</param>
    /// <param name="userID">شناسه کاربر دریافت کننده</param>
    /// <param name="creatorID">شناسه کاربر ارسال کننده</param>
    public void Email(int draftID, string to, List<KeyValueViewModel> vars, Dictionary<string, string> attachments, int userID, int creatorID)
    {
        try
        {
            var draft = _draftService.GetByID(draftID);
            if (draft != null)
            {
                string body = String.Empty;
                var root = _hostingEnvironment.ContentRootPath;
                var file = Path.Combine(root, draft.MessageBody);
                if (File.Exists(file))
                {
                    StreamWidget oStream = new StreamWidget();
                    body = oStream.Read(file);
                    if (vars != null)
                    {
                        foreach (var item in vars)
                        {
                            body = body.Replace(item.Key, item.Value);
                        }
                    }

                    var sent = _email.Send(to, draft.Subject, body, attachments, true);

                    if (sent == true)
                    {
                        var res = _notificationService.Add(new Notification()
                        {
                            MessageContent = draft.MessageBody,
                            NotificationTypeID = 4,
                            Path = to,
                            Status = draft.Status,
                            Subject = draft.Subject,
                            IsActive = true
                        }, creatorID);
                        if (res.Status == Statuses.Success)
                        {
                            _ownerService.Add(new NotificationOwner()
                            {
                                NotificationID = res.ID,
                                SeenDateTime = String.Empty,
                                UserID = userID,
                                IsActive = true
                            }, creatorID);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
        }
    }


    /// <summary>
    /// ارسال کننده پیامک رمز پویا
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cellphone"></param>
    /// <param name="secretKey"></param>
    /// <returns></returns>
    public MessageViewModel SendOTP(OTPViewModel entity)
    {
        MessageViewModel result;
        var user = _userService.GetByID(entity.UserID);
        if (user != null)
        {
            entity.Username = user.UserName;
            entity.SecretKey = user.SecretKey;
            entity.Cellphone = user.Cellphone;
            var generation = _redis.GenerateOTP(entity);
            if (generation.Status == Statuses.Success)
            {
              //  _log.ResponseLog("OTP", "SendOTP", "901", generation.Value, generation.Message, generation.Status, DateTime.Now);
                string otp = generation.Value;
                string sms = $"OTPWallet|{entity.Cellphone}|{entity.Content}|{otp}";
                result = SendSms(sms);
                return result;
            }
            else
            {

                result = generation;
             //   _log.ResponseLog("OTP", "SendOTP", "901", generation.Value, generation.Errors[0]?.ErrorMessage, generation.Status, DateTime.Now);
                return result;
            }
        }
        else
        {
            result = new MessageViewModel
            {
                Status = Statuses.Error,
                Title = Titles.Error,
                Message = Messages.UserNotFound,
                Errors = null,
                ID = 0,
                Value = ""
            };
          //  _log.ResponseLog("OTP", "SendOTP", "901", "", result.Message, result.Status, DateTime.Now);
            return result;
        }

    }


    /// <summary>
    /// متد ارسال پیام کوتاه به یک کاربر خاص از طریق پیش نویس
    /// </summary>
    /// <param name="draftID">شناسه پیش نویس</param>
    /// <param name="cellphone">شماره همراه دریافت کننده</param>
    /// <param name="content">متن پیام کوتاه</param>
    /// <param name="userID">شناسه کاربر دریافت کننده پیام</param>
    /// <param name="creatorID">شناسه کاربر ایجاد کننده پیام</param>
    public void SMS(int draftID, string cellphone, List<KeyValueViewModel> content, int userID, int creatorID)
    {
        try
        {
            var draft = _draftService.GetByID(draftID);
            if (draft != null)
            {
                string body = draft.MessageBody;
                string SmsContent = $"{draft.Path}|{cellphone}";
                if (content != null)
                {
                    foreach (var item in content)
                    {
                        if (draftID == 9 || draftID == 10)
                        {
                            body = body.Replace(item.Key, " xxx ");
                        }
                        else
                        {
                            body = body.Replace(item.Key, item.Value);
                        }

                        SmsContent = SmsContent + "|" + item.Value;
                    }
                }

                //TODO:
                var sent = _sms.Send(SmsContent);
                if (sent.MessageID > 0)
                {
                    var res = _notificationService.Add(new Notification()
                    {
                        MessageContent = body,
                        NotificationTypeID = 4,
                        Path = cellphone,
                        Status = sent.MessageID.ToString(),
                        Subject = draft.Subject,
                        Attachments = sent.TemplateID.ToString(),
                        IsActive = true
                    }, creatorID);
                    if (res.Status == Statuses.Success)
                    {
                        _ownerService.Add(new NotificationOwner()
                        {
                            NotificationID = res.ID,
                            SeenDateTime = CalenderWidget.ToJalaliDateTime(DateTime.Now),
                            UserID = userID,
                            IsActive = true
                        }, creatorID);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
        }

    }


    /// <summary>
    /// ارسال پیامک خام
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public MessageViewModel SendSms(string content)
    {
        MessageViewModel result;
        var sent = _sms.Send(content);
        if (sent.MessageID > 0)
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Success,
                Title = Titles.SMS,
                Message = Messages.SMSSent,
                Errors = null,
                ID = 1000,
                Value = sent.MessageID.ToString()
            };
        }
        else
        {
            result = new MessageViewModel()
            {
                Status = Statuses.Error,
                Title = Titles.SMS,
                Message = Messages.SMSFailed,
                Errors = null,
                ID = 0,
                Value = sent.Message
            };
        }

        return result;
    }

}