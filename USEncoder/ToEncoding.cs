using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USEncoder
{
    public class ToEncoding
    {
        public static byte[] ToSJIS(string utf8_str)
        {
            byte[] utf8_bytes = Encoding.UTF8.GetBytes(utf8_str);
            List<byte> sjis_bytes = new List<byte>();

            for (int i = 0; i < utf8_bytes.Length; i++)
            {
                byte a = utf8_bytes[i];
                if ((a >= 0xC2 && a <= 0xC3) || (a >= 0xCE && a <= 0xD1))
                {
                    // 2バイト文字
                    int code = a << 8;
                    code += utf8_bytes[++i];

                    EncodeSJIS(sjis_bytes, code);
                }
                else if (a >= 0xE2 && a <= 0xEF) 
                {
                    // 3バイト文字
                    int code = a << 16;
                    code += utf8_bytes[++i] << 8;
                    code += utf8_bytes[++i];

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
            int sjis_code = USEncoder.ToSJIS.GetCode(code);
            byte[] sjis = BitConverter.GetBytes(sjis_code);
            sjis_bytes.Add(sjis[1]);
            sjis_bytes.Add(sjis[0]);
        }

        public static byte[] ToUTF8(byte[] sjis_bytes)
        {
            List<byte> utf8_bytes = new List<byte>();

            for (int i = 0; i < sjis_bytes.Length; i++)
            {
                byte a = sjis_bytes[i];
                if (a >= 0x81 && a <= 0xEF)
                {
                    // 2バイト
                    int sjis_code = a << 8;
                    sjis_code += sjis_bytes[++i];

                    EncodeUTF8(utf8_bytes, sjis_code);
                }
                else
                {
                    // 1バイト
                    utf8_bytes.Add(a);
                }
            }

            return null;
        }

        static void EncodeUTF8(List<byte> utf8_bytes, int code)
        {
            int utf8_code = USEncoder.ToUTF8.GetCode(code);
            byte[] utf8 = BitConverter.GetBytes(utf8_code);

            byte a = utf8[0];
            if (utf8_code >= 0xE20000 && utf8_code <= 0xEF0000)
            {
                // 3バイト
                utf8_bytes.Add(utf8[2]);
                utf8_bytes.Add(utf8[1]);
                utf8_bytes.Add(utf8[0]);
            }
            else if ((utf8_code >= 0xC200 && utf8_code <= 0xC300) || (utf8_code >= 0xCE00 && utf8_code <= 0xD100))
            {
                // 2バイト
                utf8_bytes.Add(utf8[1]);
                utf8_bytes.Add(utf8[0]);
            }
            else
            {
                // 1バイト
                utf8_bytes.Add(utf8[0]);
            }
        }
    }
}
