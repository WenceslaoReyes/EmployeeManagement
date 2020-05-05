using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelSeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Tommy",
                    Department = DepartmentEnum.IT,
                    Email = "tommy@hotmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Mike",
                    Department = DepartmentEnum.HR,
                    Email = "mike@hotmail.com"
                }
                );
        }
    }
}
