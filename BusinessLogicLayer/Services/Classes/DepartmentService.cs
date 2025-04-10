using BusinessLogicLayer.DTOs.DepartmentDTOs;
using BusinessLogicLayer.Factories;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDTO());
        }

        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = unitOfWork.DepartmentRepository.GetDepartmentById(id);
            return department.ToDepartmentDetailsDTO();
        }

        public int AddDepartment(CreatedDepartmentDTO departmentDto)
        {
            unitOfWork.DepartmentRepository.Add(departmentDto.ToEntity());
            return unitOfWork.SaveChanges();
        }

        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            unitOfWork.DepartmentRepository.Update(departmentDTO.ToEntity());
            return unitOfWork.SaveChanges();
        }

        public bool DeleteDepartment(int id)
        {
            var department = unitOfWork.DepartmentRepository.GetDepartmentById(id);
            if (department == null) return false;
            else
            {
                unitOfWork.DepartmentRepository.Remove(department);
                return unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
