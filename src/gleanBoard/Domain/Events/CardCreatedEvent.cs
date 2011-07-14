using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class CardCreatedEvent : DomainEvent
    {
        public string Lane { get; set; }
        public string Title { get; set; }
    }
}