using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using AxDATABUILDERAXLibLB;
using DATABUILDERAXLibLB;

namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    public  class KV_Convert 
    {
        /// <summary>
        /// นำข้อมูลมาทำการ Convert เพื่อทำการเขียนลงใน PLC
        /// </summary>
        public  class CVSetToValve 
        {
            private DBValueConverterEx CV = new DBValueConverterEx();
            ///
            #region Set(Reading) Data int16(1Word) memory From PLC => Conversion Function
            /// <summary>
            /// Convert string to represent decimal 16-bit witchout sign to long variable.
            /// </summary>
            /// <param name="signedText"></param>
            /// <param name="Addres"></param>
            public int SignedTextToValve(string Addres,string signedText)
            {
                CommManager commManager = new CommManager();
                ///
                int iValve;
                CV.SignedTextToValue(signedText, out iValve);
                commManager.WriteDevice(Addres, iValve);
                commManager = null;
                return iValve;

            }
            /// <summary>
            /// Convert string to represent decimal 16-bit witchout sign to long variable.
            /// </summary>
            /// <param name="hexText"></param>
            /// <param name="Addres"></param>
            public int UnsignedTextToValue(string Addres, string unsignedText)
            {
                CommManager commManager = new CommManager();
                ///
                int iValve;
                CV.UnsignedTextToValue(unsignedText, out iValve);
                commManager.WriteDevice(Addres, iValve);
                commManager = null;
                return iValve;

            }
            /// <summary>
            /// Convert string to represent hexcimal 16-bit witchout sign to long variable.
            /// </summary>
            /// <param name="signed"></param>
            /// <param name="Addres"></param>
            public int HexTextToValue(string Addres , string hexText)
            {
                CommManager commManager = new CommManager();
                ///
                int iValve;
                CV.HexTextToValue(hexText, out iValve);
                commManager.WriteDevice(Addres, iValve);
                commManager = null;
                return iValve;

            }
            /// <summary>
            /// Convert string to represent 16-bit BCD to Long variable
            /// </summary>
            /// <param name="signed"></param>
            /// <param name="Addres"></param>
            public int BcdTextToValue(string Addres , string bcdText)
            {
                CommManager commManager = new CommManager();
                ///
                int iValve;
                CV.BcdTextToValue(bcdText, out iValve);
                commManager.WriteDevice(Addres, iValve);
                commManager = null;
                return iValve;

            }
            /// <summary>
            /// Convert string to represent binary 16-bit to Long variable
            /// </summary>
            /// <param name="signed"></param>
            /// <param name="Addres"></param>
            public int BinTextToValue(string Addres , string binText)
            {
                CommManager commManager = new CommManager();
                ///
                int iValve;
                CV.BinTextToValue(binText, out iValve);
                commManager.WriteDevice(Addres, iValve);
                commManager = null;
                return iValve;
            }
            #endregion

            #region Get(Reading) Data int32(2Word) memory From PLC => Conversion Function

            /// <summary>
            /// Convert string to represent decimal 32-bit with sign to number and to two long variable of the higher and lower digit
            /// </summary>
            /// <param name="strDM32"></param>
            /// <param name="strDML"></param>
            /// <param name="strDMH"></param>
            /// <param name="digit"></param>
            public bool UnsignedTextToWValue(string WunsignedText, string sAddress)
            {
                CommManager commManager = new CommManager();
                ///
                int unsignedValueL = 0, unsignedValueH = 0;
                ///
                CV.UnsignedTextToWValue(WunsignedText,
                                        out unsignedValueL, out unsignedValueH);
                int[] Valve = new int[] { unsignedValueL, unsignedValueH };
                for (int i = 0; i < Valve.Length; i++)
                    commManager.WriteDevice((int.Parse(sAddress) + i).ToString(), Valve[i]);
                commManager = null;
                return true;

            }



            /// <summary>
            ///  Convert string to represent decimal 32-bit with sign to number and to two long variable of the higher and lower digit
            /// </summary>
            /// <param name="Datafloat"></param>
            /// <param name="sAddress"></param>
            public bool SignedTextToWValue(string WsignedText, string sAddress)
            {
                CommManager commManager = new CommManager();
                ///
                int unsignedValueL = 0, unsignedValueH = 0;
                ///
                CV.SignedTextToWValue(WsignedText,
                                        out unsignedValueL, out unsignedValueH);
                int[] Valve = new int[] { unsignedValueL, unsignedValueH };
                for (int i = 0; i < Valve.Length; i++)
                    commManager.WriteDevice((int.Parse(sAddress) + i).ToString(), Valve[i]);
                commManager = null;
                return true;

            }
            /// <summary>
            ///  Convert string to represent hexcimal 32-bit to number and to two long variable of the higher and lower digit
            /// </summary>
            /// <param name="Datafloat"></param>
            /// <param name="sAddress"></param>
            public bool HexTextToWValue(string WHexText, string sAddress)
            {
                CommManager commManager = new CommManager();
                ///
                int unsignedValueL = 0, unsignedValueH = 0;
                ///
                CV.HexTextToWValue(WHexText,
                                        out unsignedValueL, out unsignedValueH);
                int[] Valve = new int[] { unsignedValueL, unsignedValueH };
                for (int i = 0; i < Valve.Length; i++)
                    commManager.WriteDevice((int.Parse(sAddress) + i).ToString(), Valve[i]);
                commManager = null;
                return true;

            }
            /// <summary>
            /// Convert string to represent 32-bit to number and to two long variable of the higher and lower digit
            /// </summary>
            /// <param name="Datafloat"></param>
            /// <param name="sAddress"></param>
            public bool BcdTextToWValue(string WBcdText, string sAddress)
            {
                CommManager commManager = new CommManager();
                ///
                int unsignedValueL = 0, unsignedValueH = 0;
                ///
                CV.BcdTextToWValue(WBcdText,
                                        out unsignedValueL, out unsignedValueH);
                int[] Valve = new int[] { unsignedValueL, unsignedValueH };
                for (int i = 0; i < Valve.Length; i++)
                    commManager.WriteDevice((int.Parse(sAddress) + i).ToString(), Valve[i]);
                commManager = null;
                return true;

            }
            /// <summary>
            /// Convert string to represent Binary 32-bit to number and to two long variable of the higher and lower digit
            /// </summary>
            /// <param name="Datafloat"></param>
            /// <param name="sAddress"></param>
            public bool BinTextToWValue(string WBinText, string sAddress)
            {
                CommManager commManager = new CommManager();
                ///
                int unsignedValueL = 0, unsignedValueH = 0;
                ///
                CV.BinTextToWValue(WBinText,
                                        out unsignedValueL, out unsignedValueH);
                int[] Valve = new int[] { unsignedValueL, unsignedValueH };
                for (int i = 0; i < Valve.Length; i++)
                    commManager.WriteDevice((int.Parse(sAddress) + i).ToString(), Valve[i]);
                commManager = null;
                return true;
            }
            #endregion

            
        }
        /// <summary>
        /// นำข้อมูลที่อ่านได้จาก PLC มาทำการ Convert เพื่อใช้ในการแสดงผล
        /// </summary>
        public  class CVGetToValve 
        {
            private  DBValueConverterEx CV = new DBValueConverterEx();

           
            #region Get(Reading) Data int16(1Word) memory From PLC => Conversion Function
            /// <summary>
            ///  Convert Long variable to decimal 16-bit format string witchout sign.
            /// </summary>
            /// <param name="iValve"></param>
            /// <returns></returns>
            public  string ValveToUnsignedText(int iValve)
            {
                string unsignedText;
                CV.ValueToUnsignedText(iValve, out unsignedText);
                return unsignedText;
            }
            /// <summary>
            ///  Convert Long variable to decimal 16-bit format string witch sign.
            /// </summary>
            /// <param name="iValve"></param>
            /// <returns></returns>
            public  string ValveToSignedText(int iValve)
            {
                string signedText;
                CV.ValueToSignedText(iValve, out signedText);
                return signedText;
            }
            /// <summary>
            ///   Convert Long variable to hexcimal 16-bit format string.
            /// </summary>
            /// <param name="iValve"></param>
            /// <returns></returns>
            public  string ValueToHexText(int iValve)
            {
                string hexText;
                CV.ValueToHexText(iValve, out hexText);
                return hexText;
            }
            /// <summary>
            ///  Convert Long variable to 1BCD format string
            /// </summary>
            /// <param name="iValve"></param>
            /// <returns></returns>
            public string ValueToBcdText(int iValve)
            {
                string bcdText;
                CV.ValueToBcdText(iValve, out bcdText);
                return bcdText;
            }
            /// <summary>
            ///   Convert Long variable to binary format string
            /// </summary>
            /// <param name="iValve"></param>
            /// <returns></returns>
            public string ValueToBinText(int iValve)
            {
                string binText;
                CV.ValueToBinText(iValve, out binText);
                return binText;
            }
            #endregion

            #region Get(Reading) Data int32(2Word) memory From PLC => Conversion Function
            /// <summary>
            /// Convert Long variable divid into the higher and lower to decimal 32-bit format string witchout sign
            /// </summary>
            /// <param name="iValveL"></param>
            /// <param name="iValveH"></param>
            /// <returns></returns>
            public string WValueToUnsignedText(int iValveL, int iValveH)
            {
                string unsignedText = string.Empty;
                ///
                CV.WValueToUnsignedText(iValveL, iValveH, out unsignedText);

                return unsignedText;
            }
            /// <summary>
            /// Convert Long variable divid into the higher and lower to decimal 32-bit format string witch sign
            /// </summary>
            /// <param name="intValveL"></param>
            /// <param name="intValveH"></param>
            /// <returns></returns>
            public string WValueToSignedText(int intValveL, int intValveH)
            {
                string signedText = string.Empty;
                ///
                CV.WValueToSignedText(intValveL, intValveH, out signedText);

                return signedText;
            }
            /// <summary>
            /// Convert Long variable divid into the higher and lower to hexcimal 32-bit format string.
            /// </summary>
            /// <param name="intValveL"></param>
            /// <param name="intValveH"></param>
            /// <returns></returns>
            public string WValueToHexText(int intValveL, int intValveH)
            {
                string hexText = string.Empty;
                ///
                CV.WValueToHexText(intValveL, intValveH, out hexText);

                return hexText;
            }
            /// <summary>
            /// Convert Long variable divid into the higher and lower to 32-bit BCD format string.
            /// </summary>
            /// <param name="intValveL"></param>
            /// <param name="intValveH"></param>
            /// <returns></returns>
            public string WValueToBcdText(int intValveL, int intValveH)
            {
                string bcdText = string.Empty;
                ///
                CV.WValueToBcdText(intValveL, intValveH, out bcdText);
                ///
                return bcdText;
            }
            /// <summary>
            /// Convert Long variable divid into the higher and lower to binary 32-bit format string.
            /// </summary>
            /// <param name="intValveL"></param>
            /// <param name="intValveH"></param>
            /// <returns></returns>
            public string WValueToBinText(int intValveL, int intValveH)
            {
                string binText = string.Empty;
                ///
                CV.WValueToBinText(intValveL, intValveH, out binText);

                return binText;
            }
            /// <summary>
            /// Convert Long variable divid into the higher and lower to single precision floating point real number format string.
            /// </summary>
            /// <param name="strDM32"></param>
            /// <param name="strDML"></param>
            /// <param name="strDMH"></param>
            /// <param name="digit"></param>
            public string WValueToFloatText(int iValveL, int iValveH)
            {
                string floatText;
                ///
                CV.WValueToFloatText(iValveL, iValveH, out floatText);

                //floatText = string.Format("{0:n3}", float.Parse(floatText));
                return string.Format("{0:n3}", float.Parse(floatText));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="kVData"></param>
            /// <returns></returns>
            public string FCV_ASCII(int[] kVData)
            {
                ///
                string[] hex = kVData.Select(x => ToHex(x)).ToArray();
                ///
                string sValve = String.Join("", hex.Select(x => x.ToString()));

                if (sValve.Length % 2 != 0)

                    sValve += "0";
                //string sValve = string.Join("", kVData.Select(bin =>bin.ToString("X")).ToArray());
              
                return HexadecimalEncoding.ConvertHexToString(sValve);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public string ToHex(int value)
            {
                return String.Format("{0:X}", value);//"0x{0:X}"
            }
            #endregion
        }
        public static class HexadecimalEncoding 
        {
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string ToHexString(string str)
            {
                var sb = new StringBuilder();

                var bytes = Encoding.Unicode.GetBytes(str);
                foreach (var t in bytes)
                {
                    sb.Append(t.ToString("X2"));
                }

                return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="hexString"></param>
            /// <returns></returns>
            public static string FromHexString(string hexString)
            {

                var bytes = new byte[hexString.Length / 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }

                return string.Join("", Encoding.Unicode.GetString(bytes).ToCharArray().Where(x => x != '\0')); // returns: "Hello world" for "48656C6C6F20776F726C64"
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="asciiString"></param>
            /// <returns></returns>
            public static string ConvertStringToHex(string asciiString)
            {
                string hex = "";
                foreach (char c in asciiString)
                {
                    int tmp = c;
                    hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                }
                return hex;
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="HexValue"></param>
            /// <returns></returns>
            public static string ConvertHexToString(string HexValue)
            {
                string StrValue = "";
                while (HexValue.Length > 0)
                {
                    StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                    HexValue = HexValue.Substring(2, HexValue.Length - 2);
                }
                return string.Join("", StrValue.ToCharArray().Where(x => x != '\0')); ;
            }
            /*public static void Main(string[] args)
            {
                string a = "Abc@1234";
                Console.WriteLine(a);
                string hex = ConvertStringToHex(a);
                Console.WriteLine(hex);
                Console.WriteLine(ConvertHexToString(hex));
            }*/

        }
    }
}
