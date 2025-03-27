using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs.DepartmentDTOs
{
    public class CreatedDepartmentDTO
    {
        public DateOnly DateOfCreation { get; set; }
        [Required(ErrorMessage = "Name is Reqired :(")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(100, 10000)]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
