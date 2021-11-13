using System;
using System.IO;
using System.Text;

namespace N8Engine.External
{
    static class FastConsole
    {
        static readonly BufferedStream _stream;

        static FastConsole()
        {
            Console.OutputEncoding = Encoding.Unicode;
            _stream = new BufferedStream(Console.OpenStandardOutput(), 0x15000);
        }

        public static void WriteLine(string text) => Write(text + "\r\n");

        public static void Write(string text)
        {
            var rgb = new byte[text.Length << 1];
            Encoding.Unicode.GetBytes(text, 0, text.Length, rgb, 0);

            lock (_stream)   // (optional, can omit if appropriate)
                _stream.Write(rgb, 0, rgb.Length);
        }

        public static void Flush() { lock (_stream) _stream.Flush(); }
    };
}