namespace NESharp;

public sealed class CPU
{
    public byte A { get; set; }
    public byte X { get; set; }
    public byte Y { get; set; }
    public byte _stackPointer { get; set; }
    public ushort _programCounter { get; set; }
    public byte _status { get; set; }

    private const byte CARRY_FLAG = 0x01;
    private const byte ZERO_FLAG = 0x02;
    private const byte INTERRUPT_DISABLE_FLAG = 0x04;
    private const byte DECIMAL_MODE_FLAG = 0x08;
    private const byte BREAK_COMMAND_FLAG = 0x10;
    private const byte OVERFLOW_FLAG = 0x40;
    private const byte NEGATIVE_FLAG = 0x80;

    private Memory _memory;

    public CPU(Memory memory)
    {
        _memory = memory;

        Reset();
    }

    public void ExecuteNextInstruction()
    {
        byte opcode = FetchByte();

        switch (opcode)
        {
            case 0xA9:
                LDA_Immediate();
                break;
            default:
                throw new NotImplementedException($"Opcode {opcode:X2} not implemented.");
        }
    }

    public void Reset()
    {
        A = 0;
        X = 0;
        Y = 0;
        _stackPointer = 0xFD;
        _programCounter = (ushort)(_memory.ReadMemory(0xFFFC) | (_memory.ReadMemory(0xFFFD) << 8));
        _status = 0x24; // Default status flag
    }

    private byte FetchByte()
    {
        byte value = _memory.ReadMemory(_programCounter);

        _programCounter++;

        return value;
    }

    private ushort FetchWord()
    {
        byte lowByte = FetchByte();
        byte highByte = FetchByte();

        return (ushort)(lowByte | (highByte << 8));
    }

    private void SetFlag(byte flag, bool condition)
    {
        if (condition)
        {
            _status |= flag;
        }
        else
        {
            _status &= (byte)-flag;
        }
    }

    private bool GetFlag(byte flag)
    {
        return (_status & flag) != 0;
    }

    private byte Immediate()
    {
        return FetchByte();
    }

    private byte ZeroPage()
    {
        byte address = FetchByte();
        return _memory.ReadMemory(address);
    }

    private void LDA_Immediate()
    {
        A = FetchByte();
        SetFlag(ZERO_FLAG, A == 0);
        SetFlag(NEGATIVE_FLAG, (A & 0x80) != 0);
    }

    private void ADC_Immediate()
    {
        byte value = Immediate();
        int result = A + value + (GetFlag(CARRY_FLAG) ? 1 : 0);
        SetFlag(CARRY_FLAG, result > 0xFF);
        SetFlag(ZERO_FLAG, (result & 0xFF) == 0);
        SetFlag(NEGATIVE_FLAG, (result & 0x80) != 0);
        SetFlag(OVERFLOW_FLAG, (~(A ^ value) & (A ^ result) & 0x80) != 0);
        A = (byte)result;
    }

    private void STA_ZeroPage()
    {
        byte address = FetchByte();
        _memory.WriteMemory(address, A);
    }
}
