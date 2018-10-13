namespace Cryptography.Data.Processors
{
    public interface IConvertProcessor<T,G>
    {
        T ConvertTo(G data);
        G ConvertBack(T blocks);
    }
}
