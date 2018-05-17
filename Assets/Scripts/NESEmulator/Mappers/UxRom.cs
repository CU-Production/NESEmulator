namespace NES
{
    public class UxRom : Mapper
    {
        private int prgRomBankZeroSelect, prgRomBankOneSelect;

        public UxRom(Cartridge cartridge) : base(cartridge)
        {
            prgRomBankZeroSelect = 0;
            prgRomBankOneSelect = cartridge.PrgRomBanks - 1;
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
                index += PrgRomBankSize * (prgRomBankOneSelect - 1);
            }

            return index;
        }

        protected override void WriteRegisters(ushort address, byte data)
        {
            prgRomBankZeroSelect = data & 0x0F;
        }

        protected override void WritePrgRam(ushort address, byte data)
        {

        }
    }
}
