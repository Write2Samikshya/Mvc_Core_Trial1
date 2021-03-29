using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMGT22.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Amu",
                   Department = Dept.IT,
                   Email = "Amu@gmail.com"
               },


                new Employee
                {
                    Id = 2,
                    Name = "Samu",
                    Department = Dept.IT,
                    Email = "Samu@gmail.com"
                }


               );



        }

    }
}
