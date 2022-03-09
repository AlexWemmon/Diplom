using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class StudentsAnswer
    {
        [Display(Name = "Код ответа")]
        public int AnswerId { get; set; }
        [Display(Name = "Код студента")]
        public int StudentId { get; set; }
        [Display(Name = "Код вопроса")]
        public int QuestId { get; set; }
        [Display(Name = "Введённый ответ")]
        public string EnteredAnswer { get; set; }

        public virtual Question Quest { get; set; }
        public virtual Student Student { get; set; }
    }
}
