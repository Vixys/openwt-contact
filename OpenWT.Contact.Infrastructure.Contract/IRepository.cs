using System.Collections.Generic;

namespace OpenWT.Contact.Infrastructure.Contract
{
    public interface IRepository<TEntity, in TId> : IReadOnlyRepository<TEntity, TId>
    {
        TEntity Update(TEntity entity);

        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        TEntity Insert(TEntity entity);

        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        void Delete(params object[] primaryKeys);

        void Delete(List<object[]> primaryKeysList);
    }
}
