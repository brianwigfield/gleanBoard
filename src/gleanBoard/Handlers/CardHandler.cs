using System;
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
            return new NewCard {Id = Guid.NewGuid().ToString(), Lane = data.Lane, Title = data.Title};
        }
    }
}