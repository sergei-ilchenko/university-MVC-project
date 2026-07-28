using Data;
using Soft.Controllers;

namespace Tests.Soft.Views;

[TestClass] [DoNotParallelize] public class PlayersHostTests : HostTests<PlayerController, PlayerData> { }