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
    public partial class UserControlExtendAI : UserControl
    {
        public UserControlExtendAI()
        {
            InitializeComponent();

            
            //combo.TextChanged += new EventHandler(combo_TextChanged);
            combo.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);

            BindSex();
            BindData();

            // 设置下拉列表框不可见

            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);
            text_Temp.Visible = false;
            text_Temp.WordWrap = false;

            this.dataGridView1.Controls.Add(combo);
            this.dataGridView1.Controls.Add(text_Temp);
            combo.Visible = false;
            text_Temp.Visible = false;
            //禁止用户改变DataGridView1の所有行的行高  
            dataGridView1.AllowUserToResizeRows = false;

            //列宽度设置
            //dataGridView1.Columns[columnUsedIndex].Width = 75;
            dataGridView1.Columns[columnVarIndex].Width = 175;
            //dataGridView1.Columns[columnChannelIndex].Width = 75;
            //dataGridView1.Columns[columnMinIndex].Width = 70;
            //dataGridView1.Columns[columnMaxIndex].Width = 70;
            dataGridView1.Columns[columnNoteIndex].Width = 200;



            //列太多，去掉最后一行填充表格
            //dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        //combbox列值，在datagridview里要显示对应字符串
        private void BindSex()
        {
            DataTable dtSex = new DataTable();
            dtSex.Columns.Add("Value");
            dtSex.Columns.Add("Name");
            DataRow drSex;
            drSex = dtSex.NewRow();
            drSex[0] = 0;
            drSex[1] = "0-10V";

            dtSex.Rows.Add(drSex);
            drSex = dtSex.NewRow();
            drSex[0] = 1;
            drSex[1] = "0-20mA";

            dtSex.Rows.Add(drSex);
            drSex = dtSex.NewRow();
            drSex[0] = 2;
            drSex[1] = "4-20mA";

            dtSex.Rows.Add(drSex);
            combo.ValueMember = "Value";
            combo.DisplayMember = "Name";
            combo.DataSource = dtSex;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        DataTable dtData = null;
        const int columnUsedIndex = 0;
        const int columnVarIndex = 1;
        const int columnChannelIndex = 2;
        const int columnAddressIndex = 3;
        const int columnTypeIndex = 4;
        const int columnFilterIndex = 5;
        const int columnMinIndex = 6;
        const int columnMaxIndex = 7;

        const int columnNoteIndex = 8;

        private RichTextBox text_Temp = new RichTextBox();

        private void BindData()
        {
            //view绑定datatable
            dtData = new DataTable();
            dtData.Columns.Add("已使用", typeof(bool));
            dtData.Columns.Add("变量名", typeof(string));
            dtData.Columns.Add("通道名");
            dtData.Columns.Add("地址");

            dtData.Columns.Add("类型");

            //dtData.Columns.Add("过滤个数",  typeof(int));
            //dtData.Columns.Add("过滤单位");

            dtData.Columns.Add("滤波时间");

            dtData.Columns.Add("最小值", typeof(int));
            dtData.Columns.Add("最大值", typeof(int));

            dtData.Columns.Add("注释");


            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "";
            drData[2] = "%IW20";
            drData[3] = "0-10mA";  //类型
            drData[4] = 0; //
            drData[5] = 10;    //滤波
            drData[6] = 0;
            drData[7] = 30000;
            drData[8] = 10000;

            //drData[5] = "注释1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "AI1";
            drData[2] = "%IW24";
            drData[3] = "0-20mA";
            drData[4] = 0;
            drData[5] = 10;    //滤波 10ms
            drData[6] = 0;
            drData[7] = 30000;
            drData[8] = 10000;

            //drData[5] = "注释2";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "AI2";
            drData[2] = "%IW28";
            drData[3] = "0-20mA";
            drData[4] = 0;
            drData[5] = 10;    //滤波
            drData[6] = 0;
            drData[7] = 30000;
            drData[8] = 10000;

            //drData[5] = "注释3";
            dtData.Rows.Add(drData);

            this.dataGridView1.DataSource = dtData;
        }

        #region 变量定义
        ComboBox combo = new ComboBox();
        int colIndex = 2, rowIndex = 0;
        #endregion

        #region 事件
        private void combo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.Rows[rowIndex].Cells[colIndex].Value.ToString() != combo.Text)
                {
                    dataGridView1.Rows[rowIndex].Cells[colIndex].Value = combo.SelectedItem;
                    combo.Visible = false;
                }
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCurrentIndex = this.combo.SelectedIndex;
            if (iCurrentIndex < 0) return;
            DataTable dt = (DataTable)combo.DataSource;
            DataRow dr = dt.Rows[iCurrentIndex];
            int ID = Int32.Parse(dr["Value"].ToString());
            string str = dr["Name"].ToString();

            dtData.Rows[rowIndex]["类型"] = /*ID*/str;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CurrentCellChanged(null, null);
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }


            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == columnTypeIndex)
                {
                    colIndex = dataGridView1.CurrentCell.ColumnIndex;
                    rowIndex = dataGridView1.CurrentCell.RowIndex;

                    setComboBoxItemType(colIndex, rowIndex);
                }
                else
                {
                    combo.Visible = false;
                }

                if(dataGridView1.CurrentCell.ColumnIndex == columnVarIndex
                    || dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = text_Temp.Text;
        }

        #endregion



        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            try
            {
                if(this.dataGridView1.CurrentCell.ColumnIndex == columnVarIndex
                    || this.dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    text_Temp.Left = rect.Left;
                    text_Temp.Top = rect.Top;
                    text_Temp.Width = rect.Width;
                    text_Temp.Height = rect.Height;
                    text_Temp.Visible = true;
                }

                if(dataGridView1.CurrentCell.ColumnIndex == columnTypeIndex)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    combo.Left = rect.Left;
                    combo.Top = rect.Top;
                    combo.Width = rect.Width;
                    combo.Height = rect.Height;
                    combo.Visible = true;
                }
            }
            catch
            {

            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            text_Temp.Visible = false;
            combo.Visible = false;

            if(dataGridView1.CurrentCell != null)
            {
                //dataGridView1.CurrentCell.Selected = false;
            }
            


        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[columnUsedIndex].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;


            dataGridView1.Columns[columnChannelIndex].ReadOnly = true;
            dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            combo.Visible = false;
            text_Temp.Visible = false;
        }

        private void setComboBoxItemType(int cIndex, int rIndex)
        {
            Rectangle rect = dataGridView1.GetCellDisplayRectangle(cIndex, rIndex, false);
            combo.Left = rect.Left;
            combo.Top = rect.Top;
            combo.Width = rect.Width;
            combo.Height = rect.Height;
            combo.Visible = true;

            //combo.Items.Clear();
            //combo.Items.Add("男");
            //combo.Items.Add("女");

            combo.BringToFront();
            combo.Text = dataGridView1.Rows[rIndex].Cells[cIndex].Value.ToString();
        }
    }
}
