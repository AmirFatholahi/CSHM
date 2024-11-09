namespace CSHM.Widget.Rest;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string MemberID { get; set; }
    public string CaptchaWord { get; set; }
    //public int CaptchaSessionID { get; set; }

    public string CaptchaSessionID { get; set; }

    public string Hash { get; set; }

    public string ApiKey { get; set; }
}