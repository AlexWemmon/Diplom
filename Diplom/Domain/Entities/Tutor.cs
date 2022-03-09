using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class Tutor
    {
        public Tutor()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }
        [Display(Name = "Код преподавателя")]
        public int TutorId { get; set; }
        [Display(Name = "ФИО преподавателя")]
        public string Fio { get; set; }
        [Display(Name = "Логин")]
        public string LogIn { get; set; }
        [Display(Name = "Пароль")]
        public string PassWord { get; set; }
        [Display(Name = "Роль преподателя")]
        public string TutorRole { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
