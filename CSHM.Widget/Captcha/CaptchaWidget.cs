using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using CSHM.Widget.Log;
using CSHM.Widget.Method;

namespace CSHM.Widget.Captcha;

public class CaptchaWidget : ICaptchaWidget
{
    private readonly List<SessionViewModel<int>> _sessions = new List<SessionViewModel<int>>();
    private readonly ILogWidget _log;

    public CaptchaWidget(ILogWidget log)
    {
        _log = log;
    }


    /// <summary>
    /// متد اصلی سازنده کپچا و اطلاعات جانبی مربوطه
    /// [0]:Captcha Word
    /// [1]:url in base64
    /// </summary>
    /// <returns></returns>
    public List<string> CreatCaptcha(string foreColor,string backColor)
    {
        try
        {
            List<string> result = new List<string>();
            Bitmap newBitmap = new Bitmap(150, 50, PixelFormat.Format32bppArgb);
            Graphics newGraphics = Graphics.FromImage(newBitmap);
            Rectangle newRectangle = new Rectangle(0, 0, 150, 50);
            newGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml(backColor)), newRectangle);
            Random random = new Random();
            String captchaWord = random.Next(100000, 999999).ToString();
            result.Add(captchaWord);
            Font drawCaptchaFont = new Font("Segoe Script", 20, FontStyle.Italic | FontStyle.Strikeout);
            SolidBrush drawCaptchaBrush = new SolidBrush(ColorTranslator.FromHtml(foreColor));
            PointF point = new PointF(15, 7);
            PointF point2 = new PointF(15, 9);
            newGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml(backColor), 0), newRectangle);
            newGraphics.DrawString(captchaWord, drawCaptchaFont, drawCaptchaBrush, point);
            newGraphics.DrawString(captchaWord, drawCaptchaFont, drawCaptchaBrush, point2);
            byte[] data;
            using (MemoryStream m = new MemoryStream())
            {
                newBitmap.Save(m, ImageFormat.Jpeg);
                data = m.ToArray();
            }
            string url = Convert.ToBase64String(data, 0, data.Length);
            url = "data:image/png;base64," + url;
            result.Add(url);
            return result;
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            throw;
        }
    }
}