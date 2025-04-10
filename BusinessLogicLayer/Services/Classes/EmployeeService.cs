using AutoMapper;
using BusinessLogicLayer.DTOs.EmployeeDTOs;
using BusinessLogicLayer.Factories;
using BusinessLogicLayer.Services.AttachmentService;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Repositories.Classes;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class EmployeeService(IUnitOfWork unitOfWork,IMapper _mapper,IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees(bool withTracking=false)
        {
            var Employees = unitOfWork.EmployeeRepository.GetAll(withTracking);
            var EmployeesDTO = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDTO>>(Employees);
            return EmployeesDTO;
        }

        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var Employee = unitOfWork.EmployeeRepository.GetDepartmentById(id);
            return Employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDTO>(Employee);
        }
        public bool DeleteEmployee(int id)
        {
            var Employee = unitOfWork.EmployeeRepository.GetDepartmentById(id);
            if (Employee is null) return false;
            else
            {
                Employee.IsDeleted = true;
                unitOfWork.EmployeeRepository.Update(Employee);
                return unitOfWork.SaveChanges()>0?true:false;
            }

        }

        public int AddEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            var employee = _mapper.Map<CreateEmployeeDTO, Employee>(createEmployeeDTO);
            if(createEmployeeDTO.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(createEmployeeDTO.Image, "Images");
            }
            unitOfWork.EmployeeRepository.Add(employee);
            return unitOfWork.SaveChanges();
        }


        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDTO, Employee>(updatedEmployeeDTO));
            return unitOfWork.SaveChanges();
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(EmployeeSearchName))
                employees = unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower())).Where(E=>E.IsDeleted!=true);

            var EmployeesDTO = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employees);
            return EmployeesDTO;
        }
    }
}
