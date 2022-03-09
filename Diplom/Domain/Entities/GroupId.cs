using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class GroupId
    {
        public GroupId()
        {
            Students = new HashSet<Student>();
        }
        [Display(Name = "Код группы")]
        public int GroupId1 { get; set; }
        [Display(Name = "Название группы")]
        public string GroupName { get; set; }
        [Display(Name = "Курс")]
        public string Course { get; set; }
        [Display(Name = "Код Специальности")]
        public int SpecialId { get; set; }

        public virtual Specialization Special { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
