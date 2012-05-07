using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore;

namespace ForumClientConsole
{
    class ClientConsole
    {
         ClientController controller;

        // This code is for opening Console
        // TODO - we need to try finding an more elegant way to alloc & release console
        //[DllImport("kernel32.dll")]
        //public static extern Boolean AllocConsole();
        //[DllImport("kernel32.dll")]
        //public static extern Boolean FreeConsole();

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientConsole()
        {
            controller = new ClientController();
            controller.OnUpdateFromServer += new ForumClientCore.Network.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer);
        }

        /// <summary>
        /// Menu method
        /// </summary>
        public void startMenu()
        {



            //AllocConsole(); 
            InitConsole();

            Console.WriteLine("Menu - post/quit");

            string line;

            while (true)
            {
                line = Console.ReadLine();

                string[] command = line.Split(' ');

                switch (command[0])
                {
                    case "post":
                        controller.AddMessage(command[1]);  //TODO Need to support more args
                        break;
                    case "quit":
                        freeConsole();
                        return;
                    //TODO Continue
                }
            }
            
        }

        /// <summary>
        /// This method is called when controller invoked its OnUpdate event
        /// </summary>
        /// <param name="text"></param>
        public void controller_OnUpdateFromServer(string text)
        {
            try
            {
                Console.WriteLine(text);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        /// <summary>
        /// Must to be called when quiting console
        /// </summary>
        public void freeConsole()
        {
            //FreeConsole();
        }

        private static void InitConsole()
        {
            Console.Title = "Forum Client";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WindowWidth = 100;
            Console.Beep();
            Console.BackgroundColor = ConsoleColor.Blue;
            StringBuilder sb = new StringBuilder();
            sb.Append("  $$\\     $$\\                        $$$$$$\\                                                       \n");
            sb.Append("  $$ |    $$ |                      $$  __$$\\                                                      \n");
            sb.Append("$$$$$$\\   $$$$$$$\\   $$$$$$\\        $$ /  \\__|$$$$$$\\   $$$$$$\\  $$\\   $$\\ $$$$$$\\$$$$\\            \n");
            sb.Append("\\_$$  _|  $$  __$$\\ $$  __$$\\       $$$$\\    $$  __$$\\ $$  __$$\\ $$ |  $$ |$$  _$$  _$$\\           \n");
            sb.Append("  $$ |    $$ |  $$ |$$$$$$$$ |      $$  _|   $$ /  $$ |$$ |  \\__|$$ |  $$ |$$ / $$ / $$ |          \n");
            sb.Append("  $$ |$$\\ $$ |  $$ |$$   ____|      $$ |     $$ |  $$ |$$ |      $$ |  $$ |$$ | $$ | $$ |          \n");
            sb.Append("  \\$$$$  |$$ |  $$ |\\$$$$$$$\\       $$ |     \\$$$$$$  |$$ |      \\$$$$$$  |$$ | $$ | $$ |          \n");
            sb.Append("   \\____/ \\__|  \\__| \\_______|      \\__|      \\______/ \\__|       \\______/ \\__| \\__| \\__|          \n");
            Console.WriteLine(sb);
        }


    }
}
