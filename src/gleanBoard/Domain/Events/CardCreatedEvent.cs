﻿using System;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class CardCreatedEvent : DomainEvent
    {
        public Guid Card { get; set; }
        public Guid Lane { get; set; }
        public string Title { get; set; }
        public int Postion { get; set; }
        public string Description { get; set; }

        public override int GetHashCode() { return -2051383496; }

    }

}