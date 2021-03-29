using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMGT22.Models
{
    public class MockEmployeeRepository : IEmployeeRpository
    {

        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1, Name="SAMU", Email="SAMU@gmail.com",Department=Dept.IT},
                new  Employee(){Id=2,Name="AMU",Email="AMU@gmail.com",Department=Dept.IT},

                new Employee(){Id=3,Name="RAM",Email="RAM@gmail.com",Department=Dept.HR},
                new Employee(){Id=4,Name="SITA",Email="SITA@gmail.com",Department=Dept.Medico}

                  };
        }

        public Employee Add(Employee employee)
        {

           employee.Id= _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add( employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee!=null)
            {
                _employeeList.Remove(employee);

            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                //_employeeList.Remove(employee);
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;



            }
            return employee;
        }
    }
}
