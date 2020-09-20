using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ComputerPurchases
{
    class Computer
    {
        public string firm, version;
        public double price;
        public int year;

        public Computer(string firm, string version, double price, int year)
        {
            this.firm = firm;
            this.version = version;
            this.price = price;
            this.year = year;
        }
    }
}
