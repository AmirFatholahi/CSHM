using CSHM.Core.Handlers.Interfaces;
using CSHM.Widget.Config;
using CSHM.Widget.Log;
using CSHM.Widget.Redis;

namespace CSHM.Core.Handlers;
 
public class DataRepositoryHandler : IDataRepositoryHandler
{  
    private static readonly Dictionary<string, IDataRepository> Data = new Dictionary<string, IDataRepository>();
    //private readonly Dictionary<string, POTransactionBatchViewModel> _batches = new Dictionary<string, POTransactionBatchViewModel>();
    //private readonly Dictionary<string, POTransactionViewModel> _transactions = new Dictionary<string, POTransactionViewModel>();
    //private readonly Dictionary<string, AccountInformationViewModel> _accounts = new Dictionary<string, AccountInformationViewModel>();
    private readonly ILogWidget _log;
    private readonly IRedisWidget _redis;
    private readonly string _dataRepositoryType;
    private readonly int _expirationMinute;
    public DataRepositoryHandler(ILogWidget log, IRedisWidget redisWidget)
    {
        _log = log;
        _redis = redisWidget;

        _dataRepositoryType = ConfigWidget.GetConfigValue<string>("Setting:DataRepository"); 
        _expirationMinute = ConfigWidget.GetConfigValue<int>("Setting:DataRepositoryExpireMinutes");
    }

    public string Push<T>(T value) where T : class, IDataRepository
    {
        value.ExpireTime = DateTime.Now.AddMinutes(_expirationMinute);
        value.RepositoryID = Guid.NewGuid().ToString();
        if (_dataRepositoryType == "REDIS")
        {
            var result = _redis.SetData(value.RepositoryID, value, DateTimeOffset.Now.AddMinutes(_expirationMinute));

        }
        else
        {
            foreach (var item in Data.Where(item => item.Value.ExpireTime < DateTime.Now))
            {
                Remove(item.Value.RepositoryID);
            }
            Data.Add(value.RepositoryID, (T)value.Clone());
        }
        return value.RepositoryID;
    }


   

    public void Remove(string repositoryID)
    {
        if (_dataRepositoryType == "REDIS")
        {
            _redis.RemoveData(repositoryID);
        }
        else
        {

            if (Data.ContainsKey(repositoryID))
            {
                Data.Remove(repositoryID);
            }
        }
    }

    public T Get<T>(string repositoryID) where T : class, IDataRepository
    {
        if (_dataRepositoryType == "REDIS")
        {
            var result = _redis.GetData<T>(repositoryID);
            return result;
        }
        else
        {
            if (Data.ContainsKey(repositoryID))
            {
                var item = (T)Data[repositoryID];
                if (item.ExpireTime < DateTime.Now)
                {
                    return null;
                }
                return item;
            }
            else
            {
                return null;
            }
        }
    }
    ///// <summary>
    ///// تراکنش تکی
    ///// </summary>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //public string PushInTransaction(POTransactionViewModel value)
    //{
    //    foreach (var item in _transactions.Where(item => item.Value.ExpireTime < DateTime.Now))
    //    {
    //        Remove(item.Value.RepositoryID);
    //    }
    //    value.ExpireTime = DateTime.Now.AddMinutes(3);
    //    value.RepositoryID = Guid.NewGuid().ToString();

    //    _transactions.Add(value.RepositoryID, (POTransactionViewModel)value.Clone());
    //    return value.RepositoryID;
    //}

    ///// <summary>
    ///// تراکنش دسته ای
    ///// </summary>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //public string PushInBatch(POTransactionBatchViewModel value)
    //{
    //    foreach (var item in _batches.Where(item => item.Value.Transaction.ExpireTime < DateTime.Now))
    //    {
    //        Remove(item.Value.Transaction.RepositoryID);
    //    }
    //    value.Transaction.ExpireTime = DateTime.Now.AddMinutes(3);
    //    value.Transaction.RepositoryID = Guid.NewGuid().ToString();

    //    _batches.Add(value.Transaction.RepositoryID, (POTransactionBatchViewModel)value.Clone());
    //    return value.Transaction.RepositoryID;
    //}


    ///// <summary>
    ///// استعلام حساب
    ///// </summary>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //public string PushInAccount(AccountInformationViewModel value)
    //{
    //    foreach (var item in _accounts.Where(item => item.Value.ExpireTime < DateTime.Now))
    //    {
    //        Remove(item.Value.RepositoryID);
    //    }
    //    value.ExpireTime = DateTime.Now.AddMinutes(3);
    //    value.RepositoryID = Guid.NewGuid().ToString();

    //    _accounts.Add(value.RepositoryID, (AccountInformationViewModel)value.Clone());
    //    return value.RepositoryID;
    //}


    //public void Remove(string repositoryID)
    //{
    //    try
    //    {
    //        if (_transactions.ContainsKey(repositoryID))
    //        {
    //            _transactions.Remove(repositoryID);
    //        }
    //        if (_batches.ContainsKey(repositoryID))
    //        {
    //            _batches.Remove(repositoryID);
    //        }
    //        if (_accounts.ContainsKey(repositoryID))
    //        {
    //            _accounts.Remove(repositoryID);
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        // ignored
    //    }
    //}


    ///// <summary>
    ///// دریافت تراکنش تکی
    ///// </summary>
    ///// <param name="repositoryID">شناسه انباره</param>
    ///// <returns></returns>
    //public POTransactionViewModel GetTransaction(string repositoryID)
    //{
    //    if (_transactions.ContainsKey(repositoryID))
    //    {
    //        var item = _transactions[repositoryID];
    //        if (item.ExpireTime < DateTime.Now)
    //        {
    //            return null;
    //        }
    //        return item;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    ///// <summary>
    ///// دریافت تراکنش دسته ای
    ///// </summary>
    ///// <param name="repositoryID"></param>
    ///// <returns></returns>
    //public POTransactionBatchViewModel GetBatch(string repositoryID)
    //{
    //    if (_batches.ContainsKey(repositoryID))
    //    {
    //        var item = _batches[repositoryID];
    //        if (item.Transaction.ExpireTime < DateTime.Now)
    //        {
    //            return null;
    //        }
    //        return item;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    ///// <summary>
    ///// دریافت استعلام حساب
    ///// </summary>
    ///// <param name="repositoryID"></param>
    ///// <returns></returns>
    //public AccountInformationViewModel GetAccount(string repositoryID)
    //{
    //    if (_accounts.ContainsKey(repositoryID))
    //    {
    //        var item = _accounts[repositoryID];
    //        if (item.ExpireTime < DateTime.Now)
    //        {
    //            return null;
    //        }
    //        return item;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}