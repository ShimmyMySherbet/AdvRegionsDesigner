using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFile
{
    public static class Extensions
    {
        
        public static bool ReadUntill(this StreamReader reader, out string content, out char breakout, params char[] breakoutChars )
        {
            content = null;
            breakout = ' ';
            StringBuilder bse = new StringBuilder();

            while (true)
            {
                int r = reader.Read();
                if (r == -1) return false;
                char op = (char)r;
                if (breakoutChars.Contains(op))
                {
                    breakout = op;
                    break;
                }
                else
                {
                    bse.Append(op);
                }
            }
            content = bse.ToString();
            return true;
        }


    }
}
