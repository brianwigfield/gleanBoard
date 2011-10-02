using gleanBoard.Domain.Events.Depricated;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events.Converters
{
    public class CardCreatedConverter : IEventConverter<CardCreatedEvent_v1, CardCreatedEvent>
    {
        public CardCreatedEvent Convert(CardCreatedEvent_v1 sourceEvent)
        {
            return new CardCreatedEvent
                       {
                           AggregateRootId = sourceEvent.AggregateRootId,
                           EventDate = sourceEvent.EventDate,
                           Sequence = sourceEvent.Sequence,
                           Card = sourceEvent.Card,
                           Description = "No Description",
                           Lane = sourceEvent.Lane,
                           Postion = sourceEvent.Postion,
                           Title = sourceEvent.Title
                       };
        }
    }
}