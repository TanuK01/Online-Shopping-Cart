using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitWareLib
{

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            //bool a = true;

            Console.WriteLine("Litware Organization Employee Record");
            Console.WriteLine("*****************************************");
            Console.WriteLine("\n");

            try
            {
                while (true)
                {
                    Console.WriteLine("Enter Employee Number");
                    int no = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter Employee Name");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter Employee Salary");
                    double sal = Convert.ToDouble(Console.ReadLine());

                    Employee emp = new Employee(no, name, sal);
                    employees.Add(emp);

                    emp.CalculateSalary();

                    Console.WriteLine("*****************************************");
                    Console.WriteLine("Welcome to Litware");
                    Console.WriteLine("*****************************************");
                    Console.WriteLine("\n");

                    Console.WriteLine("ENTER 0 TO EXIT");

                    int i = Convert.ToInt32(Console.ReadLine());
                    if (i == 0)
                    {
                        Environment.Exit(0);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

