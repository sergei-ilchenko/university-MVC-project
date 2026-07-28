using Facade;
namespace Tests.Facade;

[TestClass] public class MatchEntryViewTests : SealedTests<MatchEntryView, EntityView> {
    [TestMethod] public void MatchIdTest() => IsProperty<int>("Match Id");
    [TestMethod] public void TeamIdTest() => IsProperty<int>("Team Id");
    [TestMethod] public void MatchNameTest() => IsProperty<string?>("Match name");
    [TestMethod] public void TeamNameTest() => IsProperty<string?>("Team name");
}