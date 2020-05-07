using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.Converts.Data
{
    public class ToStr
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDec"></param>
        /// <returns></returns>
        public static string DecToHex1(string strDec)
        {
            string strHex = String.Format("{0:x2}", (uint)System.Convert.ToUInt32(strDec));
            return strHex.ToUpper();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strAscii"></param>
        /// <returns></returns>
        public static string hexToAscii(string strAscii)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= strAscii.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(strAscii.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i_asciiText"></param>
        /// <returns></returns>
        public static String convertAsciiTextToHex(String i_asciiText)
        {
            StringBuilder sBuffer = new StringBuilder();
            for (int i = 0; i < i_asciiText.Length; i++)
            {
                sBuffer.Append(Convert.ToInt32(i_asciiText[i]).ToString("x"));
            }
            return sBuffer.ToString().ToUpper();
        }

        /// <summary>
        /// Dec String -> Hex String
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecToHex(string s)
        {
            string DecToHex = "";
            try
            {
                //decimal decs = Decimal.Parse(s);
                long decs = Int64.Parse(s);
                DecToHex = Convert.ToString(decs, 16).PadLeft(2, '0');
                DecToHex = DecToHex.ToUpper();
            }
            catch (Exception ex)
            {
                DecToHex = ex.Message.ToString();
            }
            return DecToHex;
        }

        /// <summary>
        /// Hex String -> Dec String
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HexToDec(string s)
        {
            string HexToDec = "";
            try
            {
                HexToDec = Convert.ToInt64(s, 16).ToString();
            }
            catch (Exception ex)
            {
                HexToDec = ex.Message.ToString();
            }
            return HexToDec;
        }

        /// <summary>
        ///  Hex String -> Bin String
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] PackH(string hex)
        {
            if ((hex.Length % 2) == 1) hex += '0';
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        /// <summary>
        ///  Hex String -> Bin String
        /// </summary>
        /// <param name="hexvalue"></param>
        /// <returns></returns>
        public static string hex2binary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
            return binaryval;
        }

        /// <summary>
        /// Hex String -> Bin String
        /// </summary>
        /// <param name="strHex"></param>
        /// <param name="bit"></param>
        /// <returns>return result in specified length</returns>
        public static string hex2Bin(string strHex, int bit)
        {
            int decNumber = hex2Dec(strHex);
            return dec2Bin(decNumber).PadLeft(bit, '0');
        }

        /// <summary>
        /// Dec String -> Bin String
        /// </summary>
        /// <param name="val"></param>
        /// <param name="bit"></param>
        /// <returns>return result in specified length</returns>
        public static string dec2Bin(int val, int bit)
        {
            return Convert.ToString(val, 2).PadLeft(bit, '0');
        }

        /// <summary>
        /// Hex String -> Bin String
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        public string hex2Bin2(string strHex)
        {

            int decNumber = hex2Dec(strHex);
            return dec2Bin(decNumber);
        }
        /// <summary>
        /// Dec String -> Hex String
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string dec2Hex(int val)
        {
            return val.ToString("X");
        }
        /// <summary>
        /// Hex String -> Dec String
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        public static int hex2Dec(string strHex)
        {
            return Convert.ToInt16(strHex, 16);
        }
        /// <summary>
        /// Dec String -> Bin String
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string dec2Bin(int val)
        {
            return Convert.ToString(val, 2);
        }
        /// <summary>
        /// Bin String -> Dec String
        /// </summary>
        /// <param name="strBin"></param>
        /// <returns></returns>
        public static int bin2Dec(string strBin)
        {
            long l = Convert.ToInt64(strBin, 2);
            //int i = (int)l;
            return (int)l;
            //return Convert.ToInt16(strBin, 2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strBin"></param>
        /// <returns></returns>
        public static string bin2Hex(string strBin)
        {
            int decNumber = bin2Dec(strBin);

            return dec2Hex(decNumber);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        static String Binary2DecimalString(int[] bin)
        {
            double d = 0;

            for (int i = 0; i < bin.Length; i++)
                d += bin[i] * Math.Pow(2, bin.Length - i - 1);

            return d.ToString();
        }
    }
}
