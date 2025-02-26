using System.ComponentModel.DataAnnotations;
using MvcMyBad.Interfaces;
namespace MvcMyBad.Validators;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = (IUserValidationService)validationContext.GetService(typeof(IUserValidationService));
        var isUnique = service.IsEmailUniqueAsync((string)value).GetAwaiter().GetResult();

        if (!isUnique)
        {
            return new ValidationResult("L'adresse e-mail est déjà utilisée.");
        }

        return ValidationResult.Success;
    }
}