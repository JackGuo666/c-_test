using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.Base
{
    public partial class UserControlHighIn : UserControl
    {
        public UserControlHighIn()
        {
            InitializeComponent();

            BindData();

            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);
            text_Temp.Visible = false;
            text_Temp.WordWrap = false;

            dataGridView1.Controls.Add(text_Temp);

            //禁止用户改变DataGridView1の所有行的行高  
            dataGridView1.AllowUserToResizeRows = false;
            //列太多，去掉最后一行填充表格
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        # region
        DataTable dtData = null;
        const int columnUsedIndex = 0;
        const int columnVarIndex = 1;
        const int columnTypeIndex = 2;
        const int columnNoteIndex = 4;

        private RichTextBox text_Temp = new RichTextBox();

        #endregion


        private void BindData()
        {
            //view绑定datatable
            dtData = new DataTable();
            dtData.Columns.Add("已配置", typeof(bool));
            dtData.Columns.Add("变量名", typeof(string));
            dtData.Columns.Add("类型");
            //dtData.Columns.Add("配置");
            dtData.Columns.Add("注释");


            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "HSC0";
            drData[2] = "未配置";
            drData[3] = "";  //类型
            //drData[4] = 0; //


            //drData[5] = "注释1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "HSC1";
            drData[2] = "未配置";
            drData[3] = "";
            //drData[4] = 0;


            //drData[5] = "注释2";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "HSC2";
            drData[2] = "未配置";
            drData[3] = "";
            //drData[4] = 0;

            dtData.Rows.Add(drData);

            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "HSC3";
            drData[2] = "未配置";
            drData[3] = "";
            //drData[4] = 0;
            dtData.Rows.Add(drData);


            this.dataGridView1.DataSource = dtData;

            DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
            uninstallButtonColumn.Name = "配置";
            uninstallButtonColumn.Text = "...";

            uninstallButtonColumn.FlatStyle = FlatStyle.Standard;
            uninstallButtonColumn.CellTemplate.Style.BackColor = Color.White;
            uninstallButtonColumn.CellTemplate.Style.SelectionBackColor = Color.White;
            uninstallButtonColumn.CellTemplate.Style.ForeColor = Color.Black;
            uninstallButtonColumn.CellTemplate.Style.SelectionForeColor = Color.Black;

            //uninstallButtonColumn.FlatStyle = FlatStyle.Popup;
            //uninstallButtonColumn.DefaultCellStyle.ForeColor = Color.White;
            //uninstallButtonColumn.DefaultCellStyle.BackColor = Color.Lavender;
            //uninstallButtonColumn.DefaultCellStyle.SelectionBackColor = Color.Lavender;
            //uninstallButtonColumn.DefaultCellStyle.SelectionForeColor = Color.White;

            //显示按钮文本
            uninstallButtonColumn.UseColumnTextForButtonValue = true;


            int columnIndex = 2;
            if (dataGridView1.Columns["配置"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, uninstallButtonColumn);
            }

        }

        #region
        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.ColumnIndex == 2)
            //{
            //    e.Handled = true;

            //    using (SolidBrush brush = new SolidBrush(Color.Red))
            //    {
            //        e.Graphics.FillRectangle(brush, e.CellBounds);
            //    }
            //    ControlPaint.DrawBorder(e.Graphics, e.CellBounds, Color.Yellow, ButtonBorderStyle.Outset);
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["配置"].Index)
            {
                //Do something with your button.
                FormHighInput color = new FormHighInput();
                color.StartPosition = FormStartPosition.CenterScreen;
                color.ShowDialog();


                var row = e.RowIndex;
                var col = e.ColumnIndex;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = text_Temp.Text;
        }
        #endregion

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string varName = dataGridView1.CurrentCell.Value.ToString();

                    text_Temp.Text = this.dataGridView1.CurrentCell.Value.ToString();


                    text_Temp.Left = rect.Left;
                    text_Temp.Top = rect.Top;
                    text_Temp.Width = rect.Width;
                    text_Temp.Height = rect.Height;
                    text_Temp.Visible = true;
                    text_Temp.Focus();
                    //text_Temp.AutoSize = false;
                    this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
                    this.text_Temp.ScrollToCaret();
                }
                else
                {
                    text_Temp.Visible = false;
                }
            }
            catch
            {

            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    text_Temp.Left = rect.Left;
                    text_Temp.Top = rect.Top;
                    text_Temp.Width = rect.Width;
                    text_Temp.Height = rect.Height;
                    text_Temp.Visible = true;
                }
            }
            catch
            {

            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[columnUsedIndex].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;


            dataGridView1.Columns[columnVarIndex].ReadOnly = true; 
            dataGridView1.Columns[columnTypeIndex].ReadOnly = true;
        }
    }
}
