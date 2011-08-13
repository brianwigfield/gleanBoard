using SimpleCqrs.Commanding;

namespace gleanBoard.Domain.Commands
{
    public class SignUpCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}