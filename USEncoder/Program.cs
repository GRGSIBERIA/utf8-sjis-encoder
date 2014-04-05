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
            TestToSJIS();
            TestToUTF8();
        }

        static string ConvertEncoding(Encoding dest, string str)
        {
            byte[] str_bytes = Encoding.Unicode.GetBytes(str);
            byte[] to_bytes = Encoding.Convert(Encoding.Unicode, dest, str_bytes);
            return dest.GetString(to_bytes);
        }

        static void Check(byte[] src, byte[] clone)
        {
            Console.WriteLine("{0}, {1}", src.Length, clone.Length);
            for (int i = 0; i < src.Length; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", src[i], clone[i], src[i] == clone[i]);
            }
            Console.WriteLine("--------------------------");
        }

        static void TestToUTF8()
        {
            var sjis = ConvertEncoding(Encoding.GetEncoding(932), str);
            var sjis_bytes = Encoding.GetEncoding(932).GetBytes(sjis);
            var utf8_bytes = ToEncoding.ToUTF8(sjis_bytes);
            var clone = ConvertEncoding(Encoding.UTF8, str);
            var clone_bytes = Encoding.UTF8.GetBytes(clone);

            Check(utf8_bytes, clone_bytes);
        }

        static void TestToSJIS()
        {
            var utf8 = ConvertEncoding(Encoding.UTF8, str);
            var sjis_bytes = ToEncoding.ToSJIS(utf8);
            var clone = ConvertEncoding(Encoding.GetEncoding(932), str);
            var clone_bytes = Encoding.GetEncoding(932).GetBytes(clone);

            Check(sjis_bytes, clone_bytes);
        }
    }
}
