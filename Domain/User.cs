using Data;

namespace Domain;

public abstract class User<T>(T? d) : Entity<T>(d) where T : UserData<T> {
    public string Nick => data?.Nick ?? string.Empty;
    public string Name => data?.Name ?? string.Empty;
}