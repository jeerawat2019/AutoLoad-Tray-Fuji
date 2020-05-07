using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ai_PCSystem.Application.CSV
{

    public static class CSVUtlity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        public static void ExtractDataToCSV(DataGridView dgv)
        {

            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            //// Column headers
            //string columnsHeader = "";
            //for (int i = 0; i < dgv.Columns.Count; i++)
            //{
            //    columnsHeader += dgv.Columns[i].Name + ",";
            //}
            for (int i = 0; i < dgv.Columns.Count; i++)
                ///
                sb.Append(dgv.Columns[i].HeaderText + ",");
            //sb.Append(txtfileBuilder.m_HeaderLogs);
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.

                        sb.Append(dgvRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }
            }
            // Confirm to the user it has been completed.
            MessageBox.Show("CSV file saved.");
        }
        public static void ExtractDataToCSV(DataGridView dgv, string filename)
        {
            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            //// Column headers
            //string columnsHeader = "";
            //for (int i = 0; i < dgv.Columns.Count; i++)
            //{
            //    columnsHeader += dgv.Columns[i].Name + ",";
            //}
            for (int i = 0; i < dgv.Columns.Count; i++)
                ///
                sb.Append(dgv.Columns[i].HeaderText + ",");
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.

                        sb.Append(dgvRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // If they've selected a save location...
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, false))
            {
                // Write the stringbuilder text to the the file.
                sw.WriteLine(sb.ToString());
            }

            // Confirm to the user it has been completed.
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtDataTable"></param>
        /// <param name="strFilePath"></param>
        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


    }
}
