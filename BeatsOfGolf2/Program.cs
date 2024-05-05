using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace BeatsOfGolf2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("config.txt");

            for (int i = 0; i < input.Length; i++)
            {
                string[] command = input[i].Split(";");
                switch (command[0])
                {
                    case "1":
                        OpeningProgram(command);
                        break;
                    case "2":
                        DelayingNextCommand(command);
                        break;
                    case "3":
                        RepositioningWindow(command);
                        break;
                    case "4":
                        DoubbleClicking(command);
                        break;
                    case "5":
                        Clicking(command);
                        break;
                    case "6":
                        Dragging(command);
                        break;
                }
            }
    
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n\n\n\n\n\n |\n |\n |\t© Developed by Thymen Van Gijsel\n |\n |\n\nStartup script finished, you may close this window now . . . ");
            Console.Read();
        }

        static void OpeningProgram(string[] command)
        {
            Console.WriteLine($"trying to open {command[1]}");
            AutoFramework.OpenProgram(command[1]);
            Console.WriteLine("program opened");
            Thread.Sleep(100);
        }

        static void DelayingNextCommand(string[] command)
        {
            int time = int.Parse(command[1]);
            Console.WriteLine($"waiting {time}ms");
            Thread.Sleep(time);
            Console.WriteLine("timer finished");
        }

        static void RepositioningWindow(string[] command)
        {
            int x = int.Parse(command[2]);
            int y = int.Parse(command[3]);
            int w = int.Parse(command[4]);
            int h = int.Parse(command[5]);
            AutoFramework.RepositionWindow(command[1], x, y, w, h);
            Console.WriteLine($"repositioning {command[1]}");
        }

        static void DoubbleClicking(string[] command)
        {
            int x = int.Parse(command[1]);
            int y = int.Parse(command[2]);
            AutoFramework.DoubbleClickLeftMouseButton(x, y);
            Console.WriteLine($"double clicking at x:{x} y:{y}");
        }

        static void Clicking(string[] command)
        {
            int x = int.Parse(command[1]);
            int y = int.Parse(command[2]);
            AutoFramework.ClickLeftMouseButton(x, y);
            Console.WriteLine($"clicking at x:{x} y:{y}");
        }

        static void Dragging(string[] command)
        {
            int x = int.Parse(command[1]);
            int y = int.Parse(command[2]);
            int dx = int.Parse(command[3]);
            int dy = int.Parse(command[4]);
            AutoFramework.DragLeftMouseButton(x, y, dx, dy);
            Console.WriteLine($"draggin mouse from x:{x} y:{y} to x:{x+dx} y:{y+dy}");
        }
    }
}
