using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientConsole c = new ClientConsole();
            int exitCode = c.startMenu();
            while (exitCode == -3) //restart!
            {
                c = new ClientConsole();
                exitCode = c.startMenu();
            }
            if (exitCode == -1)
            {
                //Other errors....
            }
            if (exitCode == 0)
            {
                //all good.
            }
        }
    }
}
