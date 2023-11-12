namespace Introsort.BL
{
    /// <summary>
    /// Класс, содержащий функционал для сравнения двух значений различных типов данных. (строки и числа)
    /// </summary>
    internal class Compares
    {
        /// <summary>
        /// Переменная для хранения единственного экземпляра данного класса.
        /// </summary>
        private static Compares? _instance;

        /// <summary>
        /// Конструктор.
        /// </summary>
        private Compares(){}

        /// <summary>
        /// Свойство, содержащее метод get для получения экземпляра класса.
        /// </summary>
        public static Compares Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Compares();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Метод, содержащий функционал сравнения двух параметров.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="x">Первое значение для сравнения.</param>
        /// <param name="y">Второе значение для сравнения.</param>
        /// <returns>1 если x больше y, 0 если x равно y, -1 если x меньше y.</returns>
        public int Compare<T>(T x, T y)
        {
            if (x is int && y is int)
            {
                return Comparer<T>.Default.Compare(x, y);
            }
            else if (x is string && y is string)
            {
                return Comparer<T>.Default.Compare(x, y);
            }

            return 0;
        }
    }
}
