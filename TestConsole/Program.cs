using CommandLine;

namespace Console
{
    /// <summary>
    /// создает CLI
    /// </summary>
    public class Flags
    {
        [Option('t', "type", HelpText = "string - сортировка строк, int - сортировка чисел.")]
        public string? Type { get; set; }

        [Option('r', "reversed", HelpText = "Сортировка в обратном порядке.")]
        public bool Reversed { get; set; }

        [Option("command", HelpText = "1 - Сортировка в файле\n" +
                                      "2 - Сортировка введенных пользователем в консоль значений")]
        public string? Command { get; set; }
    }

    /// <summary>
    /// CMD приложение.
    /// </summary>
    internal class Program
    {
        #region Variables

        /// <summary>
        /// Тип сортируемых данных.
        /// </summary>
        private static string? _type;
        /// <summary>
        /// Сортировка в обратном порядке если true.
        /// </summary>
        private static bool _reversed;
        /// <summary>
        /// Флаг активности программы.
        /// </summary>
        private static bool _programIsActive = true;
        /// <summary>
        /// Команда от 1 до 2
        /// </summary>
        private static string? _command;

        #endregion

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main(string[] args)
        {
            // Обработка флагов
            Parser.Default.ParseArguments<Flags>(args)
                .WithParsed(options =>
                {
                    if (options.Type == "string" || options.Type == "int")
                    {
                        _type = options.Type;
                    }
                    else if (options.Type != null)
                    {
                        System.Console.WriteLine(
                            "Ошибка! type может быть string или int! Перезапустите программу с правильным флагами");
                        _programIsActive = false;
                    }
                    else
                    {
                        _type = "int";
                    }

                    _reversed = options.Reversed;

                    if (new[] { "1", "2" }.Contains(options.Command))
                    {
                        _command = options.Command;
                    }
                    else
                    {
                        System.Console.WriteLine("Ошибка! Неверная команда!");
                        _programIsActive = false;
                    }
                });
            while (_programIsActive)
            {
                switch (_command)
                {
                    case "1":
                        if (_type == "string")
                        {
                            SortFile<string>();
                        }
                        else if (_type == "int")
                        {
                            SortFile<int>();
                        }
                        break;
                    case "2":
                        if (_type == "string")
                        {
                            SortInput<string>();
                        }
                        else if(_type == "int")
                        {
                            SortInput<int>();
                        }
                        break;
                    default:
                        System.Console.WriteLine("Вы ввели неверную команду!");
                        _programIsActive = false;
                        break;
                }
            }

        }

        /// <summary>
        /// Сортировка в файле.
        /// </summary>
        /// <typeparam name="T">Тип данных для сортировки.</typeparam>
        private static void SortFile<T>()
        {
            System.Console.WriteLine("\nВведите полный путь до файла .txt (или END): ");
            string filePath = System.Console.ReadLine()!;
            if (filePath == "END")
            {
                _programIsActive = false;
                return;
            }
            if (filePath.Length < 4)
            {
                System.Console.WriteLine("Ошибка! Вы ввели не полный путь до файла!");
                _programIsActive = false;
                return;
            }
            if (filePath.Substring(filePath.Length - 4, 4) != ".txt")
            {
                System.Console.WriteLine("Ошибка! Вы открываете не .txt файл!");
                _programIsActive = false;
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                T[] arr = new T[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        arr[i] = (T)Convert.ChangeType(lines[i], typeof(T));
                    }
                    catch
                    {
                        System.Console.WriteLine("Ошибка! Неверный тип данных в файле");
                        _programIsActive = false;
                        return;
                    }
                }
                Introsort.BL.Introsort.IntrosortLoop(arr, 0, lines.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(lines.Length), reverse: _reversed);
                string[] updatedLinesArray = new string[lines.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    updatedLinesArray[i] = Convert.ToString(arr[i])!;
                } 
                File.WriteAllLines(filePath, updatedLinesArray);
                System.Console.WriteLine("Файл успешно отсортирован!");
            }
            catch
            {
                System.Console.WriteLine("Ошибка при открытии файла или неверно указан тип данных!");
                _programIsActive = false;
            }
        }

        /// <summary>
        /// Сортировка в командной строке.
        /// </summary>
        /// <typeparam name="T">Тип данных для сортировки.</typeparam>
        private static void SortInput<T>()
        {
            System.Console.WriteLine("Введите количество элементов для сортировки(или END): ");
            string currentCommand = System.Console.ReadLine()!;
            if (currentCommand == "END")
            {
                _programIsActive = false;
                return;
            }

            int countElements;
            try
            {
                countElements = int.Parse(currentCommand);
            }
            catch
            {
                System.Console.WriteLine("Ошибка! Неверный тип данных");
                _programIsActive = false;
                return;
            }

            if (countElements <= 0)
            {
                System.Console.WriteLine("Ошибка! Число элементов должно быть больше 0");
                _programIsActive = false;
                return;
            }
            T[] arr = new T[countElements];
            for (int i = 0; i < countElements; i++)
            {
                System.Console.Write($"Введите элемент #{i + 1}: ");
                try
                {
                    arr[i] = (T)Convert.ChangeType(System.Console.ReadLine()!, typeof(T));
                }
                catch
                {
                    System.Console.WriteLine("Ошибка! Неверный тип данных");
                    _programIsActive = false;
                    return;
                }
            }
            Introsort.BL.Introsort.IntrosortLoop(arr, 0, countElements - 1, Introsort.BL.Introsort.CalculateDepthRecursion(countElements), reverse: _reversed);
            System.Console.Write("Отсортированный список элементов: ");
            for (int i = 0; i < countElements; i++)
            {
                if (i != countElements - 1)
                {
                    System.Console.Write($"{arr[i]}, ");
                }
                else
                {
                    System.Console.Write($"{arr[i]}\n");
                }
                
            }
        }
    }
}