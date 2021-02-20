using System;
using System.IO;
using RFile.Interfaces;

namespace RFile
{
    public class RFileWriter : IRegionFileWriter, IDisposable
    {
        public Stream Stream;
        public StreamWriter Writer;

        public RFileWriter(Stream stream)
        {
            Stream = stream;
            Writer = new StreamWriter(stream);
        }

        public void WriteFlagContext(string flagName)
        {
            Writer.Write($"F[{flagName}];");
        }

        public void WriteFlagValue(string key, string value)
        {
            Writer.Write($"F:{key}={value.Replace(";", "|")};");
        }

        public void WriteMapContext(int x, int y)
        {
            Writer.Write($"MS[{x},{y}];");
        }

        public void WriteRegionContext(string regionName)
        {
            Writer.Write($"R[{regionName}];");
        }

        public void WriteRegionPoint(float X, float Y)
        {
            Writer.Write($"R:{X},{Y};");
        }

        public void Flush() => Writer.Flush();

        public void Dispose()
        {
            Writer.Dispose();
            Stream.Dispose();
        }
    }
}