using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesAlgorithm.Constants
{
    public static class AesParameters
    {
        public static readonly int STATE_ROWS = 4;
        public static readonly int STATE_COLUMNS = 4;
        public static readonly int BYTES_IN_BLOCK = STATE_ROWS * STATE_COLUMNS;
        public static readonly int[] KEY_LENGTHS = new int[] {128, 192, 256};
    }
}
