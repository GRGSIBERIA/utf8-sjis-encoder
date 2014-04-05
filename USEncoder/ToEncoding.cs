using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USEncoder
{
    /// <summary>
    /// SJISとUTF-8の相互変換を行うクラス
    /// </summary>
    public class ToEncoding
    {
        /// <summary>
        /// UTF8からSJISへ変換します
        /// </summary>
        /// <param name="utf8_str">UTF-8にエンコードされた文字列</param>
        /// <returns>SJISに変換済みのbyte[]</returns>
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
                    uint code = (uint)a << 8;
                    code += utf8_bytes[++i];

                    EncodeSJIS(sjis_bytes, code);
                }
                else if (a >= 0xE2 && a <= 0xEF) 
                {
                    // 3バイト文字
                    uint code = (uint)a << 16;
                    code += (uint)utf8_bytes[++i] << 8;
                    code += (uint)utf8_bytes[++i];

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

        static void EncodeSJIS(List<byte> sjis_bytes, uint code)
        {
            uint sjis_code = USEncoder.ToSJIS.GetCode(code);
            byte[] sjis = BitConverter.GetBytes(sjis_code);
            sjis_bytes.Add(sjis[1]);
            sjis_bytes.Add(sjis[0]);
        }

        /// <summary>
        /// SJISからUTF-8へ変換します
        /// </summary>
        /// <param name="sjis_bytes">SJISにエンコードされたbyte[]</param>
        /// <returns>UTF-8に変換済みの文字列</returns>
        public static string ToUTF8(byte[] sjis_bytes)
        {
            List<byte> utf8_bytes = new List<byte>();

            for (int i = 0; i < sjis_bytes.Length; i++)
            {
                byte a = sjis_bytes[i];
                if ((a >= 0x81 && a <= 0xEF) || (a >= 0xA1 && a <= 0xDF))
                {
                    // 2バイト
                    uint sjis_code = (uint)a << 8;
                    sjis_code += sjis_bytes[++i];

                    EncodeUTF8(utf8_bytes, sjis_code);
                }
                else
                {
                    // 1バイト
                    utf8_bytes.Add(a);
                }
            }

            return Encoding.UTF8.GetString(utf8_bytes.ToArray());
        }

        static void EncodeUTF8(List<byte> utf8_bytes, uint code)
        {
            uint utf8_code = USEncoder.ToUTF8.GetCode(code);
            byte[] utf8 = BitConverter.GetBytes(utf8_code);

            // 何バイトなのか判別できないので，数値の大きさで決める
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
                // 1バイトってことにする
                utf8_bytes.Add(utf8[0]);
            }
        }
    }
}
