using Data;
using Domain;
using Facade;
using Soft.Data;

namespace Soft.Controllers;

public sealed class TournEntryController(ApplicationDbContext c)
    : BaseController<TournEntry, TournEntryData, TournEntryView>(c, new TournEntryViewFactory(), d => new(d))
{ }