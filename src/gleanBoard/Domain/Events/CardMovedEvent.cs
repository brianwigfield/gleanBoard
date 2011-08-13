using System;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class CardMovedEvent : DomainEvent
    {
        public Guid Card { get; set; }
        public Guid Lane { get; set; }
        public int Position { get; set; }
    }
}