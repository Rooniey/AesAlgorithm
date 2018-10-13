using System;
using System.Collections.Generic;
using Cryptography.Data.Processors;
using Cryptography.Utils.KeyProvider;
using Cryptography.Utils.KeySchedule;

namespace Cryptography.Utils.Factory
{
    public class AesAlgorithmFactory : IAlgorithmFactory
    {
        private readonly IConvertProcessor<List<byte[,]>, byte[]> _processor;

        public AesAlgorithmFactory(IConvertProcessor<List<byte[,]>, byte[]> converter)
        {
            _processor = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public AesAlgorithmImp GetAlgorithm(byte[] key)
        {
            IKeyExpander expander = GetKeyExpander(key.Length);
            IRoundKeyProvider keyProvider = new RoundKeyProvider(key, expander, _processor);
            return new AesAlgorithmImp(keyProvider);
        }

        private IKeyExpander GetKeyExpander(int keyLength)
        {
            switch (keyLength)
            {
                case 16:
                case 24:
                    return new AesKeyExpander128And192(keyLength);
                case 32:
                    return  new AesKeyExpander128And192(keyLength);
                default:
                    throw new ArgumentException(
                        $"RoundKeyProvider: there is no expander for the key with length {keyLength}");
            }         
        }
    }
}
