using Data;

namespace Tests.Data;

[TestClass]
public class UserDataTests : AbstractTests<UserData<PlayerData>, EntityData<PlayerData>>
{
    protected override UserData<PlayerData> CreateObject() => new PlayerData();
    [TestMethod] public void NickTest() => IsProperty<string>();
    [TestMethod] public void NameTest() => IsProperty<string>();
}