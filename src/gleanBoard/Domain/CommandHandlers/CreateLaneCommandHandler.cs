using gleanBoard.Handlers;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.CommandHandlers
{
    public class CreateLaneCommandHandler : CommandHandler<CreateLaneCommand>
    {
        readonly IDomainRepository _repository;

        public CreateLaneCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(CreateLaneCommand command)
        {
            var lane = new AggregateRoots.Lane(command.Id, command.Name);
            _repository.Save(lane);
        }
    }
}