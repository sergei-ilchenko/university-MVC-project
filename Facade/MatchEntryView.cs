using System.ComponentModel.DataAnnotations;

namespace Facade;

public sealed class MatchEntryView : EntityView {
    [Display(Name = "Match Id")] public int MatchId { get; set; }
    [Display(Name = "Team Id")] public int TeamId { get; set; }
    [Display(Name = "Match name")] public string? MatchName { get; set; }
    [Display(Name = "Team name")] public string? TeamName { get; set; }
}