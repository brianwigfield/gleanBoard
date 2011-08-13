using System;
using gleanBoard.Domain.AggregateRoots;
using gleanBoard.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.CommandHandlers
{
    public class MoveCardCommandHandler : CommandHandler<MoveCardCommand>
    {
        readonly IDomainRepository _repository;

        public MoveCardCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(MoveCardCommand command)
        {
            var board = _repository.GetById<Board>(command.Board);
            board.MoveCard(command.Card, command.From, command.To, command.Position);
            _repository.Save(board);
        }
    }
}