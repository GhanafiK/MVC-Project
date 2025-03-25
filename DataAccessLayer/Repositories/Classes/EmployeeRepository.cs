using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext _dbContext):GenericRepository<Employee>(_dbContext),IEmployeeRepository
    {
    }
}
