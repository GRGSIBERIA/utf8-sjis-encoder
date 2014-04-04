using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USEncoder
{
    public class ToSJISEncoder
    {
        public static byte[] Encode(string utf8_str)
        {
            byte[] utf8_bytes = Encoding.UTF8.GetBytes(utf8_str);
            List<byte> sjis_bytes = new List<byte>();

            for (int i = 0; i < utf8_bytes.Length; i++)
            {
                byte a = utf8_bytes[i];
                if (a == 0xC2 || a == 0xC3 || (a >= 0xCE && a <= 0xD1))
                {
                    // 2バイト文字
                    int code = a << 8;
                    code += utf8_bytes[i + 1];
                    i += 2;

                    EncodeSJIS(sjis_bytes, code);
                }
                else if (a >= 0xE2 && a <= 0xEF) 
                {
                    // 3バイト文字
                    int code = a << 16;
                    code += utf8_bytes[i + 1] << 8;
                    code += utf8_bytes[i + 2];
                    i += 3;

                    EncodeSJIS(sjis_bytes, code);
                }
                else
                {
                    // 1バイト文字
                    sjis_bytes.Add(a);
                }
            }

            return sjis_bytes.ToArray();
        }

        static void EncodeSJIS(List<byte> sjis_bytes, int code)
        {
            int sjis_code = ToSJIS.GetCode(code);
            sjis_bytes.Add((byte)(sjis_code & 0xFF00));
            sjis_bytes.Add((byte)(sjis_code & 0xFF));
        }
    }
}
