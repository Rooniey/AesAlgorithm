using System;

namespace AesAlgorithm
{
    public static class MatrixOperations
    {
        //return new instance of byte[,] representing operation's result
        public static byte[,] Multiply(this byte[,] a, byte[,] b)
        {
            int aColumns = a.GetLength(1);
            int bRows = b.GetLength(0);
            if (aColumns != bRows)
            {
                throw new ArgumentException($"Matrices have improper dimensions ({aColumns} != {bRows}");
            }

            int resultRows = a.GetLength(0);
            int resultColumns = b.GetLength(1);

            byte[,] result = new byte[resultRows, resultColumns];

            for (int row = 0; row < resultRows; row++)
            {
                for (int column = 0; column < resultColumns; column++)
                {
                    result[row, column] = 0;
                    for (int i = 0; i < aColumns; i++)
                    {
                        byte product = ElementMultiplication(a[row, i], b[i, column]);
                        result[row, column] ^= product;
                    }
                }
            }
            return result;
        }

        //special operation for multiplication with MOD 2
        private static byte ElementMultiplication(byte a, byte b)
        {
            switch (a)
            {
                case 1: return b;
                case 2: return TableConstants.MC2[b / 16, b % 16];
                case 3: return TableConstants.MC3[b / 16, b % 16];
                default: return 0;
            }
        }

        //return new instance of byte[,] representing desired column
        public static byte[,] GetColumn(this byte[,] a, int columnNumber)
        {
            int rows = a.GetLength(0);
            int columns = a.GetLength(1);
            if (columnNumber < 0 || columnNumber > columns - 1)
            {
                throw new ArgumentException($"Matrix does not have column with index={columnNumber}");
            }

            byte[,] resultColumn = new byte[rows, 1];

            for (int row = 0; row < rows; row++)
            {
                resultColumn[row, 0] = a[row, columnNumber];
            }

            return resultColumn;
        }

        //modifies existing byte[,], by setting desired column
        public static byte[,] SetColumn(this byte[,] a, byte[,] column, int columnNumber)
        {
            int rows = a.GetLength(0);
            int columns = a.GetLength(1);
            if (columnNumber < 0 || columnNumber > columns - 1)
            {
                throw new ArgumentException($"Matrix does not have column with index={columnNumber}");
            }

            if (rows != column.GetLength(0))
            {
                throw new ArgumentException($"Matrix and column does not have the same length");
            }

            for (int row = 0; row < rows; row++)
            {
                a[row, columnNumber] = column[row, 0];
            }

            return a;
        }
    }
}