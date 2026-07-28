using Data;
using Domain;
using Facade;
using Soft.Controllers;

namespace Tests.Soft.Controllers;

[TestClass] public class MatchEntryControllerTests() :
    ControllerBaseTests<MatchEntryController, MatchEntry, MatchEntryData, MatchEntryView> {
    protected override MatchEntry? createEntity(Func<MatchEntryData> getData)
        => new(getData());
    protected override MatchEntryController CreateObject() => new(dbContext!);
}