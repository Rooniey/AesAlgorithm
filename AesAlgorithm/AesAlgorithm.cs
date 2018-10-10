using System.Collections.Generic;

namespace AesAlgorithm
{
    public class AesAlgorithm
    {
        public static readonly int STATE_ROWS = 4;
        public static readonly int STATE_COLUMNS = 4;

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

        private int GetLeftPartIndex(byte a)
        {
            return a >> 4;
        }

        private int GetRightPartIndex(byte a)
        {
            return (0x0f & a);
        }

        public void SubstituteBytes(byte[,] a)
        {
            for(int i = 0; i<STATE_ROWS; i++)
            {
                for (int j = 0; j < STATE_COLUMNS; j++)
                {
                    a[i, j] = TableConstants.RijndaelSBox[GetRightPartIndex(a[i, j]), GetLeftPartIndex(a[i, j])];
                }
            }
        }

        public byte[,] ShiftRows(byte[,] a)
        {
            byte[,] b = new byte[STATE_ROWS, STATE_COLUMNS];

            b = a;
            byte temp1 = a[1, 0];
            for (int i = 1; i < STATE_COLUMNS; i++)
            {
                b[1, i - 1] = b[1, i];
            }
            b[1, 3] = temp1;
            temp1 = a[2, 0];
            byte temp2 = a[2, 1];
            for (int i = 2; i < STATE_COLUMNS; i++)
            {
                b[2, i - 2] = b[2, i];
            }
            b[2, 2] = temp1;
            b[2, 3] = temp2;

            temp1 = a[3, 0];
            temp2 = a[3, 1];
            byte temp3 = a[3, 2];
            b[3, 0] = b[3, 3];
            b[3, 1] = temp1;
            b[3, 2] = temp2;
            b[3, 3] = temp3;
            return b;
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