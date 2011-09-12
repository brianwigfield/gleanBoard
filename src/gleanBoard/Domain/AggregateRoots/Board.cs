using System;
using System.Collections.Generic;
using gleanBoard.Domain.Events;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.AggregateRoots
{
    public class Board : AggregateRoot
    {
        readonly List<Guid> _lanes = new List<Guid>();

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
            Apply(new LaneCreatedEvent {AggregateRootId = Id, Lane = id, Name = name, Postion = position});
        }

        public void CreateCard(Guid id, Guid lane, string title, int position, string description)
        {
            if (!_lanes.Contains(lane))
                throw new ArgumentException("Lane does not exist");

            Apply(new CardCreatedEvent {AggregateRootId = Id, Card = id, Lane = lane, Title = title, Postion = position, Description = description});
        }

        public void MoveCard(Guid card, Guid fromLane, Guid toLane, int position)
        {
            Apply(new CardMovedEvent { AggregateRootId = Id, Card = card, Lane = toLane, Position = position });
        }

        public void OnLaneCreated(LaneCreatedEvent e)
        {
            _lanes.Add(e.Lane);
        }

    }
}