namespace Cryptography.Constants
{
    public static class AesParameters
    {
        public static readonly int STATE_ROWS = 4;
        public static readonly int STATE_COLUMNS = 4;
        public static readonly int BYTES_IN_BLOCK = STATE_ROWS * STATE_COLUMNS;
        public static readonly int[] KEY_LENGTHS = new int[] {128, 192, 256};
        public static readonly int MOVEMENT_128 = 16;
        public static readonly int MOVEMENT_192 = 24;
        public static readonly int MOVEMENT_256 = 32;
        public static readonly int EXPANDED_KEY_LENGTH_128 = 176;
        public static readonly int EXPANDED_KEY_LENGTH_192 = 208;
        public static readonly int EXPANDED_KEY_LENGTH_256 = 240;
    }
}
