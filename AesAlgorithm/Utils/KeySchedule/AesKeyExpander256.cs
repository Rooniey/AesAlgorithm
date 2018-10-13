using Cryptography.Constants;
using static Cryptography.Constants.AesParameters;

namespace Cryptography.Utils.KeySchedule
{
    public class AesKeyExpander256 : AesKeyExpanderBase
    {
        public AesKeyExpander256(int baseKeyLength) : base(baseKeyLength)
        {
        }

        protected override void ExpandDependingOnBaseKeyLength(byte[] keysSequence, ref int currentIndex)
        {
            ExpandNColumnsByXor(keysSequence, ref currentIndex, 3);

            for (int j = 0; j < STATE_ROWS && currentIndex < keysSequence.Length; j++)
            {
                byte value = keysSequence[currentIndex + j - 4];
                keysSequence[currentIndex + j] = TableConstants.RijndaelSBox[value / 16, value % 16];
                keysSequence[currentIndex + j] ^= keysSequence[currentIndex + j - Movement];
            }
            currentIndex += STATE_ROWS;

            ExpandNColumnsByXor(keysSequence, ref currentIndex, 3);
        }
    }
}