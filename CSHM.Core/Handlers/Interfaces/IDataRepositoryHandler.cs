namespace CSHM.Core.Handlers.Interfaces;

public interface IDataRepositoryHandler
{

    string Push<T>(T value) where T : class, IDataRepository;
    void Remove(string repositoryID);
    T Get<T>(string repositoryID) where T : class, IDataRepository;

    //string PushInTransaction(POTransactionViewModel value);

    //string PushInBatch(POTransactionBatchViewModel value);

    //string PushInAccount(AccountInformationViewModel value);

    //void Remove(string repositoryID);

    //public POTransactionViewModel GetTransaction(string repositoryID);

    //POTransactionBatchViewModel GetBatch(string repositoryID);

    //AccountInformationViewModel GetAccount(string repositoryID);
}