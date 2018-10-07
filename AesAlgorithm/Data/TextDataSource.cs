using System;

namespace AesAlgorithm.Data
{
    public class TextDataSource : IDataSource
    {
        public enum Encoding
        {
            Ascii,
            Utf8,
            Unicode
        }

        public String Text { get; set; } = "";
        public Encoding TextEncoding { get; set; } = Encoding.Ascii;

        public byte[] GetData()
        {
            switch (TextEncoding)
            {
                case Encoding.Ascii:
                    return System.Text.Encoding.ASCII.GetBytes(Text);

                case Encoding.Unicode:
                    return System.Text.Encoding.Unicode.GetBytes(Text);

                case Encoding.Utf8:
                    return System.Text.Encoding.UTF8.GetBytes(Text);

                default:
                    return null;
            }
        }
    }
}