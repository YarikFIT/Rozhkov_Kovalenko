using System;

namespace Robot_AI
{
    public class Room
    {
        public int[,] pos = new int[5, 5];

        public Room()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    pos[i, j] = 0;
                }
            }
            //Console.WriteLine("New room");
        }

        public void Set_Trash(Point p)
        {
            if (p != null)
                pos[p.x, p.y] = 1;
        }
        public void Clear_Trash(int x, int y)
        {
            if (pos[x, y] == 1)
                pos[x, y] = 0;
        }
    }

    public class Point
    {
        public int x, y;

        public Point()
        {
            x = y = 0;
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int Get_x()
        {
            return x;
        }
        public int Get_y()
        {
            return y;
        }
    }
    class Robot
    {
        public int pos_x, pos_y;
        Robot()
        {
            pos_x = pos_y = 0;
        }
        public Robot(int x, int y)
        {
            pos_x = x;
            pos_y = y;
        }
        public int[,] Clear(int[,] arr)
        {
            if (arr[pos_x, pos_y] == 1)
                arr[pos_x, pos_y] = 0;
            return arr;
        }

        int iter_i = 0, return_x, return_y;
        public void View(int[,] arr)
        {

            if (iter_i == 0)
            {
                return_x = pos_x;
                return_y = pos_y;

                for (int i = 0; i < 1; i++)
                {
                    if (pos_x == 2)
                        break;
                    if ((pos_x == 1 && (pos_y == 0 || pos_y == 4)) && (arr[pos_x - 1, pos_y] == 1))
                    { pos_x--; iter_i = 1; break; }
                    if ((pos_x == 3 && (pos_y == 0 || pos_y == 4)) && (arr[pos_x + 1, pos_y] == 1))
                    { pos_x++; iter_i = 1; break; }
                    if ((pos_x == 1) && (arr[pos_x - 1, pos_y] == 1))
                    { pos_x--; iter_i = 1; break; }
                    if ((pos_x == 1) && (arr[pos_x + 1, pos_y] == 1))
                    { pos_x++; iter_i = 1; break; }
                    if ((pos_x == 3) && (arr[pos_x - 1, pos_y] == 1))
                    { pos_x--; iter_i = 1; break; }
                    if ((pos_x == 3) && (arr[pos_x + 1, pos_y] == 1))
                    { pos_x++; iter_i = 1; break; }




                    //if(arr[pos_x+1,pos_y] == 1)
                    //{
                    //------------- if ((pos_x == 1) && ((arr[pos_x - 1, pos_y] == 1) || (arr[pos_x + 1, pos_y] == 1)))
                    //}
                }
            }

        }
        public void Return()
        {
            if (iter_i == 1 || iter_i == 2)
            {
                if (iter_i == 2)
                {
                    pos_x = return_x;
                    pos_y = return_y;
                    iter_i = 3;
                }
                if (iter_i != 3) iter_i = 2;
            }
        }

        public void Algoritm_1()
        {
            if (iter_i == 0 || iter_i == 3)
            {
                if (iter_i == 0)
                {
                    if (pos_x == 1 && pos_y < 4)
                        pos_y++;
                    else if (pos_x < 3 && pos_y == 4)
                        pos_x++;
                    else if (pos_x == 3 && pos_y > 0)
                        pos_y--;
                    else if (pos_x > 1 && pos_y == 0)
                        pos_x--;
                }
                iter_i = 0;
            }
            //iter_i = 0;
        }



    }

    public class Garbage
    {
        static Random rnd = new Random();
        static Random rnd2 = new Random();
        public Point Trash()
        {

            int value = rnd.Next(1, 2);
            if (value == 1)
            {
                int x = rnd2.Next(0, 5);
                int y = rnd2.Next(0, 5);
                Point p = new Point(x, y);
                return p;
            }
            else
                return null;
        }


    }

    public class Process
    {
        public Process()
        {
            //Console.WriteLine("Start");
        }
        Room room = new Room();
        Garbage garbage = new Garbage();
        Robot robot = new Robot(1, 0);
        public void Start()
        {
            for (int g = 0; g < 100; g++)
            {
                Console.Clear();
                Console.WriteLine(" 0 - clear\n 1 - trash\n r - robot\n\n\n Iterration : " + g);
                Console.WriteLine("_____________________________________________\n");
                room.Set_Trash(garbage.Trash());
                robot.View(room.pos);
                robot.Return();
                robot.Algoritm_1();
                room.Clear_Trash(robot.pos_x, robot.pos_y);

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (robot.pos_x == i && robot.pos_y == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                            Console.Write('r');
                            Console.ResetColor();
                        }

                        else
                            Console.Write(room.pos[i, j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                Console.Write("_____________________________________________");
                Console.WriteLine(" ");
                System.Threading.Thread.Sleep(300);

            }
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //Console.Write(room.pos[i, j]);
                    //Console.Write(" ");
                    if (room.pos[i, j] == 1)
                        sum++;
                }
                //Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Trash sum = " + sum);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                Process process = new Process();
                process.Start();
            }

            Console.ReadKey();
        }
    }
}
