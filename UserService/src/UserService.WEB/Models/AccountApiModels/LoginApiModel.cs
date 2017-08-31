using System.ComponentModel.DataAnnotations;

namespace UserService.WEB.Models.AccountApiModels
{
    public class LoginApiModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}