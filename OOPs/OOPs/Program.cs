using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectLibrary.Class.Control;
using ObjectLibrary.Interface.Control;

namespace OOPs
{
    public class Program
    {
        static void Main(string[] args)
        {
            IEmployeeBiz emp = new EmployeeBiz();
            Console.WriteLine(emp.GetEmployeeName(1));
            Console.ReadKey();
        }
    }
}
