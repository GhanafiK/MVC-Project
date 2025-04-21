using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels.RoleViewModel
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        [Display(Name ="Role Name")]
        public string Name { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }

        public List<UserRoleViewModel> Users { get; set; }= new List<UserRoleViewModel>();

    }
}
