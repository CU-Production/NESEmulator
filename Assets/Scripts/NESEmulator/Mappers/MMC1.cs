namespace NES
{
    public class Mmc1 : Mapper
    {
        private int prgRomBankZeroSelect, prgRomBankOneSelect;
        private int chrBankZeroSelect, chrBankOneSelect;

        public Mmc1(Cartridge cartridge) : base(cartridge)
        {

        }

        protected override int GetPrgRomIndex(ushort address)
        {
            var index = base.GetPrgRomIndex(address);

            if (index < PrgRomBankSize)
            {
                index += PrgRomBankSize * prgRomBankZeroSelect;
            }
            else
            {
                index += PrgRomBankSize * prgRomBankOneSelect;
            }

            return index;
        }

        protected override int GetChrIndex(ushort address)
        {
            var index = base.GetChrIndex(address);

            if (index < ChrBankSize)
            {
                index += ChrBankSize * chrBankZeroSelect;
            }
            else
            {
                index += ChrBankSize * chrBankOneSelect;
            }

            return index;
        }

        protected override void WriteRegisters(ushort address, byte data)
        {

        }
    }
}
