using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lector_2
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


    class Employee
    {
        string _name = string.Empty;
        string _title = string.Empty;
        float _salary = 0;
        int seneniorityLevel = 0;

        public Employee(string name, string jobTitle, float salary)
        {
            _name = name;
            _title = jobTitle;
            _salary = salary;
        }

        public virtual decimal CalculateYearlySalary()
        {
            decimal dec = 365m * (decimal)_salary;
            return dec + (dec/100) * seneniorityLevel * 10m;
        }
    }

    class Manager : Employee
    {
        float _bonus = 0;

        public Manager(float bonus, string name, string jobTitle, float salary) : base (name, jobTitle, salary)
        {
            _bonus = bonus;
        }

        public override decimal CalculateYearlySalary()
        {
            return base.CalculateYearlySalary() + (decimal)_bonus;
        }
    }
}
