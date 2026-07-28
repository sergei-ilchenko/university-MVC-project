namespace Data;

public abstract class UserData<T> : EntityData<T> where T : EntityData<T> {
    public string? Nick { get; set; }
    public string? Name { get; set; }
}