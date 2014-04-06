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
            {
                byte[] sjis_byte = ToEncoding.ToSJIS(str);
                byte[] clone = Encoding.Convert(Encoding.GetEncoding(932), Encoding.Unicode, sjis_byte);
                string clone_str = Encoding.Unicode.GetString(clone);
                Console.WriteLine(clone_str);
                for (int i = 0; i < sjis_byte.Length; i++)
                {
                    Console.WriteLine("{0:x}", sjis_byte[i]);
                }
            }

            {
                byte[] sjis_byte = ToEncoding.ToSJIS(str);
                string uni_str = ToEncoding.ToUnicode(sjis_byte);
                Console.WriteLine(uni_str);
            }
        }

    }
}
