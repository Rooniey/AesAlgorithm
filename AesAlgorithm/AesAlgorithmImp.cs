using System.Collections.Generic;
using Cryptography.Constants;
using Cryptography.Utils;

namespace Cryptography
{
    public class AesAlgorithmImp
    {

        public AesAlgorithmImp(byte[] key)
        {
            _keys = KeyGen.GenerateKeys(KeyGen.ConvertToKeyMatrix(key));
            switch (_keys[0].Length)
            {
                case 16:
                    _numberOfRounds = 10;
                    break;

                case 24:
                    _numberOfRounds = 12;
                    break;

                case 32:
                    _numberOfRounds = 14;
                    break;
            }
         
        }

        public List<byte[,]> Encrypt(List<byte[,]> data)
        {
            List<byte[,]> result = new List<byte[,]>();

            foreach (var dataPart in data)
            {
                AddRoundKey(dataPart, _keys[0]);
                for (int i = 1; i < _numberOfRounds; i++)
                {                   
                    SubstituteBytes(dataPart);
                    ShiftRows(dataPart);
                    MixColumns(dataPart);
                    AddRoundKey(dataPart, _keys[i]);
                }
                SubstituteBytes(dataPart);
                ShiftRows(dataPart);
                AddRoundKey(dataPart, _keys[_numberOfRounds]);

                result.Add(dataPart);
            }

            return result;
        }

        public List<byte[,]> Decrypt(List<byte[,]> encryptedData)
        {
            return null;
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
            for(int i = 0; i<AesParameters.STATE_ROWS; i++)
            {
                for (int j = 0; j < AesParameters.STATE_COLUMNS; j++)
                {
                    a[i, j] = TableConstants.RijndaelSBox[GetLeftPartIndex(a[i, j]), GetRightPartIndex(a[i, j])];
                }
            }
        }

        public void ShiftRows(byte[,] a)
        {
            for (int i = 1; i < AesParameters.STATE_ROWS; i++)
            {
                int move = i;
                while (move > 0)
                {
                    byte tmp = a[i, 0];
                    for (int j = 0; j < AesParameters.STATE_COLUMNS - 1; j++)
                    {
                        a[i, j] = a[i, j + 1];
                    }
                    a[i, AesParameters.STATE_COLUMNS - 1] = tmp;
                    move--;
                }
            }
        }

        public byte[,] ReverseShiftRows(byte[,] a)
        {
            byte[,] b = new byte[AesParameters.STATE_ROWS, AesParameters.STATE_COLUMNS];
            for (int i = 0; i < AesParameters.STATE_ROWS; i++)
            {
                for (int j = AesParameters.STATE_COLUMNS-1; j > 0; j--)
                {
                    b[i, j] = a[i, (j + i) % AesParameters.STATE_COLUMNS];
                }
            }
            return b;
            
        }


        public void MixColumns(byte[,] state)
        {
            for (int i = 0; i < AesParameters.STATE_COLUMNS; i++)
            {
                byte[,] column = state.GetColumn(i);
                byte[,] newColumn = TableConstants.GALOIS_MATRIX.Multiply(column);
                state.SetColumn(newColumn, i);
            }
        }

        public void AddRoundKey(byte[,] state, byte[,] roundKey)
        {
            for (int row = 0; row < AesParameters.STATE_ROWS; row++)
            {
                for (int column = 0; column < AesParameters.STATE_COLUMNS; column++)
                {
                    //XOR on corresponding bytes in state and roundKey matrix
                    //pamietac o zmianie !!
                    state[row, column] = (byte)(state[row, column] ^ roundKey[row, column]);
                    // pewnie zmiana row z column przy kluczu; bo niebede zamienial wszedzie miejscami u siebie xd
                }
            }
        }

        private int _numberOfRounds;
        private List<byte[,]> _keys = new List<byte[,]>();
    }
}