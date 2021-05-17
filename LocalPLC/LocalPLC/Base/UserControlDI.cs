using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using LocalPLC.Interface;

namespace LocalPLC.Base
{
    public partial class UserControlDI : UserControl, IGetModifyFlag
    {

        Dictionary<int, string> sexDic = new Dictionary<int, string>();
        Dictionary<string, int> sexReverseDic = new Dictionary<string, int>();
        // 定义下拉列表框
        private ComboBox cmb_Temp = new ComboBox();
        //
        private RichTextBox text_Temp = new RichTextBox();
        const int columnUsed = 0;
        const int columnVarIndex = 1;
        const int columnNoteIndex = 5;
        const int columnFilterIndex = 2;
        const int columnChannelIndex = 3;
        const int columnAddressIndex = 4;

        #region 
        //代理
        public delegate void setTreeNodeStatusEventHandler(string s1, Color color);
        setTreeNodeStatusEventHandler setTreeNodeStatusDelegate = null;
        #endregion



        #region 
        bool modifiedFlag = false;

        void setModifgFlag(bool flag)
        {
            modifiedFlag = flag;
        }

        //接口实现
        public bool getModifyFlag()
        {
            if(!checkDataGridView())
            {
                // 不保存
                button2_Click(null, null);
                return false;
            }

            if (modifiedFlag)
            {
                if (MessageBox.Show("是否保存修改数据?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    // 保存
                    button1_Click_1(null, null);
                }
                else
                {
                    // 不保存
                    button2_Click(null, null);
                }
            }

            return modifiedFlag;
        }

        #endregion


        public void initData()
        {
            setButtonEnable(false);
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

            string start = UserControlBase.dataManage.deviceInfoElem.deviceIdentificationElem.ioAddrStart;
            int nStart = 0;
            int count = 0;
            int.TryParse(start, out nStart);
            if (UserControlBase.dataManage.dicBitfield.ContainsKey(diType))
            {
                dtData.Clear();
                var value = UserControlBase.dataManage.dicBitfield[diType];
                //int count = 0;
                foreach(var elem in value.list)
                {
                    xml.DIData diData = new xml.DIData();

                    DataRow drData; 
                     drData = dtData.NewRow();

                    diData.used = false;
                    drData[0] = diData.used;

                    diData.varName = elem.name;
                    drData[1] = diData.varName;

                    diData.filterTime = 3;
                    drData[2] = diData.filterTime;  //单位ms

                    diData.channelName = elem.name;
                    drData[3] = diData.channelName;



                    string ioAddress = string.Format("%IX{0}.{1}", /*ConstVariable.DIADDRESSIO*/ nStart, count);
                    diData.address = ioAddress;
                    count++;
                    if ((count) % 8 == 0)
                    {
                        count = 0;
                        nStart++;
                        ioAddress = string.Format("%IX{0}.{1}", /*ConstVariable.DIADDRESSIO*/ nStart, count);

                    }


                    drData[4] = diData.address;

                    diData.note = "";
                    drData[5] = diData.note;
                                       //drData[5] = "注释1";

                    dtData.Rows.Add(drData);
                    UserControlBase.dataManage.diList.Add(diData);
                }

                this.dataGridView1.DataSource = dtData;
            }

            for(int i = 0; i <dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.White;
            }
        }

        public void refreshData()
        {
            setButtonEnable(false);
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

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.White;
            }
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
            var temp = dtData.Columns.Add("滤波(ms)");
            //temp.MaxLength = 4;

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
            ((DataGridViewTextBoxColumn)dataGridView1.Columns["滤波(ms)"]).MaxInputLength = 4;
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

        public UserControlDI(UserControl1 us)
        {
            InitializeComponent();

            setTreeNodeStatusDelegate = new setTreeNodeStatusEventHandler(us.setTreeNodeStatus);

            Pub.CRichTestBoxMenu richMenu = new Pub.CRichTestBoxMenu(text_Temp, dataGridView1);

            text_Temp.MaxLength = 30;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
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

        void firstClickTextBoxCheckInput()
        {
            if(dataGridView1.CurrentCell.Style.SelectionBackColor != Color.Red)
            {
                this.dataGridView1.CurrentCell.Style.SelectionBackColor = Color.White;
            }


            if (dataGridView1.CurrentCell.ColumnIndex == columnVarIndex)
            {
                if(text_Temp.Text.Length == 0)
                {
                    setCellColor(Color.Red, string.Format("{0} 格式不对", text_Temp.Text));
                    return;
                }

                if (!regStr.IsMatch(text_Temp.Text))
                {
                    dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                    dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                    setCellColor(Color.Red, string.Format("{0}格式不对", text_Temp.Text));

                }
                else
                {
                    char[] c = text_Temp.Text.ToCharArray();
                    if (c[0] >= '0' && c[0] <= '9')
                    {
                        dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                        dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                        setCellColor(Color.Red, string.Format("{0}第一个字符不可以为数", text_Temp.Text));

                        text_Temp.Focus();
                        text_Temp.SelectionStart = text_Temp.TextLength;
                    }
                    else
                    {
                        //判断是否重复 tip设置 不设置颜色
                        bool ret = false;
                        int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
                        string channel = dataGridView1.Rows[rowIndex].Cells[columnChannelIndex].Value.ToString();
                        string name = dataGridView1.Rows[rowIndex].Cells[columnVarIndex].Value.ToString();
                        foreach (DataRow row in dtData.Rows)
                        {
                            string curUiVarName = row[columnVarIndex].ToString();
                            string curUiChannelName = row[columnChannelIndex].ToString();
                            if (channel != curUiChannelName)
                            {
                                if (name == curUiVarName)
                                {
                                    dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                                    dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                                    setCellColor(Color.Red, string.Format("{0} 已使用", name));
                                    ret = true;
                                }
                            }
                        }

                        //判断其他模块
                        if(ret == false)
                        {
                            ret = UserControl1.UC.getReDataManager().checkVarNameDO(name, channel)
                                || UserControl1.UC.getReDataManager().checkVarNameHsc(name, channel);
                        }


                        if(!ret)
                        {
                            onlyTextBoxSetColor(Color.White);
                        }
                        else
                        {
                            dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                            dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                            setCellColor(Color.Red, string.Format("{0} 已使用", name));
                        }

                    }

                }


                
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
            {
                if (!regStrNote.IsMatch(text_Temp.Text))
                {
                    setCellColor(Color.Red, string.Format("{0}格式不对", text_Temp.Text));
                }
                else
                {
                    onlyTextBoxSetColor(Color.White);
                }
            }
        }

        void checkCurUICellColor(string name, string channel, ref bool ret)
        {

            int i = 0;
            foreach (DataRow row in dtData.Rows)
            {
                string curUiVarName = row[columnVarIndex].ToString();
                string curUiChannelName = row[columnChannelIndex].ToString();
                if (channel != curUiChannelName)
                {
                    if (name == curUiVarName)
                    {
                        ret = true;
                    }
                }

                i++;
            }
        }

        bool checkNoteStrValid(string name, int row)
        {
            if (!regStrNote.IsMatch(name))
            {
                dataGridView1.Rows[row].Cells[columnNoteIndex].Style.BackColor = Color.Red;
                dataGridView1.Rows[row].Cells[columnNoteIndex].Style.SelectionBackColor = Color.Red;


                return true;
            }

            return false;
        }


        bool checkVarNameStrValid(string name, int row)
        {
            if (!regStr.IsMatch(name))
            {
                dataGridView1.Rows[row].Cells[columnVarIndex].Style.BackColor = Color.Red;
                dataGridView1.Rows[row].Cells[columnVarIndex].Style.SelectionBackColor = Color.Red;


                return true;
            }
            else
            {
                char[] c = name.ToCharArray();
                if (c[0] >= '0' && c[0] <= '9')
                {
                    dataGridView1.Rows[row].Cells[columnVarIndex].Style.BackColor = Color.Red;
                    dataGridView1.Rows[row].Cells[columnVarIndex].Style.SelectionBackColor = Color.Red;

                    return true;
                }
                else
                {
                    //setCellColor(Color.White, "");

                    return false;
                }

            }
        }

        bool checkDataGridView()
        {
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                if(dataGridView1.Rows[i].Cells[columnVarIndex].Style.BackColor == Color.Red)
                {
                    return false;
                }
            }

            return true;
        }

        void checkTextInput()
        {
            if(dataGridView1.CurrentCell == null)
            {
                return;
            }

            //先判断当前字符串格式是否有效
            //判断输入是否有效
            if(checkVarNameStrValid(dataGridView1.CurrentCell.Value.ToString(), dataGridView1.CurrentCell.RowIndex))
            {
                setCellColor(Color.Red, string.Format("{0} 格式无效", dataGridView1.CurrentCell.Value.ToString()));
                return;
            }
            else
            {
                //当前输入有效
                setCellColor(Color.White, "");
            }


            if (dataGridView1.CurrentCell.ColumnIndex == columnVarIndex)
            {
                //先判断本界面



                var channel = UserControlBase.dataManage.diList[dataGridView1.CurrentCell.RowIndex].channelName;

                int i = 0;
                bool buttonStatus = false;
                foreach (DataRow row in dtData.Rows)
                {
                    bool ret = false;
                    string curUiVarName = row[columnVarIndex].ToString();
                    string curUiChannelName = row[columnChannelIndex].ToString();
                    if(curUiVarName.Length == 0)
                    {
                        i++;
                        continue;
                    }

                    //判断不是currentcell字段字符是否有效
                    if (checkVarNameStrValid(curUiVarName, i))
                    {
                        i++;
                        continue;
                    }

                    checkCurUICellColor(curUiVarName, curUiChannelName, ref ret);

                    if (ret)
                    {
                        dataGridView1.Rows[i].Cells[columnVarIndex].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.Red;

                        if(dataGridView1.CurrentCell.RowIndex == i)
                        {
                            setCellColor(Color.Red, string.Format("{0}已被使用", curUiVarName));
                        }

                        buttonStatus = true;
                        //return;
                    }
                    else
                    {
                        //其他模块判断
                        ret = UserControl1.UC.getReDataManager().checkVarNameDO(curUiVarName, channel)
                            || UserControl1.UC.getReDataManager().checkVarNameHsc(curUiVarName, channel);
                        if (ret)
                        {
                            dataGridView1.Rows[i].Cells[columnVarIndex].Style.BackColor = Color.Red;
                            dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.Red;

                            if (dataGridView1.CurrentCell.RowIndex == i)
                            {
                                setCellColor(Color.Red, string.Format("{0}已被使用", curUiVarName));
                            }
                            buttonStatus = true;
                            return;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[columnVarIndex].Style.BackColor = Color.White;
                            dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.White;



                        }
                    }
                    i++;
                }



                if(!buttonStatus)
                {
                    //数据没有重复

                    setButtonEnable(true);
                }

            }
            



            
            if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
            {
                if (!regStrNote.IsMatch(text_Temp.Text))
                {
                    dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                    dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                    setCellColor(Color.Red, string.Format("{0}格式不对", text_Temp.Text));
                }
                else
                {
                    setCellColor(Color.White, "");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentCell.Value.ToString() != text_Temp.Text)
            {
                //
                //checkTextInput();



                dataGridView1.CurrentCell.Value = text_Temp.Text;
                setModifgFlag(true);
            }
        }

        ToolTip tip = new ToolTip();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        void onlyTextBoxSetColor(Color color)
        {
            text_Temp.BackColor = color;
            tip.SetToolTip(text_Temp, "");
        }

        void setCellColor(Color color, string str)
        {
            if(color == Color.Red)
            {
                //utility.PrintError(str);
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
            }
            else
            {
                //setButtonEnable(true);
            }

            //text_Temp.BackColor = color;
            //dataGridView1.CurrentCell.Style.BackColor = color;
            //dataGridView1.CurrentCell.Style.SelectionBackColor = color;
            tip.SetToolTip(text_Temp, str);

            //text_Temp.Focus();
            //text_Temp.AutoSize = false;
            text_Temp.SelectAll();
            this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
            //this.text_Temp.ScrollToCaret();

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
                    text_Temp.Visible = false;
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string varName = dataGridView1.CurrentCell.Value.ToString();

                    text_Temp.Text = this.dataGridView1.CurrentCell.Value.ToString();

                    text_Temp.Left = rect.Left + 3;
                    text_Temp.Top = rect.Top + 3;
                    text_Temp.Width = rect.Width - 5;
                    text_Temp.Height = rect.Height - 5;
                    text_Temp.Visible = true;
                    text_Temp.Focus();
                    //text_Temp.AutoSize = false;
                    text_Temp.SelectAll();
                    this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
                    this.text_Temp.ScrollToCaret();

                    firstClickTextBoxCheckInput();




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


        private void MyDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitInfo = this.dataGridView1.HitTest(e.X, e.Y);
            if (hitInfo.Type == DataGridViewHitTestType.TopLeftHeader)
            {
                dataGridView1.ClearSelection();
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



        
    private Regex rNum = new Regex("^[0-9]{1,4}$");       //这个可以写成静态的，就不用老是构造
    System.Text.RegularExpressions.Regex regStr = new System.Text.RegularExpressions.Regex(@"^[\w]{1,32}$");
        System.Text.RegularExpressions.Regex regStrNote = new System.Text.RegularExpressions.Regex(@"^(.{0,32})$");
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            if (this.dataGridView1.CurrentCell.ColumnIndex == columnVarIndex)
            {
                //Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                //string varName = dataGridView1.CurrentCell.Value.ToString();
                //if(!regStr.IsMatch(varName))
                //{
                //    text_Temp.Text = this.dataGridView1.CurrentCell.Value.ToString();
                //}


                //text_Temp.Text = this.dataGridView1.CurrentCell.Value.ToString();
            }
            else if(this.dataGridView1.CurrentCell.ColumnIndex == columnFilterIndex)
            {
                //var str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //if (!r.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                //{
                //    MessageBox.Show("输入格式错误!");

                //    var listDI = UserControlBase.dataManage.diList;
                //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = listDI[e.RowIndex].filterTime.ToString();
                //    //dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                //}
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[0].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;

            dataGridView1.Columns[columnVarIndex].ReadOnly = true;
            dataGridView1.Columns[columnChannelIndex].ReadOnly = true;
            dataGridView1.Columns[columnChannelIndex].DefaultCellStyle.BackColor = Color.Lavender;
            dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
            dataGridView1.Columns[columnAddressIndex].DefaultCellStyle.BackColor = Color.Lavender;

        }

        MyRichTextBox btn = new MyRichTextBox();
        public DataGridViewTextBoxEditingControl CellEdit = null;
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(dataGridView1.CurrentCellAddress.X == columnFilterIndex)
            {
                //过滤时间
                CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                CellEdit.SelectAll();
                CellEdit.KeyPress -= Cells_KeyPress; //绑定事件
                CellEdit.KeyPress += Cells_KeyPress; //绑定事件
            }
        }


        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            if (this.dataGridView1.CurrentCellAddress.X == columnFilterIndex)//获取当前处于活动状态的单元格索引
            {
                if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }

            }
            else if(this.dataGridView1.CurrentCellAddress.X == columnVarIndex)
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

        public void getDataFromUI()
        {
            //控制器类里DI、DO、HOUT从UI界面值传到datamanage
            var listDI = UserControlBase.dataManage.diList;

            int row = 0;
            foreach (DataRow dr in dtData.Rows)
            {
                listDI[row].used = bool.Parse(dr[(int)COLUMN_DI.USED].ToString());

                //utility.PrintInfo(string.Format("{0} {1}", listDI[row].channelName, listDI[row].used));
                listDI[row].varName = dr[(int)COLUMN_DI.VARNAME].ToString();
                uint.TryParse(dr[(int)COLUMN_DI.FITERTIME].ToString(), out listDI[row].filterTime);

                listDI[row].channelName = dr[(int)COLUMN_DI.CHANNELNAME].ToString();
                listDI[row].address = dr[(int)COLUMN_DI.ADDRESS].ToString();
                listDI[row].note = dr[(int)COLUMN_DI.NOTE].ToString();

                row++;
            }
        }

        enum COLUMN_DI { USED, VARNAME, FITERTIME, CHANNELNAME, ADDRESS, NOTE };
        private void button1_Click_1(object sender, EventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
            getDataFromUI();

            setButtonEnable(false);
            setModifgFlag(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshData();
            setCellColor(Color.White, "");
            setButtonEnable(false);
            setTreeNodeStatusDelegate(ConstVariable.DI, Color.White);
            setModifgFlag(false);
        }

        void setButtonEnable(bool enable)
        {
            if (enable)
            {
                bool ret = checkDataGridView();
                button_valid.Enabled = ret;
            }
            else
            {
                button_valid.Enabled = enable;
            }

            button_cancel.Enabled = enable;

        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;
            if(row < 0 || column < 0)
            {
                return;
            }

            var cell = sender as DataGridView;
            var value = dataGridView1.CurrentCell.Value;
            var listDI = UserControlBase.dataManage.diList;

            if(cell == null || listDI.Count == 0)
            {
                return;
            }

            if (column == columnUsed)
            {

            }
            else if (column == columnVarIndex)
            {
                checkTextInput();


//                setTreeNodeStatusDelegate(ConstVariable.DI, Color.Red);

                //column 1变量名
                //checkTextInput();
                //setButtonEnable(true);
            }
            else if (column == columnFilterIndex)
            {
                //滤波时间
                if (listDI[row].filterTime.ToString() != value.ToString())
                {
                    var str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (!rNum.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 1000)
                    {
                        if(!rNum.IsMatch(str))
                        {
                            if(str.Length > 4)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1000";
                            }
                            else if(str.Length == 0)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1000";
                            }
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1000";
                        }

                        //dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                    }
                    else
                    {

                    }
                    setButtonEnable(true);
                    setModifgFlag(true);

//                    setTreeNodeStatusDelegate(ConstVariable.DI, Color.Red);
                }
            }
            else if (column == columnChannelIndex)
            {
                //通道名
            }
            else if (column == columnAddressIndex)
            {
                //通道地址
            }
            else if (column == columnNoteIndex)
            {
                //注释
                if(!checkNoteStrValid(value.ToString(), e.RowIndex))
                {
                    setButtonEnable(true);
                }
                else
                {
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                }
            }

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            //text_Temp.Hide();
            //dataGridView1_CurrentCellChanged(null, null);
            //text_Temp.Show();

            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
        }
    }




}
