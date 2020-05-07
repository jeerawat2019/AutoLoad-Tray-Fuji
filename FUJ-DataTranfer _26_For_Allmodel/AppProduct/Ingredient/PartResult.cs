using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Product.Product
{
    public class PartResult : Ingredient.Part
    {
        //var type = properties.GetValue(PartResult, null);
        public PartResult()
        {
        }
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("PartResult"), Browsable(true), Description("TrayInput")]
        //public Dictionary<string, object> mTrayInput { get;set; } = new Dictionary<string, object>()
        //{
        //    { "TrayInputData", new TrayInput() }
        //};
        public TrayInput trayInput
        {
            get;
            set;
        } = new TrayInput();
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("PartResult"), Browsable(true), Description("AV1")]
        public AVI1 mAV1
        {
            get;
            set;
        } = new AVI1();
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("PartResult"), Browsable(true), Description("AV2")]
        public AVI2 mAV2
        {
            get;
            set;
        } = new AVI2();
        /// <summary>
        /// Part Id
        /// </summary>
        //[Category("PartResult"), Browsable(true), Description("ReScreen")]
        //public ReScreen mRescreen
        //{
        //    get;
        //    set;
        //} = new ReScreen();
        ///// <summary>
        ///// Part Id
        ///// </summary>
        //[Category("PartResult"), Browsable(true), Description("PreProcess")]
        //public Pre_Process mPreProcess
        //{
        //    get;
        //    set;
        //} = new Pre_Process();
        ///// <summary>
        ///// Part Id
        ///// </summary>
        //[Category("PartResult"), Browsable(true), Description("TrayIn")]
        //public TrayInMCRecord mTrayInMCRecord
        //{
        //    get;
        //    set;
        //} = new TrayInMCRecord();
        ///// <summary>
        ///// Part Id
        ///// </summary>
        //[Category("PartResult"), Browsable(true), Description("Judgment")]
        //public Judgement mJudgement
        //{
        //    get;
        //    set;
        //} = new Judgement();

        #region class TrayInput
        public class TrayInput
        {
            public TrayInput()
            {
            }
            /// <summary>
            /// Part Id
            /// </summary>
            [Category("TrayInput"), Browsable(true), Description("Piece2DCode")]
            public string Piece2DCode
            {
                get;
                set;
            }= "";//THA911102HV212850
                  /// <summary>
                  /// Part Id
                  /// </summary>
            [Category("TrayInput"), Browsable(true), Description("TrayCodeAVI1")]
            public string TrayCodeAVI1
            {
                get;
                set;
            } = "";//0001TRAY-AVI1-RGPZ067A
                   /// <summary>
                   /// Part Id
                   /// </summary>
            [Category("TrayInput"), Browsable(true), Description("TrayCodeAVI2")]
            public string TrayCodeAVI2
            {
                get;
                set;
            } = "";//"0001TRAY-AVI2-RGPZ067A
                   /// <summary>
                   /// Part Id
                   /// </summary>
            [Category("TrayInput"), Browsable(true), Description("AVITrayPosition")]
            public string AVITrayPosition
            {
                get;
                set;
            } = "";//111
           
                   /// <summary>
                   /// Part Id
                   /// </summary>
            //[Category("TrayInput"), Browsable(true), Description("ShippingTrayPosition")]
            //public string ShippingTrayPosition
            //{
            //    get;
            //    set;
            //} = "";//222;
            //      /// <summary>
            //      /// Part Id
            //      /// </summary>
            [Category("TrayInput"), Browsable(true), Description("ProductName")]
            public string ProductName
            {
                get;
                set;
            } = "";//Product Name	RGPZ-067MW-A
                   /// <summary>
                   /// 
                   /// </summary>
            [Category("TrayInput"), Browsable(true), Description("StartTime")]
            public string StartTime
            {
                get;
                set;
            } = "";//YYYY.MM.DD.HH.MM.SS
            /// <summary>
            /// 
            /// </summary>
            [Category("TrayInput"), Browsable(true), Description("JudgeTotal")]
            public string Judge
            {
                get;
                set;
            } = "";//Judge Total
            /// <summary>
            /// Part Id
            /// </summary>
            [Category("TrayInput"), Browsable(true), Description("JudgeTotal")]
            public string JudgeTotal
            {
                get;
                set;
            } = "";//Judge Total
                   /// <summary>
                   /// Part Id
                   /// </summary>
           
                   /// <summary>
                   /// Part Id
                   /// </summary>
            //[Category("TrayInput"), Browsable(true), Description("EndTime")]
            //public string EndTime
            //{
            //    get;
            //    set;

            //} = "";//YYYY.MM.DD.HH.MM.SS
        }
        #endregion

        #region Ingredient.Item
        public class AVI1 : Ingredient.Item
        {
            public AVI1() { }
        }
        public class AVI2 : Ingredient.Item
        {
            public AVI2() { }
        }
        //public class ReScreen : Ingredient.Item
        //{
        //    public ReScreen() { }
        //}
        //public class Pre_Process : Ingredient.Item
        //{
        //    public Pre_Process() { }
        //}
        //public class TrayInMCRecord : Ingredient.Item
        //{
        //    public TrayInMCRecord() { }
        //}
        //public class Judgement
        //{
        //    public Judgement()
        //    {

        //    }
        //    /// <summary>
        //    /// Part Id
        //    /// </summary>
        //    [Category("Result"), Browsable(true), Description("item")]
        //    public List<ItemResult> items
        //    {
        //        get;
        //        set;
        //    } = new List<ItemResult>();
        //}
        #endregion
    }
    #region class ItemResult
    public class ItemResult
    {
        public ItemResult()
        {

        }
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Result"), Browsable(true), Description("Name")]
        public string Name
        {
            get;
            set;
        } = "";//FParams[900]
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Result"), Browsable(true), Description("Judge")]
        public string Judge
        {
            get;
            set;
        } = "";//OK
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Result"), Browsable(true), Description("Data")]
        public string Data
        {
            get;
            set;
        } = "";//99999999
    } 
    #endregion
}
