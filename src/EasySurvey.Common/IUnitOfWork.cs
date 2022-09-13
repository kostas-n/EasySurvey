using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Common
{
	public interface IUnitOfWork
	{
		Task<int> CommitAsync(CancellationToken cancellationToken = default);
	}
}
