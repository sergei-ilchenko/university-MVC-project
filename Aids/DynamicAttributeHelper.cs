using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Aids;

public class DynamicDateRangeAttribute : ValidationAttribute {
    private readonly string _startPropertyName;
    private readonly string _endPropertyName;
    public DynamicDateRangeAttribute(string startPropertyName, string endPropertyName) {
        _startPropertyName = startPropertyName;
        _endPropertyName = endPropertyName;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        var startDateProperty = validationContext.ObjectType.GetField(_startPropertyName, BindingFlags.NonPublic | BindingFlags.Static);
        var endDateProperty = validationContext.ObjectType.GetField(_endPropertyName, BindingFlags.NonPublic | BindingFlags.Static);

        if (startDateProperty == null) {
            return new ValidationResult($"Unknown property: {_startPropertyName}");
        }
        if (endDateProperty == null) {
            return new ValidationResult($"Unknown property: {_endPropertyName}");
        }
        var startDate = (DateTime)startDateProperty.GetValue(null);
        var endDate = (DateTime)endDateProperty.GetValue(null);

        if (value is DateTime dateValue) {
            if (dateValue < startDate || dateValue > endDate) {
                return new ValidationResult(String.Empty);
            }
        }
        return ValidationResult.Success;
    }
}