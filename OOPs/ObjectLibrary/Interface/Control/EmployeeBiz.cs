using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectLibrary.Class.Model;

namespace ObjectLibrary.Interface.Control
{
    public interface IEmployeeBiz
    {
        Employee GetEmployee(Int64 id);
        String GetEmployeeName(Int64 id);
        double GetSalary(Int64 id);
        double  GetInHandSalary(Int64 id);

    }
}
