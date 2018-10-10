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
                    a[i, j] = TableConstants.RijndaelSBox[GetLeftPartIndex(a[i, j]), GetRightPartIndex(a[i, j])];
                }
            }
        }

        public byte[,] ShiftRows(byte[,] a)
        {
            byte[,] b = new byte[STATE_ROWS, STATE_COLUMNS];

            for (int i = 0; i < STATE_ROWS; i++)
            {
                for (int j = 0; j < STATE_COLUMNS; j++)
                {
                    b[i, j] = a[i, (j + i) % STATE_COLUMNS];
                }
            }

            return b;
        }

        public byte[,] ReverseShiftRows(byte[,] a)
        {
            byte[,] b = new byte[STATE_ROWS, STATE_COLUMNS];
            for (int i = 0; i < STATE_ROWS; i++)
            {
                for (int j = STATE_COLUMNS-1; j > 0; j--)
                {
                    b[i, j] = a[i, (j + i) % STATE_COLUMNS];
                }
            }
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