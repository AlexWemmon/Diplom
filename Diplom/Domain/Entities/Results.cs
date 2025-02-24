using System.ComponentModel.DataAnnotations;

namespace Diplom.Domain.Entities;

public partial class Results
{
	[Display(Name = "Кол-во балллов")]
	public double Score { get; set; }
	[Display(Name = "Статус работы")]
	public bool passed { get; set; }
	[Display(Name = "Максимально возможный балл")]
	public double MaxScore { get; set; }
}