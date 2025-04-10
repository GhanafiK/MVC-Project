using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Shared;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Classes
{
    public class GenericRepository<T>(ApplicationDbContext _dbContext):IGenericRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return _dbContext.Set<T>().Where(E=>E.IsDeleted!=true).ToList();
            }
            else
            {
                return _dbContext.Set<T>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
            }
        }

        public T? GetDepartmentById(int id) => _dbContext.Set<T>().Find(id);

        public void Update(T Entity)
        {
            _dbContext.Set<T>().Update(Entity);
        }

        public void Add(T Entity)
        {
            _dbContext.Set<T>().Add(Entity);
        }

        public void Remove(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Predicate)
        {
            return _dbContext.Set<T>().Where(Predicate).ToList();
        }
    }
}
