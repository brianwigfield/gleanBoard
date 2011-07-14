using System;
using gleanBoard.Domain.Events;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.AggregateRoots
{
    public class Card : AggregateRoot
    {
        public Card(Guid id, string lane, string title)
        {
            Apply(new CardCreatedEvent {AggregateRootId = id, Lane = lane, Title = title});
            Id = id;
        }

        public void OnCardCreated(CardCreatedEvent e)
        {
            Id = e.AggregateRootId;
        }

    }
}