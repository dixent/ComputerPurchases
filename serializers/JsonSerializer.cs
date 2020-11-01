using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace ComputerPurchases.serializers
{
    class JsonSerializer : Serializer
    {
        public JsonSerializer(string filename)
        {
            objectsFileName = dataFolder + filename;
        }

        public override List<Computer> InitializeComputers()
        {
            string json = File.ReadAllText(objectsFileName);
            return JsonConvert.DeserializeObject<List<Computer>>(json);
        }

        public override void SaveComputers(List<Computer> computers)
        {
            string json = JsonConvert.SerializeObject(computers);
            File.WriteAllText(objectsFileName, json);
        }
    }
}
