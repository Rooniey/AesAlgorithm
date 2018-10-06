using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AesAlgorithmTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Multiplication()
        {
            AesAlgorithm.AesAlgorithm aes = new AesAlgorithm.AesAlgorithm(new byte[4, 4]);

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

            AesAlgorithm.AesAlgorithm aes = new AesAlgorithm.AesAlgorithm(new byte[4, 4]);

            aes.AddRoundKey(new byte[,]
            {
                {0x04, 0xe0, 0x48, 0x28},
                {0x66, 0xcb, 0xf8, 0x06},
                {0x81, 0x19, 0xd3, 0x26},
                {0xe5, 0x9a, 0x7a, 0x4c}
            }, 2);
        }
    }
}