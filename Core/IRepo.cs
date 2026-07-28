namespace Core;

public interface IRepo<TObject> {
    public Task<int> PageCount(byte pageSize, string? filter);
    public Task<IEnumerable<TObject>> Get(int pageIdx, byte pageSize
        , string? orderBy = null, string? filter = null);
    public Task<IEnumerable<TObject>> Get(string propertyName, int idValue);
    public Task<IEnumerable<TObject>> Get();
    public Task<TObject?> Get(int? id);
    public Task Add(TObject o);
    public Task Update(TObject o);
    public Task Delete(int id);
}