using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFile.Interfaces
{
    public interface IRegionFileWriter
    {
        void WriteMapContext(int x, int y);
        void WriteRegionContext(string regionName);
        void WriteRegionPoint(float X, float Y);
        void WriteFlagContext(string flagName);
        void WriteFlagValue(string key, string value);
    }
}
