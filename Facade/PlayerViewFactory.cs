using Data;
using Domain;

namespace Facade;

public sealed class PlayerViewFactory : UserViewFactory<PlayerData, PlayerView> {
    public override async Task<PlayerView> CreateView(PlayerData? d, bool loadLazy = false)
    {
        var v = await base.CreateView(d, loadLazy);
        if (!loadLazy) return v;
        var o = new Player(d);
        await o.LoadLazy();
        v.TeamName = o?.Team?.Name;
        return v;
    }
}