using System;
using System.Linq;
using Cryptography.Constants;
using Cryptography.Utils.Extensions;
using static Cryptography.Constants.AesParameters;

namespace Cryptography.Utils.KeySchedule
{
    public abstract class AesKeyExpanderBase : IKeyExpander
    {
        protected AesKeyExpanderBase(int baseKeyLength)
        {
            int keyBitsLength = baseKeyLength * 8;
            if (!KEY_LENGTHS.Contains(keyBitsLength))
            {
                throw new ArgumentException("AesKeyExpanderBase: only 128, 192, 256 bits base keys are allowed");
            }

            BaseKeyLength = baseKeyLength;
            switch (keyBitsLength)
            {
                case 128:
                    ExpandedKeyLength =EXPANDED_KEY_LENGTH_128;
                    Movement = MOVEMENT_128;
                    break;
                case 192:
                    ExpandedKeyLength = EXPANDED_KEY_LENGTH_192;
                    Movement = MOVEMENT_192;
                    break;
                case 256:
                    ExpandedKeyLength = EXPANDED_KEY_LENGTH_256;
                    Movement = MOVEMENT_256;
                    break;
            }
        }

        public int BaseKeyLength { get; }
        public int Movement { get; }
        public int ExpandedKeyLength { get; }

        //METHOD TEMPLATE PATTERN
        public byte[] ExpandKey(byte[] baseKey)
        {
            if (baseKey.Length != BaseKeyLength)
            {
                throw new ArgumentException($"AesKeyExpanderBase: only {BaseKeyLength} bits length encryption keys are allowed in this expander");
            }

            int currentIndex = BaseKeyLength;
            int rconIndex = 0;
            byte[] expandedKey = new byte[ExpandedKeyLength];
            Array.Copy(baseKey, 0, expandedKey, 0, BaseKeyLength);

            while (currentIndex < ExpandedKeyLength)
            {
                ScheduleCore(expandedKey, ref currentIndex, ref rconIndex);
                ExpandDependingOnBaseKeyLength(expandedKey, ref currentIndex);
            }

            return expandedKey;
        }

        protected abstract void ExpandDependingOnBaseKeyLength(byte[] keysSequence, ref int currentIndex);

        private void ScheduleCore(byte[] keysSequence, ref int currentIndex, ref int rconIndex)
        {
            //REWRITE LAST COLUMN
            for (int j = 0; j < STATE_ROWS; j++)
            {
                keysSequence[currentIndex + j] = keysSequence[(currentIndex - STATE_ROWS) + j];
            }

            //CIRCULAR LEFT ROTATE
            byte tmp = keysSequence[currentIndex];
            for (int i = 0; i < STATE_ROWS - 1; i++)
            {
                keysSequence[currentIndex + i] = keysSequence[currentIndex + i + 1];
            }
            keysSequence[currentIndex + STATE_ROWS - 1] = tmp;

            //SBOX
            for (int i = 0; i < STATE_ROWS; i++)
            {
                byte value = keysSequence[currentIndex + i];
                keysSequence[currentIndex + i] =
                    TableConstants.RijndaelSBox[value / BYTES_IN_BLOCK, value % BYTES_IN_BLOCK];
            }

            //current column first value XOR corresponding rcon value
            keysSequence[currentIndex] ^= Rcon[rconIndex++];

            //current column  XOR column {movement} bytes back
            int correspondingIndex = currentIndex - Movement;
            for (int i = 0; i < STATE_ROWS; i++)
            {
                keysSequence[currentIndex + i] ^= keysSequence[correspondingIndex + i];
            }

            currentIndex += STATE_ROWS;
        }

        protected void ExpandNColumnsByXor(byte[] keysSequence, ref int currentIndex, int nColumns)
        {
            for (int i = 0; i < nColumns; i++)
            {
                for (int j = 0; j < STATE_ROWS && currentIndex < keysSequence.Length; j++)
                {
                    keysSequence.RewriteAndXor(currentIndex + j, currentIndex + j - 4, currentIndex + j - Movement);
                }
                currentIndex += STATE_ROWS;
            }
        }

        private static readonly byte[] Rcon = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36 };
    }
}