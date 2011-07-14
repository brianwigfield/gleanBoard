using System;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{

    public class NewLaneData
    {
        public string Name { get; set; }
    }

    public class LaneHandler
    {

        public NewLane Post(NewLaneData data)
        {
            var id = Guid.NewGuid();
            Runtime.Bus.Send(new CreateLaneCommand {Id = id, Name = data.Name});
            
            return new NewLane {Id = id.ToString(), Name = data.Name};
        }

    }
}