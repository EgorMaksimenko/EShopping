using System.Threading;
using System.Threading.Tasks;
using EShopping.Core.Domain.Repositories;

namespace EShopping.Core.Domain
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }

        Task<int> CompleteAsync();

        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
