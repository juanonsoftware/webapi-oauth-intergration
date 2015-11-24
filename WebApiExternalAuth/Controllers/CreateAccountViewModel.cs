using System.ComponentModel.DataAnnotations;

namespace WebApiExternalAuth.Controllers
{
    public class CreateAccountViewModel
    {
        public bool CustomizationEnabled { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}