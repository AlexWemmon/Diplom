using System.ComponentModel.DataAnnotations;

namespace Diplom.ViewModels;

public class RegisterModel
{
	[Required(ErrorMessage = "Не указан логин")]
	public string Login { get; set; }

	[Required(ErrorMessage = "Не указан пароль")]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	[DataType(DataType.Password)]
	[Compare("Password", ErrorMessage = "Пароль введен неверно")]
	public string ConfirmPassword { get; set; }
}