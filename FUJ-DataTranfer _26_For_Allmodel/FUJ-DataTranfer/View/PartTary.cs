using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ai_Product.Ingredient.Model;
using Ai_Product.Product;

namespace FUJ_DataTranfer.View
{
    public partial class PartTary : UserControl
    {
        public PartTary()
        {
            InitializeComponent();
           
        }
        private delegate void _delCreateTrayComponent(Tray mPartTray1, string TrayId, List<PartResult> mPartList);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mPartTray1"></param>
        /// <param name="TrayId"></param>
        /// <param name="mPartList"></param>
        internal void CreateTrayComponent(Tray mPartTray1, string TrayId, List<PartResult> mPartList)
        {
            
            ///
            int i = 0;
            ///
            if (this.InvokeRequired) {
                //this.BeginInvoke(new _delBtnEventClick(btnStartRun_Click), sender, e);
                this.BeginInvoke(new _delCreateTrayComponent(CreateTrayComponent), new object[] { mPartTray1, TrayId, mPartList });
                return;
            }
            else {
                mPartList.ForEach(x =>
                    {
                        x.PartId = (i++).ToString();
                        ///
                        flpPartTray.Controls.Add(new AppProduct.View.Part(x));
                    });
                return;
                ///
            }
        }
        private delegate void _delAddResult2DCodeData(List<string> mList2DCodeFormPLC);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mList2DCodeFormPLC"></param>
        internal void AddResult2DCodeData(List<string> mList2DCodeFormPLC)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            ///
            //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            ///
            //dataGridView1.ColumnHeadersHeight = 50;
            ///
            
            ///
            if (this.InvokeRequired) {
                //this.BeginInvoke(new _delBtnEventClick(btnStartRun_Click), sender, e);
                this.BeginInvoke(new _delAddResult2DCodeData(AddResult2DCodeData), new object[] { mList2DCodeFormPLC });
                return;
            }
            else {
                mList2DCodeFormPLC.ForEach(x =>
                    {
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count.ToString(), x.ToString());
                    });

            }
        }
        private delegate void _delClearPart2DCode();
        internal void ClearPart2DCode()
        {
            ///
            if (this.InvokeRequired) {
                //this.BeginInvoke(new _delBtnEventClick(btnStartRun_Click), sender, e);
                this.BeginInvoke(new _delClearPart2DCode(ClearPart2DCode));
                return;
            }
            else {
                ///
                if (dataGridView1.Rows.Count > 1) {   ///
                    ///
                    dataGridView1.DataSource = null;
                    ///
                    dataGridView1.Rows.Clear();
                    ///
                    dataGridView1.Refresh();
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void ClearPartTrayComponent()
        {
            if (InvokeRequired) {
                // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    flpPartTray.Controls.Clear();
                }));
            }
        }
    }
}
