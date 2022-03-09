using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class Student
    {
        public Student()
        {
            StudentsAnswers = new HashSet<StudentsAnswer>();
        }
        [Display(Name = "Код студента")]
        public int StudentId { get; set; }
        [Display(Name = "ФИО студента")]
        public string Fio { get; set; }
        [Display(Name = "Код группы")]
        public int GroupId { get; set; }
        [Display(Name = "Логин")]
        public string LogIn { get; set; }
        [Display(Name = "Пароль")]
        public string PassWord { get; set; }

        public virtual GroupId Group { get; set; }
        public virtual ICollection<StudentsAnswer> StudentsAnswers { get; set; }
    }
}
