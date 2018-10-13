using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AesAlgorithmTest.TestHelpers
{
    public static class ComparisonHelpers
    {
        public static void AssertMatrixEquality(byte[,] a, byte[,] b)
        {
            Assert.IsTrue(a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1));

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Assert.AreEqual(a[i, j], b[i, j]);
                }
            }
        }

        public static void AssertArrayEquality(byte[] a, byte[] b)
        {
            Assert.IsTrue(a.Length == b.Length);

            for (int i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i], b[i]);
            }
        }
    }
}