using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOverride
{
    class TestOverride
    {
        public class Employee
        {
            public string name;

            // Basepay is defined as protected, so that it may be 
            // accessed only by this class and derived classes.
            protected decimal basepay;

            // Constructor to set the name and basepay values.
            public Employee(string name, decimal basepay)
            {
                this.name = name;
                this.basepay = basepay;
            }

            // Declared virtual so it can be overridden.
            public virtual decimal CalculatePay()
            {
                return basepay;
            }
        }

        // Derive a new class from Employee.
        public class SalesEmployee : Employee
        {
            // New field that will affect the base pay.
            private decimal salesbonus;

            // The constructor calls the base-class version, and
            // initializes the salesbonus field.
            public SalesEmployee(string name, decimal basepay,
                      decimal salesbonus) : base(name, basepay)
            {
                this.salesbonus = salesbonus;
            }

            // Override the CalculatePay method 
            // to take bonus into account.
            public override decimal CalculatePay()
            {
                return basepay + salesbonus;
            }
        }

        static void Main()
        {
            // Create some new employees.
            var employee1 = new SalesEmployee("Alice",
                          1000, 500);
            var employee2 = new Employee("Bob", 1200);

            Console.WriteLine("Employee4 " + employee1.name +
                      " earned: " + employee1.CalculatePay());
            Console.WriteLine("Employee4 " + employee2.name +
                      " earned: " + employee2.CalculatePay());

            Console.ReadKey();
        }
    }
}
