using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.Converts.Digatal
{
    public class ToBinary : Ai_PCSystem.Converts.Data.ToStr
    {

        //     int a = 60;	           /* 60 = 0011 1100 */  
        //     int b = 13;	           /* 13 = 0000 1101 */
        //     int c = 0;           

        //     c = a & b;           /* 12 = 0000 1100 */ 
        //     Console.WriteLine("Line 1 - Value of c is {0}", c );

        //     c = a | b;           /* 61 = 0011 1101 */
        //     Console.WriteLine("Line 2 - Value of c is {0}", c);

        //     c = a ^ b;           /* 49 = 0011 0001 */
        //     Console.WriteLine("Line 3 - Value of c is {0}", c);

        //     c = ~a;               /*-61 = 1100 0011 */
        //     Console.WriteLine("Line 4 - Value of c is {0}", c);

        //     c = a << 2;     /* 240 = 1111 0000 */
        //     Console.WriteLine("Line 5 - Value of c is {0}", c);

        //     c = a >> 2;     /* 15 = 0000 1111 */
        //     Console.WriteLine("Line 6 - Value of c is {0}", c);
        /// <summary>
        /// 
        /// </summary>
        [Flags]
        enum Days
        {
            None = 0,
            Sunday = 1,
            Monday = 1 << 1,   // 2
            Tuesday = 1 << 2,   // 4
            Wednesday = 1 << 3,   // 8
            Thursday = 1 << 4,   // 16
            Friday = 1 << 5,   // etc.
            Saturday = 1 << 6,
            Weekend = Saturday | Sunday,
            Weekdays = Monday | Tuesday | Wednesday | Thursday | Friday
        }
        /// <summary>
        /// 
        /// </summary>
        enum Flags
        {
            First = 0,
            Second = 1,
            Third = 2,
            SecondAndThird = 3
        }
        // later ...
        //Debug.Assert((Flags.Second | Flags.Third) == Flags.SecondAndThird);
        [Flags]
        public enum Word
        {
            Bit00 = 0x01,   // 00000001
            Bit01 = 0x02,   // 00000010
            Bit02 = 0x04,   // 00000100
            Bit03 = 0x08,   // 00001000
            Bit04 = 0x10,   // 00010000
            Bit05 = 0x20,   // 00100000
            Bit06 = 0x40,   // 01000000
            Bit07 = 0x80,   // 10000000
            Bit08 = 0x100,  // 0000000100000000
            Bit09 = 0x200,  // 0000001000000000
            Bit10 = 0x400,  // 0000010000000000
            Bit11 = 0x800,  // 0000100000000000
            Bit12 = 0x1000, // 0001000000000000
            Bit13 = 0x2000, // 0010000000000000
            Bit14 = 0x4000, // 0100000000000000
            Bit15 = 0x8000, // 1000000000000000


        }
        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum BitAnd
        {
            Bit0 = 0xF7, // 00000001    0xFE
            Bit1 = 0xFD, // 00000010    0xFD
            Bit2 = 0xFB, // 00000100    0xFB
            Bit3 = 0xF7, // 00001000    0xF7
            Bit4 = 0xEF, // 00010000    0xEF
            Bit5 = 0xDF, // 00100000    0xDF
            Bit6 = 0xBF, // 01000000    0xBF
            Bit7 = 0x7F, // 10000000    0x7F
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitField"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int GetBit(int bitField, int index)
        {
            return (bitField / (int)Math.Pow(10, index)) % 10;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitField"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetFlag(int bitField, int index)
        {
            return GetBit(bitField, index) == 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetIntBinaryString(int n)
        {


            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetIntBinaryStringRemoveZeros(int n)
        {
            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b).TrimStart('0');
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brinary"></param>
        /// <returns></returns>
        public static char[] ReverseBit(char[] brinary)
        {

            int k = 16 - brinary.Length;
            char[] create = new char[k];

            Array.Reverse(brinary);
            for (int i = 0; i < k; i++)
                create[i] = '0';

            var list = new List<char>();

            list.AddRange(brinary);//create
            list.AddRange(create);//brinary

            // ::: Call ToArray to convert List to array
            // char[] Out = list.ToArray();
            return list.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bytes"></param>
        /// <param name="strBin"></param>
        public static List<char[]> ConvertStrToCharArray(int Bytes, string strBin)
        {
            List<char[]> chaBin = new List<char[]>();

            if (string.IsNullOrEmpty(strBin))
                throw new Exception("KVBinary<ConvertStrToCharArray> No data string binary");
            else
            {
                for (int i = 0; i < strBin.Length / Bytes; i++)// for (int i = strBin.Length / Bytes; i >0 ; i--)

                    chaBin.Add(strBin.Substring(((i * Bytes)), Bytes).ToCharArray());//((i-1) * Bytes), Bytes
            }
            return chaBin;
        }

    }

}
