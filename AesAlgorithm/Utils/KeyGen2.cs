using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AesAlgorithm.Utils;
using Cryptography.Constants;
using static Cryptography.Constants.AesParameters;

namespace Cryptography.Utils
{
    public class KeyGen2
    {
        private static readonly byte[] Rcon = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36 };

        public static byte[,] ConvertToKeyMatrix(byte[] initialKey)
        {
            byte[,] keyMatrix = new byte[STATE_COLUMNS, STATE_ROWS];
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    keyMatrix[row, column] = initialKey[row*STATE_COLUMNS + column];
                }
            }

            return keyMatrix;
        }

        public static List<byte[,]> GenerateKeys(byte[,] primeKey)
        {
            List<byte[,]> keys = new List<byte[,]>();
            keys.Add(primeKey);
            for (int i = 0; i < 10; i++)
            {
                byte[,] lastColumnOfLastKey = keys[i].GetColumn(STATE_COLUMNS - 1);
                lastColumnOfLastKey.CircularRotateLeft();

                for (int j = 0; j < STATE_ROWS; j++)
                {
                    byte current = lastColumnOfLastKey[j, 0];
                    lastColumnOfLastKey[j,0] = TableConstants.RijndaelSBox[current / 16, current % 16];
                }

                lastColumnOfLastKey[0, 0] ^= Rcon[i];
                byte[,] column2 = keys[i].GetColumn(0);
                for (int j = 0; j  < STATE_COLUMNS; j++)
                {
                    lastColumnOfLastKey[j, 0] ^= column2[j, 0];
                }

                byte[,] newKey = new byte[STATE_ROWS,STATE_COLUMNS];
                newKey.SetColumn(lastColumnOfLastKey, 0);
                
                for (int column = 1; column < STATE_COLUMNS; column++)
                {
                    newKey.SetColumn(newKey.GetColumn(column-1), column);
                    for (int row = 0; row < STATE_ROWS; row++)
                    {
                        newKey[row, column] ^= keys[i][row, column];
                    }
                }

                keys.Add(newKey);
            }

            return keys;
        }

    }
}
