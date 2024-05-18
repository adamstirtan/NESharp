namespace NESharp;

public sealed class NESharpEmulator(byte[] rom)
{
    private readonly Memory _memory = new Memory(rom);
    private readonly CPU _cpu = new CPU();

    public void Run()
    {
        while (true)
        {
            _cpu.ExecuteNextInstruction();
        }
    }
}
