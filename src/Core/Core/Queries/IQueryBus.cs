﻿using System.Threading;
using System.Threading.Tasks;

namespace GoldenEye.Queries
{
    public interface IQueryBus
    {
        Task<TResponse> SendAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>;
    }
}
