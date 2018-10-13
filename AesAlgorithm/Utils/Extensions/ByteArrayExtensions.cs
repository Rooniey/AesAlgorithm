namespace Cryptography.Utils.Extensions
{
    public static class ByteArrayExtensions
    {
        public static void RewriteAndXor(this byte[] a, int indexTarget, int indexSource, int indexXorOperand)
        {
            a[indexTarget] = (byte)(a[indexSource] ^ a[indexXorOperand]);
        }
    }
}
