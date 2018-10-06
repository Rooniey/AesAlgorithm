using System.Collections.Generic;

namespace AesAlgorithm
{
    public class AesAlgorithm
    {
        public AesAlgorithm(byte[] key)
        {
            keys.Add(key);
            switch (keys.Count)
            {
                case 16:
                    numberOfRounds = 10;
                    break;

                case 24:
                    numberOfRounds = 12;
                    break;

                case 32:
                    numberOfRounds = 14;
                    break;
            }
            GenerateKeys();
        }

        public List<byte[]> Encrypt(List<byte[]> data)
        {
            return null;
        }

        public List<byte[]> Decrypt(List<byte[]> encryptedData)
        {
            return null;
        }

        private void GenerateKeys()
        {

        }

        private void MixColumns()
        {

        }

        private void AddRoundKey()
        {

        }

        private int numberOfRounds;
        private List<byte[]> keys = new List<byte[]>();
    }
}