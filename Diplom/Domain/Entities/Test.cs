using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class Test
    {
        [Display(Name = "Код теста")]
        public int TestId { get; set; }
        [Display(Name = "Имя теста")]
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
    }
}
