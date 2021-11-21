using System;

namespace ConsoleApp24
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Обычный или усечённый конус?");
            Console.WriteLine("1 - Обычный конус");
            Console.WriteLine("2 - Усечённый конус");
            Console.WriteLine();
            Console.Write("Ваш выбор: ");

            int answer = int.Parse(Console.ReadLine());

            Cone cone;
            if (answer == 1)
                cone = new Cone();
            else if (answer == 2)
                cone = new TruncatedCone();
            else
                throw new Exception("Неизвестный выбор");

            Console.WriteLine();
            Console.WriteLine("Введите параметры");
            cone.ReadFromConsole();

            Console.WriteLine();
            Console.WriteLine("Введённые параметры:");
            cone.PrintToConsole();

            Console.WriteLine();
            Console.WriteLine($"Обьем фигуры: {cone.CalculateV()}");
            Console.WriteLine($"Площадь нижнего основания: {cone.CalculateS()}");
            Console.ReadLine();
        }
    }

    public class Cone
    {
        public float Height { get; protected set; } = 0f;
        public float LowerRadius { get; protected set; } = 0f;
        public virtual void ReadFromConsole()
        {
            Console.Write("Высота: ");
            Height = float.Parse(Console.ReadLine());

            Console.Write("Радиус основания: ");
            LowerRadius = float.Parse(Console.ReadLine());
        }

        public virtual void PrintToConsole()
        {
            Console.WriteLine($"Высота: {Height}");
            Console.WriteLine($"Радиус основания: {LowerRadius}");
        }

        public virtual double CalculateV()
        {
            return 1d / 3d * Math.PI * LowerRadius * LowerRadius * Height;
        }

        public double CalculateS()
        {
            return Math.PI * LowerRadius * LowerRadius;
        }
    }

    public class TruncatedCone : Cone
    {
        public float UpperRadius { get; protected set; }

        public override void ReadFromConsole()
        {
            base.ReadFromConsole();

            Console.Write("Радиус верхнего основания: ");
            UpperRadius = float.Parse(Console.ReadLine());
        }
        public override void PrintToConsole()
        {
            base.PrintToConsole();

            Console.WriteLine(
                $"Радиус верхнего основания: {UpperRadius}"
            );
        }
        public override double CalculateV()
        {
            double Sqr(double a)
            {
                return a * a;
            }
            return
                1d / 3d * Math.PI * Height * (Sqr(UpperRadius) + UpperRadius * LowerRadius + Sqr(LowerRadius));
        }
    }
}
