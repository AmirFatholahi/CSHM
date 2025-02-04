namespace CSHM.Core.Presentations.User;

public class ResetPasswordViewModel
{
    public int UserID { get; set; }

    public string Username { get; set; }

    public string Cellphone { get; set; }

    public string AdminPassword { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmPassword { get; set; }

    public string CaptchaWord { get; set; }

    public string CaptchaSessionID { get; set; }

    public string System { get; set; }

    public string ApiKey { get; set; }

}