using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class StudentSubject
    {
        [Display(Name = "Код предмета")]
        public int SubjectId { get; set; }
        [Display(Name = "Название вопроса")]
        public string SubjectName { get; set; }
        [Display(Name = "Код преподавателя")]
        public int TutorId { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}
