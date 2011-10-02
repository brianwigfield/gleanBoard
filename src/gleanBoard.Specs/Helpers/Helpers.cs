using System;
using System.Linq;
using Machine.Specifications;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace gleanBoard.Specs.Helpers
{
    public static class Helpers
    {

        public static void ShouldHaveApplied<T>(this AggregateRoot ar) where T : DomainEvent
        {
            ShouldHaveApplied<T>(ar, _ => true);
        }

        public static void ShouldHaveApplied<T>(this AggregateRoot ar, Func<T, bool> func) where T : DomainEvent
        {
            ar.UncommittedEvents.Where(_ => _.GetType() == typeof(T))
                .Cast<T>()
                .Where(func)
                .Count().ShouldBeGreaterThan(0);
        }

        public static void ShouldNotHaveApplied<T>(this AggregateRoot ar) where T : DomainEvent
        {
            ShouldNotHaveApplied<T>(ar, _ => true);
        }

        public static void ShouldNotHaveApplied<T>(this AggregateRoot ar, Func<T, bool> func) where T : DomainEvent
        {
            ar.UncommittedEvents.Where(_ => _.GetType() == typeof (T))
                .Cast<T>()
                .Where(func)
                .Count().ShouldEqual(0);
        }

    }
}