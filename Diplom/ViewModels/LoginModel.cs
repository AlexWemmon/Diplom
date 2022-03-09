using System.ComponentModel.DataAnnotations;

namespace Diplom.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Логин")]
        [Display(Name ="Логин")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан Пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}