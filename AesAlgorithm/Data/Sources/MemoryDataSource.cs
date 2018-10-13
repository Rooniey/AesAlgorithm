namespace Cryptography.Data.Sources
{
    public class MemoryDataSource : IDataSource
    {
        public byte[] Data { get; set; }

        public byte[] GetData()
        {
            return Data;
        }
    }
}
