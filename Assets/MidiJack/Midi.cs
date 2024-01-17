
namespace MidiJack
{
    // MIDI channel names
    public enum MidiChannel
    {
        Ch1,    // 0
        Ch2,    // 1
        Ch3,
        Ch4,
        Ch5,
        Ch6,
        Ch7,
        Ch8,
        Ch9,
        Ch10,
        Ch11,
        Ch12,
        Ch13,
        Ch14,
        Ch15,
        Ch16,
        All     // 16
    }

    // MIDI message structure
    public struct MidiMessage
    {
        public uint source; // MIDI source (endpoint) ID
        public byte status; // MIDI status byte
        public byte data1;  // MIDI data bytes
        public byte data2;

        public MidiMessage(ulong data)
        {
            source = (uint)(data & 0xffffffffUL);
            status = (byte)((data >> 32) & 0xff);
            data1 = (byte)((data >> 40) & 0xff);
            data2 = (byte)((data >> 48) & 0xff);
        }

        public override string ToString()
        {
            const string fmt = "s({0:X2}) d({1:X2},{2:X2}) from {3:X8}";
            return string.Format(fmt, status, data1, data2, source);
        }
    }
}
