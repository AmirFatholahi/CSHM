using CSHM.Widget.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSHM.Widget.Rest;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Reflection;
using System.Resources;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Base;

namespace CSHM.Widget.SMS;

public class SMSWidget : ISMSWidget
{
    private readonly IRestWidget _rest;
    private readonly ILogWidget _log;
    public SMSWidget(IRestWidget rest, ILogWidget log)
    {
        _rest = rest;
        _log = log;
    }

    /// <summary>
    /// ارسال پیامک تکی
    /// </summary>
    /// <param name="content">متن پیام</param>
    /// <returns>
    public SMSOutputViewModel Send(string content)
    {
        SMSOutputViewModel result = new SMSOutputViewModel();


        var endPoint = $"api/widget/SendSms";
        try
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var parameter = new SMSViewModel { Message = content };
                var data = _rest.CallRestApi<SMSViewModel, ResultViewModel<SMSOutputViewModel>>(RestSharp.Method.Post, endPoint,parameter, ServerName.Channel);
                if (data.Message.Status == "success")
                {
                    result = data.List[0];
                }

                return result;
            }
            else
            {
                result.IsSuccess = false;
                result.MessageID = -1;
                result.TemplateID = -1;
                result.Message = "متن پیام نامعتبر می‌باشد";
                return result;
            }
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.MessageID = -1;
            result.TemplateID = -1;
            result.Message = "در ارسال پیامک استثنای درونی رخ داده است";
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0);
            return result;
        }
    }

    public List<int> Send(List<string> contents)
    {

        List<int> results = new List<int>();
        if (contents.Any())
        {
            //foreach (var content in contents)
            //{
            //    results.Add(Send(content));
            //}

            return results;
        }
        else
        {
            results.Add(-5); //not regex
            return results;
        }
    }
}