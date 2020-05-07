using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Product.Ingredient.Model
{
    public class Tray : Piece
    {

        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Tray"), Browsable(true), Description("Tray Id")]
        public int TrayId
        {
            get;
            set;
        }
        /// <summary>
        /// Part Index
        /// </summary>
        [Category("Tray"), Browsable(true), Description("Tray Index")]
        public int TrayIndex
        {
            get;
            set;
        }
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("Tray"), Browsable(true), Description("Tray Status")]
        public string TrayStatus
        {
            get;
            set;
        }
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("Tray"), Browsable(true), Description("Tray Pass")]
        public bool IsPass
        {
            get
            {
                var result = this.TrayStatus == "OK" ? true : false;
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
        public Tray() { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public Tray(string name)
            : base(name) { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public Tray(string name, int trayId)
            : base(name)
        {
            TrayId = trayId;
        }

    }
}
