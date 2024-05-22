namespace NESharp;

public sealed class NESharpEmulator
{
    private readonly Memory _memory;
    private readonly CPU _cpu;
    private readonly PPU _ppu;
    private readonly APU _apu;

    public NESharpEmulator(byte[] rom)
    {
        _memory = new(rom);

        _cpu = new(_memory);
        _ppu = new();
        _apu = new();
    }

    public void Run()
    {
        while (true)
        {
            _cpu.ExecuteNextInstruction();
            _ppu.RenderFrame();
            _apu.ProcessAudio();

            // Handle timing and synchronization
        }
    }
}
