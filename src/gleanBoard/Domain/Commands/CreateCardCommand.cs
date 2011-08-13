using System;
using SimpleCqrs.Commanding;

namespace gleanBoard.Domain.Commands
{
    public class CreateCardCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid Board { get; set; }
        public Guid Lane { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
    }
}