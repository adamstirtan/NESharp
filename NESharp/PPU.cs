namespace NESharp;

public class PPU
{
    // Registers
    public byte PPUCTRL { get; set; }
    public byte PPUMASK { get; set; }
    public byte PPUSTATUS { get; set; }
    public byte OAMADDR { get; set; }
    public byte OAMDATA { get; set; }
    public byte PPUSCROLL { get; set; }
    public byte PPUADDR { get; set; }
    public byte PPUDATA { get; set; }

    // OAM (Object Attribute Memory) for sprites
    private byte[] _oam = new byte[256];

    public const int ScreenWidth = 256;
    public const int ScreenHeight = 240;
    private byte[] _frameBuffer = new byte[ScreenWidth * ScreenHeight];

    public void RenderFrame()
    { }

    public byte[] GetFrameBuffer()
    {
        return _frameBuffer;
    }
}
