using System;
using System.Collections.Generic;
using ConsoleTables;
using System.Linq;
using ComputerPurchases.serializers;

namespace ComputerPurchases
{
    class ComputerManager
    {
        private List<Computer> computers;
        private Serializer serializer;

        private delegate void SaveAndShowComputers(object method, object[] args);
        SaveAndShowComputers saveAndShowComputers;
        public ComputerManager()
        {
            serializer = new XmlSerializer("computers.xml");

            computers = serializer.InitializeComputers();
        }

        public void ShowComputer(Computer computer)
        {

            ConsoleTable table = InitializeTable();
            int index = computers.FindIndex(selectedComputer => selectedComputer == computer);
            table.AddRow(index, computer.firm, computer.version, computer.price, computer.year);
            table.Write();

        }

        public void ShowComputers(List<Computer> computers = null)
        {
            if (computers == null) computers = this.computers;
            ConsoleTable table = InitializeTable();
            int index = 0;
            foreach (var computer in computers)
            {
                table.AddRow(index, computer.firm, computer.version, computer.price, computer.year, computer.type);
                index += 1;
            }
            table.Write();
        }

        public void AddComputer(Computer computer)
        {
            computers.Add(computer);
            serializer.SaveComputers(computers);
            ShowComputers();
        }

        public void RemoveComputer(int index)
        {
            computers.RemoveAt(index);
            serializer.SaveComputers(computers);
            ShowComputers();
        }

        public void UpdateComputer(int index, Computer computer)
        {
            computers[index] = computer;
            serializer.SaveComputers(computers);
            ShowComputers();
        }

        public void FindBy(string variableName, string value)
        {
            var variable = typeof(Computer).GetField(variableName);
            var valueWithType = Convert.ChangeType(value, variable.FieldType);

            foreach (Computer computer in computers) 
            {
                var test = variable.GetValue(computer);
                if (variable.GetValue(computer).Equals(valueWithType))
                {
                    ShowComputer(computer);
                    break;
                }
              
            }
        }
        public void SortBy(string fieldName)
        {
            var field = typeof(Computer).GetField(fieldName);
            var orderedComputers = computers.OrderBy(computer => field.GetValue(computer));
            Console.WriteLine(orderedComputers);
            ShowComputers(orderedComputers.ToList<Computer>());
        }


        public ConsoleTable InitializeTable()
        {
            return new ConsoleTable("Index", "Firm", "Version", "Price", "Year of issue", "Type");
        } 

    }
}
