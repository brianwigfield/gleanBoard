using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gleanBoard.Domain.AggregateRoots;
using gleanBoard.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace gleanBoard.Domain.CommandHandlers
{
    public class SignUpCommandHandler : CommandHandler<SignUpCommand>
    {
 
        readonly IDomainRepository _repository;

        public SignUpCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }
        
        public override void Handle(SignUpCommand command)
        {
            var account = new Account(Guid.NewGuid(), command.Username, command.Password);
            var board = new Board(Guid.NewGuid(), "Test Board");
            var backlogId = Guid.NewGuid();
            board.CreateLane(backlogId,"Backlog",0);
            board.CreateLane(Guid.NewGuid(),"Doing", 1);
            board.CreateLane(Guid.NewGuid(),"Done", 2);
            board.CreateCard(Guid.NewGuid(), backlogId, "Test Card", 0, "Welcome");
            _repository.Save(account);
            _repository.Save(board);
        }
    }
}