using Facade;
namespace Tests.Facade;

[TestClass] public class TournEntryViewTests : SealedTests<TournEntryView, EntityView>{
    [TestMethod] public void TourNIdTest() => IsProperty<int>("Tournament ID");
    [TestMethod] public void TeamIdTest() => IsProperty<int>("Team ID");
    [TestMethod] public void TourNTest() => IsProperty<string?>("Tournament name");
    [TestMethod] public void TeamTest() => IsProperty<string?>("Team name");
}