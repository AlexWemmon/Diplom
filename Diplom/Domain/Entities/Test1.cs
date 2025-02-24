using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Diplom.Domain.Entities;

#nullable disable

namespace Diplom;

public partial class Test1
{
	public Test1()
	{
		Participants = new HashSet<Participant>();
		Questions = new HashSet<Question>();
	}
	[Display(Name = "Код теста")]
	public int TestId { get; set; }
	[Display(Name = "Название теста")]
	public string TestName { get; set; }
	[Display(Name = "Код предмета")]
	public int SubjectId { get; set; }
	[Display(Name = "Код автора")]
	public int AuthorId { get; set; }
	[Display(Name = "Время на прохождение теста")]
	public TimeSpan TestTime { get; set; }
	[Display(Name = "Проходной балл")]
	public int MinScore { get; set; }
	[Display(Name = "Время сдачи теста")]
	public DateTime TestDate { get; set; }
	public virtual ICollection<Participant> Participants { get; set; }
	public virtual ICollection<Question> Questions { get; set; }
}