# About
Helper class which writes to txt file. Useful when running scheduled scripts and you want to have something to double check what was done and when. Screenshot reference is added to the file.

Runs with `netcoreapp3.1`, `net5.0` and `net6.0` only Windows.

## Usage
Initiate logger like this (uses ```System.AppDomain.CurrentDomain.BaseDirectory``` to determine the destination directory):
```cs
var log = new SessionLog();
```

or define your own destination like this:
```cs
var log = new SessionLog(@"C:\Users\USER\Desktop");
```

You can log text like this:
```cs
log.Write("Hello World!");
```

You can screenshot like this:
```cs
log.Screenshot();
```

Or retrieve the path of the log file like this:
```cs
string publicProperty = log.FilePath;
```

# Example
```cs
using System;
using Log.to.Text.File;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new SessionLog(@"C:\Users\USER\Desktop");

            try
            {
                log.Write("I declared an array");
                int[] myArray = new int[1];
                log.Write("I set 99 to second index");
                myArray[1] = 99;
            }
            catch (System.Exception e)
            {
                log.Screenshot();
                log.Write(e);
            }
        }
    }
}
```

# Session_Log_07122022_125838.txt
```txt
12/7/2022 12:58:38 PM >> I declared an array
12/7/2022 12:58:38 PM >> I set 99 to second index
12/7/2022 12:58:38 PM >> Screenshot_12072022_125838.png
12/7/2022 12:58:38 PM >> EXCEPTION MESSAGE: Index was outside the bounds of the array.
12/7/2022 12:58:38 PM >> STACK TRACE:    at Log_to_TXT.Program.Main(String[] args) in C:\Users\USER\source\repos\Log_to_TXT\Log_to_TXT\Program.cs:line 15
```
