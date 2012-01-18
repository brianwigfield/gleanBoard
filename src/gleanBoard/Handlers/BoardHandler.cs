using gleanBoard.Resources;

namespace gleanBoard.Handlers
{
    public class BoardHandler
    {
        public object Get()
        {
            var repo = Runtime.Locator.Resolve<ViewRepository>();
            return repo.Get<Board>() ?? new Board();

        }
    }
}