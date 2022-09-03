using System.ComponentModel.DataAnnotations;

namespace TglCA.Mvc.PL.Models
{
    public record UserInputModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; init; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; init; } = default!;
    }
}
