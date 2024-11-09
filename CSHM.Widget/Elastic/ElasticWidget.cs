using Nest;

namespace CSHM.Widget.Elastic;

public class ElasticWidget : IElasticWidget
{
    private readonly IElasticClient _elastic;
    public ElasticWidget(IElasticClient elasticClinet)
    {
        _elastic = elasticClinet;
    }

    public async Task<bool> Create<T>(T source, string indexName) where T : class
    {
        var res = await _elastic.IndexAsync(source, x => x.Index(indexName).Refresh(Elasticsearch.Net.Refresh.False));
        var result = res.IsValid;
        return result;
    }


    public async Task<bool> Update<T>(T source, string id, string indexName) where T : class
    {
        var res = await _elastic.IndexAsync(source, i => i
            .Index(indexName)
            .Id(id)
            .Refresh(Elasticsearch.Net.Refresh.True));
        var result = res.IsValid;
        return result;
    }


    public async Task<bool> Delete(string id, string indexName)
    {
        IDeleteRequest request = new DeleteRequest(indexName, id);
        var res = await _elastic.DeleteAsync(request);
        var result = res.IsValid;
        return result;
    }


    public async Task<T> Read<T>(string id, string indexName) where T : class
    {
        IGetRequest request = new GetRequest(indexName, id);
        var res = await _elastic.GetAsync<T>(request);
        var result = res.Source;
        return result;
    }

    public async Task<bool> BulkCreate<T>(List<T> list, string indexName) where T : class
    {
        var res = await _elastic.BulkAsync(x => x.Index(indexName).IndexMany(list));
        var result = res.IsValid;
        return result;
    }
}