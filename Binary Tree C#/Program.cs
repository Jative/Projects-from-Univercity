namespace Lab8_14
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree t = new Tree();

            Console.WriteLine("Создан пустой корневой элемент\n");
            Console.WriteLine("Команды:");
            Console.WriteLine("1. Добавить элемент");
            Console.WriteLine("2. Редактировать элемент");
            Console.WriteLine("3. Удалить элемент");
            Console.WriteLine("4. Сумма чисел, кратных 11");
            Console.WriteLine("5. Вывести дерево");
            Console.WriteLine("0. Завершить программу");

            bool running = true;
            while(running)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                try
                {
                    Console.Write("Команда: ");
                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1":
                            Console.Write("Значение добавляемого элемента: ");
                            string strVal = Console.ReadLine();
                            try
                            {
                                int.Parse(strVal);
                            } catch { throw new Exception("Для добавления доступны только целые числа!"); }
                            int val = int.Parse(strVal);
                            strVal = val.ToString();

                            t.Insert(strVal);
                            Console.WriteLine("Элемент добавлен успешно");
                            break;

                        case "2":
                            Console.Write("Старое значение: ");
                            string oldVal = Console.ReadLine();

                            Console.Write("Новое значение: ");
                            string newVal = Console.ReadLine();
                            try
                            {
                                int.Parse(newVal);
                            }
                            catch { throw new Exception("Элементом дерева может быть только число!"); }
                            int value = int.Parse(newVal);
                            strVal = value.ToString();

                            t.SetValue(oldVal, newVal);
                            Console.WriteLine("Значение изменено успешно");
                            break;

                        case "3":
                            Console.Write("Значение удаляемого элемента: ");
                            string delVal = Console.ReadLine();

                            t.Remove(delVal);
                            Console.WriteLine("Элемент удалён успешно");
                            break;

                        case "4":
                            int sum = 0, count = 0;

                            string[] strVals = t.Display(t).TrimEnd().Split(' ');
                            foreach (string strval in strVals)
                            {
                                try
                                {
                                    int intVal = int.Parse(strval);
                                    if (intVal % 11 == 0)
                                    {
                                        sum += intVal;
                                        count++;
                                    }
                                } catch { }
                            }

                            if (count == 0)
                            {
                                Console.WriteLine("В дереве нет чисел, кратных 11!");
                            }
                            else
                            {
                                Console.WriteLine($"Сумма: {sum}");
                            }
                            break;

                        case "5":
                            Console.WriteLine(t.Draw(t));
                            break;

                        case "0":
                            running = false;
                            Console.WriteLine("Программа завершена");
                            break;

                        default:
                            Console.WriteLine("Неизвестная команда!");
                            break;
                    }
                } catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}