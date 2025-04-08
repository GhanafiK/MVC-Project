using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Departments
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();
    }
}
