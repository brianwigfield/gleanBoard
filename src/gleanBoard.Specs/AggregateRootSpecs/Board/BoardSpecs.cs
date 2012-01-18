using System;
using gleanBoard.Domain.Events;
using gleanBoard.Specs.Helpers;
using Machine.Specifications;

namespace gleanBoard.Specs.AggregateRootSpecs.Board
{

    public class When_adding_a_lane
    {
        Establish that = () =>
            board = new Domain.AggregateRoots.Board(Guid.NewGuid(), "Test Board");

        Because of = () => 
            board.CreateLane(lane, "Test Lane", 0);

        It should_add_the_lane = () =>
            board.ShouldHaveApplied<LaneCreatedEvent>(_ => _.Lane == lane && _.Name == "Test Lane" && _.Postion == 0);

        static Guid lane = Guid.NewGuid();
        static Domain.AggregateRoots.Board board;
    }

    public class When_adding_a_card
    {
        Establish that = () =>
            {
                board = new Domain.AggregateRoots.Board(Guid.NewGuid(), "");
                board.HadApplied(new LaneCreatedEvent {Lane = Guid.NewGuid(), Name = "Test Lane", Postion = 0});
                board.HadApplied(new LaneCreatedEvent {Lane = lane, Name = "Test Lane 2", Postion = 0});
            };

        Because of = () => 
            board.CreateCard(Guid.NewGuid(), lane, "A Card", 0, "Testing");

        It should_add_it_the_to_correct_lane = () =>
            board.ShouldHaveApplied<CardCreatedEvent>(_ => _.Lane == lane && _.Title == "A Card");

        static Guid lane = Guid.NewGuid();
        static Domain.AggregateRoots.Board board;
    }

    public class When_adding_a_card_to_lane_that_doesnt_exist
    {
        Establish that = () =>
        {
            board = new Domain.AggregateRoots.Board(Guid.NewGuid(), "");
            board.HadApplied(new LaneCreatedEvent { Lane = Guid.NewGuid(), Name = "Test Lane", Postion = 0 });
        };

        Because of = () =>
            ex = Catch.Exception(() => board.CreateCard(Guid.NewGuid(), Guid.NewGuid(), "A Card", 0, "Testing"));

        It should_raise_an_exception = () =>
            ex.ShouldBeOfType<ArgumentException>();

        It should_not_add_the_card = () =>
            board.ShouldNotHaveApplied<CardCreatedEvent>();

        static Exception ex;
        static Domain.AggregateRoots.Board board;
    }
}
