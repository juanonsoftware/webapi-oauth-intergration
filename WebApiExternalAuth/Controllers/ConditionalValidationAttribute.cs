using System.ComponentModel.DataAnnotations;

namespace WebApiExternalAuth.Controllers
{
    public class ConditionalValidationAttribute : ValidationAttribute
    {
        private readonly IConditionalValidator _conditionalValidator;

        public ConditionalValidationAttribute(IConditionalValidator conditionalValidator)
        {
            _conditionalValidator = conditionalValidator;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var objectInstance = validationContext.ObjectInstance;

            if (_conditionalValidator.IsConditionValid(objectInstance))
            {
                return _conditionalValidator.ValidateInstance(objectInstance);
            }

            return base.IsValid(value, validationContext);
        }
    }
}