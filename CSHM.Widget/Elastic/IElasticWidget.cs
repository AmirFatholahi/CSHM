namespace CSHM.Widget.Elastic;

public interface IElasticWidget
{
    Task<bool> Create<T>(T source, string indexName) where T : class;

    Task<bool> Update<T>(T source, string id, string indexName) where T : class;

    Task<bool> Delete(string id, string indexName);

    Task<T> Read<T>(string id, string indexName) where T : class;

    Task<bool> BulkCreate<T>(List<T> list, string indexName) where T : class;
}