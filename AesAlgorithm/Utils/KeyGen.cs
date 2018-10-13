using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cryptography.Constants;
using static Cryptography.Constants.AesParameters;

namespace Cryptography.Utils
{
    public class KeyGen
    {
        private static readonly byte[] Rcon = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36 };

        public static byte[,] ConvertToKeyMatrix(byte[] initialKey)
        {
            byte[,] keyMatrix = new byte[STATE_COLUMNS, STATE_ROWS];
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    keyMatrix[row, column] = initialKey[column*STATE_COLUMNS + row];
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

        public static byte[] GenerateKeys(byte[] primeKey)
        {
            int keyLength = primeKey.Length;
            int numberOfBytesToExpand, movement;
            switch (keyLength)
            {
                case 16: numberOfBytesToExpand = 176;
                    movement = 16;
                    break;
                case 24: numberOfBytesToExpand = 208;
                    movement = 24;
                    break;
                case 32: numberOfBytesToExpand = 240;
                    movement = 32;
                    break;
                default:
                    throw new ArgumentException("Improper initial key length; only 128, 192, 256 bits keys");
            }

            byte[] keysSequence = new byte[numberOfBytesToExpand];
            Array.Copy(primeKey, 0, keysSequence, 0, primeKey.Length);

            int currentByte = keyLength;
            int rcon = 0;
            while (currentByte < numberOfBytesToExpand)
            {

                for (int j = 0; j < STATE_ROWS; j++)
                {
                    keysSequence[currentByte + j] = keysSequence[ (currentByte - STATE_ROWS) + j];
                }

                //LEFT ROTATE
                byte tmp = keysSequence[currentByte];
                for (int i = 0; i < STATE_ROWS - 1; i++)
                {
                    keysSequence[currentByte + i] = keysSequence[currentByte + i + 1];
                }
                keysSequence[currentByte + STATE_ROWS - 1] = tmp;

                //SBOX
                for (int i = 0; i < STATE_ROWS; i++)
                {
                    byte value = keysSequence[currentByte + i];
                    keysSequence[currentByte + i] = TableConstants.RijndaelSBox[value / BYTES_IN_BLOCK, value % BYTES_IN_BLOCK];
                }

                keysSequence[currentByte] ^= Rcon[rcon];
                rcon++;

                for (int i = 0; i < STATE_ROWS; i++)
                {
                    keysSequence[currentByte + i] ^= keysSequence[currentByte - movement + i];
                }

                int innerByte = currentByte + STATE_ROWS;
                if (keyLength == 16)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < STATE_ROWS; j++)
                        {
                            keysSequence[innerByte + j] = keysSequence[innerByte + j - 4];
                            keysSequence[innerByte + j] ^= keysSequence[innerByte + j - movement];
                        }

                        innerByte+=4;
                    }               
                } else if (keyLength == 24)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < STATE_ROWS && innerByte < 208; j++)
                        {
                            keysSequence[innerByte + j] = keysSequence[innerByte + j - 4];
                            keysSequence[innerByte + j] ^= keysSequence[innerByte + j - movement];
                        }

                        innerByte += 4; 
                    }
                } else if (keyLength == 32)
                {
                    for (int i = 0; i < 3 ; i++)
                    {
                        for (int j = 0; j < STATE_ROWS && innerByte < 240; j++)
                        {
                            keysSequence[innerByte + j] = keysSequence[innerByte + j - 4];
                            keysSequence[innerByte + j] ^= keysSequence[innerByte + j - movement];
                        }

                        innerByte += 4;
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < STATE_ROWS && innerByte < 240; j++)
                        {
                            keysSequence[innerByte + j] = keysSequence[innerByte + j - 4];
                            byte value = keysSequence[innerByte + j];
                            keysSequence[innerByte + j] = TableConstants.RijndaelSBox[value / 16, value % 16];                          
                            keysSequence[innerByte + j] ^= keysSequence[innerByte + j - movement];
                        }

                        innerByte += 4;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < STATE_ROWS && innerByte < 240; j++)
                        {
                            keysSequence[innerByte + j] = keysSequence[innerByte + j - 4];
                            keysSequence[innerByte + j] ^= keysSequence[innerByte + j - movement];
                        }

                        innerByte += 4;
                    }
                }


                currentByte += movement;
            }


            return keysSequence;
        }

    }
}
