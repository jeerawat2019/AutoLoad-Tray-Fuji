using Ai_PCSystem.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Product.Ingredient
{
    public enum PartStatus
    {
        [Str_Enum.StringValueAttribute("OK")]
        OK = 1,
        [Str_Enum.StringValueAttribute("NG")]
        NG = 2,
        [Str_Enum.StringValueAttribute("ERR 2DCODE")]
        Error2DCode = 3,
        [Str_Enum.StringValueAttribute("ERR POSC.")]
        ErrorPosc = 4,
        [Str_Enum.StringValueAttribute("ERR PICKUP")]
        ErrorPickUp = 5,
    }
     public class Part : Piece
     {
        
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Part"), Browsable(true), Description("Part Id")]
        public string PartId
        {
            get;
            set;
        } = "0";
        /// <summary>
        /// Part Index
        /// </summary>
        [Category("Part"), Browsable(true), Description("Part Index")]
        public string PartIndex
        {
            get;
            set;
        } = "0";
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("Part"), Browsable(true), Description("Part Status")]
        public PartStatus PartStatus
        {
            get;
            set;
        } = PartStatus.OK;
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("Part"), Browsable(true), Description("Part Status")]
        public bool IsPass
        {
            get
            {
                var result = this.PartStatus == PartStatus.OK ? true : false;
                return result;
            }

            set
            {
                var parser = value == true ? "OK" : "NG";
                
            }
        }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Part() { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public Part(string name)
            : base(name) { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public Part(string name, string partId)
            : base(name)
        {
            PartId = partId;
        }

    }
}

