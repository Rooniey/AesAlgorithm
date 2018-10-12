using AesAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static AesAlgorithmTest.TestHelpers.ComparisonHelpers;

namespace AesAlgorithmTest
{
    [TestClass]
    public class TransformationsTest
    {
        private AesAlgorithmImp _aesAlgorithmImp;

        [TestInitialize]
        public void SetUp()
        {
            _aesAlgorithmImp = new AesAlgorithmImp(new byte[16]);
        }

        [TestMethod]
        public void MixColumns_WhenCalled_ShouldCorrectlyModifyState()   
        {
            byte[,] state = new byte[,]
            {
                {0xd4, 0xe0, 0xb8, 0x1e},
                {0xbf, 0xb4, 0x41, 0x27},
                {0x5d, 0x52, 0x11, 0x98},
                {0x30, 0xae, 0xf1, 0xe5}
            };
            byte[,] expectedStateAfterTransformation = new byte[,]
            {
                {0x04, 0xe0, 0x48, 0x28},
                {0x66, 0xcb, 0xf8, 0x06},
                {0x81, 0x19, 0xd3, 0x26},
                {0xe5, 0x9a, 0x7a, 0x4c}
            };
            _aesAlgorithmImp.MixColumns(state);
            AssertMatrixEquality(state, expectedStateAfterTransformation);
        }

        [TestMethod]
        public void AddRoundKey_WhenCalled_ShouldCorrectlyModifyState()
        {
            byte[,] state = new byte[,]
            {
                {0x04, 0xe0, 0x48, 0x28},
                {0x66, 0xcb, 0xf8, 0x06},
                {0x81, 0x19, 0xd3, 0x26},
                {0xe5, 0x9a, 0x7a, 0x4c}
            };
            //INVERTED ROWS WITH COLUMNS
            byte[,] roundKey = new byte[,]
            {
                {0xa0, 0xfa, 0xfe, 0x17},
                {0x88, 0x54, 0x2c, 0xb1},
                {0x23, 0xa3, 0x39, 0x39},
                {0x2a, 0x6c, 0x76, 0x05}
            };
            byte[,] expectedStateAfterTransformation = new byte[,]
            {
                {0xa4, 0x68, 0x6b, 0x02},
                {0x9c, 0x9f, 0x5b, 0x6a},
                {0x7f, 0x35, 0xea, 0x50},
                {0xf2, 0x2b, 0x43, 0x49}
            };
            _aesAlgorithmImp.AddRoundKey(state, roundKey);
            AssertMatrixEquality(state, expectedStateAfterTransformation);
        }

        [TestMethod]
        public void SubstituteBytes_WhenCalled_ShouldCorrectlyModifyState()
        {
            byte[,] state = new byte[,]
            {
                {0x19, 0xa0, 0x9a, 0xe9},
                {0x3d, 0xf4, 0xc6, 0xf8},
                {0xe3, 0xe2, 0x8d, 0x48},
                {0xbe, 0x2b, 0x2a, 0x08}
            };
            byte[,] expectedStateAfterTransformation = new byte[,]
            {
                {0xd4, 0xe0, 0xb8, 0x1e},
                {0x27, 0xbf, 0xb4, 0x41},
                {0x11, 0x98, 0x5d, 0x52},
                {0xae, 0xf1, 0xe5, 0x30}
            };
            _aesAlgorithmImp.SubstituteBytes(state);
            AssertMatrixEquality(state, expectedStateAfterTransformation);
        }

        [TestMethod]
        public void ShiftRows_WhenCalled_ShouldCorrectlyModifyState()
        {
            byte[,] state = new byte[,]
            {
                {0xd4, 0xe0, 0xb8, 0x1e},
                {0x27, 0xbf, 0xb4, 0x41},
                {0x11, 0x98, 0x5d, 0x52},
                {0xae, 0xf1, 0xe5, 0x30}
            };
            byte[,] expectedStateAfterTransformation = new byte[,]
            {
                {0xd4, 0xe0, 0xb8, 0x1e},
                {0xbf, 0xb4, 0x41, 0x27},
                {0x5d, 0x52, 0x11, 0x98},
                {0x30, 0xae, 0xf1, 0xe5}
            };
            _aesAlgorithmImp.ShiftRows(state);
            AssertMatrixEquality(state, expectedStateAfterTransformation);
        }
    }
}
