using System;
using System.Text;

namespace ForumClient.edu.forum.client.ui
{
    class ConsoleManager
    {
        static void Main(string[] args)
        {
            InitConsole();
            Console.Read();
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
