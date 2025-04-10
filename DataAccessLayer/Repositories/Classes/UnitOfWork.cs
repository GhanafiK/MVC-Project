using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _departmentRepository = new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(dbContext));
            _dbContext = dbContext;
        }

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public int SaveChanges() =>  _dbContext.SaveChanges();
    }
}
