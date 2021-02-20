using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RFile.Models;

namespace RFile
{
    public class RFileReader
    {
        public bool Load(Stream stream, out List<FRegion> regions, out Tuple<int, int> mapSize)
        {
            Dictionary<string, FRegion> regionindex = new Dictionary<string, FRegion>(StringComparer.InvariantCultureIgnoreCase);
            regions = new List<FRegion>();
            mapSize = new Tuple<int, int>(-1, -1);

            string RegionContext = null;
            string FlagContext = null;

            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, false, 128, true))
            {
                while (!reader.EndOfStream)
                {
                    StringBuilder bse = new StringBuilder();

                    if (!reader.ReadUntill(out string baseParam, out char breakout, ':', '[', ';')) return false;
                    baseParam = baseParam.ToUpper();
                    Console.WriteLine($"Param: '{baseParam}', Breakout: {breakout}");
                    if (baseParam == "R" && breakout == '[')
                    {
                        if (!reader.ReadUntill(out RegionContext, out _, ']')) return false;
                        reader.ReadUntill(out string extension, out _, ';');
                        if (!regionindex.ContainsKey(RegionContext)) regionindex.Add(RegionContext, new FRegion() { Name = RegionContext });
                    }
                    else if (baseParam == "R" && breakout == ':')
                    {
                        if (!reader.ReadUntill(out string strX, out _, ',') || !reader.ReadUntill(out string strY, out _, ';')) return false;

                        if (!float.TryParse(strX, out float x) || !float.TryParse(strY, out float y)) return false;
                        if (RegionContext == null) throw new Exception("Invalid region context!");

                        regionindex[RegionContext].Positions.Add(new Tuple<float, float>(x, y));
                    }
                    else if (baseParam == "MS" && breakout == '[')
                    {
                        if (!reader.ReadUntill(out string sXstr, out _, ',') || !reader.ReadUntill(out string sYstr, out _, ']')) return false;
                    }
                    else if (breakout != ';')
                    {
                        if (!reader.ReadUntill(out _, out _, ';')) return false;
                    }
                }

                regions = regionindex.Values.ToList();
                return true;
            }
        }
    }
}