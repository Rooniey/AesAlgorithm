using AesAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AesAlgorithm.Data.Processors;

namespace AesAlgorithmTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Multiplication()
        {
            AesAlgorithm.AesAlgorithmImp aes = new AesAlgorithm.AesAlgorithmImp(new byte[4, 4]);

            aes.MixColumns(new byte[,]
            {
                {212, 224, 184, 30},
                {191, 180, 65, 39},
                {93, 82, 17, 152},
                {48, 174, 241, 229}
            });
        }

        [TestMethod]
        public void AddRoundKey()
        {
            //special key to compare the output, set key in method implementation
            //            byte[,] roundKey = new byte[,]
            //            {
            //                {0xa0, 0x88, 0x23, 0x2a},
            //                {0xfa, 0x54, 0xa3, 0x6c},
            //                {0xfe, 0x2c, 0x39, 0x76},
            //                {0x17, 0xb1, 0x39, 0x05}
            //            };

            AesAlgorithm.AesAlgorithmImp aes = new AesAlgorithm.AesAlgorithmImp(new byte[4, 4]);

            aes.AddRoundKey(new byte[,]
            {
                {0x04, 0xe0, 0x48, 0x28},
                {0x66, 0xcb, 0xf8, 0x06},
                {0x81, 0x19, 0xd3, 0x26},
                {0xe5, 0x9a, 0x7a, 0x4c}
            }, 2);
        }

        [TestMethod]
        public void ConvertToAesBlock()
        {
            AesDataProcessor processor = new AesDataProcessor();

            processor.ConvertToBlocks(new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9
                }
            );
        }

        [TestMethod]
        public void generateKeyswithKeyGen()
        {
            List<byte[,]> keys = new List<byte[,]>();
            keys.Add(new byte[4,4] {
                { 0xaa, 0xbb, 0xcc, 0xdd },
                { 0x0a, 0x0b, 0x0c, 0x0d },
                { 0xa0, 0xb0, 0xc0, 0xd0 },
                { 0x01, 0x02, 0x03, 0x04 },
            });

            keys = KeyGen.GenerateKeys(keys, 10);
            System.Console.WriteLine(keys);
        }
    }
}