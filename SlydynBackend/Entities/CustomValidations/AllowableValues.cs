using System.ComponentModel.DataAnnotations;

namespace Entities.CustomValidations;

[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public class AllowedStringValue: ValidationAttribute
{
  public string[]? AllowedValues { get; set; }

  public override bool IsValid(object? value)
  {
    if (value == null) return true;
    return AllowedValues != null && AllowedValues.Contains(value.ToString());
  }
}