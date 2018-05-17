namespace NES
{
    public class Mapper
    {
        protected const ushort PrgRomBankSize = 0x4000;
        protected const ushort ChrBankSize = 0x1000;

        protected const ushort PrgRomAddress = 0x8000;
        protected const ushort PrgRamAddress = 0x6000;
        protected const ushort ChrAddress = 0x2000;

        public enum MirrorMode
        {
            Horizontal,
            Vertical
        };

        public MirrorMode mirrorMode;

        protected Cartridge cartridge;

        public Mapper(Cartridge cartridge)
        {
            this.cartridge = cartridge;

            mirrorMode = (cartridge.Flag6 & 1) == 1 ? MirrorMode.Vertical : MirrorMode.Horizontal;
        }

        public byte Read(ushort address)
        {
            byte data = 0;

            if (address >= PrgRomAddress)
            {
                data = cartridge.PrgRom[GetPrgRomIndex(address)];
            }
            else if (address >= PrgRamAddress)
            {
                data = cartridge.PrgRAM[GetPrgRamIndex(address)];
            }
            else if (address < ChrAddress)
            {
                data = cartridge.Chr[GetChrIndex(address)];
            }
            return data;
        }

        protected virtual int GetPrgRomIndex(ushort address)
        {
            return address - PrgRomAddress;
        }

        protected virtual int GetPrgRamIndex(ushort address)
        {
            return address - PrgRamAddress;
        }

        protected virtual int GetChrIndex(ushort address)
        {
            return address;
        }

        public void Write(ushort address, byte data)
        {
            if (address >= PrgRomAddress)
            {
                WriteRegisters(address, data);
            }
            else if (address >= PrgRamAddress)
            {
                WritePrgRam(address, data);
            }
            else if (address < ChrAddress && cartridge.ChrBanks == 0)
            {
                WriteChrRam(address, data);
            }
        }

        protected virtual void WriteRegisters(ushort address, byte data)
        {

        }

        protected virtual void WritePrgRam(ushort address, byte data)
        {
            cartridge.PrgRAM[GetPrgRamIndex(address)] = data;
        }

        protected virtual void WriteChrRam(ushort address, byte data)
        {
            cartridge.Chr[GetChrIndex(address)] = data;
        }
    }
}
