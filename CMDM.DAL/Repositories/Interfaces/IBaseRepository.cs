using CMDM.Core.Models;

namespace CMDM.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> 
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
