using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class DepartmentDetailsDTO
    {
        public int Id { get; set; } // PK
        public int CreatedBy { get; set; } //refere to useer Id
        public DateOnly DateOfCreation { get; set; }
        public int LastModifiedBy { get; set; } //refere to useer Id
        public DateOnly DateOfModification { get; set; }
        public bool IsDeleted { get; set; } //soft delete
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
