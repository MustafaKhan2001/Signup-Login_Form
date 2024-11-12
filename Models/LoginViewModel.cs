using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "User Name Or Email Is Required")]
        [MaxLength(20, ErrorMessage = "Max 50 characters allowed.")]
        [DisplayName("Username Or Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or Min 5 characters allowed.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
