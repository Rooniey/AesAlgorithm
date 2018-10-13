using System.Collections.Generic;
using Cryptography.Data.Processors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AesAlgorithmTest.TestHelpers.ComparisonHelpers;

namespace AesAlgorithmTest
{
    [TestClass]
    public class AesDataProcessorTest
    {
        private BlockDataProcessor _blockProcessor;
        private static readonly byte[] _flatData = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        private static readonly byte[] _paddedFlatData = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        private static readonly List<byte[,]> _blocks = new List<byte[,]>
        {
            new byte[,] {
                {1, 5, 9,  13},
                {2, 6, 10, 14},
                {3, 7, 11, 15},
                {4, 8, 12, 16}
            },
            new byte[,]
            {
                {17, 0, 0, 0},
                {0,  0, 0, 0},
                {0,  0, 0, 0},
                {0,  0, 0, 0}
            }
        };

        [TestInitialize]
        public void SetUp()
        {
            _blockProcessor = new BlockDataProcessor();
        }

        [TestMethod]
        public void ConvertToBlocks_WhenCalledWithNotEmptyData_ShouldReturnListOfColumnMajorBlocks()
        {
            List<byte[,]> result = _blockProcessor.ConvertTo(_flatData);
            Assert.IsTrue(result.Count == _blocks.Count);
            for (int i = 0; i < result.Count; i++)
            {
                AssertMatrixEquality(result[i], _blocks[i]);
            }
        }
            
        [TestMethod]
        public void ConvertToByteArray_WhenCalledWithNotEmptyData_ShouldReturnFlattedData()
        {
            byte[] result = _blockProcessor.ConvertBack(_blocks);
            AssertArrayEquality(result, _paddedFlatData);
        }

    }
}
