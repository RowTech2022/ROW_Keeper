using System;
using System.ComponentModel.DataAnnotations;

namespace Keeper.Client.Validation
{
    public class TrimWhitespaceAttribute(int maxLength) : ValidationAttribute
    {
        private int MaxLength { get; set; } = maxLength;

        public TrimWhitespaceAttribute() : this(-1)
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var trimValue = value.ToString()!.Trim();

            validationContext.ObjectType
                .GetProperty(validationContext.MemberName!)?
                .SetValue(validationContext.ObjectInstance, trimValue);

            switch (MaxLength)
            {
                case -1:
                    return ValidationResult.Success;
                case 0:
                case < -1:
                    throw new InvalidOperationException(
                        "TrimWhitespaceAttribute must have a length value greater than zero. Use TrimWhitespaceAttribute() without parameters to remove all leading and trailing whitespace characters from the current string.");
                default:
                    return trimValue.Length > MaxLength
                        ? new ValidationResult(
                            $"The field {validationContext.DisplayName} must be a string or array type with a maximum length of '{MaxLength}'.")
                        : ValidationResult.Success;
            }
        }
    }
}