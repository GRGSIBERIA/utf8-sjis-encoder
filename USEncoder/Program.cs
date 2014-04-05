using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USEncoder
{
    class Program
    {
        const string str = "こんにちは日本チルドレンchildren";

        static void Main(string[] args)
        {
            var a = Encoding.BigEndianUnicode.GetBytes("あ艦");
            foreach (var b in a)
                Console.WriteLine(b);
        }

    }
}
