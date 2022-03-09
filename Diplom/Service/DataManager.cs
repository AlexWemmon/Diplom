using Diplom.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Service
{
    public class DataManager
    {
        public IQuestionsItemsRepository _questsRepository;
        public IStudentsRepository _studRepository;
        public ITestRepository _TestRepository;
        public DataManager(IQuestionsItemsRepository questsRepository,
            IStudentsRepository studRepository, ITestRepository TestRepository)
        {
            _questsRepository = questsRepository;
            _studRepository = studRepository;
            _TestRepository = TestRepository;
        }
    }
}
