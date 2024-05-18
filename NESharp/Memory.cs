namespace NESharp;

public class Memory
{
    private byte[] ram = new byte[0x800]; // 2KB of RAM
    private byte[] prgRom;

    public Memory(byte[] prgRom)
    {
        this.prgRom = prgRom;
    }

    public byte ReadMemory(ushort address)
    {
        if (address < 0x2000)
        {
            return ram[address % 0x800];
        }
        else if (address >= 0x8000)
        {
            return prgRom[address - 0x8000];
        }

        // Handle other memory regions...
        throw new NotImplementedException($"Memory read at address {address:X4} not implemented.");
    }

    public void WriteMemory(ushort address, byte value)
    {
        if (address < 0x2000)
        {
            ram[address % 0x800] = value;
            return;
        }

        // Handle other memory regions...
        throw new NotImplementedException($"Memory write at address {address:X4} not implemented.");
    }
}
