using System;
using System.ComponentModel.DataAnnotations;

namespace Keeper.Client.Validation
{
    public class TrimWhitespaceAttribute : ValidationAttribute
    {
        private int MaxLength { get; set; }

        public TrimWhitespaceAttribute()
        {
            MaxLength = -1;
        }

        public TrimWhitespaceAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var trimValue = value.ToString().Trim();

            validationContext.ObjectType
                .GetProperty(validationContext.MemberName)?
                .SetValue(validationContext.ObjectInstance, trimValue);

            if (MaxLength == -1) return ValidationResult.Success;

            if (MaxLength == 0 || MaxLength < -1)
                throw new InvalidOperationException(
                    "TrimWhitespaceAttribute must have a length value greater than zero. Use TrimWhitespaceAttribute() without parameters to remove all leading and trailing whitespace characters from the current string.");

            return trimValue.Length > MaxLength
                ? new ValidationResult(
                    $"The field {validationContext.DisplayName} must be a string or array type with a maximum length of '{MaxLength}'.")
                : ValidationResult.Success;
        }
    }
}