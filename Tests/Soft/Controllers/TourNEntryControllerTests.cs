using Data;
using Domain;
using Facade;
using Soft.Controllers;

namespace Tests.Soft.Controllers;

[TestClass] public class TourNEntryControllerTests() :
    ControllerBaseTests<TournEntryController, TournEntry, TournEntryData, TournEntryView> {
    protected override TournEntry? createEntity(Func<TournEntryData> getData)
        => new(getData());
    protected override TournEntryController CreateObject() => new(dbContext!);
}