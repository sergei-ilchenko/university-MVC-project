using Data;
using Domain;

namespace Facade;

public sealed class TourNViewFactory : AbstractViewFactory<TourNData, TourNView> {
    public TourNView Create(TourN t)
    {
        return new TourNView
        {
            Id = t.Id ?? 0,
            Title = t.Title,
            StartDate = t.StartDate,
            PrizePool = t.PrizePool,
            Sponsor = t.Sponsor,
            nrParticipants = t.nrParticipants,
            Winner = t.Winner,
            Status = t.Status
        };
    }
}