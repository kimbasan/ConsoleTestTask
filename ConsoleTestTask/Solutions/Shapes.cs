using ConsoleTestTask.Util;

namespace ConsoleTestTask.Solutions
{
    internal class Shapes
    {
        public static void Start()
        {
            bool repeat = true;
            var menuItems = new string[] {
                    "1 - Квадрат",
                    "2 - Прямоугольник",
                    "3 - Круг",
                    "4 - Ромб",
                    "5 - Выход"
                };

            do
            {
                Console.WriteLine("Выберите фигуру:");
                int choice = InputHandler.GetMenuSelection(menuItems);

                switch (choice)
                {
                    case 1: PrintShape(CreateSquare()); break;
                    case 2: PrintShape(CreateRectangle()); break;
                    case 3: PrintShape(CreateCircle()); break;
                    case 4: PrintShape(CreateRhombus()); break;
                    case 5: repeat = false; break;
                }

            } while (repeat);
        }

        private static Shape CreateCircle()
        {
            return new Circle(InputHandler.GetDouble("Введите радиус: "));
        }

        private static Shape CreateSquare()
        {
            return new Square(InputHandler.GetDouble("Введите сторону квадрата: "));
        }

        private static Shape CreateRectangle()
        {
            return new Rectangle(InputHandler.GetDouble("Введите первую сторону прямоугольника: "),
                InputHandler.GetDouble("Введите вторую сторону прямоугольника: "));
        }

        private static Shape CreateRhombus()
        {
            return new Rhombus(InputHandler.GetDouble("Введите сторону ромба: "), InputHandler.GetFloat("Введите угол между сторонами ромба (в градусах): "));
        }

        private static void PrintShape(Shape shape)
        {
            bool valid = shape.IsValid();
            Console.WriteLine($"Фигура возможна: {valid}");
            if (valid)
            {
                Console.WriteLine($"Периметр : {shape.GetPerimeter()}");
                Console.WriteLine($"Площадь : {shape.GetArea()}");
            }
            InputHandler.PauseForAnyKey();
        }

    }

    abstract class Shape
    {
        public abstract bool IsValid();
        public abstract double GetPerimeter();
        public abstract double GetArea();
    }

    class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double GetArea()
        {
            if (IsValid())
            {
                return radius * radius * Math.PI;
            }
            else
            {
                return 0;
            }

        }

        public override double GetPerimeter()
        {
            if (IsValid())
            {
                return 2 * radius * Math.PI;
            }
            else
            {
                return 0;
            }

        }

        public override bool IsValid()
        {
            return radius > 0;
        }
    }

    abstract class Parallelogram : Shape
    {
        private double sideA;
        private double sideB;
        private float angle;

        public Parallelogram(double sideA, double sideB, float angle)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.angle = angle;
        }

        public override double GetArea()
        {
            if (IsValid())
            {
                return sideA * sideB * Math.Sin(ConvertToRadian(angle));
            }
            else
            {
                return 0;
            }
        }

        public override double GetPerimeter()
        {
            if (IsValid())
            {
                return (sideA + sideB) * 2;
            }
            else
            {
                return 0;
            }
        }

        public override bool IsValid()
        {
            return sideA > 0 && sideB > 0 && angle > 0 && angle < 180;
        }

        private static double ConvertToRadian(float angle)
        {
            return angle * (Math.PI) / 180;
        }
    }

    class Rhombus : Parallelogram
    {
        public Rhombus(double side, float angle) : base(side, side, angle)
        {
        }
    }

    class Rectangle : Parallelogram
    {
        public Rectangle(double sideA, double sideB) : base(sideA, sideB, 90)
        {
        }
    }

    class Square : Rectangle
    {
        public Square(double side) : base(side, side)
        {
        }
    }

}
