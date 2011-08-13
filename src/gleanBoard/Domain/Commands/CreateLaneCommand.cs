using System;
using SimpleCqrs.Commanding;

namespace gleanBoard.Domain.Commands
{
    public class CreateLaneCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid Board { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }
}