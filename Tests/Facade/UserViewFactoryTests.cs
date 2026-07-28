using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class UserViewFactoryTests :
    AbstractTests<UserViewFactory<PlayerData, PlayerView>, AbstractViewFactory<PlayerData, PlayerView>> {
    protected override UserViewFactory<PlayerData, PlayerView> CreateObject()
        => new PlayerViewFactory();
}
