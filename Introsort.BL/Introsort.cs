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
        public static void IntrosortLoop(int[] a, int start, int end, int depthLimit, bool reverse = false, Delegate? key = null)
        {
            if (end - start > MaxCountNumbers)
            {
                if(depthLimit == 0)
                {
                    HeapSort(a, start, end, reverse);
                    return; 
                }
                depthLimit--;
                int pivot = Partition(a, start, end, reverse);
                IntrosortLoop(a, 0, pivot - 1, depthLimit, reverse);
                IntrosortLoop(a, pivot + 1, end, depthLimit, reverse);
            }
            else
            {
                InsertionSort(a, start, end, reverse);
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
        /// <returns>Индекс опорного элемента pivot.</returns>
        public static int Partition(int[] a, int start, int end, bool reverse = false)
        {
            int pivot = a[end];
            int i = start - 1;

            for(int j = start; j <= end - 1; j++)
            {
                if (a[j] < pivot && reverse is false || a[j] >= pivot && reverse)
                {
                    i++;
                    int keepVariable = a[i];
                    a[i] = a[j];
                    a[j] = keepVariable;
                }
                if(j == end - 1)
                {
                    i++;
                    int keepVariable = a[i];
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
        public static void HeapSort(int[] a, int start, int end, bool reverse = false)
        {
            int n = end - start + 1;
            int[] newArray = new int[n];
            int j = 0;
            for (int i = start; i <= end; i++)
            {
                newArray[j] = a[i];
                j++;
            }

            // Создание дерева
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(newArray, i, n, reverse);
            }

            // Сортировка массива
            for (int i = n - 1; i >= 0; i--)
            {
                int keepElement = newArray[i];
                newArray[i] = newArray[0];
                newArray[0] = keepElement;
                Heapify(newArray, 0, i, reverse);
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
        private static void Heapify(int[] a, int i, int n, bool reverse = false)
        {
            int leftElementIndex = i * 2 + 1;
            int rightElementIndex = i * 2 + 2;
            int largestIndex = i;

            if((leftElementIndex < n && a[leftElementIndex] > a[largestIndex] && reverse is false) ||
                (leftElementIndex < n && a[leftElementIndex] < a[largestIndex] && reverse))
            {
                largestIndex = leftElementIndex;
            }

            if((rightElementIndex < n && a[rightElementIndex] > a[largestIndex] && reverse is false) ||
                (rightElementIndex < n && a[rightElementIndex] < a[largestIndex] && reverse))
            {
                largestIndex = rightElementIndex;
            }

            if(i != largestIndex)
            {
                int keepElement = a[i];
                a[i] = a[largestIndex];
                a[largestIndex] = keepElement;

                Heapify(a, largestIndex, n, reverse);
            }
        }

        /// <summary>
        /// Сортировка вставками.
        /// </summary>
        /// <param name="a">Массив для сортировки.</param>
        /// <param name="start">Индекс элемента, с которого начинается сортировка.</param>
        /// <param name="end">Индекс элемента, которым заканчивается сортировка.</param>
        /// <param name="reverse">Параметр, определяющий как будет осуществляться сортировка(по неубыванию или невозрастанию).</param>
        public static void InsertionSort(int[] a, int start, int end, bool reverse = false)
        {
            for(int i = start + 1; i <= end; i++)
            {
                int currentElement = a[i];
                int j = i;
                while ((j > 0 && a[j - 1] > currentElement && reverse is false) ||
                    (j > 0 && a[j - 1] < currentElement && reverse)) 
                {
                    a[j] = a[j - 1];
                    j--;
                }
                a[j] = currentElement;
            }
        }
    }
}