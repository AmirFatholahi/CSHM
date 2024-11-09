namespace CSHM.Widget.Rest;

public class ServerAddressViewModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public string PassWord { get; set; }
    public string LoginEndPoint { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireTime { get; set; }
    public bool Status { get; set; }
    public ServerName ServerName
    {
        get
        {
            try
            {
                return (ServerName)Enum.Parse(typeof(ServerName), Name);
            }
            catch
            {
                return ServerName.Unknown;
            }
        }
    }
}