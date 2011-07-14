using gleanBoard.Domain.AggregateRoots;
using gleanBoard.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.CommandHandlers
{
    public class CreateCardCommandHandler : CommandHandler<CreateCardCommand>
    {
        readonly IDomainRepository _repository;

        public CreateCardCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(CreateCardCommand command)
        {
            var newCard = new Card(command.Id, command.Lane, command.Title);
            _repository.Save(newCard);
        }
    }
}