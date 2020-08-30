using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenWT.Contact.Infrastructure.Contract
{
    public interface IReadOnlyRepository<TEntity, in TId>
    {
        TEntity GetById(TId entityId, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null);
        IEnumerable<TEntity> GetByIds(IEnumerable<TId> entityIds, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null);
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null);
    }
}
