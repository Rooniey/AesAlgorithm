using AesAlgorithm.Utils;
using System.Collections.Generic;

namespace AesAlgorithm
{
    public class AesAlgorithm
    {
        public static readonly int STATE_ROWS = 4;
        public static readonly int STATE_COLUMNS = 4;
        public static readonly int[] KEY_SIZES = new int[] { 128, 192, 256 };

        public AesAlgorithm(byte[,] key)
        {
            keys.Add(key);
            switch (keys.Count)
            {
                case 16:
                    numberOfRounds = 10;
                    break;

                case 24:
                    numberOfRounds = 12;
                    break;

                case 32:
                    numberOfRounds = 14;
                    break;
            }
            GenerateKeys();
        }

        public List<byte[]> Encrypt(List<byte[,]> data)
        {
            return null;
        }

        public List<byte[]> Decrypt(List<byte[,]> encryptedData)
        {
            return null;
        }

        private void GenerateKeys()
        {
        }

        public void MixColumns(byte[,] state)
        {
            for (int i = 0; i < STATE_COLUMNS; i++)
            {
                byte[,] column = state.GetColumn(i);
                byte[,] newColumn = TableConstants.GALOIS_MATRIX.Multiply(column);
                state.SetColumn(newColumn, i);
            }
        }

        public void AddRoundKey(byte[,] state, int round)
        {
            byte[,] roundKey = keys[round];

            for (int row = 0; row < STATE_ROWS; row++)
            {
                for (int column = 0; column < STATE_COLUMNS; column++)
                {
                    //XOR on corresponding bytes in state and roundKey matrix
                    state[row, column] = (byte)(state[row, column] ^ roundKey[row, column]);
                }
            }
        }

        private int numberOfRounds;
        private List<byte[,]> keys = new List<byte[,]>();
    }
}