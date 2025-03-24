using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services
{
    internal interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDTO departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDTO> GetAllDepartments();
        DepartmentDetailsDTO? GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDTO departmentDTO);
    }
}