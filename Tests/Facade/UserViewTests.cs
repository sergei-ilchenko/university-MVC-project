using Facade;

namespace Tests.Facade;

[TestClass] public class UserViewTests : AbstractTests<UserView, EntityView> {
    protected override UserView CreateObject() => new PlayerView();
    [TestMethod] public void NickTest() => IsProperty<string?>();
    [TestMethod] public void NameTest() => IsProperty<string?>();
}