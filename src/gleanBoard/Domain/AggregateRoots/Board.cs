using System;
using gleanBoard.Domain.Events;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.AggregateRoots
{
    public class Board : AggregateRoot
    {

        public Board() {}

        public Board(Guid id, string name)
        {
            Apply(new BoardCreatedEvent {AggregateRootId = id, Name = name});
        }

        public void OnBoardCreated(BoardCreatedEvent e)
        {
            Id = e.AggregateRootId;
        }

        public void CreateLane(Guid id, string name, int position)
        {
            Apply(new LaneCreatedEvent {AggregateRootId = Id, Id = id, Name = name, Postion = position});
        }

        public void CreateCard(Guid id, Guid lane, string title, int position)
        {
            Apply(new CardCreatedEvent {AggregateRootId = Id, Card = id, Lane = lane, Title = title, Postion = position});
        }

        public void MoveCard(Guid card, Guid fromLane, Guid toLane, int position)
        {
            Apply(new CardMovedEvent { AggregateRootId = Id, Card = Id, Lane = toLane, Position = position });
        }

    }
}