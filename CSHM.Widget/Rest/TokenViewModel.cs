namespace CSHM.Widget.Rest;

public class TokenViewModel
{
    public bool Status { get; set; }

    public string Token { get; set; }

    public string TokenType { get; set; }

    public string RefreshToken { get; set; }

    public string IdToken { get; set; }

    public DateTime Expiration { get; set; }

}