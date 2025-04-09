using DataAccessLayer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configrations
{
    internal class DepartmentConfigration : BaseEntityConfiguration<Department>,IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("VarChar(20)");
            builder.Property(D => D.Code).HasColumnType("VarChar(20)");
            builder.HasMany(D=>D.Employees)
                .WithOne(E=>E.Department)
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);
        }
    }
}
