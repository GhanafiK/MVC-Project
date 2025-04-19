namespace PresentationLayer.ViewModels.UserViewModel
{
    public class UserViewModel
    {
        public string ID { get; set; } = default!;
        public string FirstName { get; set; }= default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; }= default!;
        public string PhoneNumber { get; set; } = default!;
        public IEnumerable<string> Roles { get; set; }=new HashSet<string>();
    }
}
