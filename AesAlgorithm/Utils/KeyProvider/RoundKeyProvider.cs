using System.Collections.Generic;
using Cryptography.Data.Processors;
using Cryptography.Utils.KeySchedule;

namespace Cryptography.Utils.KeyProvider
{
    public class RoundKeyProvider : IRoundKeyProvider
    {
        public int KeysNumber { get; }

        private readonly List<byte[,]> _keys;

        public RoundKeyProvider(byte[] encryptionKey, 
            IKeyExpander expander,
            IConvertProcessor<List<byte[,]>, byte[]> processor)
        {
            byte[] expandedKey = expander.ExpandKey(encryptionKey);
            _keys = processor.ConvertTo(expandedKey);
            KeysNumber = _keys.Count;
        }

        public byte[,] GetKey(int round)
        {
            return _keys[round];
        }
    }
}
