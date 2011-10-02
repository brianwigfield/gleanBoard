using gleanBoard.Domain.Events;
using gleanBoard.Domain.Events.Converters;
using gleanBoard.Domain.Events.Depricated;
using Machine.Fakes;
using Machine.Specifications;

namespace gleanBoard.Specs.AggregateRootSpecs.Board
{
    public class When_converting_from_card_event_1_to_current : WithSubject<CardCreatedConverter>
    {

        Establish that = () =>
            {
                oldEvent = new CardCreatedEvent_v1
                               {
                                   Title = "Test"
                               };
            };

        Because of = () =>
            newEvent = Subject.Convert(oldEvent);

        It should_convert_it_propertly = () =>
            newEvent.Title.ShouldEqual("Test");

        static CardCreatedEvent newEvent;
        static CardCreatedEvent_v1 oldEvent;

    }
}
