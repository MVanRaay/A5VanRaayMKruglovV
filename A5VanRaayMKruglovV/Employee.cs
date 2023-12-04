/* Employee.cs
 * A class to define Employee Objects and Behaviors
 * 
 * Revision History:
 *      Martin Van Raay and Vitaliy Kruglov, 2023.12.01: created
 *      Martin Van Raay and Vitaliy Kruglov, 2023.12.04: changed EditEmployee and DisplayEmployee methods from static to non-static
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5VanRaayMKruglovV
{
    internal class Employee
    {
        // Attributes
        private readonly int id;
        private string name;
        private int salary;

        // Default Constructor (private)
        private Employee()
        {
        }

        // Non-default Constructor (private)
        private Employee(int _id, string _name, int _salary)
        {
            id = _id;
            name = _name;
            salary = _salary;
        }

        // Behaviors:

        // public static method to return index if id entered matches existing record
        public static int GetIndex(int _id)
        {
            // Variable
            int index = -1;

            for (int i = 0; i < Program.employees.Count; i++)
            {
                if (_id == Program.employees[i].id)
                {
                    index = i;
                }
            }

            return index;
        }

        // public static method to validate input and return an integer
        public static int ValidateInput(string _input, string type)
        {
            // Variables
            bool inputValid = false;
            int input = 0;

            // Try/Catch to validate _input
            try
            {
                // Throw exception if _input is empty
                if (_input == "")
                {
                    throw new ArgumentNullException();
                }

                // Attempt to convert _input to int
                input = Convert.ToInt32(_input);

                // split exception throwing depending on input type
                if (type == "newID")
                {
                    // Throw exception if choice is not within valid range for ID numbers
                    if (input < 1 || input > 999999)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    // Throw exception if id matches an existing record
                    if (GetIndex(input) != -1)
                    {
                        throw new Exception();
                    }
                }
                else if (type == "findID")
                {
                    // Throw exception if choice is not within valid range for ID numbers
                    if (input < 1 || input > 999999)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    // Throw exception if id does not match an existing record
                    if (GetIndex(input) == -1)
                    {
                        throw new Exception();
                    }
                }
                else if (type == "salary")
                {
                    // throw exception if input is not within valid range for salary
                    if (input < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
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
                if (type == "newID" || type == "findID")
                {
                    Console.WriteLine("ERROR: Input must be between (1-999999).");
                }
                else if (type == "salary")
                {
                    Console.WriteLine("ERROR: Input must be greater than 0.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("ERROR: Input must be an integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("ERROR: Input must be small (or large) enough to fit in an int32 variable.");
            }
            catch (Exception)
            {
                if (type == "newID")
                {
                    Console.WriteLine("ERROR: Input matches existing record, please enter a new unique number.");
                }
                else if (type == "findID")
                {
                    Console.WriteLine("ERROR: Input does not match any existing records, please enter an employee number associated with an account.");
                }
            }

            // Message to inform user they will be returned to main menu if input is invalid
            if (inputValid != true)
            {
                Console.WriteLine("\nPress any key to return to the Main Menu");
                Console.ReadKey();

                input = 0;
            }

            return input;
        }

        // public static method to validate input and return a string
        public static string ValidateInput(string _input)
        {
            // Variables
            bool inputValid = false;
            string input = "";

            // Try/Catch to validate _input
            try
            {
                // Throw exception if _input is empty
                if (_input == "")
                {
                    throw new ArgumentNullException();
                }

                // Attempt to assign _input to input
                input = _input;

                // Validate input if no exceptions thrown
                inputValid = true;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ERROR: Input cannot be empty.");
            }
            catch (FormatException)
            {
                Console.WriteLine("ERROR: Input must be a string.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("ERROR: Input must be small enough to fit in a string variable.");
            }

            // Message to inform user they will be returned to main menu if input is invalid
            if (inputValid != true)
            {
                Console.WriteLine("\nPress any key to return to the Main Menu");
                Console.ReadKey();

                input = "";
            }

            return input;
        }

        // public static method to validate inputs and send them to Constructor
        public static void AddEmployee(int _id, string _name, int _salary)
        {
            // Validate info to be entered is good
            _id = ValidateInput(Convert.ToString(_id), "newID");
            _name = ValidateInput(_name);
            _salary = ValidateInput(Convert.ToString(_salary), "salary");

            // If inputs are valid, call Constructor and add to global list
            if (_id != 0 && _name != "" && _salary != 0)
            {
                Employee employee = new Employee(_id, _name, _salary);
                Program.employees.Add(employee);

                Console.WriteLine($"Success! Employee #{employee.id} created and saved.");
                Console.WriteLine("\nPress any key to return to the Main Menu");
                Console.ReadKey();
            }
        }

        // public method to edit employee info
        public void EditEmployee(int editChoice, string _newInfo)
        {
            // Variables
            string newName;
            int newSalary;

            // update appropriate attribute and display updated information
            if (editChoice == 1)
            {
                newName = ValidateInput(_newInfo);

                if (newName != "")
                {
                    name = newName;

                    Console.WriteLine($"Success! Employee #{id} changed and saved:");
                    DisplayEmployee();

                    Console.WriteLine("\nPress any key to return to the Main Menu");
                    Console.ReadKey();
                }
            }
            else if (editChoice == 2)
            {
                newSalary = ValidateInput(_newInfo, "salary");

                if (newSalary != 0)
                {
                    salary = newSalary;

                    Console.WriteLine($"Success! Employee #{id} changed and saved:");
                    DisplayEmployee();

                    Console.WriteLine("\nPress any key to return to the Main Menu");
                    Console.ReadKey();
                }
            }
        }

        // public method to display an employee's info
        public void DisplayEmployee()
        {
            Console.WriteLine("\n*****Employee Record*****");
            Console.WriteLine($"Employee Number: {id}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Monthly Salary: ${salary}");
            Console.WriteLine("******End of Record******");
        }
    }
}
