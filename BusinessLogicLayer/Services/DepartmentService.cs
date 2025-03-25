using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Factories;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDTO());
        }

        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);
            return department.ToDepartmentDetailsDTO();
        }

        public int AddDepartment(CreatedDepartmentDTO departmentDto) => _departmentRepository.Add(departmentDto.ToEntity());

        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO) => _departmentRepository.Update(departmentDTO.ToEntity());

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);
            if (department == null) return false;
            else
            {
                int Result = _departmentRepository.Remove(department);
                return Result > 0 ? true : false;
            }
        }
    }
}
