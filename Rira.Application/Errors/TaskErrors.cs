using Rira.Application.ViewModel;

namespace Rira.Application.Errors
{
    public class TaskErrors
    {
        public static ErrorViewModel NotFound { get { return new ErrorViewModel { Status = 404, Massage = "تسک مورد نظر پیدا نشد" }; } }
        public static ErrorViewModel ValuesAreNotValid { get { return new ErrorViewModel { Status = 400, Massage = "اطلاعات وارد شده صحیح نمیباشد" }; } }
    }
}
