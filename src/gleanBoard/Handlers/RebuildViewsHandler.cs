using System;
using System.Collections.Generic;
using gleanBoard.Domain.Events;
using SimpleCqrs.Eventing;

namespace gleanBoard.Handlers
{
    public class RebuildViewsHandler
    {
        public bool Get()
        {
            var events = Runtime.Locator.Resolve<IEventStore>().GetEventsByEventTypes(new List<Type> { typeof(BoardCreatedEvent), typeof(CardCreatedEvent), typeof(LaneCreatedEvent), typeof(CardMovedEvent) }, DateTime.Now.AddYears(-1), DateTime.Now);
            Runtime.Locator.Resolve<IEventBus>().PublishEvents(events);
            return true;
        }
    }
}