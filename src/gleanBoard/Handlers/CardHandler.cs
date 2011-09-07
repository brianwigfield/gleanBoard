using System;
using gleanBoard.Domain.Commands;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{
    public class NewCardData
    {
        public string Board { get; set; }
        public string Lane { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
    }

    public class CardHandler
    {
        public NewCard Post(NewCardData data)
        {
            var id = Guid.NewGuid();
            Runtime.Bus.Send(new CreateCardCommand {Id = id, Board = Guid.Parse(data.Board), Lane = Guid.Parse(data.Lane), Title = data.Title, Position = data.Position, Description = data.Description});

            return new NewCard {Id = id.ToString(), Lane = data.Lane, Title = data.Title};
        }
    }
}