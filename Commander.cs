using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerPurchases
{
    class Commander
    {
        private static readonly string helpMessage =
@"Write COMMAND & ARGS;
ARGS can does't exist!
Examples:
Help
Exit
ShowComputers
AddComputer & firm_name version price year type
RemoveComputer & index
UpdateComputer & firm_name version price year type
FindBy & field value
SortBy & field";

        private static readonly string[] continueAnswers = { "", "\n", "Exit" };
        private static ComputerManager computerManager = new ComputerManager();
        public static void ListenCommands()
        {
            string answer = "";
            string methodName;
            string[] args;

            while (answer != "Exit")
            {
                Console.WriteLine("Enter command please(for help run \"Help\"):");
                answer = Console.ReadLine();

                if (Array.IndexOf(continueAnswers, answer) > -1) continue;

                try {
                    args = answer.Split(" & ");
                    if (args.Length == 1)
                    {
                        args = new string[] { };
                    }
                    else
                    {
                        args = args[1].ToString().Split(' ');
                    }
                    methodName = answer.Split(" & ")[0];
                    typeof(Commander).GetMethod(answer.Split(" & ")[0]).Invoke(typeof(Commander), new object[] { args });
                } catch (Exception e)
                {
                    Console.WriteLine("Command invalid!");
                }
            }
        }

        public static void Help(string[] args = null)
        {
            Console.WriteLine(helpMessage);
        }

        public static void AddComputer(string[] args = null)
        {
            string firm = args[0], version = args[1];
            double price = Convert.ToDouble(args[2]);
            int year = Convert.ToInt32(args[3]);

            computerManager.AddComputer(new Computer(firm, version, price, year, args[4]));
        }

        public static void RemoveComputer(string[] args = null)
        {
            int index = Convert.ToInt32(args[0]);

            computerManager.RemoveComputer(index);
        }

        public static void ShowComputers(string[] args = null)
        {
            computerManager.ShowComputers();
        }

        public static void UpdateComputer(string[] args = null)
        {
            string firm = args[1], version = args[2];
            double price = Convert.ToDouble(args[3]);
            int index = Convert.ToInt32(args[0]), year = Convert.ToInt32(args[4]);

            computerManager.UpdateComputer(index, new Computer(firm, version, price, year, args[5]));
        }

        public static void FindBy(string[] args = null)
        {
            string variableName = args[0], value = args[1];

            computerManager.FindBy(variableName, value);
        }

        public static void SortBy(string[] args = null)
        {   
            string field = args[0];

            computerManager.SortBy(field);
        }
    }
}
