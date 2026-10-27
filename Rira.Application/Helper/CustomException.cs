using Rira.Application.ViewModel;

namespace Rira.Application.Helper
{
    public class CustomException : Exception
    {
        public ErrorViewModel Error { get; }

        public CustomException(ErrorViewModel error)
            : base(error.Massage)
        {
            Error = error;
        }
    }
}
