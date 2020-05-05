using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _Employees; 

        public MockEmployeeRepository()
        {
            _Employees = new List<Employee>(){
            new Employee() {Id = 1, Name = "Wences", Email = "wences@hotmail.com", Department = DepartmentEnum.IT },
            new Employee() {Id = 2, Name = "Miguel", Email = "miguel@hotmail.com", Department = DepartmentEnum.HR },
            new Employee() {Id = 3, Name = "Fer", Email = "fer@hotmail.com", Department = DepartmentEnum.Accounting }
            };
        }

        public Employee CreateEmployee(Employee employee)
        {
            employee.Id = _Employees.Max(emp => emp.Id + 1);
            _Employees.Add(employee);
            return employee;

        }

        public Employee Delete(int id)
        {
            Employee emp = _Employees.FirstOrDefault(emp => emp.Id == id);
            if(emp != null)
            {
                _Employees.Remove(emp);
            }
            return emp;
        }

        public Employee GetEmployee(int id)
        {
            return _Employees.FirstOrDefault(emp => emp.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _Employees;
        }

        public Employee Upadate(Employee employeeChanges)
        {
            Employee emp = _Employees.FirstOrDefault(emp => emp.Id == employeeChanges.Id);
            if (emp != null)
            {
                emp.Department = employeeChanges.Department;
                emp.Email = employeeChanges.Email;
                emp.Name = employeeChanges.Name;
            }
            return emp;
        }
    }
}
