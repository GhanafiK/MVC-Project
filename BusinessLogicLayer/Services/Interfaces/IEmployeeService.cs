 using BusinessLogicLayer.DTOs.EmployeeDTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreateEmployeeDTO createEmployeeDTO);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDTO> GetAllEmployees(bool withTracking=false);
        IEnumerable<EmployeeDTO> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDTO? GetEmployeeById(int id);
        int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO);
    }
}