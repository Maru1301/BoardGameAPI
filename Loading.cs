using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class Loading
    {
        static readonly List<string> Loadings = new()
            {
                "L",
                "Lo",
                "Loa",
                "Load",
                "Loadi",
                "Loadin",
                "Loading",
                "Loading.",
                "Loading..",
                "Loading..."
            };
        public static void Show()
        {
            int x = 0;

            int time = 10;
            int millisec = 150;
            while (x < time)
            {
                Console.Clear();
                Console.WriteLine(Loadings[x % Loadings.Count]);
                Thread.Sleep(millisec);
                x++;
            }
        }
    }
}
