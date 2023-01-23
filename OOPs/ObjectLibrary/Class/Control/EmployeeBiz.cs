using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectLibrary.Class.Model;
using ObjectLibrary.Constant;
using ObjectLibrary.Interface.Control;

namespace ObjectLibrary.Class.Control
{
    public class EmployeeBiz : IEmployeeBiz
    {
        private static List<Employee> _employees = new List<Employee>();
        public EmployeeBiz()
        {
            Employee employee = new Employee
            {
                Name = "Nikolash",
                Id = 1,
                Address = "104 Baker Street",
                Salary = 100000
            };
            if (_employees.Count == 0 || _employees.Exists(m => m.Name != employee.Name && m.Id != employee.Id))
                _employees.Add(employee);
        }

        public Employee GetEmployee(long id)
        {
            return _employees.Find(m => m.Id == id);
        }

        public string GetEmployeeName(long id)
        {
            return _employees.Find(m => m.Id == id).Name;
        }

        public double GetInHandSalary(long id)
        {
            double salary = GetSalary(id);
            return calculateInHand(salary);
        }

        public double GetSalary(long id)
        {
            return (double)_employees.Find(m => m.Id == id).Salary;
        }

        private double calculateInHand(double salary)
        {
            return salary - salary * Constants.DEDUCTION;
        }
    }
}
