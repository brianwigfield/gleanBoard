using System;
using System.Collections.Generic;
using gleanBoard.Domain.Events;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace gleanBoard.Domain.AggregateRoots
{
    public class Account : AggregateRoot
    {

        public Account(Guid id, string username, string password)
        {
            Apply(new AccountCreatedEvent {AggregateRootId = id, UserName = username, Password = password});
        }

        public void AddBoard(Guid board, bool isOwner)
        {
            Apply(new BoardAttachedEvent {AggregateRootId = Id, Board = board, IsOwner = isOwner});
        }

        public void OnAccountCreated(AccountCreatedEvent e)
        {
            Id = e.AggregateRootId;
            UserName = e.UserName;
            Password = e.Password;
        }

        public void OnBoardAttached(BoardAttachedEvent e)
        {
            Boards.Add(new AccountBoard {Board = e.Board, IsOwner = e.IsOwner});
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public List<AccountBoard> Boards { get; set; }
    }

    public class AccountBoard
    {
        public Guid Board { get; set; }
        public bool IsOwner { get; set; }
    }


    public class BoardAttachedEvent : DomainEvent
    {
        public Guid Board { get; set; }
        public bool IsOwner { get; set; }
    }
}