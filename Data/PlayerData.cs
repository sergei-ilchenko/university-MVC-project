namespace Data;

public sealed class PlayerData : UserData<PlayerData> {
    public Nationality? Nationality { get; set; }
    public DateTime Born { get; set; }
    public int Age { get; set; }
    public int TeamId { get; set; }
}