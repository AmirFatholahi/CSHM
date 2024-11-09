using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Presentation.Base;
using CSHM.Presentations.Media;
using CSHM.Core.Services.Interfaces;
using CSHM.Domain;
using CSHM.Widget.Convertor;
using CSHM.Widget.File;
using CSHM.Widget.Image;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.Redis;
using CSHM.Presentation.Resources;
using CSHM.Presentations.Media;

namespace CSHM.Core.Handlers
{
    public class MediaHandler : IMediaHandler
    {
        private readonly IMediaService _mediaService;
        private readonly ISettingService _setting;
        private readonly IFileWidget _file;
        private readonly IExtensionTypeService _extensionTypeService;
        private readonly ILogWidget _log;

        public MediaHandler(IMediaService mediaService, ISettingService setting, IFileWidget file, IExtensionTypeService extensionTypeService, ILogWidget log)
        {
            _mediaService = mediaService;
            _setting = setting;
            _file = file;
            _extensionTypeService = extensionTypeService;
            _log = log;
        }



        /// <summary>
        /// درج و ویرایش یک مدیا
        /// </summary>
        /// <param name="mediaFile"></param>
        /// <param name="creatorID"></param>
        /// <returns></returns>
        public MessageViewModel AddOrUpdate(MediaFileViewModel mediaFile, int creatorID)
        {
            MessageViewModel result = new MessageViewModel();
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            try
            {
                Random rand = new Random();
                var rndNumber = rand.Next(10000000, 99999999);
                var entity = new Media()
                {
                    ID = mediaFile.Media.ID,
                    FileName = $"{mediaFile.Media.EntityName.Substring(0, 3).ToUpper()}-{mediaFile.Media.EntityID}-{rndNumber}",
                    EntityName = mediaFile.Media.EntityName,
                    EntityID = mediaFile.Media.EntityID,
                    MediaTypeID = mediaFile.Media.MediaTypeID,
                    Sequence = mediaFile.Media.Sequence,
                    DisplayName = mediaFile.Media.DisplayName,
                    Size = mediaFile.File?.Length ?? 0,
                    ExtensionName = Path.GetExtension(mediaFile.File?.FileName) ?? "",
                    IsDefault = mediaFile.Media.IsDefault,
                    IsConfirm = mediaFile.Media.IsConfirm,
                    IsActive = mediaFile.Media.IsActive,
                };
                errors = _mediaService.ValidateForm(entity);
                if (errors.Any())
                {
                    result = new MessageViewModel()
                    {
                        Status = Statuses.Error,
                        Title = Titles.Error,
                        Message = Messages.SaveFailed,
                        Errors = errors,
                        ID = 0,
                        Value = ""
                    };
                    //TODO: log
                    return result;
                }
                var extensionTypes = GenerateExtensionViewModel();

                if (mediaFile.File != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(mediaFile.File.ContentDisposition).FileName?.Trim('"');
                    var extension = Path.GetExtension(fileName)?.Replace(".", "");
                    var currentStream = mediaFile.File.OpenReadStream();
                    var currentExtensionTypes = extensionTypes.Where(x => x.Postfix.ToLower() == extension?.ToLower()).ToList();
                    var isValidMatcher = _file.FileExtensionMatcher(currentStream, currentExtensionTypes);
                    if (isValidMatcher == false)
                    {
                        errors.Add(new ErrorViewModel()
                        {
                            ErrorCode = "1055",
                            ErrorMessage = string.Format(Errors.FileMatcherIsNotValid, fileName)
                        });
                        result = new MessageViewModel()
                        {
                            Status = Statuses.Error,
                            Title = Titles.Error,
                            Message = Messages.ReferToErrorsList,
                            Errors = errors,
                            ID = 0,
                            Value = ""
                        };
                        return result;
                    }

                    byte[]? data = null;

                    if (extensionTypes.Any(x => x.Postfix == extension && x.IsImage == true))
                    {
                        var currentBitmap = new Bitmap(currentStream);
                        if (mediaFile.IsNeedToGrayscale == true)
                        {
                            try
                            {
                                currentBitmap = ImageWidget.ToGrayScale(currentBitmap);
                            }
                            catch (Exception ex)
                            {
                                return result;
                            }
                        }

                        var maxSize = _setting.GetAll(true, x => x.GroupCode == entity.EntityName.ToUpper() && x.Key == "Size").FirstOrDefault()?.Value.ToInt() ?? 100;
                        var fileSize = _file.GetFileSize(currentBitmap);

                        if (mediaFile.IsNeedToCompress == true && fileSize / 1024 > maxSize)
                        {
                            var compressPercentage = _setting.GetAll(true, x => x.GroupCode == entity.EntityName.ToUpper() && x.Key == "Compress").FirstOrDefault()?.Value.ToInt() ?? 50;
                            currentBitmap = ImageWidget.Compress(currentBitmap, compressPercentage);
                        }
                        if (_file.GetFileSize(currentBitmap) / 1024 > maxSize)
                        {
                            errors.Add(new ErrorViewModel
                            {
                                ErrorCode = "",
                                ErrorMessage = "حجم فایل بیش از حد مجاز می باشد"
                            });

                            result = new MessageViewModel()
                            {
                                Status = Statuses.Error,
                                Title = Titles.Error,
                                Message = Messages.ReferToErrorsList,
                                Errors = errors,
                                ID = 0,
                                Value = ""
                            };
                            //Todo:Log
                            return result;
                        }
                        ImageConverter converter = new ImageConverter();
                        data = converter.ConvertTo(currentBitmap, typeof(byte[])) as byte[];
                    }
                    else
                    {
                        data = ImageWidget.ToBinary(mediaFile.File, false, 0, 0, null);
                    }

                    if (data == null)
                    {
                        errors.Add(new ErrorViewModel
                        {
                            ErrorCode = "",
                            ErrorMessage = "ثبت عکس با خطا مواجه گردید"
                        });

                        result = new MessageViewModel()
                        {
                            Status = Statuses.Error,
                            Title = Titles.Error,
                            Message = Messages.ReferToErrorsList,
                            Errors = errors,
                            ID = 0,
                            Value = ""
                        };
                        //Todo:Log
                        return result;
                    }
                    else
                    {
                        entity.ExtensionTypeID = currentExtensionTypes.FirstOrDefault()?.ID ?? 0;
                        entity.Data = data;

                    }
                }

                //add
                if (entity.ID == 0)
                {
                    if (mediaFile.File == null)
                    {
                        errors.Add(new ErrorViewModel
                        {
                            ErrorCode = "",
                            ErrorMessage = "ثبت عکس جدید بدون فایل امکان پذیر نمی باشد"
                        });

                        result = new MessageViewModel()
                        {
                            Status = Statuses.Error,
                            Title = Titles.Error,
                            Message = Messages.ReferToErrorsList,
                            Errors = errors,
                            ID = 0,
                            Value = ""
                        };
                        //Todo:Log
                        return result;

                    }
                    var exist = _mediaService.GetAll(null, x => x.EntityID == entity.EntityID && x.EntityName == entity.EntityName && x.Data == entity.Data).FirstOrDefault();
                    if (exist != null)
                    {
                        errors.Add(new ErrorViewModel()
                        {
                            ErrorCode = "",
                            ErrorMessage = Messages.RecordExisted
                        });
                        result = new MessageViewModel()
                        {
                            Status = Statuses.Error,
                            Title = Titles.Error,
                            Message = Messages.RecordExisted,
                            Errors = errors,
                            ID = 0,
                            Value = ""
                        };
                        //Todo:Log
                        return result;
                    }
                    else
                    {
                        result = _mediaService.Add(entity, creatorID);
                        return result;

                    }
                }
                //update
                else
                {
                    var exist = _mediaService.GetByID(entity.ID);
                    if (exist == null)
                    {
                        errors.Add(new ErrorViewModel()
                        {
                            ErrorCode = "",
                            ErrorMessage = Errors.RecordNotFound
                        });
                        result = new MessageViewModel()
                        {
                            Status = Statuses.Error,
                            Title = Titles.Error,
                            Message = Errors.RecordNotFound,
                            Errors = errors,
                            ID = 0,
                            Value = ""
                        };
                        //TODO: Log Error 
                        return result;
                    }
                    else
                    {
                        exist.DisplayName = entity.DisplayName;
                        exist.IsDefault = entity.IsDefault;
                        exist.IsConfirm = entity.IsConfirm;
                        exist.IsActive = entity.IsActive;
                        result = _mediaService.Edit(exist, creatorID);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
                result = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
                return result;
            }
        }



        /// <summary>
        /// متد اصلی دانلود یک فایل و نمایش تصویر
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityID"></param>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <param name="showOnlyConfirmed"></param>
        /// <returns></returns>
        public FileResultViewModel DownloadOnce(string fileName, bool showOnlyConfirmed)
        {
            FileResultViewModel result = new FileResultViewModel();
            try
            {
                var exist = _mediaService.GetMedia(fileName);
                if (exist == null)
                {
                    result = _mediaService.ImageNotFound();
                    return result;
                }
                var media = _mediaService.GetAll(true, x => (showOnlyConfirmed == false || x.IsConfirm == showOnlyConfirmed) && x.ID == exist.ItemID && x.FileName == exist.FileName).FirstOrDefault();
                if (media != null)
                {
                    result = new FileResultViewModel
                    {
                        FileName = media.FileName + media.ExtensionName,
                        ContentType = media.ExtensionType.Title,
                        Stream = new MemoryStream(media.Data)
                    };
                    return result;
                }
                else
                {
                    result = _mediaService.ImageNotFound();
                    return result;
                }

            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
                return result;
            }
        }

        // <summary>
        /// متد اصلی دانلود یک فایل و نمایش تصویر
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityID"></param>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <param name="showOnlyConfirmed"></param>
        /// <returns></returns>
        public FileResultViewModel Download(string fileName, bool showOnlyConfirmed)
        {
            FileResultViewModel result = new FileResultViewModel();
            try
            {
                var exist = _mediaService.GetMedia(fileName);
                if (exist == null)
                {
                    result = _mediaService.ImageNotFound();
                    return result;
                }
                var media = _mediaService.GetAll(null, x => (showOnlyConfirmed == false) && x.ID == exist.ItemID && x.FileName == exist.FileName).FirstOrDefault();
                if (media != null)
                {
                    result = new FileResultViewModel
                    {
                        FileName = media.FileName + "." + media.ExtensionType.Postfix,
                        ContentType = media.ExtensionType.Title,
                        Stream = new MemoryStream(media.Data)
                    };
                    return result;
                }
                else
                {
                    result = _mediaService.ImageNotFound();
                    return result;
                }

            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
                return result;
            }
        }




        public MessageViewModel RemoveAllByEntity<T>(int entityID, int creatorID)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                var media = _mediaService.GetAll(true, x => x.EntityID == entityID && x.EntityName == typeof(T).Name).ToList();
                foreach (var medium in media)
                {
                    result = _mediaService.Remove(medium.ID, creatorID);
                }

                return result;
            }

            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName());
                result = new MessageViewModel
                {
                    Status = Statuses.Error,
                    Message = _log.GetExceptionMessage(ex)
                };
                return result;
            }
        }

       


        public List<ExtensionTypeViewModel> GenerateExtensionViewModel()
        {
            var extensionTypes = _extensionTypeService.GetAll(true);
            var result = extensionTypes.Select(item => new ExtensionTypeViewModel()
            {
                ID = item.ID,
                ExtensionName = item.Title,
                Matcher = item.Matcher,
                MatcherType = item.MatcherType,
                Postfix = item.Postfix,
                IsImage = item.IsImage

            }).ToList();
            return result;
        }

    }

}

