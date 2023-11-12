
namespace Introsort.BL
{
    internal class Keys
    {
        /// <summary>
        /// Переменная для хранения единственного экземпляра данного класса.
        /// </summary>
        private static Keys? _instance;

        /// <summary>
        /// Конструктор.
        /// </summary>
        private Keys()
        {
        }

        /// <summary>
        /// Свойство, содержащее метод get для получения экземпляра класса.
        /// </summary>
        public static Keys Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Keys();
                }

                return _instance;
            }
        }

        public dynamic GetKey<T>(T x)
        {
            return x!;
        }
    }
}
