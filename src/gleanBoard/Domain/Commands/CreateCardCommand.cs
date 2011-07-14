using System;
using SimpleCqrs.Commanding;

namespace gleanBoard.Domain.Commands
{
    public class CreateCardCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Lane { get; set;}
        public string Title { get; set; }
    }
}