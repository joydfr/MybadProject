using System.ComponentModel.DataAnnotations;
using MvcMyBad.Interfaces;
namespace MvcMyBad.Validators;

public class UniquePseudoAttribute : ValidationAttribute
{


    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = (IUserValidationService)validationContext.GetService(typeof(IUserValidationService));
        var isUnique = service.IsPseudoUniqueAsync((string)value).GetAwaiter().GetResult();

        if (!isUnique)
        {
            return new ValidationResult("Le pseudo est déjà utilisé.");
        }

        return ValidationResult.Success;
    }

}