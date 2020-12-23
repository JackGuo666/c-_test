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
    public partial class UserControlExtendAO : UserControl
    {
        public UserControlExtendAO()
        {
            InitializeComponent();

            combo.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);


            bindType();
            BindData();

            this.dataGridView1.Controls.Add(combo);
            combo.Visible = false;
        }

        #region 变量定义
        ComboBox combo = new ComboBox();
        int colIndex = 2, rowIndex = 0;
        DataTable dtData = null;
        #endregion

        void bindType()
        {
            DataTable dtSex = new DataTable();
            dtSex.Columns.Add("Value");
            dtSex.Columns.Add("Name");
            DataRow drSex;
            drSex = dtSex.NewRow();
            drSex[0] = 0;
            drSex[1] = "0..10V";

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

        private void BindData()
        {
            //view绑定datatable
            dtData = new DataTable();
            dtData.Columns.Add("已使用", typeof(bool));
            dtData.Columns.Add("通道名");
            dtData.Columns.Add("地址");
            dtData.Columns.Add("类型");

            dtData.Columns.Add("最大值", typeof(int));
            dtData.Columns.Add("最小值", typeof(int));
            dtData.Columns.Add("故障预警值", typeof(int));

            dtData.Columns.Add("变量名");
            //dtData.Columns.Add("注释");


            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "AI0";
            drData[2] = "%IW20";
            drData[3] = "0-20mA";  //类型
            drData[4] = 0; //
            drData[5] = 10000;    //滤波
            drData[6] = 0;
            drData[7] = "";
            //drData[5] = "注释1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "AI1";
            drData[2] = "%IW24";
            drData[3] = "0-20mA";
            drData[4] = 0;
            drData[5] = 10000;    //滤波
            drData[6] = 0;
            drData[7] = "";
            //drData[5] = "注释2";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "AI2";
            drData[2] = "%IW28";
            drData[3] = "0-20mA";
            drData[4] = 0;
            drData[5] = 10000;    //滤波
            drData[6] = 0;
            drData[7] = "";
            //drData[5] = "注释3";
            dtData.Rows.Add(drData);

            this.dataGridView1.DataSource = dtData;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "类型")
            {
                colIndex = e.ColumnIndex;
                rowIndex = e.RowIndex;

                setComboBoxItemType(colIndex, rowIndex);
            }
            else
            {
                combo.Visible = false;
            }
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
