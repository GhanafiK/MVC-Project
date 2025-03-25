using DataAccessLayer.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configrations
{
    internal class EmployeeConfiguration : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("VarChar(50)");
            builder.Property(E => E.Address).HasColumnType("VarChar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E=>E.Gender).HasConversion(gender=>gender.ToString(),_Gender=>(Gender) Enum.Parse(typeof(Gender),_Gender));
            builder.Property(E => E.EmployeeType).HasConversion(type => type.ToString(), _EmployeeType => (EmployeeType)Enum.Parse(typeof(EmployeeType), _EmployeeType));
            base.Configure(builder);
        }
    }
}
