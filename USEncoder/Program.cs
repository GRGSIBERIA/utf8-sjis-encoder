﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USEncoder
{
    class Program
    {
        const string str = "こんにちは日本";

        static void Main(string[] args)
        {
            TestToSJIS();
            TestToUTF8();
        }

        static void TestToUTF8()
        {
            var sjis = ConvertEncoding(Encoding.GetEncoding(932), str);
            var sjis_bytes = Encoding.GetEncoding(932).GetBytes(sjis);
            var utf8_bytes = ToEncoding.ToUTF8(sjis_bytes);
            var utf8 = Encoding.UTF8.GetString(utf8_bytes);
            var clone = ConvertEncoding(Encoding.UTF8, str);
            var clone_bytes = Encoding.UTF8.GetBytes(clone);


            Console.WriteLine("{0}, {1}", utf8_bytes.Length, clone_bytes.Length);
            if (utf8_bytes.Length != clone_bytes.Length) throw new IndexOutOfRangeException("長さが合ってない");
            for (int i = 0; i < utf8_bytes.Length; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", utf8_bytes[i], clone_bytes[i], utf8_bytes[i] == clone_bytes[i]);
            }
        }

        static string ConvertEncoding(Encoding dest, string str)
        {
            byte[] str_bytes = Encoding.Unicode.GetBytes(str);
            byte[] to_bytes = Encoding.Convert(Encoding.ASCII, dest, str_bytes);
            return dest.GetString(to_bytes);
        }

        static void TestToSJIS()
        {
            var utf8 = ConvertEncoding(Encoding.UTF8, str);
            var sjis_bytes = ToEncoding.ToSJIS(utf8);
            var clone = ConvertEncoding(Encoding.GetEncoding(932), str);
            var clone_bytes = Encoding.GetEncoding(932).GetBytes(clone);

            Console.WriteLine("{0}, {1}", sjis_bytes.Length, clone_bytes.Length);
            if (sjis_bytes.Length != clone_bytes.Length) throw new IndexOutOfRangeException("長さが合ってない");
            for (int i = 0; i < sjis_bytes.Length; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", sjis_bytes[i], clone_bytes[i], sjis_bytes[i] == clone_bytes[i]);
            }
            Console.WriteLine("--------------------------");
        }
    }
}
