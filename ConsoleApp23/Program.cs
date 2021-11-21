using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ConsoleApp23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Перемещать квадрат на стрелку влево и стрелку вправо или вращать?");
            Console.WriteLine("1 - перемещать");
            Console.WriteLine("2 - вращать");
            Console.Write("Ваш выбор: ");
            int choice = int.Parse(Console.ReadLine());

            SquareTransformer transformer = null;
            if (choice == 1)
                transformer = new SquareTransformer();
            else if (choice == 2)
                transformer = new Rotation();
            else
                throw new Exception("Неизвестный ответ");

            transformer.ReadParameters();

            Console.Clear();
            PrintKeys();
            Console.WriteLine();
            transformer.PrintParameters();

            while (true)
            {
                var keyInfo = Console.ReadKey();

                bool exit = false;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        transformer.LeftArrow();
                        break;
                    case ConsoleKey.RightArrow:
                        transformer.RightArrow();
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                }

                if (exit)
                    return;

                Console.Clear();
                PrintKeys();
                Console.WriteLine();
                transformer.PrintParameters();
            }
        }
        private static void PrintKeys()
        {
            Console.WriteLine("Стрелка влево - повернуть/переместить влево");
            Console.WriteLine("Стрелка вправо - повернуть/переместить вправо");
            Console.WriteLine("Escape - выход");
        }
    }
    class Rotation : SquareTransformer
    {
        double RotationAngle = 10d;
        public override void LeftArrow()
        {
            Rotate(RotationAngle);
        }

        public override void RightArrow()
        {
            Rotate(-RotationAngle);
        }

        protected void Rotate(double degrees)
        {
            double radians = degrees / 180d * Math.PI;

            for (int i = 0; i < squareVertexes.Length; i++)
            {
                double x = squareVertexes[i].X;
                double y = squareVertexes[i].Y;

                double x1 = x * Math.Cos(radians) - y * Math.Sin(radians);
                double y1 = x * Math.Sin(radians) + y * Math.Cos(radians);

                squareVertexes[i].X = Convert.ToSingle(x1);
                squareVertexes[i].Y = Convert.ToSingle(y1);
            }
        }
    }
    public class SquareTransformer
    {
        public PointF[] squareVertexes;

        public void ReadParameters()
        {
            Console.Write("X: ");
            float x = float.Parse(Console.ReadLine());

            Console.Write("Y: ");
            float y = float.Parse(Console.ReadLine());

            Console.Write("Сторона: ");
            float side = float.Parse(Console.ReadLine());

            squareVertexes = new PointF[]
            {
                new PointF(x,y),
                new PointF(x + side, y),
                new PointF(x + side, y - side),
                new PointF(x,y - side)
            };
        }

        public virtual void LeftArrow()
        {
            Move(new SizeF(-1f, 0f));
        }

        public virtual void RightArrow()
        {
            Move(new SizeF(1f, 0f));
        }

        public void PrintParameters()
        {

            Console.WriteLine("Координаты вершин квадрата:");

            int i;
            for (i = 0; i < squareVertexes.Length - 1; i++)
                Console.Write(
                    $"({squareVertexes[i].X};" +
                    $"{squareVertexes[i].Y}), "
                );

            Console.WriteLine(squareVertexes[i]);
        }

        protected void Move(SizeF offset)
        {
            for (int i = 0; i < squareVertexes.Length; i++)
                squareVertexes[i] += offset;
        }

    }

}