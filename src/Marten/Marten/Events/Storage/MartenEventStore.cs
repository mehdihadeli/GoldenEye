using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoldenEye.Events;
using GoldenEye.Events.Store;
using GoldenEye.Objects.General;
using Marten;
using MartenEvents = Marten.Events;

namespace GoldenEye.Marten.Events.Storage
{
    public class MartenEventStore: IEventStore
    {
        private readonly IDocumentSession documentSession;

        public MartenEventStore(IDocumentSession documentSession)
        {
            this.documentSession = documentSession ?? throw new ArgumentException(nameof(documentSession));
        }

        public void SaveChanges()
        {
            documentSession.SaveChanges();
        }

        public Task SaveChangesAsync(CancellationToken token = default)
        {
            return documentSession.SaveChangesAsync(token);
        }

        public Guid Append(Guid streamId, params IEvent[] events)
        {
            return documentSession.Events.Append(streamId, events.Cast<object>().ToArray()).Id;
        }

        public Guid Append(Guid streamId, int version, params IEvent[] events)
        {
            return documentSession.Events.Append(streamId, version, events.Cast<object>().ToArray()).Id;
        }

        public Task<Guid> AppendAsync(Guid streamId, params IEvent[] events)
        {
            return Task.FromResult(Append(streamId, events));
        }

        public Task<Guid> AppendAsync(Guid streamId, int version, params IEvent[] events)
        {
            return Task.FromResult(Append(streamId, version, events));
        }

        public Task<Guid> AppendAsync(Guid streamId, CancellationToken cancellationToken = default,
            params IEvent[] events)
        {
            return Task.FromResult(Append(streamId, events));
        }

        public Task<Guid> AppendAsync(Guid streamId, int version, CancellationToken cancellationToken = default,
            params IEvent[] events)
        {
            return Task.FromResult(Append(streamId, version, events));
        }

        public TEntity Aggregate<TEntity>(Guid streamId, int version = 0, DateTime? timestamp = null)
            where TEntity : class, new()
        {
            return documentSession.Events.AggregateStream<TEntity>(streamId, version, timestamp);
        }

        public Task<TEntity> AggregateAsync<TEntity>(Guid streamId, int version = 0, DateTime? timestamp = null, CancellationToken cancellationToken = default)
            where TEntity : class, new()
        {
            return documentSession.Events.AggregateStreamAsync<TEntity>(streamId, version, timestamp);
        }

        public Task<TEntity> AggregateAsync<TEntity>(Guid streamId, CancellationToken cancellationToken = default,
            int version = 0, DateTime? timestamp = null) where TEntity : class, new()
        {
            return documentSession.Events.AggregateStreamAsync<TEntity>(streamId, version, timestamp,
                token: cancellationToken);
        }

        public TEvent FindById<TEvent>(Guid id)
            where TEvent : class, IEvent, IHaveGuidId
        {
            return documentSession.Events.Load<TEvent>(id)?.Data;
        }

        public async Task<TEvent> FindByIdAsync<TEvent>(Guid id, CancellationToken cancellationToken = default)
            where TEvent : class, IEvent, IHaveGuidId
        {
            return (await documentSession.Events.LoadAsync<TEvent>(id, cancellationToken))?.Data;
        }

        public IList<IEvent> Query(Guid? streamId = null, int? version = null, DateTime? timestamp = null)
        {
            return Filter(streamId, version, timestamp)
                .ToList()
                .Select(ev => ev.Data)
                .OfType<IEvent>()
                .ToList();
        }

        public Task<IList<IEvent>> QueryAsync(Guid? streamId = null, int? version = null, DateTime? timestamp = null)
        {
            return QueryAsync(default, streamId, version, timestamp);
        }

        public async Task<IList<IEvent>> QueryAsync(CancellationToken cancellationToken = default,
            Guid? streamId = null, int? version = null, DateTime? timestamp = null)
        {
            return (await Filter(streamId, version, timestamp)
                    .ToListAsync(cancellationToken))
                .Select(ev => ev.Data)
                .OfType<IEvent>()
                .ToList();
        }

        public IList<TEvent> Query<TEvent>(Guid? streamId = null, int? version = null, DateTime? timestamp = null)
            where TEvent : class, IEvent
        {
            return Query(streamId, version, timestamp)
                .OfType<TEvent>()
                .ToList();
        }

        public Task<IList<TEvent>> QueryAsync<TEvent>(Guid? streamId = null, int? version = null,
            DateTime? timestamp = null) where TEvent : class, IEvent
        {
            return QueryAsync<TEvent>(default, streamId, version, timestamp);
        }

        public async Task<IList<TEvent>> QueryAsync<TEvent>(CancellationToken cancellationToken = default,
            Guid? streamId = null, int? version = null, DateTime? timestamp = null) where TEvent : class, IEvent
        {
            return (await QueryAsync(cancellationToken, streamId, version, timestamp))
                .OfType<TEvent>()
                .ToList();
        }

        private IQueryable<MartenEvents.IEvent> Filter(Guid? streamId, int? version, DateTime? timestamp)
        {
            var query = documentSession.Events.QueryAllRawEvents().AsQueryable();

            if (streamId.HasValue)
                query = query.Where(ev => ev.StreamId == streamId);

            if (version.HasValue)
                query = query.Where(ev => ev.Version >= version);

            if (timestamp.HasValue)
                query = query.Where(ev => ev.Timestamp >= timestamp);

            return query;
        }

        public class EventProjectionStore: IEventProjectionStore
        {
            private readonly IDocumentSession documentSession;

            public EventProjectionStore(IDocumentSession documentSession)
            {
                this.documentSession = documentSession ?? throw new ArgumentException(nameof(documentSession));
            }

            public IQueryable<TProjection> Query<TProjection>()
            {
                return documentSession.Query<TProjection>();
            }

            IQueryable<TProjection> IEventProjectionStore.CustomQuery<TProjection>(string query)
            {
                return documentSession.Query<TProjection>(query).AsQueryable();
            }

            TProjection IEventProjectionStore.GetById<TProjection>(Guid id)
            {
                return Query<TProjection>()
                    .SingleOrDefault(p => p.Id == id);
            }

            Task<TProjection> IEventProjectionStore.GetByIdAsync<TProjection>(Guid id,
                CancellationToken cancellationToken)
            {
                return Query<TProjection>()
                    .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
            }
        }
    }
}
