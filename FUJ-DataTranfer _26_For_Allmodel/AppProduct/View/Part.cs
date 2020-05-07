using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ai_Product.Product;
using Ai_PCSystem.Strings;

namespace AppProduct.View
{
    public partial class Part : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public Part()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public Part(PartResult x)
        {
            InitializeComponent();
            ///
            ViewPart(x);
        }
        private delegate void _delViewPart(PartResult x);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        private void ViewPart(PartResult x)
        {
            ///
            if (this.InvokeRequired) {
                ///
                this.BeginInvoke(new _delViewPart(ViewPart), new object[] { x });
                return;
            }
            else {
                ///
                lbl2DCode.Text = string.Format("[{0}],{1}", x.PartId, x.trayInput.Piece2DCode);
                ///
                var strPartResult = Str_Enum.StringEnum.GetStringValue(x.PartStatus);
                ///
                lblStatus.Text = string.Format("{0}", strPartResult);
                ///
                switch (x.PartStatus) {

                    case Ai_Product.Ingredient.PartStatus.OK:
                        this.BackColor = Color.GreenYellow;
                        break;
                    case Ai_Product.Ingredient.PartStatus.NG:
                        this.BackColor = Color.Red;
                        break;
                    case Ai_Product.Ingredient.PartStatus.Error2DCode:
                        this.BackColor = Color.Brown;
                        break;
                    case Ai_Product.Ingredient.PartStatus.ErrorPosc:
                        this.BackColor = Color.Blue;
                        break;
                    case Ai_Product.Ingredient.PartStatus.ErrorPickUp:
                        this.BackColor = Color.Red;
                        break;
                    default:
                        break;
                }
                tableLayoutPanel1.Refresh();
                tableLayoutPanel1.Update();
            }
           
        }
    }
}
