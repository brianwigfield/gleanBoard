using System;
using System.Collections.Generic;
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
            Lanes = new List<Guid>();
        }

        public void CreateLane(Guid id, string name, int position)
        {
            Apply(new LaneCreatedEvent {AggregateRootId = Id, Lane = id, Name = name, Postion = position});
        }

        public void CreateCard(Guid id, Guid lane, string title, int position, string description)
        {
            if (!Lanes.Contains(lane))
                throw new ArgumentException("Lane does not exist");

            Apply(new CardCreatedEvent {AggregateRootId = Id, Card = id, Lane = lane, Title = title, Postion = position, Description = description});
        }

        public void MoveCard(Guid card, Guid fromLane, Guid toLane, int position)
        {
            Apply(new CardMovedEvent { AggregateRootId = Id, Card = card, Lane = toLane, Position = position });
        }

        public void OnLaneCreated(LaneCreatedEvent e)
        {
            Lanes.Add(e.Lane);
        }

        public List<Guid> Lanes { get; set; }

        public class Lane
        {
            public List<Guid> Cards { get; set; }
        }

    }

}