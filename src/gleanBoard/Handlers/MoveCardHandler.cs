using System;
using gleanBoard.Domain.Commands;

namespace gleanBoard.Handlers
{
    public class MoveCardHandler
    {

        public class MoveLaneData
        {
            public string Board { get; set; }
            public string Card { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public int Position { get; set; }
        }

        public CardMoved Post(MoveLaneData data)
        {
            Runtime.Bus.Send(new MoveCardCommand { Board = Guid.Parse(data.Board), Card = Guid.Parse(data.Card), From = Guid.Parse(data.From), To = Guid.Parse(data.To), Position = data.Position });
            return new CardMoved {Result = true};
        }

    }
}