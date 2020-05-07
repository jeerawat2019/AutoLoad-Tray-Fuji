using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.Tool.Switch
{
    public class PBArray : Ai_PCSystem.Converts.Digatal.ToBinary
    {
         /// <summary>
        /// 
        /// </summary>
        private static string m_str = "";
        /// <summary>
        /// 
        /// </summary>
        private static int m_word = 0;
        /// <summary>
        /// 
        /// </summary>
        private static char[] m_index;
        /// <summary>
        /// 
        /// </summary>
        public static int DataDec = 0;
        /// <summary>
        /// 
        /// </summary>
        public static string DataBin = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ButtonTag"></param>
        /// <param name="StrBinary"></param>
        /// <returns></returns>
        public static string ArrayPBOnOffConvertToData(int ButtonTag)
        {
          
            //var indexbit = Enum.GetValues(typeof(DT_Brinary.Word)).OfType<DT_Brinary.Word>().ToArray()[ButtonTag];
            PBArray.Word[] result = (PBArray.Word[])Enum.GetValues(typeof(PBArray.Word));
            //Array resualt = Enum.GetValues(typeof(DT_Brinary.Word));
            DataDec = (int)result.GetValue(ButtonTag);
            ///
            DataBin = PBArray.dec2Bin(DataDec, 16);
            ///
            m_str = m_str.PadLeft(8, '0');
            ///
            m_index = m_str.ToCharArray();
            ///
            Array.Reverse(m_index);

            if (m_index[ButtonTag] == '0')
                /// 00000000 | 00000001
                m_word = m_word | GetEnumVaueWord()[ButtonTag];
            else
                /// 00000001 & 11111110
                m_word = m_word & ~GetEnumVaueWord()[ButtonTag];

            m_str = PBArray.GetIntBinaryString(m_word);
            ///
            return PBArray.bin2Hex(m_str.Substring(0, 32));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ButtonTag"></param>
        /// <returns></returns>
        public static string ArrayPBOnConvertToData(int ButtonTag)
        {
            //m_str = "";
            //var indexbit = Enum.GetValues(typeof(DT_Brinary.Word)).OfType<DT_Brinary.Word>().ToArray()[ButtonTag];
            PBArray.Word[] result = (PBArray.Word[])Enum.GetValues(typeof(PBArray.Word));
            //Array resualt = Enum.GetValues(typeof(DT_Brinary.Word));
            DataDec = (int)result.GetValue(ButtonTag);

            DataBin = PBArray.dec2Bin(DataDec, 16);
            ///
            m_str = m_str.PadLeft(16, '0');
            ///
            m_index = m_str.ToCharArray();
            ///
            Array.Reverse(m_index);

            if (m_index[ButtonTag] == '0')
                /// 00000000 | 00000001
                m_word = m_word | GetEnumVaueWord()[ButtonTag];

            m_str = PBArray.GetIntBinaryString(m_word);
            ///
            return PBArray.bin2Hex(m_str.Substring(0, 32));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ButtonTag"></param>
        /// <returns></returns>
        public static string ArrayPBOnConvertToDatas(int ButtonTag)
        {
            //var indexbit = Enum.GetValues(typeof(DT_Brinary.Word)).OfType<DT_Brinary.Word>().ToArray()[ButtonTag];
            PBArray.Word[] result = (PBArray.Word[])Enum.GetValues(typeof(PBArray.Word));
            //Array resualt = Enum.GetValues(typeof(DT_Brinary.Word));
            DataDec = (int)result.GetValue(ButtonTag);

            DataBin = PBArray.dec2Bin(DataDec, 16);

            //Array.Reverse(DataBin);

            return PBArray.bin2Dec(DataBin).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ButtonTag"></param>
        /// <returns></returns>
        public static string ArrayPBOffConvertToData(int ButtonTag)
        {
            //var indexbit = Enum.GetValues(typeof(DT_Brinary.Word)).OfType<DT_Brinary.Word>().ToArray()[ButtonTag];
            PBArray.Word[] result = (PBArray.Word[])Enum.GetValues(typeof(PBArray.Word));
            //Array resualt = Enum.GetValues(typeof(DT_Brinary.Word));
            DataDec = (int)result.GetValue(ButtonTag);
            ///
            DataBin = PBArray.dec2Bin(DataDec, 16);
            ///
            m_str = m_str.PadLeft(16, '0');
            ///
            m_index = m_str.ToCharArray();
            ///
            Array.Reverse(m_index);

            if (m_index[ButtonTag] == '1')
                /// 00000001 & 11111110
                m_word = m_word & ~GetEnumVaueWord()[ButtonTag];

            m_str = PBArray.GetIntBinaryString(m_word);
            ///
            return PBArray.bin2Hex(m_str.Substring(0, 32));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int[] GetEnumVaueWord()
        {
            Array enumValueArray = Enum.GetValues(typeof(PBArray.Word));

            int[] output = new int[enumValueArray.Length];
            for (int i = 0; i < enumValueArray.Length; i++)
            {
                output[i] = (int)enumValueArray.GetValue(i);
            }
            return output;
        }
    }  
}
