namespace Cryptography.Utils.KeySchedule
{
    public class AesKeyExpander128And192 : AesKeyExpanderBase
    {
        private readonly int _columnsToExpandInOneIteration; 

        public AesKeyExpander128And192(int baseKeyLength) : base(baseKeyLength)
        {
            _columnsToExpandInOneIteration = baseKeyLength == 16 ? 3 : 5;
        }

        protected override void ExpandDependingOnBaseKeyLength(byte[] keysSequence, ref int currentIndex)
        {
            ExpandNColumnsByXor(keysSequence, ref currentIndex, _columnsToExpandInOneIteration);
        }
    }
}