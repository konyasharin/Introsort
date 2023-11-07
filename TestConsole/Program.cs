int[] arr1 = { 2, -3, 3, 10, 57, 23 };

string[] arr = { "123", "eeeew", "abs", "cbd", "a" };
static int KeyString(string str)
{
    return char.ConvertToUtf32(str[0].ToString(), 0);
}

Introsort.BL.Introsort.IntrosortLoop(arr, 0, arr.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(arr.Length), key: KeyString);
for (int i = 0; i < arr.Length; i++)
{
    Console.WriteLine(arr[i]);
}