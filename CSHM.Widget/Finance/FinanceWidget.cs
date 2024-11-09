using CSHM.Widget.Convertor;

namespace CSHM.Widget.Finance;
public static class FinanceWidget
{


    /// <summary>
    /// سازنده شناسه دستور پرداخت
    /// </summary>
    /// <returns></returns>
    public static string PaymentOrderIdentifirGenerator(int id)
    {
        string result = string.Empty;
        Random oRandom = new Random();
        int rand = oRandom.Next(11, 99);
        string number = string.Concat(rand, id.ToString().PadLeft(9, '0'));
        int checkDigit = SismooniCheckDigit(number.ToLong());
        result = string.Concat(number, checkDigit);
        return result;
    }


    /// <summary>
    /// سازنده شناسه یکتای عمومی بصورت لحظه ای
    /// </summary>
    /// <param name="part1">قسمت اول ارسالی</param>
    /// <param name="part2">قسمت دوم ارسالی</param>
    /// <param name="prefix">پیشوند</param>
    /// <returns></returns>
    public static string UniqueIdentifierGenerator(int part1, int part2, string prefix)
    {
        string result;
        Random oRandom = new Random();
        int rand = oRandom.Next(101, 999);
        string number = string.Empty;
        if (part1 != 0)
        {
            number = string.Concat(rand, part1.ToString().PadLeft(6, '0'));
        }
        if (part2 != 0)
        {
            number = string.Concat(number, part2.ToString().PadLeft(6, '0'));
        }
        int checkDigit = SismooniCheckDigit(number.ToLong());
        string now = DateTime.Now.ToString("yyyyMMddHHmmss");

        number = string.Concat(number, now);
        result = prefix + string.Concat(number, checkDigit);
        return result;
    }




    /// <summary>
    /// سازنده شماره سریال ووچر
    /// </summary>
    /// <returns></returns>
    public static string GenerateVoucherNumber(int id)
    {
        string result = string.Empty;
        Random oRandom = new Random();
        int rand = oRandom.Next(11, 99);
        string number = "3" + rand.ToString();
        number = string.Concat(number, id.ToString().PadLeft(12, '0'));
        int checkDigit = SismooniCheckDigit(number.ToLong());
        result = string.Concat(number, checkDigit);
        return result;
    }


    public static string GeneratePocketNumber(int id,int pocketTypeID)
    {
        string result = string.Concat(pocketTypeID.ToString().PadLeft(2, '0'), id.ToString().PadLeft(12, '0'));
      
        int checkDigit = SismooniCheckDigit(result.ToLong());
        result = string.Concat(result, checkDigit);
        return result;
    }



    /// <summary>
    /// چک دیجیت الگوریتم سیسمونی
    /// بر مبنای وزن- الزاما 11 رقمی
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private static int SismooniCheckDigit(long number)
    {
        string input = number.ToString();
        int[] weights = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }; // وزن ارقام به ترتیب از راست به چپ
        int sum = 0;

        // حلقه برای جمع وزن‌داری ارقام
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * int.Parse(input[i].ToString());
        }

        //  check digit
        int result = (sum % 11 == 10) ? 0 : sum % 11;
        return result;
    }

    /// <summary>
    /// سازنده رشته تصادفی
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GenerateString(int length)
    {
        string result = string.Empty;
        if (length < 2)
        {
            return result;
        }
        string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        Random oRandom = new Random();
        int rand = 0;
        for (int i = 0; i < length - 2; i++)
        {

            rand = oRandom.Next(0, 25);
            result = result + alphabets[rand];
        }
        rand = oRandom.Next(11, 99);
        result = result + rand.ToString();
        return result;
    }

    /// <summary>
    /// سازنده شماره شبا
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <param name="accountType"></param>
    /// <returns></returns>
    public static string IBANGenerator(string accountNumber, string accountType)
    {
        string str3 = "0123";
        if (!CheckDigitAccount(accountNumber))
        {
            return "";
        }
        if (!(string.IsNullOrEmpty(accountType) || str3.Contains(accountType)))
        {
            return "";
        }
        if (string.IsNullOrEmpty(accountType))
        {
            accountType = "0";
        }
        string str = "IR";
        string str32 = "016" + accountType + accountNumber.PadLeft(0x12, '0');
        decimal num = decimal.Parse(str32 + "182700");
        decimal num2 = 98M - (num % 97M);
        string str2 = num2.ToString().PadLeft(2, '0');
        string result = (str + str2 + str32);
        return result;
    }

    /// <summary>
    /// چک دیجیت شماره شبا
    /// </summary>
    /// <param name="acc"></param>
    /// <returns></returns>
    private static bool CheckDigitAccount(string acc)
    {
        long num = long.Parse(acc.Substring(acc.Length - 1, 1));
        short num2 = GetDigitAccount(acc.Substring(0, acc.Length - 1));
        return (num2 == num);
    }

    /// <summary>
    /// متد بازگشتی چک دیجت شماره شبا
    /// </summary>
    /// <param name="acc"></param>
    /// <returns></returns>
    public static short GetDigitAccount(string acc)
    {
        long num5 = 0L;
        long num4 = 1L;
        for (int i = acc.Length - 1; i >= 0; i--)
        {
            num5 += Int64.Parse(((double)(double.Parse(acc.Substring(i, 1)) * num4)).ToString()) % 11L;
            num4 *= 2L;
        }
        num5 = num5 % 10L;
        short num2 = (short)(num5 % 10L);
        return num2;
    }




}