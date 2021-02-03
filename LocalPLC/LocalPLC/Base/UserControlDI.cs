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
        //
        private RichTextBox text_Temp = new RichTextBox();
        const int columnVarIndex = 1;
        const int columnNoteIndex = 5;
        const int columnFilterIndex = 2;
        const int columnChannelIndex = 3;
        const int columnAddressIndex = 4;
        public void initData()
        {
            string diType = "";
            foreach(var modbule in UserControlBase.dataManage.deviceInfoElem.connector.moduleList)
            {
                if(modbule.baseName == "DI")
                {
                    var moduleID = modbule.moduleID;

                    foreach(var moduleElem in UserControlBase.dataManage.modules.list)
                    {
                        var paraList = moduleElem.connectModules.list;
                        if(moduleElem.moduleID == moduleID)
                        {
                            foreach (var para in paraList)
                            {
                                string type = para.type;
                                string[] strArr = type.Split(new Char[] { ':' });
                                if (strArr.Length == 2)
                                {
                                    //串口type localTypes
                                    string localType = strArr.ElementAt(0);
                                    diType = strArr.ElementAt(1);

                                    break;
                                }
                            }
                        }

                    }
                }
            }

            if (UserControlBase.dataManage.dicBitfield.ContainsKey(diType))
            {
                dtData.Clear();
                var value = UserControlBase.dataManage.dicBitfield[diType];
                int count = 0;
                foreach(var elem in value.list)
                {
                    xml.DIData diData = new xml.DIData();

                    DataRow drData;
                    drData = dtData.NewRow();

                    diData.used = false;
                    drData[0] = diData.used;

                    diData.varName = "";
                    drData[1] = diData.varName;

                    diData.filterTime = 3;
                    drData[2] = diData.filterTime;  //单位ms

                    diData.channelName = elem.name;
                    drData[3] = diData.channelName;

                    string ioAddress = string.Format("%IX{0}.{1}", ConstVariable.DIADDRESSIO
                        , count);
                    diData.address = ioAddress;
                    drData[4] = diData.address;

                    diData.note = "";
                    drData[5] = diData.note;
                                       //drData[5] = "注释1";

                    dtData.Rows.Add(drData);
                    UserControlBase.dataManage.diList.Add(diData);
                    count++;
                }

                this.dataGridView1.DataSource = dtData;
            }
        }

        public void refreshData()
        {
            dtData.Clear();
            foreach (var diData in UserControlBase.dataManage.diList)
            {
                DataRow drData;
                drData = dtData.NewRow();

                drData[0] = diData.used;
                drData[1] = diData.varName;
                drData[2] = diData.filterTime;  //单位ms
                drData[3] = diData.channelName;
                drData[4] = diData.address;
                drData[5] = diData.note;
                //drData[5] = "注释1";

                dtData.Rows.Add(drData);
            }

            this.dataGridView1.DataSource = dtData;
        }

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
            drSex[0] = "0";
            drSex[1] = "无滤波器";

            sexDic.Add(0, "无滤波器");
            sexReverseDic.Add("无滤波器", 0);

            dtSex.Rows.Add(drSex);
            drSex = dtSex.NewRow();
            drSex[0] = "3";
            drSex[1] = "3ms";
            sexDic.Add(3, "3ms");
            sexReverseDic.Add("3ms", 3);

            dtSex.Rows.Add(drSex);
            drSex = dtSex.NewRow();
            drSex[0] = "12";
            drSex[1] = "12ms";
            sexDic.Add(12, "12ms");
            sexReverseDic.Add("12ms", 12);

            dtSex.Rows.Add(drSex);
            cmb_Temp.ValueMember = "Value";
            cmb_Temp.DisplayMember = "Name";
            cmb_Temp.DataSource = dtSex;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public DataTable dtData = new DataTable();
        private void BindData()
        {
            //view绑定datatable
            //DataTable dtData = new DataTable();
            dtData.Columns.Add("已使用", typeof(bool));
            dtData.Columns.Add("变量名");
            dtData.Columns.Add("滤波");
            dtData.Columns.Add("通道名");
            dtData.Columns.Add("地址");
            dtData.Columns.Add("注释");

            //DataRow drData;
            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "DI0";
            //drData[2] = "%IX0.0";
            //drData[3] = "";
            //drData[4] = "0";    //滤波
            ////drData[5] = "注释1";
            //dtData.Rows.Add(drData);
            //drData = dtData.NewRow();
            //drData[0] = 2;
            //drData[1] = "DI1";
            //drData[2] = "%IX0.1";
            //drData[3] = "";
            //drData[4] = "3";
            ////drData[5] = "注释2";
            //dtData.Rows.Add(drData);
            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "DI2";
            //drData[2] = "%IX0.2";
            //drData[3] = "";
            //drData[4] = "12";
            ////drData[5] = "注释3";
            //dtData.Rows.Add(drData);
            
            this.dataGridView1.DataSource = dtData;
        }


        //private DataTable dt = new DataTable();
        //private void InitDatable()
        //{
        //    //新建列
        //    DataColumn col1 = new DataColumn("已使用", typeof(bool));
        //    DataColumn col2 = new DataColumn("通道名", typeof(string));
        //    DataColumn col3 = new DataColumn("地址", typeof(string));
        //    DataColumn col4 = new DataColumn("变量名", typeof(string));
        //    DataColumn col5 = new DataColumn("滤波", typeof(string));
        //    DataColumn col6 = new DataColumn("注释", typeof(string));
        //    DataGridViewComboBoxColumn col7 = new DataGridViewComboBoxColumn();
        //    col7.Items.Add("Admin");
        //    col7.Items.Add("Normal");
        //    col7.HeaderText = "test";

        //    //添加列
        //    dt.Columns.Add(col1);
        //    dt.Columns.Add(col2);
        //    dt.Columns.Add(col3);
        //    dt.Columns.Add(col4);
        //    dt.Columns.Add(col5);
        //    dt.Columns.Add(col6);

        //    this.dataGridView1.DataSource = dt.DefaultView;

        //    this.dataGridView1.Columns.Add(col7);
        //    this.dataGridView1.Columns[6].DisplayIndex = 2;



        //}

        //void InitTableData()
        //{

        //    dt.Rows.Clear();//清空数据

        //    //foreach (HumanInfo hi in list)
        //    {
        //        DataRow dr = dt.NewRow();//新增行
        //        dr[0] = true;
        //        dr[1] = "DI0";
        //        dr[2] = "%I0.0";
        //        dr[3] = "test";
        //        dr[4] = "3ms";
        //        dr[5] = "test";

        //        this.dt.Rows.Add(dr);//增加行

        //    }

        //    dataGridView1.DataSource = dt;
        //}

        public UserControlDI(string name)
        {
            InitializeComponent();

            //InitDatable();
            //InitTableData();

            // 绑定性别下拉列表框
            //BindSex();

            ////绑定数据表
            BindData();

            // 设置下拉列表框不可见
            cmb_Temp.Visible = false;

            //添加下拉列表框事件
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);

            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);

            text_Temp.Visible = false;
            text_Temp.WordWrap = false;
            text_Temp.ScrollBars = RichTextBoxScrollBars.None;
            //text_Temp.setParent(dataGridView1);


            //将下拉列表框加入到DataGridView控件中
            this.dataGridView1.Controls.Add(cmb_Temp);
            this.dataGridView1.Controls.Add(text_Temp);

            //最后一列自动填充表格
            dataGridView1.Columns[columnVarIndex].Width = 200;



            //禁止用户改变DataGridView1の所有行的行高  
            dataGridView1.AllowUserToResizeRows = false;
            //最后一列填充表格
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //this.dataGridView1.Columns[2].DisplayIndex = 1;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sexReverseDic.ContainsKey(((ComboBox)sender).Text))
            {
                dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;
                dataGridView1.CurrentCell.Tag = sexReverseDic[((ComboBox)sender).Text];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentCell.Value.ToString() == text_Temp.Text)
            {
                //
                UserControl1.UC.checkVarName(text_Temp.Text);

                dataGridView1.CurrentCell.Value = text_Temp.Text;
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
                if(this.dataGridView1.CurrentCell.ColumnIndex == columnVarIndex ||
                    this.dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
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


                //if (this.dataGridView1.CurrentCell.ColumnIndex == columnFilterIndex)
                //{
                //    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                //    string sexValue = dataGridView1.CurrentCell.Value.ToString();

                //    if(sexReverseDic.ContainsKey(sexValue))
                //    {
                //        cmb_Temp.Text = sexValue;
                //    }
                //    else
                //    {
                //        int index = 0;
                //        int.TryParse(sexValue, out index);
                //        if(sexDic.ContainsKey(index))
                //        {
                //            cmb_Temp.Text = sexDic[index];
                //            var table = (DataTable)dataGridView1.DataSource;
                //            table.Rows[this.dataGridView1.CurrentCell.RowIndex][this.dataGridView1.CurrentCell.ColumnIndex] = index;
                //        }
                //    }

                //    cmb_Temp.Left = rect.Left;
                //    cmb_Temp.Top = rect.Top;
                //    cmb_Temp.Width = rect.Width;
                //    cmb_Temp.Height = rect.Height;
                //    cmb_Temp.Visible = true;

                //    cmb_Temp.FlatStyle = FlatStyle.Popup;
                //}
                //else
                //{
                //    cmb_Temp.Visible = false;
                //}
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
                if (dataGridView1.Rows[i].Cells[4].Value != null && dataGridView1.Rows[i].Cells[4].Tag == null
                    && dataGridView1.Rows[i].Cells[4].ColumnIndex == 4)
                {
                    string value = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    if (sexReverseDic.ContainsKey(dataGridView1.Rows[i].Cells[4].Value.ToString()))
                    {
                        dataGridView1.Rows[i].Cells[4].Tag = sexReverseDic[dataGridView1.Rows[i].Cells[4].Value.ToString()];
                    }
                    //else
                    //{
                    //    dataGridView1.Rows[i].Cells[4].Tag = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    //}

                    int index = -1;
                    bool isNum = int.TryParse(value, out index);
                    if(isNum)
                    {
                        if (sexDic.ContainsKey(index))
                        {
                            dataGridView1.Rows[i].Cells[4].Value = sexDic[index];
                            dataGridView1.Rows[i].Cells[4].Tag = index.ToString();
                        }
                    }


                    //if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "1")
                    //{
                    //    dataGridView1.Rows[i].Cells[4].Value = "男";
                    //}
                    //else if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "0")
                    //{
                    //    dataGridView1.Rows[i].Cells[4].Value = "女";
                    //}
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

                str = table.Rows[i][3].ToString();
                str = table.Rows[i][4].ToString();

                int value = sexReverseDic[str];

            }


        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (this.dataGridView1.CurrentCell.ColumnIndex == 4)
            //{
            //    string str = this.dataGridView1.CurrentCell.Value.ToString();
            //    int value = 0;
            //    int.TryParse(str, out value);
            //    if (sexDic.ContainsKey(value))
            //    {
            //        this.dataGridView1.CurrentCell.Value = sexDic[value];
            //    }
            //}
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            if (this.dataGridView1.CurrentCell.ColumnIndex == columnVarIndex)
            {
                Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                string varName = dataGridView1.CurrentCell.Value.ToString();

                text_Temp.Text = this.dataGridView1.CurrentCell.Value.ToString();
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[0].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;

            dataGridView1.Columns[columnChannelIndex].ReadOnly = true;
            dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
        }

        MyRichTextBox btn = new MyRichTextBox();
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == columnVarIndex 
                    || this.dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
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
    }
}
