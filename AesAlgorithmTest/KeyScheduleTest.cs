using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Cryptography.Utils.KeySchedule;
using static AesAlgorithmTest.TestHelpers.ComparisonHelpers;

namespace AesAlgorithmTest
{
    [TestClass]
    public class KeyScheduleTest
    {
        private IKeyExpander _expander;
        
        [TestMethod]
        public void Check128BitKeyExpansion()
        {
            _expander = new AesKeyExpander128And192(16);
            byte[] keySequence = _expander.ExpandKey(new byte[16]);
            byte lastByte = keySequence[keySequence.Length - 1];
            byte expectedLastByte = 0x8e;
            Assert.AreEqual(lastByte, expectedLastByte);
        }

        [TestMethod]
        public void Check192BitKeyExpansion()
        {
            _expander = new AesKeyExpander128And192(24);
            byte[] keySequence = _expander.ExpandKey(new byte[24]);
            byte lastByte = keySequence[keySequence.Length - 1];
            byte expectedLastByte = 0x70;
            Assert.AreEqual(lastByte, expectedLastByte);
        }

        [TestMethod]
        public void Check256BitKeyExpansion()
        {
            _expander = new AesKeyExpander256(32);
            byte[] keySequence = _expander.ExpandKey(new byte[32]);
            byte lastByte = keySequence[keySequence.Length - 1];
            byte expectedLastByte = 0x85;
            Assert.AreEqual(lastByte, expectedLastByte);
        }
    }
}