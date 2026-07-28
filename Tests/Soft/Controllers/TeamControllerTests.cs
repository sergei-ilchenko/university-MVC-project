using Core;
using Data;
using Domain;
using Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Soft.Controllers;
using Tests.Domain;

namespace Tests.Soft.Controllers;

[TestClass]
public class TeamControllerTests : ControllerBaseTests<TeamController, Team, TeamData, TeamView> {
    protected override Team? createEntity(Func<TeamData> getData) => new(getData());
    protected override TeamController CreateObject() => new(dbContext!);

    [TestInitialize]
    public override void Initialize() {
        base.Initialize();
        RegisterMockServices();
    }

    protected override async Task get(int pageIdx, string? orderBy = null, string? filter = null, int? selectedId = null) {
        list = dbSet!.ToList();

        SetupControllerServices();
        var result = await obj!.Index(pageIdx, orderBy, filter, selectedId);
        IsOfType(result, typeof(ViewResult));
    }

    protected override void SeedIndexTestData() {
        for (int i = 1; i <= 5; i++) {
            var team = new TeamData { Name = $"Test{i}", PlayersCount = 4 + i };
            dbContext.Teams.Add(team);
            dbContext.SaveChanges();

            var rating = new RatingData { TeamId = team.Id, Value = 100 * i, UpdatedAt = DateTime.UtcNow };
            dbContext.Ratings.Add(rating);
            dbContext.SaveChanges();
        }
    }

    protected override void SetupControllerServices() {
        var serviceProvider = BuildServiceProvider();
        var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
        obj!.ControllerContext = new ControllerContext {
            HttpContext = httpContext,
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor(),
            RouteData = new Microsoft.AspNetCore.Routing.RouteData()
        };
        var urlHelperFactory = serviceProvider.GetRequiredService<IUrlHelperFactory>();
        obj.Url = urlHelperFactory.GetUrlHelper(obj.ControllerContext);
    }

    private void RegisterMockServices() {
        Services.Register<IPlayersRepo>(new MockPlayerRepo(0));
        Services.Register<IRatingsRepo>(new MockRatingRepo(0));
    }

    private ServiceProvider BuildServiceProvider() {
        var services = new ServiceCollection();
        services.AddSingleton(dbContext!);
        services.AddControllersWithViews();
        services.AddSingleton<IPlayersRepo>(new MockPlayerRepo(0));
        services.AddSingleton<IRatingsRepo>(new MockRatingRepo(0));
        return services.BuildServiceProvider();
    }
}