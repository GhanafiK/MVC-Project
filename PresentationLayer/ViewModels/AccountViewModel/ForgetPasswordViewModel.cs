using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels.AccountViewModel
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = null!;
    }
}
