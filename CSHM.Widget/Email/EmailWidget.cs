using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSHM.Widget.Config;
using CSHM.Widget.Method;
using Microsoft.Extensions.Configuration;
using CSHM.Widget.Calendar;
using CSHM.Widget.Security;
using System.Text.RegularExpressions;

namespace CSHM.Widget.Email;

public class EmailWidget : IEmailWidget
{

    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _from;
    private readonly string _password;

    public EmailWidget()
    {
        _smtpServer = ConfigWidget.GetConfigValue<string>("EmailConfiguration:SmtpServer");
        _smtpPort = ConfigWidget.GetConfigValue<int>("EmailConfiguration:SmtpPort");
        _from = ConfigWidget.GetConfigValue<string>("EmailConfiguration:From");
        _password = ConfigWidget.GetConfigValue<string>("EmailConfiguration:Password");

    }

    ///<summary>
    ///بررسی معتبر بودن ایمیل
    ///<summary>
    public bool IsEmailValid(string email)
    {
        bool result;
        if (email == null)
        {
            return false;
        }
        string pattren = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_{|}~]+@[a-zA-Z0-9-]+(?:\.[]a-zA-Z0-9-]+)*$"; //ساخت شابلون برای ایمیل
        
        var regex = new Regex(pattren);//ایجاد regex از روی شابلون
        
        result = regex.IsMatch(email); //بررسی شابلون از روی ایمیل دریافتی
        
        return result;
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="content"></param>
    /// <param name="attachments">ضمیمه ها: پارامتر اول نام فایل و پارامتر دوم بیس 64</param>
    /// <param name="isBodyHtml"></param>
    /// <returns></returns>
    public bool Send(string to, string subject, string content, Dictionary<string,string> attachments, bool isBodyHtml)
    {
        bool result = false;
        try
        {
            SmtpClient smtpClient = new SmtpClient(_smtpServer, _smtpPort);
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            //var pass = _password.Decrypt();
            var pass = "";
            smtpClient.Credentials = new NetworkCredential(_from, pass);

            MailMessage mail = new MailMessage(_from, to);
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = isBodyHtml;
            if(attachments!=null && attachments.Any())
            {
                foreach (var item in attachments)
                {
                        
                    var imageData = Convert.FromBase64String(item.Value);
                    MemoryStream memoryStream = new MemoryStream(imageData);
                    var fileName = item.Key;
                  
                    var attachment = new Attachment(memoryStream, fileName);
                    attachment.ContentId = Path.GetFileNameWithoutExtension(fileName);
                    mail.Attachments.Add(attachment);
                }
            }

            smtpClient.Send(mail);
            result = true;
            return result;
        }
        catch (Exception ex)
        {
            result = false;
            return result;
        }
    }

   

      
}