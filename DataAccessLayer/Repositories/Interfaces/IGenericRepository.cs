using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        int Add(T Entity);
        IEnumerable<T> GetAll(bool WithTracking = false);
        T? GetDepartmentById(int id);
        int Remove(T Entity);
        int Update(T Entity);
    }
}
