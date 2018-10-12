using System.Collections.Generic;
using Cryptography;
using Cryptography.Data.Processors;
using Cryptography.Data.Sources;

namespace AesAlgorithm
{
    public class AesService : ISymmetricCryptoService
    {
        public IDataProcessor DataProcessor { get; set; }
        
        public AesService(IDataProcessor processor)
        {
            DataProcessor = processor;
        }

        public byte[] Encrypt(byte[] key, IDataSource dataSource)
        {
            List<byte[,]> blocks = DataProcessor.ConvertToBlocks(dataSource.GetData());
            AesAlgorithmImp aes = new AesAlgorithmImp(key);
            List<byte[,]> encryptedblocks = aes.Encrypt(blocks);
            return DataProcessor.ConvertToByteArray(encryptedblocks);
        }

        public byte[] Decrypt(byte[] key, IDataSource dataSource)
        {
            List<byte[,]> blocks = DataProcessor.ConvertToBlocks(dataSource.GetData());
            AesAlgorithmImp aes = new AesAlgorithmImp(key);
            List<byte[,]> decryptedBlocks = aes.Decrypt(blocks);
            return DataProcessor.ConvertToByteArray(decryptedBlocks);
        }
    }
}