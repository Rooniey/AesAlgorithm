using System.Collections.Generic;

namespace Cryptography.Data.Processors
{
    public interface IDataProcessor
    {
        List<byte[,]> ConvertToBlocks(byte[] data);
        byte[] ConvertToByteArray(List<byte[,]> blocks);
    }
}
