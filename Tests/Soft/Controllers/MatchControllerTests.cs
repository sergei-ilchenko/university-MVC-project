using Data;
using Domain;
using Facade;
using Soft.Controllers;

namespace Tests.Soft.Controllers;

[TestClass] public class MatchControllerTests() :
    ControllerBaseTests<MatchController, Match, MatchData, MatchView> {
    protected override Match? createEntity(Func<MatchData> getData)
        => new(getData());
    protected override MatchController CreateObject() => new(dbContext!);
}