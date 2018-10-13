namespace Cryptography.Utils.Factory
{
    public interface IAlgorithmFactory
    {
        AesAlgorithmImp GetAlgorithm(byte[] key);
    }
}
