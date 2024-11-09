using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Reflection;
using CSHM.Presentation.Base;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Config;
using CSHM.Widget.Excel;
using CSHM.Widget.Image;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.Redis;
using CSHM.Presentation.Resources;
using CSHM.Presentations.Media;

namespace CSHM.Core.Services;

public class MediaService : Repository<Media, MediaViewModel>, IMediaService
{

    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly IExcelWidget _excel;
    private readonly IRedisWidget _redis;
    private readonly string _baseURL;
    private readonly IWebHostEnvironment _environment;
    public MediaService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IRedisWidget redis, IWebHostEnvironment environment) : base(context, log, mapper)
    {
        _log = log;
        _mapper = mapper;
        _excel = excel;
        _redis = redis;
        _baseURL = ConfigWidget.GetConfigValue<string>("PublishedServerAddresses:0:Address");
        _environment = environment;
    }


    /// <summary>
    /// لیست فایل های یک موجودیت
    /// </summary>
    /// <param name="entityName">نام موجودیت</param>
    /// <param name="entityID">شناسه موجودیت</param>
    /// <param name="activate"></param>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public ResultViewModel<MediaViewModel> SelectAll(string entityName, int entityID, bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        ResultViewModel<MediaViewModel> result = new ResultViewModel<MediaViewModel>();
        try
        {
            IQueryable<Media> items;
            Expression<Func<Media, bool>> condition = x => x.EntityName == entityName && x.EntityID == entityID && (string.IsNullOrWhiteSpace(filter) || x.DisplayName.Contains(filter) || x.FileName.Contains(filter));
            items = GetAll(activate, condition, pageNumber, pageSize, o => o.IsDefault, true);
            result.List = MapToViewModel(items);
            foreach (var item in result.List)
            {
                var fileName = SetMedia(item);
                item.Url = $"{_baseURL}media/{fileName}{item.ExtensionName}";
            }
            result.TotalCount = Count(activate, condition);

            result.Message = result.TotalCount > 0
                ? new MessageViewModel { Status = Statuses.Success }
                : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }


    public override ResultViewModel<MediaViewModel> SelectAll(bool? activate, string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<MediaViewModel>();
        try
        {
            IQueryable<Media> items;
            Expression<Func<Media, bool>> condition = x => string.IsNullOrWhiteSpace(filter) || x.DisplayName.Contains(filter) || x.FileName.Contains(filter);
            items = GetAll(activate, condition, pageNumber, pageSize);
            result.List = MapToViewModel(items);
            foreach (var item in result.List)
            {
                var fileName = SetMedia(item);
                item.Url = $"{_baseURL}media/{fileName}{item.ExtensionName}";
            }

            result.TotalCount = Count(activate, condition);

            result.Message = result.TotalCount > 0
                ? new MessageViewModel { Status = Statuses.Success }
                : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }

    /// <summary>
    /// ایجاد نشانی تصویر در ردیس
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Guid SetMedia(MediaViewModel entity)
    {
        //redis
        var guid = Guid.NewGuid();
        MediaTempViewModel temp = new MediaTempViewModel()
        {
            EntityID = entity.EntityID,
            Entityname = entity.EntityName,
            ItemID = entity.ID,
            ExtensionName = entity.ExtensionName,
            FileName = entity.FileName
        };
        _redis.SetData("MEDIA-" + guid.ToString(), temp, DateTimeOffset.Now.AddMinutes(1));
        return guid;
    }

    /// <summary>
    /// دریافت نشانی تصویر از ردیس
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public MediaTempViewModel GetMedia(string fileName)
    {
        fileName = fileName.Split('.')[0];
        var result = _redis.GetData<MediaTempViewModel>("MEDIA-" + fileName);
        if (result != null)
        {
            _redis.RemoveData("MEDIA-" + fileName);
        }
        return result;
    }

    /// <summary>
    /// عدم وجود تصویر 
    /// </summary>
    /// <returns></returns>
    public FileResultViewModel ImageNotFound()
    {
        FileResultViewModel result;
        var path = _environment.WebRootPath + "\\images\\image-not-found.png";
        var notFoundArray = ImageWidget.ToBinary(path);
        result = new FileResultViewModel
        {
            FileName = "image-not-found.png",
            ContentType = "image/png",
            Stream = new MemoryStream(notFoundArray)
        };
        return result;
    }



    public ResultViewModel<MediaViewModel> SelectAllByEntity<T>(bool? activate, int entityID, bool? isDefault = null, bool? showOnlyConfirmed = null,
        string? filter = null, int? pageNumber = null, int pageSize = 20)
    {
        var result = new ResultViewModel<MediaViewModel>();
        try
        {
            IQueryable<Media> items;
            Expression<Func<Media, bool>> condition = x =>
                (string.IsNullOrWhiteSpace(filter) || x.DisplayName.Contains(filter) || x.FileName.Contains(filter)) &&
                x.EntityName == typeof(T).Name &&
                x.EntityID == entityID &&
                (showOnlyConfirmed == null || x.IsConfirm == showOnlyConfirmed) &&
                (isDefault == null || x.IsDefault == isDefault);

            items = GetAll(activate, condition, pageNumber, pageSize);


            result.List = MapToViewModel(items);

            result.TotalCount = Count(activate, condition);

            result.Message = result.TotalCount > 0
                ? new MessageViewModel { Status = Statuses.Success }
                : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }






    /// <summary>
    /// پیشفرض نمودن یک مدیا
    /// </summary>
    /// <param name="id"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel SetAsDefault(int id, int creatorID)
    {
        MessageViewModel result = new MessageViewModel();
        var errors = new List<ErrorViewModel>();
        try
        {
            var media = GetByID(id);
            MessageViewModel setAlldefaultTofalseMessage = SetAllDefaultToFalse(media.EntityName, media.EntityID, id, creatorID);

            media.IsDefault = true;
            Edit(media, creatorID);
            result = new MessageViewModel
            {
                Status = Statuses.Success,
                Title = Titles.Save,
                Message = Messages.SaveSuccessed,
                Errors = errors,
                ID = 0,
                Value = ""
            };

            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }

    }


    /// <summary>
    /// تغییر همه موجودیت ها به وضعیت غیر پیشفرض 
    /// </summary>
    /// <param name="entityName"></param>
    /// <param name="entityID"></param>
    /// <param name="currentID"></param>
    /// <param name="creatorID"></param>
    /// <returns></returns>
    public MessageViewModel SetAllDefaultToFalse(string entityName, int entityID, int currentID, int creatorID)
    {
        MessageViewModel result;
        var errors = new List<ErrorViewModel>();
        try
        {
            var items = GetAll(null, x => x.EntityName == entityName && x.EntityID == entityID && x.ID != currentID && x.IsDefault == true);
            foreach (var item in items)
            {
                item.IsDefault = false;
                Update(item, creatorID);
            }
            Save();
            result = new MessageViewModel
            {
                Status = Statuses.Success,
                Title = Titles.API,
                Message = Messages.APIExecuted,
                Errors = errors,
                ID = 0,
                Value = ""
            };
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
            result = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
            return result;
        }
    }


    /// <summary>
    /// اعتبارسنجی فرم
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public List<ErrorViewModel> ValidateForm(Media entity)
    {
        List<ErrorViewModel> result = new List<ErrorViewModel>();

        //Required
        if (string.IsNullOrEmpty(entity.DisplayName) || string.IsNullOrWhiteSpace(entity.DisplayName))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نام نمایشی")
            });
        }
        if (string.IsNullOrEmpty(entity.EntityName) || string.IsNullOrWhiteSpace(entity.EntityName))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نام موجودیت")
            });
        }
        if (string.IsNullOrEmpty(entity.FileName) || string.IsNullOrWhiteSpace(entity.FileName))
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نام فایل")
            });
        }
        if (entity.MediaTypeID == null || entity.MediaTypeID == 0)
        {
            result.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error930,
                ErrorMessage = string.Format(Messages.FieldIsRequired, "نوع مدیا")
            });
        }
        return result;
    }


}
