using Core;
using Domain;
using Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Soft.Data;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddRazorPages();
        builder.Services.AddAuthentication()
            .AddCookie()
    .AddGoogle(googleOptions =>
    {
        var googleAuth = builder.Configuration.GetSection("Authentication:Google");
        googleOptions.ClientId = googleAuth["ClientId"];
        googleOptions.ClientSecret = googleAuth["ClientSecret"];
        googleOptions.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
        googleOptions.SaveTokens = true;

        googleOptions.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            context.Response.Redirect(context.RedirectUri + "&prompt=select_account");
            return Task.CompletedTask;
        };
    });

        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<DbContext, ApplicationDbContext>();
        builder.Services.AddTransient<ITourNRepo, TourNRepo>();
        builder.Services.AddTransient<ITournEntriesRepo, TournEntriesRepo>();
        builder.Services.AddTransient<ITeamsRepo, TeamsRepo>();
        builder.Services.AddTransient<IPlayersRepo, PlayersRepo>();
        builder.Services.AddTransient<IMatchRepo, MatchRepo>();
        builder.Services.AddTransient<IMatchEntriesRepo, MatchEntriesRepo>();
        builder.Services.AddTransient<IRatingsRepo, RatingsRepo>();

        Services.init(builder.Services);
        var app = builder.Build();

        if (app.Environment.EnvironmentName == "Testing")
        {
            app.UseMigrationsEndPoint();
        }
        else if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            SeedData(app);
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapStaticAssets();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.MapRazorPages()
            .WithStaticAssets();

        var supportedCultures = new[] { "et-EE" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("et-EE")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
        app.Run();
    }
    private static void SeedData(WebApplication app)
    {
        Task.Run(async () => {
            IServiceProvider? services = null;
            try
            {
                using var scope = app.Services.CreateScope();
                services = scope.ServiceProvider;
                var db = services.GetRequiredService<ApplicationDbContext>();
                await new DbInitializer(db).Initialize(0);
            }
            catch (Exception e)
            {
                var logger = services?.GetRequiredService<ILogger<Program>>();
                logger?.LogError(e, "An error occurred while seeding the database.");
            }
        });
    }
}
