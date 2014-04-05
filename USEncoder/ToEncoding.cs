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
        public static byte[] ToSJIS(string unicode_str)
        {
            byte[] uni_bytes = Encoding.BigEndianUnicode.GetBytes(unicode_str); // 対応表がBigEndianだった
            List<byte> sjis_bytes = new List<byte>();

            for (int i = 0; i < uni_bytes.Length; i++)
            {
                ushort uni_code = (ushort)(uni_bytes[i] << 8);
                uni_code += uni_bytes[i + 1];

                ushort sjis_code = USEncoder.ToSJIS.GetCode(uni_code);
                sjis_bytes.Add((byte)(sjis_code & 0xFF00));
                if (sjis_code >= 0x8140)
                {
                    sjis_bytes.Add((byte)(sjis_code & 0xFF));
                    ++i;
                }
            }

            return sjis_bytes.ToArray();
        }

        public static string ToUnicode(byte[] sjis_bytes)
        {
            List<byte> uni_bytes = new List<byte>();

            for (int i = 0; i < sjis_bytes.Length; i++)
            {
                ushort sjis_code = (ushort)(sjis_bytes[i] << 8);
                sjis_code += sjis_bytes[i + 1];

                // まだ途中
            }

            return null;
        }
    }
}
