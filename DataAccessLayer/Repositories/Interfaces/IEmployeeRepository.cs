using DataAccessLayer.Models.Departments;
using DataAccessLayer.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
    }
}
