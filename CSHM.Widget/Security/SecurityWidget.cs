using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CSHM.Widget.Security;

public static class SecurityWidget
{
    private const int Keysize = 128;

    // This constant determines the number of iterations for the password bytes generation function.
    private const int DerivationIterations = 1000;



    public static string GetSHA1(this string plain)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(plain);

        var sha1 = SHA1.Create();
        byte[] hashBytes = sha1.ComputeHash(bytes);
        var result = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            var hex = b.ToString("x2");
            result.Append(hex);
        }
        return result.ToString();
    }
    public static bool CompareWithSHA1(string plain, string hashed)
    {
        string hash = GetSHA1(plain);
        if (hash == hashed)
            return true;
        else
            return false;
    }

    public static string GetSHA256(this string plain)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(plain);

        var sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(bytes);
        var result = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            var hex = b.ToString("x2");
            result.Append(hex);
        }
        return result.ToString();
    }
    public static bool CompareWithSHA256(string plain, string hashed)
    {
        string hash = GetSHA256(plain);
        if (hash == hashed)
            return true;
        else
            return false;
    }

    public static void AntiXSS<T>(T entity)
    {
        Type t = entity.GetType();

        PropertyInfo[] props = t.GetProperties();
        string concat = string.Empty;
        if (typeof(T).Name == "Notification")
        {
            return;
        }
        foreach (var p in props)
        {
            concat = concat + p.GetValue(entity)?.ToString();
            //if (p != null)
            //    HTMLEncode(p, entity);
        }
        if (Regex.IsMatch(concat, "<.*?>"))
        {
            throw new InvalidOperationException();
        }
    }


    public static void HTMLEncode<T>(PropertyInfo p, T entity)
    {
        Type t = p.PropertyType;
        while (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            t = Nullable.GetUnderlyingType(t);
        }
        p.SetValue(entity, Convert.ChangeType(System.Web.HttpUtility.HtmlEncode(p.GetValue(entity)), t));
    }


    public static string ExtractClaim(string token,string claimType)
    {
        string result = string.Empty;
        if(!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(claimType))
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadToken(token) as JwtSecurityToken;
            var value = securityToken.Claims.FirstOrDefault(claim => claim.Type == claimType).Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                result = value;
            }
        }
        return result;
    }
    

    // ***********************************************************************************************************
    // ***********************************************************************************************************
    // ***********************************************************************************************************
    // *********************************************************************************************************** GCM

    /// <summary>
    /// رمز کننده متقارن 256 بیتی
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string EncryptGCM(string plainText, string key)
    {
        string result = "";
        byte[] keyToByte = Convert.FromBase64String(key);
        var toBytes = ASCIIEncoding.UTF8.GetBytes(plainText); //plain Text to Byte Array
        var iv = RandomNumberGenerator.GetBytes(12); // 12 bytes for the IV
        var tag = new byte[16]; // 16 bytes for the tag
        var cipherTextBytes = new byte[toBytes.Length];

        using (var aesGcm = new AesGcm(keyToByte))
        {
            aesGcm.Encrypt(iv, toBytes, cipherTextBytes, tag);
        }

        // Combine nonce, ciphertext, and tag into a single byte array
        var resultByte = new byte[iv.Length + cipherTextBytes.Length + tag.Length];
        Buffer.BlockCopy(iv, 0, resultByte, 0, iv.Length);
        Buffer.BlockCopy(cipherTextBytes, 0, resultByte, iv.Length, cipherTextBytes.Length);
        Buffer.BlockCopy(tag, 0, resultByte, iv.Length + cipherTextBytes.Length, tag.Length);
        result = Convert.ToBase64String(resultByte);
        return result;
    }


    /// <summary>
    /// رمزگشای متقارن 256 بیتی
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static EncryptionViewModel DecryptGCM(string cipherText, string key)
    {
        EncryptionViewModel result = new EncryptionViewModel();
        try
        {
            byte[] keyToByte = Convert.FromBase64String(key);
            var cipherBytes = Convert.FromBase64String(cipherText);
            var iv = new byte[12];
            var tag = new byte[16];
            var toBytes = new byte[cipherBytes.Length - iv.Length - tag.Length];

            Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(cipherBytes, iv.Length, toBytes, 0, toBytes.Length);
            Buffer.BlockCopy(cipherBytes, iv.Length + toBytes.Length, tag, 0, tag.Length);

            var plaintextBytes = new byte[toBytes.Length];

            using (var aesGcm = new AesGcm(keyToByte))
            {
                aesGcm.Decrypt(iv, toBytes, tag, plaintextBytes);
            }

            result.PlainText = ASCIIEncoding.UTF8.GetString(plaintextBytes);
            result.IsValid = true;
            return result;
        }
        catch (Exception ex)
        {
            result.PlainText = "";
            result.IsValid = false;
            return result;

        }

    }

    /// <summary>
    /// تولید کننده کلید 256 بیتی
    /// </summary>
    /// <returns></returns>
    public static string GenerateGCMKey()
    {
        string result = "";
        byte[] key = RandomNumberGenerator.GetBytes(32);
        result = Convert.ToBase64String(key);
        return result;
    }

    /// <summary>
    /// تولید کننده API KEY
    /// </summary>
    /// <returns></returns>
    public static string GenerateApiKey()
    {
        string result = "";
        byte[] key1 = RandomNumberGenerator.GetBytes(32);
        byte[] key2 = RandomNumberGenerator.GetBytes(32);
        byte[] key3 = RandomNumberGenerator.GetBytes(32);
        result = Convert.ToBase64String(key1) + Convert.ToBase64String(key2) + Convert.ToBase64String(key3);
        result = result.Substring(0, 128);
        return result;
    }

    /// <summary>
    /// کلید اصلی رمز کننده
    /// </summary>
    /// <returns></returns>
    public static string GetDefaultSecretKey()
    {
        return "fZONY4U8JP7lsCkDS/VHY6cVi3mZtq0IC4o6I8k6m/8=";
    }




}