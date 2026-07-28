namespace Core;

public interface IEntity {
    public int? Id { get; }
    public Task LoadLazy();
}