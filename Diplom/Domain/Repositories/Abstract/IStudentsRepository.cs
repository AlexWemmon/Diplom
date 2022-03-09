using Diplom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain.Repositories.Abstract
{
    public interface IStudentsRepository
    {
        IQueryable<Student> GetStudents();        
        Student GetStudentsById(int ? id);
        void SaveStudent(Student entity);
        void DeleteStudent(int ? id);
    }
}
