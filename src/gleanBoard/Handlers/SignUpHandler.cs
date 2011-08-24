using gleanBoard.Domain.Commands;

namespace gleanBoard.Handlers
{

    public class SignUpData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class SignUpHandler
    {

        public Signup Get()
        {
            return new Signup {Result = true};
        }

        public Signup Post(SignUpData data)
        {
            if (data.Password != data.PasswordConfirm)
                return new Signup { Result = false };

            Runtime.Bus.Send(new SignUpCommand {Username = data.UserName, Password = data.Password});
            return new Signup { Result = true };
        }
    }
}