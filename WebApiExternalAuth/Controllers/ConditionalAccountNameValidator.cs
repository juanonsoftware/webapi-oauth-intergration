using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiExternalAuth.Controllers
{
    public class ConditionalAccountNameValidator : IConditionalValidator
    {
        public bool IsConditionValid(object objectInstance)
        {
            throw new NotImplementedException();
        }

        public ValidationResult ValidateInstance(object objectInstance)
        {
            throw new NotImplementedException();
        }
    }
}