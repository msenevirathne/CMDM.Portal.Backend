using CMDM.Core.Common;
using CMDM.DAL.Database;
using CMDM.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMDM.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> DbSet;
        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await DbSet.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
