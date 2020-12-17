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
    public partial class UserControlDI : UserControl
    {

        Dictionary<int, string> sexDic = new Dictionary<int, string>();
        Dictionary<string, int> sexReverseDic = new Dictionary<string, int>();
        // 定义下拉列表框
        private ComboBox cmb_Temp = new ComboBox();
        /// <summary>
        /// 绑定性别下拉列表框
        /// </summary>
        private void BindSex()
        {
            DataTable dtSex = new DataTable();
            dtSex.Columns.Add("Value");
            dtSex.Columns.Add("Name");
            DataRow drSex;
            drSex = dtSex.NewRow();
            drSex[0] = "1";
            drSex[1] = "男";

            sexDic.Add(1, "男");
            sexReverseDic.Add("男", 1);

            dtSex.Rows.Add(drSex);
            drSex = dtSex.NewRow();
            drSex[0] = "0";
            drSex[1] = "女";
            sexDic.Add(0, "女");
            sexReverseDic.Add("女", 0);

            dtSex.Rows.Add(drSex);
            cmb_Temp.ValueMember = "Value";
            cmb_Temp.DisplayMember = "Name";
            cmb_Temp.DataSource = dtSex;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BindData()
        {
            //view绑定datatable
            DataTable dtData = new DataTable();
            dtData.Columns.Add("ID");
            dtData.Columns.Add("Name");
            dtData.Columns.Add("Sex");
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "张三";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "李四";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 3;
            drData[1] = "王五";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 4;
            drData[1] = "小芳";
            drData[2] = "0";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 5;
            drData[1] = "小娟";
            drData[2] = "0";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 6;
            drData[1] = "赵六";
            drData[2] = "1";
            dtData.Rows.Add(drData);
            this.dataGridView1.DataSource = dtData;
        }


        private DataTable dt = new DataTable();
        private void InitDatable()
        {
            //新建列
            DataColumn col1 = new DataColumn("已使用", typeof(bool));
            DataColumn col2 = new DataColumn("通道名", typeof(string));
            DataColumn col3 = new DataColumn("地址", typeof(string));
            DataColumn col4 = new DataColumn("变量名", typeof(string));
            DataColumn col5 = new DataColumn("滤波", typeof(string));
            DataColumn col6 = new DataColumn("注释", typeof(string));
            DataGridViewComboBoxColumn col7 = new DataGridViewComboBoxColumn();
            col7.Items.Add("Admin");
            col7.Items.Add("Normal");
            col7.HeaderText = "test";

            //添加列
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            dt.Columns.Add(col5);
            dt.Columns.Add(col6);

            this.dataGridView1.DataSource = dt.DefaultView;

            this.dataGridView1.Columns.Add(col7);
            this.dataGridView1.Columns[6].DisplayIndex = 2;



        }

        void InitTableData()
        {

            dt.Rows.Clear();//清空数据

            //foreach (HumanInfo hi in list)
            {
                DataRow dr = dt.NewRow();//新增行
                dr[0] = true;
                dr[1] = "DI0";
                dr[2] = "%I0.0";
                dr[3] = "test";
                dr[4] = "3ms";
                dr[5] = "test";

                this.dt.Rows.Add(dr);//增加行

            }

            dataGridView1.DataSource = dt;
        }

        public UserControlDI(string name)
        {
            InitializeComponent();

            //InitDatable();
            //InitTableData();

            // 绑定性别下拉列表框
            BindSex();

            ////绑定数据表
            BindData();

            // 设置下拉列表框不可见
            cmb_Temp.Visible = false;

            //添加下拉列表框事件
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);

            //将下拉列表框加入到DataGridView控件中
            this.dataGridView1.Controls.Add(cmb_Temp);
            //this.dataGridView1.Columns[2].DisplayIndex = 0;
        }

        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "男")
            {
                dataGridView1.CurrentCell.Value = "男";
                dataGridView1.CurrentCell.Tag = "1";
            }
            else
            {
                dataGridView1.CurrentCell.Value = "女";
                dataGridView1.CurrentCell.Tag = "0";
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if(this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    sexValue = dataGridView1.CurrentCell.Tag.ToString();

                    if(sexReverseDic.ContainsKey(sexValue))
                    {
                        cmb_Temp.Text = sexValue;
                    }
                    else
                    {
                        int index = 0;
                        int.TryParse(sexValue, out index);
                        if(sexDic.ContainsKey(index))
                        {
                            cmb_Temp.Text = sexDic[index];
                            var table = (DataTable)dataGridView1.DataSource;
                            table.Rows[this.dataGridView1.CurrentCell.RowIndex][this.dataGridView1.CurrentCell.ColumnIndex] = index;
                        }
                    }

                    cmb_Temp.Left = rect.Left;
                    cmb_Temp.Top = rect.Top;
                    cmb_Temp.Width = rect.Width;
                    cmb_Temp.Height = rect.Height;
                    cmb_Temp.Visible = true;

                    cmb_Temp.FlatStyle = FlatStyle.Popup;
                }
                else
                {
                    cmb_Temp.Visible = false;
                }
            }
            catch
            {
            }
        }

        Dictionary<int, string> dicArr = new Dictionary<int, string>();
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value != null && dataGridView1.Rows[i].Cells[2].Tag == null
                    && dataGridView1.Rows[i].Cells[2].ColumnIndex == 2)
                {
                    if(sexReverseDic.ContainsKey(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    {
                        dataGridView1.Rows[i].Cells[2].Tag = sexReverseDic[dataGridView1.Rows[i].Cells[2].Value.ToString()];
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[2].Tag = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    }

                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "1")
                    {
                        dataGridView1.Rows[i].Cells[2].Value = "男";
                    }
                    else if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[2].Value = "女";
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)dataGridView1.DataSource;
            string str = table.Rows[0][0].ToString();
            int count = table.Rows.Count;
            for(int i = 0; i < count; i++)
            {
                str = table.Rows[i][0].ToString();
                str = table.Rows[i][1].ToString();
                str = table.Rows[i][2].ToString();

            }


        }
    }
}
