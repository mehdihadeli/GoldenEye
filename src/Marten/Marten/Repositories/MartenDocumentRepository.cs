using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoldenEye.Exceptions;
using GoldenEye.Extensions.Collections;
using GoldenEye.Objects.General;
using GoldenEye.Repositories;
using Marten;

namespace GoldenEye.Marten.Repositories
{
    public class MartenDocumentRepository<TEntity>:
        IRepository<TEntity>,
        IReadonlyRepository<TEntity>
        where TEntity : class, IHaveId
    {
        private readonly IDocumentSession documentSession;

        public MartenDocumentRepository(IDocumentSession documentSession)
        {
            this.documentSession = documentSession ?? throw new ArgumentException(nameof(documentSession));
        }

        public Task<TEntity> FindById(object id, CancellationToken cancellationToken = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id),"Id needs to have value");

            return id switch
            {
                Guid guid => documentSession.LoadAsync<TEntity>(guid, cancellationToken),
                long l => documentSession.LoadAsync<TEntity>(l, cancellationToken),
                int i => documentSession.LoadAsync<TEntity>(i, cancellationToken),
                _ => documentSession.LoadAsync<TEntity>(id.ToString(), cancellationToken)
            };
        }

        public async Task<TEntity> GetById(object id, CancellationToken cancellationToken = default)
        {
            var entity = await FindById(id, cancellationToken);

            return entity ?? throw NotFoundException.For<TEntity>(id);
        }

        public IQueryable<TEntity> Query()
        {
            return documentSession.Query<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> RawQuery(string query,
            CancellationToken cancellationToken = default, params object[] queryParams)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (queryParams == null)
                throw new ArgumentNullException(nameof(queryParams));

            return await documentSession.QueryAsync<TEntity>(query, cancellationToken, queryParams);
        }

        public Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            documentSession.Insert(entity);

            return Task.FromResult(entity);
        }

        public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            documentSession.Update(entity);

            return Task.FromResult(entity);
        }

        public Task<TEntity> Update(TEntity entity, int expectedVersion, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            documentSession.Delete(entity);

            return Task.FromResult(entity);
        }

        public Task<TEntity> Delete(TEntity entity, int expectedVersion, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(object id, CancellationToken cancellationToken = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            switch (id)
            {
                case Guid guid:
                    documentSession.Delete<TEntity>(guid);
                    break;
                case long l:
                    documentSession.Delete<TEntity>(l);
                    break;
                case int i:
                    documentSession.Delete<TEntity>(i);
                    break;
                default:
                    documentSession.Delete<TEntity>(id.ToString());
                    break;
            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteById(object id, int expectedVersion, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges(CancellationToken cancellationToken = default)
        {
            return documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}
