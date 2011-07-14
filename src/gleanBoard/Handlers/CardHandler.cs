using System;
using gleanBoard.Domain.Commands;
using gleanBoard.Resources;

namespace gleanBoard.Handlers
{
    public class NewCardData
    {
        public string Title { get; set; }
        public string Lane { get; set; }
    }

    public class CardHandler
    {
        public NewCard Post(NewCardData data)
        {
            var id = Guid.NewGuid();
            Runtime.Bus.Send(new CreateCardCommand {Id = id, Lane = data.Lane, Title = data.Title});

            return new NewCard {Id = id.ToString(), Lane = data.Lane, Title = data.Title};
        }
    }
}