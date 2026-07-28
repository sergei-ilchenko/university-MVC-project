using Data;
using Domain;
using Facade;
using Soft.Controllers;

namespace Tests.Soft.Controllers;

[TestClass] public class PlayerControllerTests() :
    ControllerBaseTests<PlayerController, Player, PlayerData, PlayerView> {
    protected override Player? createEntity(Func<PlayerData> getData)
        => new(getData());
    protected override PlayerController CreateObject() => new(dbContext!);
}