using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Models.ViewModel
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage ="Username must be filled!")]
        public string USerName { get; set; }

        [Required(ErrorMessage ="Emailid Required")]
        [RegularExpression(@"^\\S+@\\S+\\.\\S+$", ErrorMessage ="Please enter valid Email id")]
        public string EmailID { get; set; }
        [Required(ErrorMessage ="Password required")]
        [MaxLength(8, ErrorMessage ="password length max 8 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm password required")]
        [Compare("Password" , ErrorMessage ="Password is not identical!")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }

    public class LoginModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Emailid Required")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        public string USerName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
