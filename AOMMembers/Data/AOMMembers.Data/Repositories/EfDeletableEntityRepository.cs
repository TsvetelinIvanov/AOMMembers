using Microsoft.EntityFrameworkCore;
using AOMMembers.Data.Common.Models;
using AOMMembers.Data.Common.Repositories;
using System.Linq.Expressions;

namespace AOMMembers.Data.Repositories
{
    public class EfDeletableEntityRepository<TEntity> : EfRepository<TEntity>, IDeletableEntityRepository<TEntity> where TEntity : class, IDeletableEntity
    {
        public EfDeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking() => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public IQueryable<TEntity> AllAsNoTrackingWithDeleted() => base.AllAsNoTracking().IgnoreQueryFilters();

        public override async Task<TEntity> GetByIdAsync(params object[] id)
        {
            TEntity entity = await base.GetByIdAsync(id);

            if (entity?.IsDeleted ?? false)
            {
                entity = null;
            }

            return entity;
        }

        public Task<TEntity> GetByIdWithDeletedAsync(params object[] id)
        {
            Expression<Func<TEntity, bool>> byIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);

            return this.AllWithDeleted().FirstOrDefaultAsync(byIdPredicate);
        }

        public void HardDelete(TEntity entity)
        {
            base.Delete(entity);
        }

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;

            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            this.Update(entity);
        }
    }
}