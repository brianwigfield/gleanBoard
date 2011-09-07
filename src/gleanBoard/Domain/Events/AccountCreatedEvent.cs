using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.Events
{
    public class AccountCreatedEvent : DomainEvent
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}