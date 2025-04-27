namespace PresentationLayer.ViewModels.RoleViewModel
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }

        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 

        public string? Email { get; set; }
    }
}
