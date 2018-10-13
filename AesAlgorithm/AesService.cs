using System.Collections.Generic;
using Cryptography.Data.Processors;
using Cryptography.Data.Sources;
using Cryptography.Utils.Factory;

namespace Cryptography
{
    public class AesService : ISymmetricCryptoService
    {
        private readonly IConvertProcessor<List<byte[,]>, byte[]> _dataProcessor;
        private readonly IAlgorithmFactory _algorithmFactory;
        
        public AesService()
        {
            _dataProcessor = new BlockDataProcessor();
            _algorithmFactory = new AesAlgorithmFactory(new BlockDataProcessor());
        }

        public byte[] Encrypt(byte[] key, IDataSource dataSource)
        {
            //TODO implement new data processor; adding padding and 16 bytes (how many bytes were padded)
            List<byte[,]> blocks = _dataProcessor.ConvertTo(dataSource.GetData());
            AesAlgorithmImp aes = _algorithmFactory.GetAlgorithm(key);
            List<byte[,]> encryptedBlocks = aes.Encrypt(blocks);
            return _dataProcessor.ConvertBack(encryptedBlocks);
        }

        public byte[] Decrypt(byte[] key, IDataSource dataSource)
        {
            List<byte[,]> blocks = _dataProcessor.ConvertTo(dataSource.GetData());
            AesAlgorithmImp aes = _algorithmFactory.GetAlgorithm(key);
            List<byte[,]> decryptedBlocks = aes.Decrypt(blocks);
            //TODO converter get last 2 blocks; read how many bytes were padded; remove padded bytes
            return _dataProcessor.ConvertBack(decryptedBlocks);
        }

        
    }
}