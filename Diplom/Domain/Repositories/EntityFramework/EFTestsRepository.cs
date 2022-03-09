using Diplom.Domain.Repositories.Abstract;
using Diplom;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Diplom.Domain.Repositories.EntityFramework
{
    public class EFTestRepository : ITestRepository
    {
        private test_CursachContext _context;
        public EFTestRepository (test_CursachContext context)
        {
            _context = context;
        }
        public void DeleteTest(int ? id)
        {
            _context.Remove(new Test1() { TestId = id.Value });
            _context.SaveChanges();
        }
        public Test1 GetTestById(int? id)
        {
            return _context.Tests1.FirstOrDefault(m => m.TestId == id);
        }
        public IQueryable<Test1> GetTests() =>_context.Tests1; 
        public void SaveTest(Test1 entity)
        {
            if (entity.TestId == default) _context.Entry(entity).State = EntityState.Added;
            else _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public bool TestExist(int? id)=> _context.Tests1.Any(e => e.TestId == id);
    }
}
