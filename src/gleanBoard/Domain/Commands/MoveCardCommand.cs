using System;
using SimpleCqrs.Commanding;

namespace gleanBoard.Domain.Commands
{
    public class MoveCardCommand : ICommand
    {
        public Guid Board { get; set; }
        public Guid Card { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public int Position { get; set; }
    }
}