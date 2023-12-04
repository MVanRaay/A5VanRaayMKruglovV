/* Program.cs
 * A Program to create, edit and view employee records stored in a list
 * 
 * Revision History:
 *      Martin Van Raay and Vitaliy Kruglov, 2023.12.01: created
 *      Martin Van Raay and Vitaliy Kruglov, 2023.12.04: changed EditEmployee and DisplayEmployee methods to account for class behavior change from from static to non-static
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A5VanRaayMKruglovV
{
    internal class Program
    {
        // Global public List to store Employee objects
        public static List<Employee> employees = new List<Employee>();

        static void Main(string[] args)
        {
            // Variables
            int menuChoice;

            //Main Menu
            do
            {
                Console.Clear();
                Console.WriteLine("*****Employee Records*****");
                Console.WriteLine("Please choose (1-4):");
                Console.WriteLine("1. Add New Employee");
                Console.WriteLine("2. Edit Existing Employee");
                Console.WriteLine("3. Display Employee");
                Console.WriteLine("4. Exit");

                menuChoice = ValidateMenu(Console.ReadLine(), 1, 4);

                // Call appropriate method for task
                if (menuChoice == 1)
                {
                    AddEmployee();
                }
                else if (menuChoice == 2)
                {
                    EditEmployee();
                }
                else if (menuChoice == 3)
                {
                    DisplayEmployee();
                }
                else if (menuChoice == 4)
                {
                    Console.WriteLine("\nBye for now.");
                }

            } while (menuChoice != 4);

            Console.ReadKey();
        }

        // Method for Validating Menu Input
        static int ValidateMenu(string input, int min, int max)
        {
            // Variables
            bool inputValid = false;
            int choice = 0;

            // Try/Catch to validate input
            try
            {
                // Throw exception if input is empty
                if (input == "")
                {
                    throw new ArgumentNullException();
                }

                // Attempt to convert input to int
                choice = Convert.ToInt32(input);

                // Thorw exception if choice is not within valid range for Menu
                if (choice < min || choice > max)
                {
                    throw new ArgumentOutOfRangeException();
                }

                // Validate input if no exceptions thrown
                inputValid = true;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ERROR: Input cannot be empty.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"ERROR: Input must be between ({min}-{max}).");
            }
            catch (FormatException)
            {
                Console.WriteLine($"ERROR: Input must be an integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("ERROR: Input must be small (or large) enough to fit in an int32 variable.");
            }

            // If input is invalid Give user message that they will be returned to main menu
            if (inputValid != true)
            {
                Console.WriteLine("\nPress any key to return to the Main Menu");
                Console.ReadKey();

                choice = 0;
            }

            return choice;
        }

        // Method to Create a New Employee
        static void AddEmployee()
        {
            // Variables
            int id;
            string name;
            int salary;

            // **Note: I am aware the id, name and salary inputs are validated twice, this is so that the user is
            // informed of an invalid input as soon as they give it, and returned to the main menu immediately, while
            // allowing the class to ensure valid data entry independently**

            // Prompt user for employee info
            Console.Write("Enter Employee ID Number (1 - 999999): ");
            id = Employee.ValidateInput(Console.ReadLine(), "newID");

            if (id != 0)
            {
                Console.Write("Enter Employee's Name: ");
                name = Employee.ValidateInput(Console.ReadLine());

                if (name != "")
                {
                    Console.Write("Enter Employee's Monthly Salary: ");
                    salary = Employee.ValidateInput(Console.ReadLine(), "salary");

                    if (salary != 0)
                    {
                        Employee.AddEmployee(id, name, salary);
                    }
                }
            }
        }

        // Method to Edit an Existing Employee
        static void EditEmployee()
        {
            // Variables
            int index;
            int editChoice;
            string newInfo = "";

            Console.Write("Enter Employee Number (1-999999): ");
            index = Employee.GetIndex(Employee.ValidateInput(Console.ReadLine(), "findID"));

            if (index != -1)
            {
                Employee employee = employees[index];
                employee.DisplayEmployee();

                Console.WriteLine("\nWhich value do you want to change (1/2)?");
                Console.WriteLine("1. Employee Name");
                Console.WriteLine("2. Employee Salary");

                editChoice = ValidateMenu(Console.ReadLine(), 1, 2);


                if (editChoice != 0)
                {
                    if (editChoice == 1)
                    {
                        Console.Write("Change Name to: ");
                        newInfo = Console.ReadLine();
                    }
                    else if (editChoice == 2)
                    {
                        Console.Write("Change Salary to: ");
                        newInfo = Console.ReadLine();
                    }

                    employee.EditEmployee(editChoice, newInfo);
                }
            }
        }

        // Method to display an Employee Record
        static void DisplayEmployee()
        {
            // Variable
            int index;

            if (employees.Count == 0)
            {
                // message if list is empty
                Console.WriteLine("There are not currently any employees saved.");
                Console.WriteLine("\nPress any key to return to the Main Menu");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Enter Employee Number: ");
                index = Employee.GetIndex(Employee.ValidateInput(Console.ReadLine(), "findID"));

                if (index != -1)
                {
                    Employee employee = employees[index];
                    employee.DisplayEmployee();

                    Console.WriteLine("\nPress any key to return to the Main Menu");
                    Console.ReadKey();
                }
            }
        }
    }
}
