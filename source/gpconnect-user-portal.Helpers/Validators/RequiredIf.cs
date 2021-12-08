using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Helpers.Validators
{
    public class RequiredIfAttribute : RequiredAttribute
    {
        private string PropertyName { get; set; }
        private object Value { get; set; }

        public RequiredIfAttribute(string propertyName, object value, string errorMessage = "")
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Value = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            var propertyValue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (propertyValue.ToString() == Value.ToString() && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
