using System;
using System.Collections.Generic;
using System.Linq;
using gleanBoard.Domain.Events;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Views
{
    public class BoardView :
        IHandleDomainEvents<BoardCreatedEvent>,
        IHandleDomainEvents<LaneCreatedEvent>,
        IHandleDomainEvents<CardCreatedEvent>, 
        IHandleDomainEvents<CardMovedEvent>
    {
        readonly ViewRepository _repo;

        public BoardView(ViewRepository repo)
        {
            _repo = repo;
        }

        public void Handle(CardCreatedEvent domainEvent)
        {
            var board = _repo.Get<Resources.Board>();
            var lane = board.Lanes.First(x => x.Id == domainEvent.Lane);
            if (lane.Cards == null)
                lane.Cards = new List<Resources.Card>();

            lane.Cards.Add(new Resources.Card
                               {
                                   Id = domainEvent.Card,
                                   Title = domainEvent.Title
                               });
            _repo.Save(board);
        }

        public void Handle(BoardCreatedEvent domainEvent)
        {
            var board = _repo.Get<Resources.Board>();
            if (board == null)
                board = new Resources.Board();

            board.Id = domainEvent.AggregateRootId;
            board.Name = domainEvent.Name;
            _repo.Save(board);
        }

        public void Handle(CardMovedEvent domainEvent)
        {
            var board = _repo.Get<Resources.Board>();
            var fromLane = board.Lanes.Single(x => x.Cards != null && x.Cards.Any(c => c.Id == domainEvent.Card));

            var card = fromLane.Cards.Single(x => x.Id == domainEvent.Card);

            fromLane.Cards.Remove(card);
            var toLane = board.Lanes.Single(x => x.Id == domainEvent.Lane);
            if (toLane.Cards == null)
                toLane.Cards = new List<Resources.Card>();
            
            toLane.Cards.Insert(domainEvent.Position, card);

            _repo.Save(board);
        }

        public void Handle(LaneCreatedEvent domainEvent)
        {
            var board = _repo.Get<Resources.Board>();
            if (board.Lanes == null)
                board.Lanes = new List<Resources.Lane>();

            board.Lanes.Insert(domainEvent.Postion, new Resources.Lane
                                                        {
                                                            Id = domainEvent.Lane, 
                                                            Name = domainEvent.Name
                                                        });
            _repo.Save(board);
        }
    }
}