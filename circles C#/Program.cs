namespace Lab4_14
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Circle> circles = new List<Circle>();

            Console.WriteLine("Команды:");
            Console.WriteLine("1: Добавить окружность");
            Console.WriteLine("2: Узнать площадь окружности");
            Console.WriteLine("3: Изменить координаты центра окружности");
            Console.WriteLine("4: Изменить радиус окружности");
            Console.WriteLine("0: Завершить программу");

            bool running = true;
            while (running)
            {
                Console.WriteLine("======================================================");

                try
                {
                    Console.Write("Команда: ");
                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1":
                            Console.Write("Координаты центра окружности по оси абцисс(X): ");
                            float center_x = float.Parse(Console.ReadLine());

                            Console.Write("Координаты центра окружности по оси ординат(Y): ");
                            float center_y = float.Parse(Console.ReadLine());

                            Console.Write("Радиус окружности: ");
                            float radius = float.Parse(Console.ReadLine());


                            Circle circle = new Circle(center_x, center_y, radius);

                            circles.Add(circle);
                            Console.WriteLine($"Окружность сохранена по индексу {circles.Count - 1}");

                            break;

                        case "2":
                            Console.Write("Индекс окружности: ");
                            int areaInd = int.Parse(Console.ReadLine());

                            if (areaInd < 0 || areaInd >= circles.Count)
                            {
                                throw new Exception("Окружность по данному индексу не существует!");
                            }

                            Console.WriteLine(circles[areaInd].CalculateArea);
                            break;

                        case "3":
                            Console.Write("Индекс окружности: ");
                            int changeCoordsInd = int.Parse(Console.ReadLine());

                            if (changeCoordsInd < 0 || changeCoordsInd >= circles.Count)
                            {
                                throw new Exception("Окружность по данному индексу не существует!");
                            }

                            circles[changeCoordsInd].ChangeCoords();
                            break;

                        case "4":
                            Console.Write("Индекс окружности: ");
                            int changeRadiusInd = int.Parse(Console.ReadLine());

                            if (changeRadiusInd < 0 || changeRadiusInd >= circles.Count)
                            {
                                throw new Exception("Окружность по данному индексу не существует!");
                            }

                            Console.Write("Новый радиус: ");
                            float new_radius = float.Parse(Console.ReadLine());
                            circles[changeRadiusInd].Radius = new_radius;
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Неизвестная команда!");
                            break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}