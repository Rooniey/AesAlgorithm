using System;
using System.Collections.Generic;
using Cryptography.Constants;

namespace AesAlgorithm.Utils
{
    public static class KeyGen
    {
        // first and second hex digits are sbox indexes
       private static byte GetSboxValue(byte input) => TableConstants.RijndaelSBox[input / 16, input % 16];
       
        public static List<byte[,]> GenerateKeys(List<byte[,]> keys, int numberOfRounds)
        {  
            // the first 11 16-byte matrices are always calculated as follows
            for(int i = 0; i < 10; i++)
            {
                byte[] temp = new byte[4]
                {
                    keys[i][3,0], keys[i][3,1], keys[i][3,2], keys[i][3,3]
                };

                temp = RotateBytesLeft(temp);

                for(int row = 0; row < 4; row++)
                {
                    temp[row] = GetSboxValue(temp[row]); 
                }

                temp[0] ^= Rcon[i + 1];

                // xor with the first column of the last key
                for (int row = 0; row < 4; row++)
                {
                    temp[row] ^= keys[i][0, row];
                }


                byte[,] tmpMatrix = new byte[4, 4];

                for(int row = 0; row < 4; row++)
                {
                    tmpMatrix[0, row] = temp[row];
                }

                // xor the 3 leftover columns of the last key with the last new column
                for(int column = 1; column < 4; column++)
                {
                    for(int row = 0; row < 4; row++)
                    {
                        tmpMatrix[column, row] = (byte)(keys[i][column, row] ^ tmpMatrix[column - 1, row]);
                    }
                }

                keys.Add(tmpMatrix);
                // schody
            }
            

            return keys;
        }

        public static byte[,] ConvertToKeyMatrix(byte[] initialKey)
        {
            byte[,] keyMatrix = new byte[AesParameters.STATE_COLUMNS, AesParameters.STATE_ROWS];
            for (int column = 0; column < 4; column++)
            {
                for (int j = 0; j < 4; j++)
                {
                    keyMatrix[column, j] = initialKey[column * 4 + j];
                }
            }

            return keyMatrix;
        }

        private static byte[] RotateBytesLeft(byte[] byteArrayInput) // assume length of 4
        {
            byte[] byteArray = new byte[4];
            Array.Copy(byteArrayInput, byteArray, 4);

            byte tmp = byteArray[0];
            for (int i = 0; i < 3; i++)
            {
                byteArray[i] = byteArray[i + 1];
            }
            byteArray[3] = tmp;

            return byteArray;
        }

        private static readonly byte[] Rcon = { 0x8b, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36 };
    }
}
