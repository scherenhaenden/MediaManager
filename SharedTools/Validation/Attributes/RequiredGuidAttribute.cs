using System.ComponentModel.DataAnnotations;

namespace SharedTools.Validation.Attributes;

public class RequiredGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is Guid guidValue) return guidValue != Guid.Empty;

        return false;
    }
}