using System;
using System.Collections.Generic;

namespace JENPY.Storage
{

    public class DataStore
    {
        public static IDictionary<String, string> DataValues { get; set;}

        static DataStore() {
            DataValues = new Dictionary<string, string>();

            String BackupFileName = StorageConstants.FileName;
            restore(DataValues, BackupFileName);      
        }

        private static void restore(IDictionary<string, string> dataValues, string backupFileName)
        {
            string[] lines = System.IO.File.ReadAllLines(backupFileName);
            foreach (String keyVal in lines) {
                string[] pair = keyVal.Split(',');
                dataValues.Add(pair[0], pair[1]);
            }
        }
    }


}
