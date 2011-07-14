using System;
using gleanBoard.Domain.Events;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.AggregateRoots
{
    public class Lane : AggregateRoot
    {
        public Lane(Guid id, string name)
        {
            Apply(new LaneCreatedEvent {AggregateRootId = id, Name = name});
        }

        public void OnLaneCreated(LaneCreatedEvent e)
        {
            Id = e.AggregateRootId;
        }

    }
}