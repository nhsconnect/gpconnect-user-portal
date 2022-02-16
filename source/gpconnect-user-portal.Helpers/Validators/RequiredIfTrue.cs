using System;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Helpers.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfTrueAttribute : RequiredAttribute
    {
        private string PropertyName { get; set; }

        public RequiredIfTrueAttribute(string propertyName, string errorMessageResourceName, Type errorMessageResourceType)
        {
            PropertyName = propertyName;
            ErrorMessageResourceName = errorMessageResourceName;
            ErrorMessageResourceType = errorMessageResourceType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            bool.TryParse(type.GetProperty(PropertyName)?.GetValue(instance)?.ToString(), out bool propertyValue);

            if (propertyValue && string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
