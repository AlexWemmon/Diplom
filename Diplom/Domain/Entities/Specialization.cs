using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diplom
{
    public partial class Specialization
    {
        public Specialization()
        {
            GroupIds = new HashSet<GroupId>();
            Participants = new HashSet<Participant>();
        }
        [Display(Name = "Код специальности")]
        public int SpecialId { get; set; }
        [Display(Name = "Название специальности")]
        public string SpecialName { get; set; }

        public virtual ICollection<GroupId> GroupIds { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
