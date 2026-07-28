using Data;
using Domain;

namespace Facade;

public sealed class TournEntryViewFactory : AbstractViewFactory<TournEntryData, TournEntryView> {
    public override async Task<TournEntryView> CreateView(TournEntryData? d, bool loadLazy = false)
    {
        var v = await base.CreateView(d, loadLazy);
        if (!loadLazy) return v;
        var o = new TournEntry(d);
        await o.LoadLazy();
        v.TourN = o.TourN?.Title;
        v.Team = o.Team?.Name;
        return v;
    }
}