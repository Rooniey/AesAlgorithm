using AesAlgorithm.Data.Processors;
using AesAlgorithm.Data.Sources;

namespace AesAlgorithm
{
    public class AesService
    {
        public enum Mode
        {
            Encryption,
            Decryption
        }

        public IDataSource DataSource { get; set; }

        public IDataProcessor DataProcessor { get; set; }
        

        public AesService(IDataSource dataSource)
        {
            DataSource = dataSource;
        }
    }
}