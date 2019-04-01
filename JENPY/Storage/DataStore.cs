using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JENPY.Storage
{

    public class DataStore
    {
        public static IDictionary<String, string> DataValues { get; set; }
        static Timer timer;

        static DataStore()
        {
            DataValues = new Dictionary<string, string>();

            String BackupFileName = StorageConstants.FileName;
            Restore(DataValues, BackupFileName);
            PeriodicBackup();
        }

        private static void PeriodicBackup()
        {
            int MilliTenHours = 60 * 60 * 10 * 1000;
            Console.WriteLine("debug - in periodic backup once for {0} miliseconds", MilliTenHours);
            timer = new Timer(new TimerCallback(BackupHelper), null, 0, MilliTenHours);
        }

        private static void BackupHelper(Object obj)
        {
            String BackupFileName = StorageConstants.FileName;
            BackupData(DataValues, BackupFileName);
        }

        private static void BackupData(IDictionary<string, string> dataValues, string backupFileName)
        {
            Console.WriteLine("running backup...");
            //Adding a timestamp
            StringBuilder sb = new StringBuilder();
            sb.Append(backupFileName);
            sb.Append("_");
            sb.Append(DateTime.Now.ToString("M-d-yyyy"));
            //sb.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            string OutputName = sb.ToString();


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(OutputName, false))
            {
                foreach (KeyValuePair<string, string> entry in dataValues)
                {
                    String s = String.Format("{0},{1}", entry.Key, entry.Value);
                    file.WriteLine(s);
                }
            }

        }

        private static void Restore(IDictionary<string, string> dataValues, string backupFileName)
        {
            Console.WriteLine("running backup on file {0}", backupFileName);
            string[] lines = System.IO.File.ReadAllLines(backupFileName);
            foreach (String keyVal in lines)
            {
                string[] pair = keyVal.Split(',');
                dataValues.Add(pair[0], pair[1]);
            }
        }

    }


}
