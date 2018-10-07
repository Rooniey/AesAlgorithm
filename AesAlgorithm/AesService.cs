using AesAlgorithm.Data;

namespace AesAlgorithm
{
    public class AesService
    {
        private readonly AesDataProcessor _dataProcessor = new AesDataProcessor();

        public IDataSource DataSource { get; set; }

        public AesService(IDataSource dataSource)
        {
            DataSource = dataSource;
        }
    }
}