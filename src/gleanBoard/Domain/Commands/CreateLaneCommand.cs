using System;
using SimpleCqrs.Commanding;

namespace gleanBoard.Handlers
{
    public class CreateLaneCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}