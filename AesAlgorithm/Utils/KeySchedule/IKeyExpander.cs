namespace Cryptography.Utils.KeySchedule
{
    public interface IKeyExpander
    {
        byte[] ExpandKey(byte[] baseKey);
    }
}
