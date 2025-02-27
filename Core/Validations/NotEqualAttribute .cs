using System.ComponentModel.DataAnnotations;

namespace Core.Validations
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public NotEqualAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value?.ToString();
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                return new ValidationResult($"Property {_comparisonProperty} not found.");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance)?.ToString();

            if (currentValue == comparisonValue)
                return new ValidationResult("The new password must be different from the current one.");

            return ValidationResult.Success;
        }
    }

}
