using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebserverProg_2016_11_15_KevinLarsson
{
    class Program
    {
        static World[,] world = new World[Console.WindowWidth, Console.WindowHeight];
        public static void Main(string[] args)
        {
            CreateTasks();
            BeginTheLoop();
            Console.Read();
        }

        private static void BeginTheLoop()
        {
            for (;;)
            {
                //change this foreach loop - Bug
                foreach (var item in world)
                {
                    if (item != null)
                    {
                        GetRandomCord(item.RobotInRoom);
                    }
                }
            }
        }

        private static void GetRandomCord(Robot robot)
        {
            Random rnd = new Random();
            World worldPlace = new World();
            world[robot.XValue, robot.YValue] = null;
            int dice = rnd.Next(1, 5);
            var isTrue = CheckPlace(robot, dice);
            switch (dice)
            {
                case 1:
                    if (isTrue != true)
                    {
                        ClearPlace(robot.XValue, robot.YValue);
                        if (robot.XValue != 0)
                        {
                            robot.XValue -= 1;
                        }
                    }
                    break;
                case 2:
                    if (isTrue != true)
                    {
                        ClearPlace(robot.XValue, robot.YValue);
                        if (robot.YValue != 0)
                        {
                            robot.YValue -= 1;
                        }
                    }
                    break;
                case 3:
                    if (isTrue != true)
                    {
                        ClearPlace(robot.XValue, robot.YValue);
                        if (robot.XValue != (Console.WindowWidth - 1))
                        {
                            robot.XValue += 1;
                        }
                    }
                    break;
                case 4:
                    if(isTrue != true)
                    {
                        ClearPlace(robot.XValue, robot.YValue);
                        if (robot.YValue != (Console.WindowHeight - 1))
                        {
                            robot.YValue += 1;
                        }
                    }
                    break;
                default:
                    break;
            }
            world[robot.XValue, robot.YValue] = worldPlace;
            worldPlace.RobotInRoom = robot;
            WriteAt(robot.XValue, robot.YValue, robot.Name);
            Thread.Sleep(500);
        }

        private static bool CheckPlace(Robot robot, int pos)
        {
            bool isTrue = true;
            if ((robot.XValue > 0) && (pos == 1))
            {
                if ((world[robot.XValue - 1, robot.YValue] == null))
                {
                    isTrue = false;
                }
            }
            else if ((robot.YValue > 0) && (pos == 2))
            {
                if ((world[robot.XValue, robot.YValue - 1] == null))
                {
                    isTrue = false;
                }
            }
            else if ((robot.XValue < 79) && (pos == 3))
            {
                if ((world[robot.XValue + 1, robot.YValue] == null))
                {
                    isTrue = false;
                }
            }
            else if ((robot.YValue < 24) && (pos == 4))
            {
                if ((world[robot.XValue, robot.YValue + 1] == null))
                {
                    isTrue = false;
                }
            }

            return isTrue;
        }

        private static void ClearPlace(int left, int top)
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;
            Console.CursorVisible = false;
            Console.SetCursorPosition(left, top);

            Console.Write(" ");
            
            Console.SetCursorPosition(currentLeft, currentTop);
            Console.CursorVisible = true;
        }

        public static void CreateTasks()
        {
            Task<string[]> parent = Task.Run(() =>
            {
                var results = new string[3];
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);
                tf.StartNew(() => results[0] = "1");
                tf.StartNew(() => results[1] = "2");
                tf.StartNew(() => results[2] = "3");
                return results;
            });
            var finalTask = parent.ContinueWith(parentTask =>
            {
                foreach (var item in parentTask.Result)
                {
                    World worldPlace = new World();
                    var rndNum = GetRandomNumber();
                    Thread.Sleep(3000);
                    world[rndNum[0], rndNum[1]] = worldPlace;
                    worldPlace.RobotInRoom = new Robot(item, rndNum[0], rndNum[1]);
                    WriteAt(rndNum[0], rndNum[1], item);
                    //Console.WriteLine(item);
                }
            });
            Task.WaitAll(finalTask);
        }
        public static void WriteAt(int left, int top, string s)
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;
            Console.CursorVisible = false;
            Console.SetCursorPosition(left, top);

            GetRandomColor();
            Console.Write(s);
            Console.ResetColor();

            Console.SetCursorPosition(currentLeft, currentTop);
            Console.CursorVisible = true;
        }
        public static void GetRandomColor()
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            var colorLength = colors.Length;
            Random rnd = new Random();
            int colorNum = rnd.Next(colorLength);

            Console.ForegroundColor = colors[4];
        }
        public static int[] GetRandomNumber()
        {
            Random rnd = new Random();
            var w = Console.WindowWidth;
            var h = Console.WindowHeight;
            int numLeft = rnd.Next(w);
            int numTop = rnd.Next(2, h);
            int[] array = new int[2];

            array[0] = numLeft;
            array[1] = numTop;


            return array;
        }
    }
}
