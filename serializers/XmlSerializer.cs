using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ComputerPurchases.serializers
{
    class XmlSerializer : Serializer
    {
        private System.Xml.Serialization.XmlSerializer serializer;

        public XmlSerializer(string filename)
        {
            objectsFileName = dataFolder + filename;
        }

        public override List<Computer> InitializeComputers()
        {
            XDocument xdoc = XDocument.Load(objectsFileName);
            List<Computer> computers = new List<Computer>();

            foreach (XElement elem in xdoc.Element("Computers").Elements("Computer"))
            {
                computers.Add(new Computer(
                    elem.Element("firm").Value,
                    elem.Element("version").Value,
                    Convert.ToDouble(elem.Element("price").Value),
                    Convert.ToInt32(elem.Element("year").Value),
                    Convert.ToInt32(elem.Element("type").Value)
                )) ;
            }

            return computers;
        }

        public override void SaveComputers(List<Computer> computers)
        {
            XDocument xdoc = XDocument.Load(objectsFileName);

            xdoc.Element("Computers").Remove();
            xdoc.Add(new XElement("Computers", computers.Select(computer => computer.toXml()).ToArray()));
            xdoc.Save(objectsFileName);
        }
    }

    static class ComputerUtility
    {
        public static XElement toXml(this Computer computer)
        {
            return new XElement("Computer", new List<XElement> {
                 new XElement("firm", computer.firm),
                 new XElement("version", computer.version),
                 new XElement("price", computer.price),
                 new XElement("year", computer.year),
                 new XElement("type", computer.intType)
            });
        }
    }
}
