using Data;
using Domain;
using Facade;
using Microsoft.AspNetCore.Mvc;
using Soft.Controllers;

namespace Tests.Soft.Controllers;

[TestClass] public class BaseControllerTests() :
    ControllerBaseTests<BaseController<Match, MatchData, MatchView>, Match, MatchData, MatchView> {
    protected override BaseController<Match, MatchData, MatchView> CreateObject() => new MatchController(dbContext!);
    [TestMethod] public override void IsSealedTest() =>
        IsFalse(typeof(BaseController<Match, MatchData, MatchView>).IsSealed);
    [TestMethod] public void IsAbstractTest() =>
        IsTrue(typeof(BaseController<Match, MatchData, MatchView>).IsAbstract);
    [TestMethod] public override void IsBaseTypeTest() =>
        AreEqual(typeof(BaseController<Match, MatchData, MatchView>).BaseType, typeof(Controller));
    protected override Match? createEntity(Func<MatchData> getData) => new(getData());
}