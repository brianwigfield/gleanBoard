using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class LaneCreatedEvent : DomainEvent
    {
        public string Name { get; set; }
    }
}