namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
       
        [DataTestMethod]
        #region Tests 1
        [DataRow(new int[] { })]
        [DataRow(new[] { 1 })]
        [DataRow(new[] { 1, 2, 3, 4, 5 })]
        [DataRow(new[] { 0, 0, 0, 55, 55, 60 })]
        [DataRow(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 })]
        [DataRow(new[] { 8, 0, 42, 3, 4, 8, 0, 45, 50, 9999, 7 })]
        [DataRow(new[] { -5, 0, 9, -999, 874, 35, -4, -5, 0 })]
        [DataRow(new[] { 1, 1, 1 })]
        #endregion
        public void TestSortNumbersIncrease(int[] array)
        {
            int[] keepArray = new int[array.Length];
            array.CopyTo(keepArray, 0);
            Array.Sort(keepArray);
            Introsort.BL.Introsort.IntrosortLoop(array, 0, array.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(array.Length));
            for (var i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(keepArray[i], array[i]);
            }
        }

        [DataTestMethod]
        #region Tests 2
        [DataRow(new int[] { })]
        [DataRow(new[] { 1 })]
        [DataRow(new[] { 1, 2, 3, 4, 5 })]
        [DataRow(new[] { 0, 0, 0, 55, 55, 60 })]
        [DataRow(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 })]
        [DataRow(new[] { 8, 0, 42, 3, 4, 8, 0, 45, 50, 9999, 7 })]
        [DataRow(new[] { -5, 0, 9, -999, 874, 35, -4, -5, 0 })]
        [DataRow(new[] { 1, 1, 1 })]
        #endregion
        public void TestSortNumbersDecrease(int[] array)
        {
            int[] keepArray = new int[array.Length];
            array.CopyTo(keepArray, 0);
            Array.Sort(keepArray);
            Array.Reverse(keepArray);
            Introsort.BL.Introsort.IntrosortLoop(array, 0, array.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(array.Length), true);
            for (var i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(keepArray[i], array[i]);
            }
        }

        [DataTestMethod]
        #region Tests 3
        [DataRow(new string[] { })]
        [DataRow(new[] { "a" })]
        [DataRow(new[] { "a", "b", "c", "d", "e" })]
        [DataRow(new[] { "aa", "aa", "aa", "ab", "ac", "b" })]
        [DataRow(new[] { "e", "d", "c", "b", "a" })]
        [DataRow(new[] { "abc", "a", "foo", "bar", "booz", "baz", "spam", "love" })]
        [DataRow(new[] { "abc", "abc", "abc" })]
        [DataRow(new[] { "" })]
        #endregion
        public void TestSortStringsIncrease(string[] array)
        {
            string[] keepArray = new string[array.Length];
            array.CopyTo(keepArray, 0);
            Array.Sort(keepArray);
            Introsort.BL.Introsort.IntrosortLoop(array, 0, array.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(array.Length));
            for (var i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(keepArray[i], array[i]);
            }
        }

        [DataTestMethod]
        #region Tests 4
        [DataRow(new string[] { })]
        [DataRow(new[] { "a" })]
        [DataRow(new[] { "a", "b", "c", "d", "e" })]
        [DataRow(new[] { "aa", "aa", "aa", "ab", "ac", "b" })]
        [DataRow(new[] { "e", "d", "c", "b", "a" })]
        [DataRow(new[] { "abc", "a", "foo", "bar", "booz", "baz", "spam", "love" })]
        [DataRow(new[] { "abc", "abc", "abc" })]
        [DataRow(new[] { "" })]
        #endregion
        public void TestSortStringsDecrease(string[] array)
        {
            string[] keepArray = new string[array.Length];
            array.CopyTo(keepArray, 0);
            Array.Sort(keepArray);
            Array.Reverse(keepArray);
            Introsort.BL.Introsort.IntrosortLoop(array, 0, array.Length - 1, Introsort.BL.Introsort.CalculateDepthRecursion(array.Length), reverse: true);
            for (var i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(keepArray[i], array[i]);
            }
        }
    }
}