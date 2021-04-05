using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ArenaFighter3
{
    public class Logger
    {
        private LogIo _log;
        private string[] _logContents;
        private string _logContent;
        private List<Round.RoundResult> _roundList = new List<Round.RoundResult>();

        public Logger()
        {
            this._log = new LogIo();
            this._logContent = "";
            this._logContents = new string[0];
        }
        public Logger(string logData) : this()
        {
            this._logContent = logData;
        }
        public Logger(string[] logData) : this()
        {
            this._logContents = logData;
        }
        public void LogRound(Round.RoundResult data)
        
        {
            this._roundList.Add(data);
        }

        public void ClearRoundLog()
        {
            this._roundList.Clear();
        }

        public List<Round.RoundResult> GetRoundLog()
        {
            return this._roundList;
        }


        public void WriteLog(string logEntry)
        {
            _ = this._log.WriteLog(logEntry);
        }
        public void WriteRoundLog()
        {
            _ = this._log.WriteLog(this._roundList);
        }
       

        private protected class LogIo
        {
            private string _path;
            private string _basePath = @"../../../logs/";
            private string _logName = "ArenaFighter.log";
            private string _logEntry;
            private string[] _logEntries = new string[0];

            public LogIo()
            {
                this._path = this._basePath + this._logName;
            }
            public LogIo(string fileName)
            {
                this._path = this._basePath + fileName;
            }

            public string Path
            {
                get { return this._path; }
            }
            public string LogName
            {
                get { return this._logName; }
                /*set { this._logName = value; }*/
            }
            public async Task WriteLog(string data)
            {
                this._path = this._basePath + this._logName;
                this._logEntry = "";
                this._logEntry = DateTime.Now.ToString() + "," + data;
                await File.AppendAllTextAsync(this._path, this._logEntry, System.Text.Encoding.UTF8);
            }
            public async Task WriteLog(string[] data)
            {
                int i = 0;
                Array.Resize(ref this._logEntries, 0);
                foreach(string entry in data)
                {
                    Array.Resize(ref this._logEntries, this._logEntries.Length + 1);
                    this._logEntries[i] = DateTime.Now.ToString() + "," + entry;
                    i++;
                }
                this._path = this._basePath + this._logName;
                await File.AppendAllLinesAsync(this._path, data, System.Text.Encoding.UTF8);
            }
            public async Task WriteLog(List<Round.RoundResult> data)
            {
                
                this._path = this._basePath + this._logName;
                this._logEntry = "";
                foreach(Round.RoundResult result in data)
                {
                    this._logEntry += result.RoundNumber.ToString() + "," + result.Winner.ToString() + "," + result.UserScore.ToString() + "," + result.ComputerScore.ToString() + "\r";
                }
                await File.AppendAllTextAsync(this._path, this._logEntry, System.Text.Encoding.UTF8);
            }
        }
    }

    public static class DeleteLogs
    {
        static ConsoleKeyInfo key = new ConsoleKeyInfo();
        public static bool DeleteAllLogs()
        {
            string folder = @"../../../logs/";
            string[] files = Directory.GetFiles(folder);

            foreach(string file in files)
            {
                if (file.Substring(file.Length - 4, 4) == ".log")
                {
                    Console.Write("DELETE file: " + file+" Y/N ");
                    key = Console.ReadKey(false);
                    if (key.KeyChar.ToString().ToLower() == "y")
                    {
                        File.Delete(file);
                        Console.WriteLine("\nFile " + file + " deleted!");
                    }
                }
            }
            return true;
        }
    }
    
}
