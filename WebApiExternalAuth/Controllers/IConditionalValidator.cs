using System.ComponentModel.DataAnnotations;

namespace WebApiExternalAuth.Controllers
{
    public interface IConditionalValidator
    {
        bool IsConditionValid(object objectInstance);

        ValidationResult ValidateInstance(object objectInstance);
    }
}