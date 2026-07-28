using Core;
using Data;

namespace Domain;

public abstract class Entity<TData>(TData? d): IEntity where TData : EntityData<TData> {
    public TData? data { get; } = d?.Clone();
    public int? Id => data?.Id;
    public virtual async Task LoadLazy() => await Task.CompletedTask;
    protected internal static async Task<TItem?> getItem<TRepo, TItem>(int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
        => await Services.GetItem<TRepo, TItem>(id);
    protected internal static async Task<TItem?> getItem<TRepo, TItem>(string propertyName, int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
        => await Services.GetItem<TRepo, TItem>(propertyName, id);
    protected internal static async Task<IEnumerable<TItem?>> getList<TRepo, TItem>(string propertyName, int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
        => await Services.GetList<TRepo, TItem>(propertyName, id);
}