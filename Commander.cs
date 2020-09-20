using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerPurchases
{
    class Commander
    {
        private const string helpMessage =
@"Write COMMAND & ARGS;
ARGS can does't exist!
Examples:
Help
Exit
ShowComputers
AddComputer & firm_name version price year
RemoveComputer & index
UpdateComputer & firm_name version price year
FindBy & field value
SortBy & field";

        private string[] continueAnswers = new string[] { "", "\n", "Exit" };
        private ComputerManager computerManager = new ComputerManager();
        public void ListenCommands()
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
                    GetType().GetMethod(answer.Split(" & ")[0]).Invoke(this, new object[] { args });
                } catch (Exception e)
                {
                    Console.WriteLine("Command invalid!");
                }
            }
        }

        public void Help(string[] args = null)
        {
            Console.WriteLine(helpMessage);
        }

        public void AddComputer(string[] args = null)
        {
            string firm = args[0], version = args[1];
            double price = Convert.ToDouble(args[2]);
            int year = Convert.ToInt32(args[3]);

            computerManager.AddComputer(new Computer(firm, version, price, year));
        }

        public void RemoveComputer(string[] args = null)
        {
            int index = Convert.ToInt32(args[0]);

            computerManager.RemoveComputer(index);
        }

        public void ShowComputers(string[] args = null)
        {
            computerManager.ShowComputers();
        }

        public void UpdateComputer(string[] args = null)
        {
            string firm = args[1], version = args[2];
            double price = Convert.ToDouble(args[3]);
            int index = Convert.ToInt32(args[0]), year = Convert.ToInt32(args[4]);

            computerManager.UpdateComputer(index, new Computer(firm, version, price, year));
        }

        public void FindBy(string[] args = null)
        {
            string variableName = args[0], value = args[1];

            computerManager.FindBy(variableName, value);
        }

        public void SortBy(string[] args = null)
        {   
            string field = args[0];

            computerManager.SortBy(field);
        }
    }
}
