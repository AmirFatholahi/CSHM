namespace CSHM.Core.Handlers.Interfaces;

public interface IDataRepository : ICloneable
{
    public string RepositoryID { get; set; }//شناسه ریپوزیتوری

    public DateTime? ExpireTime { get; set; }//زمان انقضا از ریپوزیتوری
}