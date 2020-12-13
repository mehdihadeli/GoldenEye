using System;
using System.Collections.Generic;
using GoldenEye.Core.Objects.General;

namespace GoldenEye.DDD.Events
{
    public abstract class EventSource: IEventSource
    {
        protected EventSource()
        {
            PendingEvents = new Queue<IEvent>();
        }

        public Guid Id { get; protected set; }
        object IHaveId.Id => Id;

        public Queue<IEvent> PendingEvents { get; }

        protected void Append(IEvent @event)
        {
            PendingEvents.Enqueue(@event);
        }
    }
}
