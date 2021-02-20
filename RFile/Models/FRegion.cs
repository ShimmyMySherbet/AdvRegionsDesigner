using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFile.Models
{
    public class FRegion
    {
        public string Name;
        public List<Tuple<float, float>> Positions = new List<Tuple<float, float>>();
        public Dictionary<string, Dictionary<string, string>> FlagValues = new Dictionary<string, Dictionary<string, string>>();
    }
}
