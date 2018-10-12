using Cryptography.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AesAlgorithmTest
{
    [TestClass]
    public class KeyScheduleTest
    {
        [TestMethod]
        public void CheckKeyGeneration()
        {
            List<byte[,]> keys = KeyGen2.GenerateKeys(new byte[4, 4]);
            System.Console.WriteLine(keys);
        }
    }
}