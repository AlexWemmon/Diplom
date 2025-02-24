using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom;

public partial class Question
{
	public Question()
	{
		RightAnswers = new HashSet<RightAnswer>();
		StudentsAnswers = new HashSet<StudentsAnswer>();
	}
	[Display(Name = "Код Вопроса")]
	public int QuestId { get; set; }
	[Display(Name = "Кол-во баллов за вопрос")]
	public int QuestScore { get; set; }
	[Display(Name = "Текст вопроса")]
	public string QuestText { get; set; }
	[Display(Name = "Код теста")]
	public int TestId { get; set; }
	[Display(Name = "Фото")]
	public string Photo { get; set; }

	public virtual Test1 Test { get; set; }
	public virtual ICollection<RightAnswer> RightAnswers { get; set; }
	public virtual ICollection<StudentsAnswer> StudentsAnswers { get; set; }
}