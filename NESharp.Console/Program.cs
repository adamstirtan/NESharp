using NESharp;

byte[] rom = File.ReadAllBytes("path/to/rom.nes");

var emulator = new NESharpEmulator(rom);
emulator.Run();