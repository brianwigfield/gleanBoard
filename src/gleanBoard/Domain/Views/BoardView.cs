using System;
using System.Linq;
using gleanBoard.Domain.Events;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Views
{
    public class BoardView : IHandleDomainEvents<CardCreatedEvent>, IHandleDomainEvents<LaneCreatedEvent>
    {
        readonly Resources.Board _board;

        public BoardView(Resources.Board board)
        {
            _board = board;
        }

        public void Handle(CardCreatedEvent domainEvent)
        {
            var lane = _board.Lanes.First(x => x.Id == domainEvent.Lane);
            if (lane.Cards == null)
                lane.Cards = new System.Collections.Generic.List<Resources.Card>();

            lane.Cards.Add(new Resources.Card
                               {
                                   Id = domainEvent.AggregateRootId.ToString(),
                                   Title = domainEvent.Title
                               });
        }

        public void Handle(LaneCreatedEvent domainEvent)
        {
            _board.Lanes.Add(new Resources.Lane {Id = domainEvent.AggregateRootId.ToString(), Name = domainEvent.Name});
        }
    }
}