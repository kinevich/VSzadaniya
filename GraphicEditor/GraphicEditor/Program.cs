using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GraphicEditor
{
    class Program
    {
        abstract class Shape
        {
            public virtual double X { get; set; }
            public virtual double Y { get; set; }
            public abstract double Area();
            public virtual void SetCoordinates(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        class Line : Shape
        {
            public double Length { get; set; }

            public Line(double length)
            {
                Length = length;
            }

            public override double Area()
            {
                return 0;
            }
        }

        class Triangle : Shape
        {
            public double Side1 { get; set; }
            public double Side2 { get; set; }
            public double Angle { get; set; }
            public Triangle(double side1, double side2, double angle)
            {
                Side1 = side1;
                Side2 = side2;
                Angle = angle;
            }

            public override double Area()
            {
                return Math.Floor(Side1 * Side2 * Math.Sin(Angle * Math.PI / 180) / 2);
            }                                             //Conversion to degrees
        }

        class Circle : Shape
        {
            public double Radius { get; set; }
            public Circle(double radius)
            {
                Radius = radius;
            }

            public override double Area()
            {
                return Math.Floor(Math.PI * Radius * Radius);
            }
        }

        class Rectangle : Shape
        {
            public double Height { get; set; }
            public double Width { get; set; }
            public Rectangle(double height, double width)
            {
                Height = height;
                Width = width;
            }

            public override double Area()
            {
                return Math.Floor(Height * Width);
            }
        }

        class GraphicEditor
        {
            public List<Shape> list = new List<Shape>();

            public void AddItem(Shape s)
            {
                list.Add(s);
            }

            public void RemoveItem(int index)
            {
                list.RemoveAt(index);
            }

            public double AreaOfAll()
            {
                double sum = 0;
                foreach (Shape s in list)
                {
                    sum += s.Area();
                }
                return sum;
            }

            public void GetInformationOfAll()
            {
                int i = 0;
                foreach (Shape s in list)
                {
                    ++i;
                    Console.WriteLine($"{i}.Index:{list.IndexOf(s)} ; Area:{s.Area()} ; Coordinates: X:{s.X},Y:{s.Y}.");
                }
                Console.WriteLine();
            }

            public void ChangeCoordinates(int index, double x, double y)
            {
                list[index].SetCoordinates(x, y);
            }
        }
        static void Main(string[] args)
        {
            GraphicEditor ge = new GraphicEditor();
            Line line1 = new Line(3);
            Triangle triangle1 = new Triangle(4, 5, 30);
            Circle circle1 = new Circle(5);
            Rectangle rectangle1 = new Rectangle(8, 9);
            ge.AddItem(line1);
            ge.AddItem(triangle1);
            ge.AddItem(circle1);
            ge.AddItem(rectangle1);
            ge.ChangeCoordinates(1, 4, 5);
            ge.GetInformationOfAll();
            ge.AreaOfAll();
            ge.RemoveItem(2);
            ge.GetInformationOfAll();
        }
    }
}
