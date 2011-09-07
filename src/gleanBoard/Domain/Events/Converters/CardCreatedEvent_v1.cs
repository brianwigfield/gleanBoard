using System;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events.Converters
{
    public class CardCreatedEvent_v1 : DomainEvent
    {
        public Guid Card { get; set; }
        public Guid Lane { get; set; }
        public string Title { get; set; }
        public int Postion { get; set; }

        public override int GetHashCode() { return 1726483226;}

    }
}