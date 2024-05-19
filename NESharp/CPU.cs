namespace NESharp;

public sealed class CPU
{
    public byte A { get; set; }
    public byte X { get; set; }
    public byte Y { get; set; }
    public byte _stackPointer { get; set; }
    public ushort _programCounter { get; set; }
    public byte _status { get; set; }

    private Memory _memory;

    public CPU(Memory memory)
    {
        _memory = memory;

        Reset();
    }

    public void ExecuteNextInstruction()
    {
        byte opcode = FetchByte();
    }

    public void Reset()
    {
        A = 0;
        X = 0;
        Y = 0;
        _stackPointer = 0xFD;
        _programCounter = (ushort)(_memory.ReadMemory(0xFFFC) | (_memory.ReadMemory(0xFFFD) << 8));
    }

    private byte FetchByte()
    {
        byte value = ReadMemory(_programCounter);

        _programCounter++;

        return value;
    }
}
