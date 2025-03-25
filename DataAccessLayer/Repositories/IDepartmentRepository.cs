
using DataAccessLayer.Models.Departments;

namespace DataAccessLayer.Repositories
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        IEnumerable<Department> GetAll(bool WithTracking = false);
        Department? GetDepartmentById(int id);
        int Remove(Department department);
        int Update(Department department);
    }
}