using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public class AnswerStudentModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]

        public string Password { get; set; }
    }
}
