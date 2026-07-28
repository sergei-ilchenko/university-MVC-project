using System.ComponentModel.DataAnnotations;
using Data;

namespace Facade;

public sealed class TourNView : EntityView {

    internal const string sentenceEx = @"^[A-Z][a-zA-Z\s]*$";
    [RegularExpression(sentenceEx), Required, StringLength(16, MinimumLength = 3)] public string? Title { get; set; }

    [Display(Name = "Start Date"), DataType(DataType.Date), Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; } = DateTime.Today;
    [Display(Name = "Prize Pool"), Range(0, 10000000), DataType(DataType.Currency)] public decimal PrizePool { get; set; }
    [RegularExpression(sentenceEx), StringLength(16, MinimumLength = 3)] public string? Sponsor { get; set; }
    
    [Display(Name = "Participants"), Range(2, 16), Required, RegularExpression(@"^(2|4|8|16)$", ErrorMessage = "Only values 2, 4, 8, and 16 are allowed.")]
    public int nrParticipants { get; set; }
    [RegularExpression(sentenceEx), StringLength(16, MinimumLength = 3)] public string? Winner { get; set; }
    public Status? Status { get; set; }
}