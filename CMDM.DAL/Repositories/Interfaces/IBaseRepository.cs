namespace CMDM.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> 
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
