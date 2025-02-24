using System.Collections;

namespace Diplom.Domain.Repositories.Abstract;

public interface IReportRepository
{
	IEnumerable CreateReports(string studName);
}