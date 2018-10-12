using Cryptography.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static AesAlgorithmTest.TestHelpers.ComparisonHelpers;

namespace AesAlgorithmTest
{
    [TestClass]
    public class KeyScheduleTest
    {
        [TestMethod]
        public void CheckKeyGeneration()
        {
            List<byte[,]> keys = KeyGen2.GenerateKeys(new byte[4, 4]);
            byte[,] expectedLastKey = new byte[,]
            {
                {0xb4, 0x3e, 0x23, 0x6f}, 
                {0xef, 0x92, 0xe9, 0x8f},
                {0x5b, 0xe2, 0x51, 0x18},
                {0xcb, 0x11, 0xcf, 0x8e}
            };
            AssertMatrixEquality(keys[keys.Count-1], expectedLastKey);
        }
    }
}