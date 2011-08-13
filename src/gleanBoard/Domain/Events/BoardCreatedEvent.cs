using System;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class BoardCreatedEvent : DomainEvent
    {
        public string Name { get; set; }
    }
}