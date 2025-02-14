using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMyBad.Models;
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; }
    public string Pseudo { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}
public class GetUserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Pseudo { get; set; }
    public bool IsAdmin { get; set; }
}
public class PostUserDto
{
    [Required(ErrorMessage = "Le nom ne peut pas être vide.Veuillez saisir un nom")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Le pseudo est obligatoire")]
    public string Pseudo { get; set; }
    [Required(ErrorMessage = "Veuillez saisir une adresse e-mail.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
      ErrorMessage = "L'adresse e-mail n'est pas valide.")]
    [DefaultValue("exemple@domaine.com")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%/*?&]{12,}$",
    ErrorMessage = "Le mot de passe doit contenir au moins 12 caractères, dont une masjucule, une minuscule et un caractère spéciale.")]
    [DefaultValue("Myp@ssword123!")]
    public string Password { get; set; }
    [Required(ErrorMessage = "veuillez Confirmer votre mot de passe.")]
    [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
    public string ConfirmPassaword { get; set; }

    [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
    [RegularExpression(@"^(\+33|0)[1-9](\d{2}){4}$",
    ErrorMessage = "Le numéro de téléphone n'est pas valide. Exemple : 06 12 34 56 78 ou +33 6 12 34 56 78.")]
    public string PhoneNumber { get; set; }
}