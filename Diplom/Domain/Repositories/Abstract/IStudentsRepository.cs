using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Domain.Repositories.Abstract;

public interface IStudentsRepository
{
	IQueryable<Student> GetStudents();
	Task<Student> GetStudentsById(int? id);
	void SaveStudent(Student entity);
	void DeleteStudent(int ? id);
}