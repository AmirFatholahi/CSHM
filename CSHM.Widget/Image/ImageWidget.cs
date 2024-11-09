using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;

namespace CSHM.Widget.Image;

public static class ImageWidget
{
    public static byte[] ToBinary(IFormFile file, bool isThumbnail, int width, int height, ImageFormat format)
    {
        byte[] result;
        using var origStream = new MemoryStream();
        file.CopyTo(origStream);

        if (isThumbnail == false)
        {
            result = origStream.ToArray();
            return result;
        }

        //در صورت نیاز به تامبنیل یا تغییر سایز عکس
        var image = System.Drawing.Image.FromStream(origStream);
        int sourceWidth = image.Width;
        int sourceHeight = image.Height;
        if (sourceWidth < sourceHeight)
        {
            int buff = width;
            width = height;
            height = buff;
        }
        int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
        float nPercent = 0, nPercentW = 0, nPercentH = 0;
        nPercentW = ((float)width / (float)sourceWidth);
        nPercentH = ((float)height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((width -
                      (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((height -
                      (sourceHeight * nPercent)) / 2);
        }
        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);
        var thumb = image.GetThumbnailImage(destWidth, destHeight, () => false, IntPtr.Zero);

        using var thumbStream = new MemoryStream();
        thumb.Save(thumbStream, format);

        result = thumbStream.ToArray();
        return result;
    }

    /// <summary>
    /// متد آپلود عکس آواتار
    /// </summary>
    /// <param name="file"></param>
    /// <param name="maxSize"></param>
    /// <param name="isNeedToCompress"></param>
    /// <param name="isNeedToGrayscale"></param>
    /// <returns></returns>
    public static byte[]? ToBinary(IFormFile file, int maxSize, bool isNeedToCompress = false, bool isNeedToGrayscale = false)
    {
        byte[]? result = null;
        try
        {

            if (file.Length / 1024 > maxSize)
            {
                return result;
            }
            result = ToBinary(file, false, 0, 0, null);
            return result;
        }
        catch
        {
            return null;
        }


    }

    public static byte[] ToBinary(string path)
    {
        byte[] imageArray =System.IO.File.ReadAllBytes(path);
        return imageArray;
    }

    public static string ToBase64(byte[] byteArray)
    {
        string result = string.Empty;
        if (byteArray != null)
        {
            result = Convert.ToBase64String(byteArray);
        }
        return result;
    }

    /// <summary>
    /// دریافت اندازه فایل
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static long GetFileSize(System.IO.Stream stream)
    {
        return stream.Length;
    }

    /// <summary>
    /// تبدیل مسیر مقصد به استریم
    /// </summary>
    /// <param name="path">مسیر مقصد</param>
    /// <returns></returns>
    public static System.IO.Stream GenerateStreamFromString(string path)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(path);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }


    /// <summary>
    /// دریافت طول و عرض عکس
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public static Tuple<int, int> GetImageDimension(System.IO.Stream original)
    {
        using var image = new Bitmap(original);
        var height = image.Height;
        var width = image.Width;
        return new Tuple<int, int>(width, height);
    }


    /// <summary>
    /// دریافت طول و عرض عکس
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static Tuple<int, int> GetImageDimension(Bitmap image)
    {
        var height = image.Height;
        var width = image.Width;
        return new Tuple<int, int>(width, height);
    }

    /// <summary>
    /// فشرده ساز
    /// </summary>
    /// <param name="original">فایل دریافت شده از کاربر</param>
    /// <param name="dest">فایل استریم خروجی</param>
    /// <param name="quality">کیفیت</param>
    /// <returns></returns>
    public static FileStream Compress(System.IO.Stream original, FileStream dest, int quality)
    {
        using (Bitmap bmpl = new Bitmap(original))
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(qualityEncoder, quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmpl.Save(dest, jpgEncoder, myEncoderParameters);
            return dest;
        }


    }


    /// <summary>
    /// فشرده ساز
    /// </summary>
    /// <param name="original">فایل دریافت شده از کاربر</param>
    /// <param name="dest"></param>
    /// <param name="quality">کیفیت</param>
    /// <returns></returns>
    public static Bitmap Compress(Bitmap original, int quality)
    {
        MemoryStream dest = new MemoryStream();
        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
        System.Drawing.Imaging.Encoder qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
        EncoderParameters myEncoderParameters = new EncoderParameters(1);
        EncoderParameter myEncoderParameter = new EncoderParameter(qualityEncoder, quality);
        myEncoderParameters.Param[0] = myEncoderParameter;
        original.Save(dest, jpgEncoder, myEncoderParameters);

        return new Bitmap(dest);

    }


    /// <summary>
    /// متد تبدیل به سیاه و سفید
    /// </summary>
    /// <param name="original">فایل ورودی</param>
    /// <returns></returns>
    public static Bitmap ToGrayScale(Bitmap original)
    {
        //create a blank bitmap the same size as original
        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //get a graphics object from the new image
        using (Graphics g = Graphics.FromImage(newBitmap))
        {

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                        new float[] {.3f, .3f, .3f, 0, 0},
                        new float[] {.59f, .59f, .59f, 0, 0},
                        new float[] {.11f, .11f, .11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            using (ImageAttributes attributes = new ImageAttributes())
            {

                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                //draw the original image on the new image
                //using the grayscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
        }
        return newBitmap;
    }


    /// <summary>
    /// متد تبدیل به سیاه و سفید
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public static Bitmap ToGrayScale(System.IO.Stream original)
    {
        return ToGrayScale(new Bitmap(original));
    }


    /// <summary>
    /// متد اصلی تبدیل به سیاه و سفید
    /// </summary>
    /// <param name="original"></param>
    /// <param name="destination"></param>
    /// <param name="imageFormat"></param>
    /// <returns></returns>
    public static FileStream ToGrayScale(System.IO.Stream original, FileStream destination, ImageFormat imageFormat)
    {
        var grayed = ToGrayScale(original);
        grayed.Save(destination, imageFormat);
        return destination;
    }


    /// <summary>
    /// تغییر اندازه
    /// </summary>
    /// <param name="original">فایل دریافت شده از کاربر</param>
    /// <param name="dest">فایل استریم خروجی</param>
    /// <param name="scale">درصد کوچکسازی</param>
    /// <returns></returns>
    public static Bitmap Resize(Bitmap original, int scale)
    {
        var newWidth = original.Width * scale / 100;
        var newHeight = original.Height * scale / 100;
        var newBitmap = new Bitmap(original, newWidth, newHeight);
        return newBitmap;
    }




    #region Private Method
    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }


    #endregion

}