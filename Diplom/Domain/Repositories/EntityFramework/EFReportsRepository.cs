using Diplom.Domain.Repositories.Abstract;
using System.Collections;
using System.Collections.Generic;

namespace Diplom.Domain.Repositories.EntityFramework
{
	public class EFReportsRepository : IReportRepository
	{
		private test_CursachContext _context;
		public EFReportsRepository(test_CursachContext context)
		{
			_context = context;
		}
		public IEnumerable CreateReports(string studName)
		{
			var answers = _context.StudentsAnswers.ToList();
			var alltests = new List<Test1>();
			var questions = new List<Question>();
			int i = 0;
			var userName = User.Identity.Name;
			var studId = _context.Students
				.Where(x => x.LogIn == userName)
				.Select(x => x.StudentId)
				.First();

			while (i < answers.Count)
			{
				foreach (var item in _context.Questions)
				{
					if (answers[i].QuestId == item.QuestId && answers[i].StudentId == studId)
					{
						questions.Add(item);
					}
				}
				i++;
			}

			foreach (var item in _context.Tests1)
			{
				for (int j = 0; j < questions.Count; j++)
				{
					if (item.TestId == questions[j].TestId)
					{
						alltests.Add(item);
						break;
					}
				}
			}
			return alltests.ToArray();
		}
	}
}
