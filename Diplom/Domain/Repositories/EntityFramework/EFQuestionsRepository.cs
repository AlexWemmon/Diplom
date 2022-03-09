using Diplom.Domain.Repositories.Abstract;
using Diplom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Domain.Repositories.EntityFramework
{
    public class EFQuestionsRepository : IQuestionsItemsRepository
    {
        private readonly test_CursachContext _context;
        public EFQuestionsRepository() { }
        private EFQuestionsRepository(test_CursachContext context)
        {
            _context = context;
        }
        public void DeleteQuestion(int ? id)
        {
            _context.Remove(new Question() { QuestId = id.Value});
            _context.SaveChanges();
        }
        public Question GetQuestById(int ? id) =>
          _context.Questions.
            FirstOrDefault(x => x.QuestId == id);
        public IQueryable<Question> GetQuestions() => _context.Questions;
        public void SaveQuestion(Question entity)
        {
            if (entity.QuestId == default) _context.Entry(entity).State = EntityState.Added;
            else _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
