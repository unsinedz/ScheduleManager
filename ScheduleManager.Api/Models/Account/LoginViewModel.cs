using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Api.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Errors_Required")]
        [Display(Name = "Login_UserName")]
        public string UserName { get; set; }

        [Display(Name = "Login_Password")]
        [Required(ErrorMessage = "Errors_Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Login_RememberMe")]
        [Required(ErrorMessage = "Errors_Required")]
        [UIHint("Checkbox")]
        public bool RememberMe { get; set; } = false;
    }
}