using System;

namespace Claritty.Src;

public static class Debug
{
    public static bool EnableLogging = true;

    public static void Log(string msg)
    {
        if (EnableLogging)
        {
            Console.WriteLine(msg);
        }
    }
}
