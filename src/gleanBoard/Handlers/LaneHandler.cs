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
            return new NewLane {Id = Guid.NewGuid().ToString(), Name = data.Name};
        }

    }
}