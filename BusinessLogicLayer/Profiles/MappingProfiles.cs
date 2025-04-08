using AutoMapper;
using BusinessLogicLayer.DTOs.EmployeeDTOs;
using DataAccessLayer.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType.ToString()))
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));
            CreateMap<Employee, EmployeeDetailsDTO>()
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType.ToString()))
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest=>dest.HiringDate,Options=>Options.MapFrom(src=>DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest=>dest.HiringDate,Options=>Options.MapFrom(src=>src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            CreateMap<UpdatedEmployeeDTO, Employee>()
                .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue))); ;
        }
    }
}
