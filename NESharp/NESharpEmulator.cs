namespace NESharp;

public sealed class NESharpEmulator
{
    private readonly Memory _memory;
    private readonly CPU _cpu;

    public NESharpEmulator(byte[] rom)
    {
        _memory = new(rom);
        _cpu = new(_memory);
    }

    public void Run()
    {
        while (true)
        {
            _cpu.ExecuteNextInstruction();
        }
    }
}
