using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Members
{
    public class DataReader
    {

        public string[] readConnectionStrings()
        {
            string[] connectionString = new string[3];

            using (StreamReader reader = new StreamReader("dbConnectionString.txt"))
            {
                connectionString = reader.ReadToEnd().Split(';');
            }

            return connectionString;
        }
    }
}
