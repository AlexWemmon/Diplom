using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class RightAnswer
    {
        [Display(Name = "Код ответа")]
        public int AnswerId { get; set; }
        [Display(Name = "Код Вопроса")]
        public int QuestId { get; set; }
        [Display(Name = "Правильный ответ")]
        public string RightAnswer1 { get; set; }

        public virtual Question Quest { get; set; }
    }
}
