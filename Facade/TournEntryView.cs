using System.ComponentModel.DataAnnotations;

namespace Facade;

public sealed class TournEntryView : EntityView {
    [Display(Name = "Tournament ID")] public int TourNId { get; set; }
    [Display(Name = "Team ID")] public int TeamId { get; set; }
    [Display(Name = "Tournament name")] public string? TourN { get; set; }
    [Display(Name = "Team name")] public string? Team { get; set; }
}