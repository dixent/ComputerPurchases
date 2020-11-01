using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ComputerPurchases
{
    [Serializable]
    class Computer
    {
        public string firm, version;
        public double price;
        public int year;
        public int intType { get; private set; }
        public string type {
            get { return TYPES[intType]; }
            set { intType = prepareType(value); }
        }

        private readonly string[] TYPES = { "PC", "laptop" };


        [JsonConstructor]
        public Computer(string firm, string version, double price, int year, int intType)
        {
            this.firm = firm;
            this.version = version;
            this.price = price;
            this.year = year;
            this.intType = prepareType(intType);
        }

        public Computer(string firm, string version, double price, int year, string type)
        {
            this.firm = firm;
            this.version = version;
            this.price = price;
            this.year = year;
            intType = prepareType(type);
        }

        private int prepareType(string type)
        {
            
            if (int.TryParse(type, out _))
            {
                return prepareType(Int32.Parse(type));
            } else
            {
                int intType = Array.IndexOf(TYPES, type);
                if (intType < 0)
                {
                    throw new ArgumentException("Wrong type!");
                }
                return intType;
            }    
        }

        private int prepareType(int type)
        {
            if (TYPES.Length - 1 < type)
            {
                throw new ArgumentException("Wrong type!");
            }
            return type;
        }
    }
}
