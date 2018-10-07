using System;
using System.Collections.Generic;
using System.Linq;

namespace AesAlgorithm
{
    public class AesDataProcessor
    {
        public static readonly int DEFAULT_BYTES_IN_BLOCK = AesAlgorithm.STATE_ROWS * AesAlgorithm.STATE_COLUMNS;

        public List<byte[,]> ConvertToAesBlocks(byte[] flatData)
        {
            if (flatData == null || !flatData.Any())
            {
                throw new ArgumentException("AesDataProcessor: no flatData to process (null or empty)");
            }

            List<byte> paddedFlatData = PadData(flatData);
            int numberOfBlocks = paddedFlatData.Count / 16;

            List<byte[,]> blocks = new List<byte[,]>(numberOfBlocks);

            for (int i = 0; i < numberOfBlocks; i++)
            {
                byte[,] block = new byte[AesAlgorithm.STATE_ROWS, AesAlgorithm.STATE_COLUMNS];

                for (int j = 0; j < DEFAULT_BYTES_IN_BLOCK; j++)
                {
                    block[j % AesAlgorithm.STATE_ROWS, j / AesAlgorithm.STATE_COLUMNS] =
                        paddedFlatData[i * (DEFAULT_BYTES_IN_BLOCK - 1) + j];
                }
                blocks.Add(block);
            }

            return blocks;
        }

        private List<byte> PadData(byte[] data)
        {
            List<byte> paddedData = data.ToList();
            int numberOfPaddingBytes = DEFAULT_BYTES_IN_BLOCK - (data.Length % DEFAULT_BYTES_IN_BLOCK);
            for (int i = 0; i < numberOfPaddingBytes; i++)
            {
                paddedData.Add(0);
            }
            return paddedData;
        }
    }
}