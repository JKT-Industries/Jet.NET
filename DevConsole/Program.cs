using System;
using System.Diagnostics;

namespace DevConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test();
        }

        private static void Test()
        {
            var tokenResponse = Jet.Main.RetrieveToken("test", "test").Result;

            if (!tokenResponse.Success)
            {
                Debug.WriteLine(tokenResponse.Message);
            }
        }
    }
}
