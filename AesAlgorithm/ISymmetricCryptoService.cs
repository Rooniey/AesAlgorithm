using Cryptography.Data.Sources;

namespace Cryptography
{
    public interface ISymmetricCryptoService
    {
        byte[] Encrypt(byte[] key, IDataSource dataSource);
        byte[] Decrypt(byte[] key, IDataSource dataSource);
    }
}
