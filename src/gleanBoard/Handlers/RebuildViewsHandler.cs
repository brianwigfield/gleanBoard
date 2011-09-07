using System;

namespace gleanBoard.Handlers
{
    public class RebuildData
    {
        public DateTime? AsOf { get; set; }
    }

    public class RebuildViewsHandler
    {
        public bool Get(RebuildData data)
        {
            Runtime.Locator.Resolve<SimpleCqrs.Utilites.DomainEventReplayer>().ReplayEventsForHandlerType(
                typeof(Domain.Views.BoardView), 
                DateTime.Now.AddYears(-1), 
                data.AsOf.GetValueOrDefault(DateTime.Now));
            return true;
        }
    }
}