namespace Lab4_14
{
    class Circle
    {
        float? radius = null;
        Point center;

        public float? Radius {
            get
            {
                return radius;
            }
            set
            {
                float? oldRadius = radius;
                double? oldArea = CalculateArea;

                radius = value;
                
                if (oldRadius == null)
                {
                    return;
                }
                else if (radius <= 0.6 && radius >= 0.5)
                {
                    Catcher(oldArea);
                }
            }
        }

        public double? CalculateArea
        {
            get
            {
                double? area = Math.PI * Radius * Radius;

                return area;
            }
        }

        public void ChangeCoords()
        {
            Console.Write($"Новое значение X(было {center.X}): ");
            float new_x = float.Parse(Console.ReadLine());

            Console.Write($"Новое значение Y(было {center.Y}): ");
            float new_y = float.Parse(Console.ReadLine());

            center.X = new_x;
            center.Y = new_y;
            Console.WriteLine("Координаты были изменены успешно");
        }

        private void ChangedRadius(double? oldArea)
        {
            Console.WriteLine($"Успех! Площадь изменена с {oldArea} на 1!");
        }

        public delegate void Handler(double? oldArea);
        public event Handler Catcher;

        public delegate void Ex(float radius);
        public event Ex Except;

        private void GetEx(float radius)
        {
            throw new InvalidRadiusException(radius);
        }

        public Circle()
            : this(0, 0, 1)
        { }

        public Circle(float center_x = 0, float center_y = 0, float radius = 1)
        {
            if (radius < 0)
            {
                Except = new(GetEx);
                Except(radius);
            }

            Catcher = new(ChangedRadius);
            center = new Point("O", center_x, center_y);
            Radius = radius;
        }
    }
}
