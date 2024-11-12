using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="First Name Is Required")]
        [MaxLength(50,ErrorMessage="Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name Is Required")]
        [MaxLength(20, ErrorMessage = "Max 50 characters allowed.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [MaxLength(256, ErrorMessage = "Max 50 characters allowed.")]
      
        public string Password { get; set; }
       

    }
}
