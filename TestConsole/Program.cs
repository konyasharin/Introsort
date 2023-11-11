int[] arr1 = { 2, -3, 3, 10, 57, 23, 20 };

string[] arr = { "123", "eeeew", "abs", "cbd", "a" };



Introsort.BL.Introsort.IntrosortLoop<int>(arr1, 0, arr1.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(arr.Length), key: null);
for (int i = 0; i < arr1.Length; i++)
{
    Console.WriteLine(arr1[i]);
}