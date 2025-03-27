using BusinessLogicLayer.DTOs.EmployeeDTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreateEmployeeDTO createEmployeeDTO);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDTO> GetAllEmployees();
        EmployeeDetailsDTO? GetEmployeeById(int id);
        int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO);
    }
}