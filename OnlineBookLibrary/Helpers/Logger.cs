using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Helpers
{
    public  class Logger: IDisposable
    {
        string id = Guid.NewGuid().ToString();
        private string controller;
        static string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Log.txt";
        private static Object lockObject = new Object();
        Stopwatch stopwatch = new Stopwatch();

        public void Log(string message)
        {
            string logMessage = string.Empty;
            if (controller == null)
                controller = string.Empty;
            logMessage = stopwatch.ElapsedMilliseconds +" "+ controller +": "+message;
            lock (lockObject)
            {
                using (StreamWriter streamWriter = new StreamWriter(FilePath,true))
                {
                    streamWriter.WriteLine(logMessage);
                    streamWriter.Close();
                }
            }
        }

        public Logger()
        {
            string logMessage = id + " " + DateTime.Now + " Start Logging";
            using (StreamWriter streamWriter = new StreamWriter(FilePath,true))
            {
                streamWriter.WriteLine(logMessage);
                streamWriter.Close();
            }
            stopwatch.Start();
        }

        public Logger(string controller):this()
        {
            this.controller = controller;
        }
        
        public void Dispose()
        {
            string logMessage = id + " " + DateTime.Now + " Stop Logging, total elapsed msec: "+ stopwatch.ElapsedMilliseconds;
            using (StreamWriter streamWriter = new StreamWriter(FilePath, true))
            {
                streamWriter.WriteLine(logMessage);
                streamWriter.Close();
            }
            stopwatch.Stop();
        }
    }
}