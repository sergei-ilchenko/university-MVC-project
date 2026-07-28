using Aids;
using Data;
using System.ComponentModel.DataAnnotations;

namespace Facade;
public sealed class PlayerView : UserView {
    protected static DateTime start = DateTime.Today.AddYears(-70);
    protected static DateTime end = DateTime.Today.AddYears(-13);
    internal const string sentenceEx = @"^[A-Z][a-zA-Z\s]*$";
    [Display(Name = "Player"), RegularExpression(sentenceEx), Required, StringLength(16, MinimumLength = 3)] public string? Nick { get; set; }
    [RegularExpression(sentenceEx), Required, StringLength(16, MinimumLength = 3)] public string? Name { get; set; }
    public Nationality? Nationality { get; set; }
    
    [Display(Name = "Born"), DataType(DataType.Date), Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DynamicDateRange(nameof(start), nameof(end), ErrorMessage = "Player age must be between 13 and 70 years")]
    public DateTime Born { get; set; } = DateTime.Today;
    [Display(Name = "Age")] public int Age => DateTime.Today.Year - Born.Year - (DateTime.Today < Born.AddYears(DateTime.Today.Year - Born.Year) ? 1 : 0);
    [Display(Name = "Current Team ID")] public int TeamId { get; set; }
    [Display(Name = "Current Team")] public string? TeamName { get; set; }
}