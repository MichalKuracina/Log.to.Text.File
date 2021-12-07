using System;
using System.IO;

namespace Log.to.Text.File
{
    class SessionLog
    {
        private readonly string _directory;
        private readonly string _fileName;
        private readonly string _path;

        public SessionLog()
        {
            this._directory = System.AppDomain.CurrentDomain.BaseDirectory + "SessionLogs\\";
            this._fileName = "Session_Log_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".txt";
            this._path = _directory + _fileName;

            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
        }

        public SessionLog(string directory)
        {
            this._directory = directory.EndsWith("\\") ? directory + "SessionLogs\\" : directory + "\\SessionLogs\\";
            this._fileName = "Session_Log_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".txt";
            this._path = _directory + _fileName;

            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
        }

        public string FilePath
        {
            get { return _path; }
        }

        public void Write(string text)
        {
            using StreamWriter sw = System.IO.File.AppendText(_path);
            sw.WriteLine($"{DateTime.Now} {text}");
        }

        public void Write(Exception e)
        {
            using StreamWriter sw = System.IO.File.AppendText(_path);
            sw.WriteLine($"{DateTime.Now} EXCEPTION MESSAGE: {e.Message}");
            sw.WriteLine($"{DateTime.Now} STACK TRACE: {e.StackTrace}");
        }
    }
}
