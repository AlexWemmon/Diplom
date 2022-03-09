using Diplom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain.Repositories.Abstract
{
    public interface ITestRepository
    {
        IQueryable<Test1> GetTests();
        /*IQueryable<Test1> GetAvailableTest(int? studId);*/
        Test1 GetTestById(int ? id);
        void SaveTest(Test1 entity);
        void DeleteTest(int ? id);
        bool TestExist(int? id);
    }
}
