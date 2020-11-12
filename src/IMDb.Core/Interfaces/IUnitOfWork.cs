using System.Threading.Tasks;

namespace IMDb.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
