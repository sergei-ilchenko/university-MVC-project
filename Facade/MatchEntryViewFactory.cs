using Data;
using Domain;

namespace Facade;

public sealed class MatchEntryViewFactory : AbstractViewFactory<MatchEntryData, MatchEntryView> {
    public override async Task<MatchEntryView> CreateView(MatchEntryData? d, bool loadLazy = false)
    {
        var v = await base.CreateView(d, loadLazy);
        if (!loadLazy) return v;
        var o = new MatchEntry(d);
        await o.LoadLazy();
        v.MatchName = o.Match?.Title;
        v.TeamName = o.Team?.Name;
        return v;
    }
}