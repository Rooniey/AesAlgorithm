using System;
using System.Collections.Generic;
using System.Linq;
using static AesAlgorithm.Constants.AesParameters;

namespace AesAlgorithm.Data.Processors
{
    public class AesDataProcessor : IDataProcessor
    {
        public List<byte[,]> ConvertToBlocks(byte[] flatData)
        {
            if (flatData == null || flatData.Length == 0)
            {
                throw new ArgumentException("AesDataProcessor (ConvertToAesBlocks): no data to process (null or empty)");
            }

            List<byte> paddedFlatData = PadDataWithZeroes(flatData);
            int numberOfBlocks = paddedFlatData.Count / 16;

            List<byte[,]> blocks = new List<byte[,]>(numberOfBlocks);

            for (int i = 0; i < numberOfBlocks; i++)
            {
                byte[,] block = new byte[STATE_ROWS, STATE_COLUMNS];

                for (int j = 0; j < BYTES_IN_BLOCK; j++)
                {
                    block[j % STATE_ROWS, j / STATE_COLUMNS] =
                        paddedFlatData[i * BYTES_IN_BLOCK + j];
                }
                blocks.Add(block);
            }

            return blocks;
        }

        public byte[] ConvertToByteArray(List<byte[,]> blocks)
        {
            if (blocks == null || blocks.Count == 0)
            {
                throw new ArgumentException("AesDataProcessor (ConvertToByteArray): no data to process (null or empty)");
            }
            
            byte[] flatData = new byte[blocks.Count * BYTES_IN_BLOCK];

            for (int i = 0; i < blocks.Count; i++)
            {
                for (int j = 0; j < BYTES_IN_BLOCK; j++)
                {
                    flatData[i * BYTES_IN_BLOCK + j] = blocks[i][j % STATE_ROWS,
                        j / STATE_COLUMNS];
                }
            }
            return flatData;
        }

        private List<byte> PadDataWithZeroes(byte[] data)
        {
            List<byte> paddedData = data.ToList();
            int numberOfPaddingBytes = BYTES_IN_BLOCK - (data.Length % BYTES_IN_BLOCK);
            for (int i = 0; i < numberOfPaddingBytes; i++)
            {
                paddedData.Add(0);
            }
            return paddedData;
        }
    }
}