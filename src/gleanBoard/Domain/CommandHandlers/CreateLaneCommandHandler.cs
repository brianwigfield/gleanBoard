using gleanBoard.Domain.AggregateRoots;
using gleanBoard.Domain.Commands;
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
            var board = _repository.GetById<Board>(command.Board);
            board.CreateLane(command.Id, command.Name, command.Position);
            _repository.Save(board);
        }
    }
}