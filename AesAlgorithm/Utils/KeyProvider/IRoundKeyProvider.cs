namespace Cryptography.Utils.KeyProvider
{
    public interface IRoundKeyProvider
    {
        int KeysNumber { get; }
        byte[,] GetKey(int round);  
    }
}
