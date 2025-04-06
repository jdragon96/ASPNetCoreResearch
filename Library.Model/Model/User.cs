using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("User ID")]
        public string UserID { get; set; }

        [Required]
        [DisplayName("User Password")]
        public string UserPassword { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DisplayName("User Role")]
        public UserRoleType Role { get; set; } = UserRoleType.ADMIN;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
