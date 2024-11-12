using System.ComponentModel.DataAnnotations;

namespace CustomAuth.Models
{
    public class RegistrationViewModel
    {

        [Required(ErrorMessage = "First Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        // [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name Is Required")]
        [MaxLength(20, ErrorMessage = "Max 50 characters allowed.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [StringLength(20,MinimumLength =5, ErrorMessage = "Max 20 or Min 5 characters allowed.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }


        [Compare("Password", ErrorMessage ="Please Confirm Your Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
