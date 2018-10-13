namespace Cryptography.Data.Sources
{
    public class MemoryDataSource : IDataSource
    {
        public byte[] Data { get; }

        public MemoryDataSource(byte[] sourceBytes)
        {
            Data = sourceBytes;
        }

        public byte[] GetData()
        {
            return Data;
        }
    }
}
