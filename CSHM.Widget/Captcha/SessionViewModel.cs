namespace CSHM.Widget.Captcha;

public class SessionViewModel<T>
{
    public T Session { get; set; }
    public int Value { get; set; }
    public string ObserverID { get; set; }
    public DateTime ExpireTime { get; set; }
}