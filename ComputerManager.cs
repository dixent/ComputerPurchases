using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;
using System.Linq;
using System.ComponentModel;

namespace ComputerPurchases
{
    class ComputerManager
    {
        private List<Computer> computers;
        private string computersFileName = Environment.CurrentDirectory + "../../../../data/computers.json";
        public ComputerManager()
        {
            InitializeComputers();
        }

        private void InitializeComputers()
        {
            string json = File.ReadAllText(computersFileName);
            computers = JsonConvert.DeserializeObject<List<Computer>>(json);
        }

        private void SaveComputers()
        {
            string json = JsonConvert.SerializeObject(computers);
            File.WriteAllText(computersFileName, json);
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
                table.AddRow(index, computer.firm, computer.version, computer.price, computer.year);
                index += 1;
            }
            table.Write();
        }

        public void AddComputer(Computer computer)
        {
            computers.Add(computer);
            SaveComputers();
            ShowComputers();
        }

        public void RemoveComputer(int index)
        {
            computers.RemoveAt(index);
            SaveComputers();
            ShowComputers();
        }

        public void UpdateComputer(int index, Computer computer)
        {
            computers[index] = computer;
            SaveComputers();
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
            return new ConsoleTable("Index", "Firm", "Version", "Price", "Year of issue");
        } 

    }
}
