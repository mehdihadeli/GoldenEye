using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoldenEye.Aggregates;
using GoldenEye.Extensions.Collections;
using GoldenEye.Objects.General;
using MediatR;

namespace GoldenEye.Repositories
{
    public static class RepositoryExtensions
    {
        public static async Task<IReadOnlyCollection<TEntity>> AddAll<TEntity>(
            this IRepository<TEntity> repository,
            CancellationToken cancellationToken = default,
            params TEntity[] entities
        )
        where TEntity : class, IHaveId
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (entities.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(entities), entities.Length,
                    $"{nameof(AddAll)} needs to have at least one entity provided.");

            var result = new List<TEntity>();
            foreach (var entity in entities)
            {
                result.Add(await repository.Add(entity, cancellationToken));
            }
            return result;
        }

        public static async Task<Unit> GetAndUpdate<TEntity>(
            this IRepository<TEntity> repository,
            Guid id,
            Action<TEntity> action,
            CancellationToken cancellationToken = default
        )
            where TEntity : class, IHaveId
        {
            var entity = await repository.GetById(id, cancellationToken);

            action(entity);

            await repository.Update(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
