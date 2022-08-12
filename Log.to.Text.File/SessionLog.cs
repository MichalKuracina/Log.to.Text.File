using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Log.to.Text.File
{
    public class SessionLog
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
            sw.WriteLine($"{DateTime.Now} >> {text}");
        }

        public void Write(Exception e)
        {
            using StreamWriter sw = System.IO.File.AppendText(_path);
            sw.WriteLine($"{DateTime.Now} >> EXCEPTION MESSAGE: {e.Message}");
            sw.WriteLine($"{DateTime.Now} >> STACK TRACE: {e.StackTrace}");
        }

        public void Screenshot()
        {
            ////Create a new bitmap.
            Bitmap bmpScreenshot = new Bitmap(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height, 
                PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            Bitmap clone = bmpScreenshot.Clone(new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), PixelFormat.Format8bppIndexed);

            string screenshotFileName = "Screenshot_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".png";

            // Save the screenshot to the specified path that the user has chosen.
            clone.Save(_directory + screenshotFileName, ImageFormat.Png);

            // Make entry to the log
            Write(screenshotFileName);
        }

    }
}
