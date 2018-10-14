using Cryptography.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Data.Processors
{
    internal class BlockProcessor : IConvertProcessor<List<byte[,]>, byte[]>
    {
        public List<byte[,]> ConvertTo(byte[] flatData)
        {
            if (flatData == null || flatData.Length == 0)
            {
                throw new ArgumentException("BlockPaddingProcessor (ConvertToAesBlocks): no data to process (null or empty)");
            }

            List<byte> keyBlock = flatData.ToList();
            int numberOfBlocks = keyBlock.Count / 16;

            List<byte[,]> blocks = new List<byte[,]>(numberOfBlocks);

            for (int i = 0; i < numberOfBlocks; i++)
            {
                byte[,] block = new byte[AesParameters.STATE_ROWS, AesParameters.STATE_COLUMNS];

                for (int j = 0; j < AesParameters.BYTES_IN_BLOCK; j++)
                {
                    block[j % AesParameters.STATE_ROWS, j / AesParameters.STATE_COLUMNS] =
                        keyBlock[i * AesParameters.BYTES_IN_BLOCK + j];
                }

                blocks.Add(block);
            }

            return blocks;
        }

        public byte[] ConvertBack(List<byte[,]> keyBlocks)
        {
            if (keyBlocks == null || keyBlocks.Count == 0)
            {
                throw new ArgumentException("BlockPaddingProcessor (ConvertToByteArray): no data to process (null or empty)");
            }

            byte[] flatKeyArray = new byte[keyBlocks.Count * AesParameters.BYTES_IN_BLOCK];

            for (int i = 0; i < keyBlocks.Count; i++)
            {
                for (int j = 0; j < AesParameters.BYTES_IN_BLOCK; j++)
                {
                    flatKeyArray[i * AesParameters.BYTES_IN_BLOCK + j] = keyBlocks[i][j % AesParameters.STATE_ROWS,
                        j / AesParameters.STATE_COLUMNS];
                }
            }
            return flatKeyArray;
        }
    }
}