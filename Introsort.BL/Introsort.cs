namespace Introsort.BL
{
    /// <summary>
    /// Класс, содержащий все методы, для работы интроспективной сортировки
    /// </summary>
    public class Introsort
    {
        /// <summary>
        /// Число элементов, необходимое для QuickSort.
        /// </summary>
        private static readonly int MaxCountNumbers = 16;

        /// <summary>
        /// Вычисляет глубину рекурсии для заданного числа элементов.
        /// </summary>
        /// <param name="n">Число элементов в массиве.</param>
        /// <returns>Глубина рекурсии.</returns>
        public static int CalculateDepthRecursion(int n)
        {
            return 2*((int)MathF.Log2(n));
        }

        /// <summary>
        /// Сортировка Introsort.
        /// </summary>
        /// <param name="a">Массив для сортировки.</param>
        /// <param name="start">Начальный индекс для сортировки.</param>
        /// <param name="end">Конечный индекс для сортировки.</param>
        /// <param name="depthLimit">Глубина рекурсии.</param>
        /// <param name="reverse">Параметр, определяющий как будет осуществляться сортировка(по неубыванию или невозрастанию).</param>
        /// <param name="key">Функция для вычисления ключа, по которому происходит сортировка.</param>
        /// <param name="cmp">Функция для сравнения двух значений при сортировке</param>
        public static void IntrosortLoop<T>(T[] a, int start, int end, int depthLimit, bool reverse = false, Func<T, dynamic>? key = null, Comparison<T>? cmp = null)
        {
            if (cmp == null)
            {
                cmp = Compares.Instance.Compare;
            }

            if (key == null)
            {
                key = Keys.Instance.GetKey;
            }

            if (end - start > MaxCountNumbers)
            {
                if(depthLimit == 0)
                {
                    HeapSort(a, start, end, reverse: reverse, cmp: cmp);
                    return; 
                }
                depthLimit--;
                int pivot = Partition(a, start, end, reverse: reverse, cmp: cmp);
                IntrosortLoop(a, 0, pivot - 1, depthLimit, reverse: reverse, key: key, cmp: cmp);
                IntrosortLoop(a, pivot + 1, end, depthLimit, reverse: reverse, key: key, cmp: cmp);
            }
            else
            {
                InsertionSort(a, start, end, reverse: reverse, cmp: cmp);
            }
        }

        /// <summary>
        /// Разделение массива, по опорной точке pivot
        /// (один из шагов QuickSort).
        /// </summary>
        /// <param name="a">Массив для сортировки.</param>
        /// <param name="start">Начальный индекс для сортировки.</param>
        /// <param name="end">Конечный индекс для сортировки.</param>
        /// <param name="reverse">Параметр, определяющий как будет осуществляться сортировка(по неубыванию или невозрастанию).</param>
        /// <param name="cmp">Функция для сравнения двух значений при сортировке</param>
        /// <returns>Индекс опорного элемента pivot.</returns>
        public static int Partition<T>(T[] a, int start, int end, bool reverse = false, Comparison<T>? cmp = null)
        {
            T pivot = a[end];
            int i = start - 1;

            for(int j = start; j <= end - 1; j++)
            {
                if (cmp!(a[j], pivot) < 0 && reverse is false || cmp(a[j], pivot) >= 0 && reverse)
                {
                    i++;
                    T keepVariable = a[i];
                    a[i] = a[j];
                    a[j] = keepVariable;
                }
                if(j == end - 1)
                {
                    i++;
                    T keepVariable = a[i];
                    a[i] = a[end];
                    a[end] = keepVariable;
                }
            }
            return i;
        }

        /// <summary>
        /// HeapSort (Пирамидальная сортировка).
        /// </summary>
        /// <param name="a">Массив для сортировки.</param>
        /// <param name="start">Индекс элемента, с которого начинаем сортировку.</param>
        /// <param name="end">Индекс элемента, которым заканчивается сортировка.</param>
        /// <param name="reverse">Параметр, определяющий как будет осуществляться сортировка(по неубыванию или невозрастанию).</param>
        /// <param name="cmp">Функция для сравнения двух значений при сортировке.</param>
        public static void HeapSort<T>(T[] a, int start, int end, bool reverse = false, Comparison<T>? cmp = null)
        {
            int n = end - start + 1;
            T[] newArray = new T[n];
            int j = 0;
            for (int i = start; i <= end; i++)
            {
                newArray[j] = a[i];
                j++;
            }

            // Создание дерева
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(newArray, i, n, reverse: reverse, cmp: cmp);
            }

            // Сортировка массива
            for (int i = n - 1; i >= 0; i--)
            {
                T keepElement = newArray[i];
                newArray[i] = newArray[0];
                newArray[0] = keepElement;
                Heapify(newArray, 0, i, reverse: reverse, cmp: cmp);
            }

            // В старый массив вносим изменения
            j = 0;
            for (int i = start; i <= end; i++)
            {
                a[i] = newArray[j];
                j++;
            }
        }

        /// <summary>
        /// Метод для создания кучи с наибольшим элементом вверху дерева.
        /// </summary>
        /// <param name="a">Массив для которого создается дерево.</param>
        /// <param name="i">Текущая вершина дерева.</param>
        /// <param name="n">Размер массива(дерева).</param>
        /// <param name="reverse">Параметр, определяющий как будет осуществляться сортировка(по неубыванию или невозрастанию).</param>
        /// <param name="cmp">Функция для сравнения двух значений при сортировке.</param>
        private static void Heapify<T>(T[] a, int i, int n, bool reverse = false, Comparison<T>? cmp = null)
        {
            int leftElementIndex = i * 2 + 1;
            int rightElementIndex = i * 2 + 2;
            int largestIndex = i;
            if ((leftElementIndex < n && cmp!(a[leftElementIndex], a[largestIndex]) > 0 && reverse is false) ||
                (leftElementIndex < n && cmp!(a[leftElementIndex], a[largestIndex]) < 0 && reverse))
            {
                largestIndex = leftElementIndex;
            }

            if((rightElementIndex < n && cmp!(a[rightElementIndex], a[largestIndex]) > 0 && reverse is false) ||
                (rightElementIndex < n && cmp!(a[rightElementIndex], a[largestIndex]) < 0 && reverse))
            {
                largestIndex = rightElementIndex;
            }

            if(i != largestIndex)
            {
                T keepElement = a[i];
                a[i] = a[largestIndex];
                a[largestIndex] = keepElement;

                Heapify(a, largestIndex, n, reverse: reverse, cmp: cmp);
            }
        }

        /// <summary>
        /// Сортировка вставками.
        /// </summary>
        /// <param name="a">Массив для сортировки.</param>
        /// <param name="start">Индекс элемента, с которого начинается сортировка.</param>
        /// <param name="end">Индекс элемента, которым заканчивается сортировка.</param>
        /// <param name="reverse">Параметр, определяющий как будет осуществляться сортировка(по неубыванию или невозрастанию).</param>
        /// <param name="cmp">Функция для сравнения двух значений при сортировке.</param>
        public static void InsertionSort<T>(T[] a, int start, int end, bool reverse = false, Comparison<T>? cmp = null)
        {
            for(int i = start + 1; i <= end; i++)
            {
                T currentElement = a[i];
                int j = i;
                while ((j > 0 && cmp!(a[j - 1], currentElement) > 0 && reverse is false) ||
                    (j > 0 && cmp!(a[j - 1], currentElement) < 0 && reverse)) 
                {
                    a[j] = a[j - 1];
                    j--;
                }
                a[j] = currentElement;
            }
        }
    }
}