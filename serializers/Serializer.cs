using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerPurchases.serializers
{
    abstract class Serializer
    {
        protected readonly string dataFolder = Environment.CurrentDirectory + "../../../../data/";
        protected string objectsFileName;

        public virtual List<Computer> InitializeComputers() { return new List<Computer>(); }
        public virtual void SaveComputers(List<Computer> computers) { }
    }
}
