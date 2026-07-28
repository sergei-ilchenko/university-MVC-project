using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

public sealed class TourNData : EntityData<TourNData> {
    public string? Title { get; set; }
    public DateTime StartDate { get; set; }
    [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)] public decimal PrizePool { get; set; }
    public string? Sponsor { get; set; }
    public int nrParticipants { get; set; }
    public string? Winner { get; set; }
    public Status? Status { get; set; }
}