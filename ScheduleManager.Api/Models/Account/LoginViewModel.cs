using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Api.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Login_UserName")]
        public string UserName { get; set; }

        [Display(Name = "Login_Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Login_RememberMe")]
        [UIHint("Checkbox")]
        public bool RememberMe { get; set; } = false;
    }
}