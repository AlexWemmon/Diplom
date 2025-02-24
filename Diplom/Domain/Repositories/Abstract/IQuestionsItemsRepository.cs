using System.Linq;

namespace Diplom.Domain.Repositories.Abstract;

public interface IQuestionsItemsRepository
{
	IQueryable<Question> GetQuestions();
	Question GetQuestById(int ? id);
	void SaveQuestion(Question entity);
	void DeleteQuestion(int ? id);
}