using System.Collections.Generic;
using Cryptography.Constants;
using Cryptography.Utils.Extensions;
using Cryptography.Utils.KeyProvider;

namespace Cryptography
{
    public class AesAlgorithmImp
    {
        private readonly IRoundKeyProvider _roundKeyProvider;

        public AesAlgorithmImp(IRoundKeyProvider keyProvider)
        {
            _roundKeyProvider = keyProvider;
        }

        public List<byte[,]> Encrypt(List<byte[,]> data)
        {
            List<byte[,]> result = new List<byte[,]>();
            int standardRounds = _roundKeyProvider.KeysNumber - 1;

            foreach (var dataPart in data)
            {
                AddRoundKey(dataPart, _roundKeyProvider.GetKey(0));
                for (int i = 1; i < standardRounds; i++)
                {                   
                    SubstituteBytes(dataPart);
                    ShiftRows(dataPart);
                    MixColumns(dataPart);
                    AddRoundKey(dataPart, _roundKeyProvider.GetKey(i));
                }
                SubstituteBytes(dataPart);
                ShiftRows(dataPart);
                AddRoundKey(dataPart, _roundKeyProvider.GetKey(standardRounds));

                result.Add(dataPart);
            }

            return result;
        }

        public List<byte[,]> Decrypt(List<byte[,]> encryptedData)
        {
            List<byte[,]> result = new List<byte[,]>();
            int standardRounds = _roundKeyProvider.KeysNumber - 1;

            foreach (var dataPart in encryptedData)
            {

                AddRoundKey(dataPart, _roundKeyProvider.GetKey(standardRounds));

                for (int i = standardRounds - 1; i > 0; i--)
                {
                    ReverseSubstituteBytes(dataPart);
                    ReverseShiftRows(dataPart);
                    AddRoundKey(dataPart, _roundKeyProvider.GetKey(i));
                    InverseMixColumns(dataPart);
                }

                ReverseSubstituteBytes(dataPart);
                ReverseShiftRows(dataPart);
                AddRoundKey(dataPart, _roundKeyProvider.GetKey(0));

                result.Add(dataPart);
            }

            return result;
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

<<<<<<< Updated upstream
        public void ReverseSubstituteBytes(byte[,] a)
=======
        public void InversedSubstituteBytes(byte[,] a)
>>>>>>> Stashed changes
        {
            for (int i = 0; i < AesParameters.STATE_ROWS; i++)
            {
                for (int j = 0; j < AesParameters.STATE_COLUMNS; j++)
                {
                    a[i, j] = TableConstants.InversedRijndaelSBox[GetLeftPartIndex(a[i, j]), GetRightPartIndex(a[i, j])];
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

        public void ReverseShiftRows(byte[,] a)
        {
            for (int i = 1; i < AesParameters.STATE_ROWS; i++)
            {
                int move = i;
                while (move > 0)
                {
                    byte tmp = a[i, AesParameters.STATE_COLUMNS - 1];
                    for (int j = AesParameters.STATE_COLUMNS - 1; j > 0; j--)
                    {
                        a[i, j] = a[i, j - 1];
                    }
                    a[i, 0] = tmp;
                    move--;
                }
            }
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

        public void InverseMixColumns(byte[,] state)
        {
            for (int i = 0; i < AesParameters.STATE_COLUMNS; i++)
            {
                byte[,] column = state.GetColumn(i);
                byte[,] newColumn = TableConstants.INV_GALOIS_MATRIX.Multiply(column);
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
                    state[row, column] = (byte)(state[row, column] ^ roundKey[row, column]);
                }
            }
        }
    }
}