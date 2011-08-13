using System;
using gleanBoard.Domain.Commands;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{

    public class NewLaneData
    {
        public string Board { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }

    public class LaneHandler
    {

        public NewLane Post(NewLaneData data)
        {
            var id = Guid.NewGuid();
            Runtime.Bus.Send(new CreateLaneCommand {Id = id, Board = Guid.Parse(data.Board), Name = data.Name, Position = data.Position });
            return new NewLane {Id = id.ToString(), Name = data.Name};
        }

    }
}