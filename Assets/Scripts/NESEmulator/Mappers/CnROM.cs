namespace NES
{
    public class CnRom : Mapper
    {
        private int chrBankSelect;

        public CnRom(Cartridge cartridge) : base(cartridge)
        {
            chrBankSelect = 0;
        }
        protected override int GetPrgRomIndex(ushort address)
        {
            var index = base.GetPrgRomIndex(address);

            if (cartridge.PrgRomBanks == 2 && index >= PrgRomBankSize)
            {
                index -= PrgRomBankSize;
            }

            return index;
        }

        protected override int GetChrIndex(ushort address)
        {
            
            int index = base.GetChrIndex(address);
            index += ChrBankSize * chrBankSelect;
            return index;
        }

        protected override void WriteRegisters(ushort address, byte data)
        {
            chrBankSelect = data & 0x0F;
        }

        protected override void WritePrgRam(ushort address, byte data)
        {

        }
    }
}
