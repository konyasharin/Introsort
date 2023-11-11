namespace Introsort.BL
{
    internal class Compares
    {
        private static Compares? _instance;

        private Compares(){}

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
