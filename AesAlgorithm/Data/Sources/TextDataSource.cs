using System;
using AesAlgorithm.Utils;

namespace AesAlgorithm.Data.Sources
{
    public class TextDataSource : IDataSource
    {
        public String Text { get; set; } = "";
        public TextUtility.Encoding TextEncoding { get; set; } = TextUtility.Encoding.Ascii;

        public byte[] GetData()
        {
            return Text.ToByteArray(TextEncoding);
        }
    }
}