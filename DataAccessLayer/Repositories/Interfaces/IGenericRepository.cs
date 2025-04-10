using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T Entity);
        IEnumerable<T> GetAll(bool WithTracking = false);
        IEnumerable<T> GetAll(Expression<Func<T,bool>> Predicate);
        T? GetById(int id);
        void Remove(T Entity);
        void Update(T Entity);
    }
}
