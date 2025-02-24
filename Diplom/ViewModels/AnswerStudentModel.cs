using System.ComponentModel.DataAnnotations;

namespace Diplom.ViewModels;

public class AnswerStudentModel
{
	[Required(ErrorMessage = "Не указан Login")]
	public string Login { get; set; }

	[Required(ErrorMessage = "Не указан пароль")]

	public string Password { get; set; }
}