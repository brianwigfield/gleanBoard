using System;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class LaneCreatedEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Postion { get; set; }
    }
}