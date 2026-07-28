using Facade;

namespace Tests.Facade;

[TestClass]
public class EntityViewTests : AbstractTests<EntityView, object> {
    protected override EntityView CreateObject() => new TeamView();
    [TestMethod] public void IdTest() => IsProperty<int>();
}