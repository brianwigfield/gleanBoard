using System;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class LaneCreatedEvent : DomainEvent
    {
        public Guid Lane { get; set; }
        public string Name { get; set; }
        public int Postion { get; set; }
    }
}