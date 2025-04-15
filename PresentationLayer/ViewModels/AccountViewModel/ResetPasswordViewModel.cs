using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels.AccountViewModel
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
