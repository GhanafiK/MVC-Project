using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models.Departments;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext _dbContext) : GenericRepository<Department>(_dbContext),IDepartmentRepository
    {
    }
}
