using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalPLC.Interface;

namespace LocalPLC.Base
{
    public partial class UserControlDO : UserControl, IGetModifyFlag
    {
        #region 
        //代理
        public delegate void setTreeNodeStatusEventHandler(string s1, string name);
        setTreeNodeStatusEventHandler setTreeNodeStatusDelegate = null;
        #endregion

        public UserControlDO(/*string name, */UserControl1 us)
        {
            InitializeComponent();

            setTreeNodeStatusDelegate = new setTreeNodeStatusEventHandler(us.setTreeNodeStatus);

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            text_Temp.MaxLength = 30;

            setButtonEnable(false);
            //this.DoubleBuffered = true;
            //this.DoubleBuffered = true;//设置本窗体
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            BindData();

            Pub.CRichTestBoxMenu richMenu = new Pub.CRichTestBoxMenu(text_Temp, dataGridView1);

            // 设置下拉列表框不可见
            text_Temp.Visible = false;
            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);
            //this.text_Temp.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.richTextBox1_PreviewKeyDown);
            text_Temp.WordWrap = false;
            text_Temp.ScrollBars = RichTextBoxScrollBars.None;



            this.dataGridView1.Controls.Add(text_Temp);

            //禁止用户改变DataGridView1の所有行的行高  
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Columns[columnVarIndex].Width = 200;
            //dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //MyRichTextBox和RichTextBox都可以
        private RichTextBox text_Temp = new RichTextBox();
        const int columnVarIndex = 1;
        const int columnChannelIndex = 2;
        const int columnAddressIndex = 3;
        const int columnNoteIndex = 4;
        const int columnUsedIndex = 0;

        #region
        //接口
        bool modifiedFlag = false;
        void setModifgFlag(bool flag)
        {
            modifiedFlag = flag;
            if(flag)
            {
                setTreeNodeStatusDelegate(ConstVariable.DO, ConstVariable.DO);
            }
        }

        public bool getModifyFlag()
        {
            if(!checkDataGridView())
            {
                //不保存
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



            public void refreshData()
        {
            dtData.Clear();
            foreach(var doData in UserControlBase.dataManage.doList)
            {
                DataRow drData = dtData.NewRow();
                drData[columnUsedIndex] = doData.used;
                drData[columnVarIndex] = doData.varName;
                drData[columnChannelIndex] = doData.channelName;
                drData[columnAddressIndex] = doData.address;
                drData[columnNoteIndex] = doData.note;
                dtData.Rows.Add(drData);
            }

            this.dataGridView1.DataSource = dtData;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.White;
            }
        }
        public void initData()
        {
            var list = UserControlBase.dataManage.deviceInfoElem.connector.moduleList;
            string doType = "";
            foreach (var elem in list)
            {
                if (elem.moduleID == "DIG_OUT")
                {
                    var modulesList = UserControlBase.dataManage.modules.list;
                    foreach (var elemModule in modulesList)
                    {
                        if (elemModule.moduleID == elem.moduleID)
                        {
                            foreach (var para in elemModule.connectModules.list)
                            {
                                //doType
                                string type = para.type;
                                string[] strArr = type.Split(new Char[] { ':' });
                                if (strArr.Length == 2)
                                {
                                    string localType = strArr.ElementAt(0);
                                    doType = strArr.ElementAt(1);

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
            if (UserControlBase.dataManage.dicBitfield.ContainsKey(doType))
            {
                dtData.Clear();
                var value = UserControlBase.dataManage.dicBitfield[doType];
                //int count = 0;
                foreach(var elem in value.list)
                {
                    xml.DOData diData = new xml.DOData();

                    DataRow drData;
                    drData = dtData.NewRow();
                    diData.used = false;
                    drData[0] = diData.used;

                    diData.varName = elem.name;
                    drData[1] = diData.varName;

                    diData.channelName = elem.name;
                    drData[2] = diData.channelName;

                    //string start = UserControlBase.dataManage.deviceInfoElem.deviceIdentificationElem.ioAddrStart;
                    string ioAddress = string.Format("%QX{0}.{1}", /*ConstVariable.DIADDRESSIO*/ nStart, count);
                    diData.address = ioAddress;
                    count++;
                    if ((count) % 8 == 0)
                    {
                        count = 0;
                        nStart++;
                        ioAddress = string.Format("%QX{0}.{1}", /*ConstVariable.DIADDRESSIO*/ nStart, count);

                    }

                    drData[3] = diData.address;

                    diData.note = "";
                    drData[4] = diData.note;
                    //drData[4] = "0";    //滤波
                    //drData[5] = "注释1";
                    dtData.Rows.Add(drData);

                    UserControlBase.dataManage.doList.Add(diData);
                }


                this.dataGridView1.DataSource = dtData;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.White;
            }
        }


        public DataTable dtData = null;
        private void BindData()
        {
            //view绑定datatable
            dtData = new DataTable();
            dtData.Columns.Add("已使用", typeof(bool));
            dtData.Columns.Add("变量名");
            dtData.Columns.Add("通道名");
            dtData.Columns.Add("地址");
            dtData.Columns.Add("注释");

            /*
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "DO0";
            drData[2] = "%QX0.0";
            drData[3] = "";
            //drData[4] = "0";    //滤波
            //drData[5] = "注释1";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 0;
            drData[1] = "DO1";
            drData[2] = "%QX0.1";
            drData[3] = "";
            //drData[4] = "3";
            //drData[5] = "注释2";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "DO2";
            drData[2] = "%QX0.2";
            drData[3] = "";
            //drData[4] = "12";
            //drData[5] = "注释3";
            dtData.Rows.Add(drData);
            */

            this.dataGridView1.DataSource = dtData;
        }

        ToolTip tip = new ToolTip();
        void setCellColor(Color color, string str)
        {
            if (color == Color.Red)
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

            text_Temp.Focus();
            //text_Temp.AutoSize = false;
            this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
            this.text_Temp.ScrollToCaret();
        }


        void onlyTextBoxSetColor(Color color)
        {
            text_Temp.BackColor = color;
            tip.SetToolTip(text_Temp, "");
        }


        void firstClickTextBoxCheckInput()
        {
            if (dataGridView1.CurrentCell.Style.SelectionBackColor != Color.Red)
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
                    setCellColor(Color.Red, string.Format("{0} 格式不对", text_Temp.Text));

                }
                else
                {
                    char[] c = text_Temp.Text.ToCharArray();
                    if (c[0] >= '0' && c[0] <= '9')
                    {
                        dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                        dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                        setCellColor(Color.Red, string.Format("{0} 第一个字符不可以为数", text_Temp.Text));

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
                        if (ret == false)
                        {
                            ret = UserControl1.UC.getReDataManager().checkVarNameDI(name, channel)
                                || UserControl1.UC.getReDataManager().checkVarNameHsc(name, channel);
                        }


                        if (!ret)
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
                    setCellColor(Color.Red, string.Format("{0} 格式不对", text_Temp.Text));
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




        //void checkTextInput()
        //{
        //    if (dataGridView1.CurrentCell == null)
        //    {
        //        return;
        //    }

        //    //先判断界面
        //    var channel = UserControlBase.dataManage.doList[dataGridView1.CurrentCell.RowIndex].channelName;
        //    bool ret = false;

        //    foreach (DataRow row in dtData.Rows)
        //    {
        //        string curUiVarName = row[columnVarIndex].ToString();
        //        string curUiChannelName = row[columnChannelIndex].ToString();
        //        if (channel != curUiChannelName)
        //        {
        //            if (text_Temp.Text == curUiVarName)
        //            {
        //                ret = true;
        //            }
        //        }
        //    }

        //    if(ret)
        //    {
        //        //界面有重复的
        //        setCellColor(Color.Red, string.Format("{0} 已被使用", text_Temp.Text));
        //        return;
        //    }
        //   else
        //    {
        //        ret = UserControl1.UC.getReDataManager().checkVarNameDI(text_Temp.Text, channel);
        //        if (ret)
        //        {
        //            setCellColor(Color.Red, string.Format("{0} 已被使用", text_Temp.Text));
        //            return;
        //        }
        //    }



        //    if (dataGridView1.CurrentCell.ColumnIndex == columnVarIndex)
        //    {
        //        if (!regStr.IsMatch(text_Temp.Text))
        //        {

        //            setCellColor(Color.Red, string.Format("{0} 格式不对", text_Temp.Text));

        //        }
        //        else
        //        {
        //            char[] c = text_Temp.Text.ToCharArray();
        //            if (c[0] >= '0' && c[0] <= '9')
        //            {
        //                setCellColor(Color.Red, string.Format("{0} 第一个字符不可以为数", text_Temp.Text));

        //                text_Temp.Focus();
        //                text_Temp.SelectionStart = text_Temp.TextLength;
        //            }
        //            else
        //            {

        //                setCellColor(Color.White, "");
        //            }

        //        }
        //    }
        //    else if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex)
        //    {
        //        if (!regStrNote.IsMatch(text_Temp.Text))
        //        {
        //            setCellColor(Color.Red, string.Format("{0} 格式不对", text_Temp.Text));
        //        }
        //        else
        //        {
        //            setCellColor(Color.White, "");
        //        }
        //    }
        //}


        bool checkDataGridView()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[columnVarIndex].Style.BackColor == Color.Red)
                {
                    return false;
                }
            }

            return true;
        }

        void checkTextInput()
        {
            if (dataGridView1.CurrentCell == null)
            {
                return;
            }


            //先判断当前字符串格式是否有效
            //判断输入是否有效
            if (checkVarNameStrValid(dataGridView1.CurrentCell.Value.ToString(), dataGridView1.CurrentCell.RowIndex))
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



                var channel = UserControlBase.dataManage.doList[dataGridView1.CurrentCell.RowIndex].channelName;

                int i = 0;
                bool buttonStatus = false;
                foreach (DataRow row in dtData.Rows)
                {
                    bool ret = false;
                    string curUiVarName = row[columnVarIndex].ToString();
                    string curUiChannelName = row[columnChannelIndex].ToString();
                    if (curUiVarName.Length == 0)
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

                        if (dataGridView1.CurrentCell.RowIndex == i)
                        {
                            setCellColor(Color.Red, string.Format("{0}已被使用", curUiVarName));
                        }

                        buttonStatus = true;
                        //return;
                    }
                    else
                    {
                        //其他模块判断
                        ret = UserControl1.UC.getReDataManager().checkVarNameDI(curUiVarName, channel)
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



                if (!buttonStatus)
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
            if (dataGridView1.CurrentCell.Value.ToString() != text_Temp.Text)
            {
                //
                //checkTextInput();

                dataGridView1.CurrentCell.Value = text_Temp.Text;
                setModifgFlag(true);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
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
                    //text_Temp.Select(text_Temp.SelectionStart, 0);
                    text_Temp.SelectionStart = text_Temp.TextLength;
                    text_Temp.ScrollToCaret();

                    firstClickTextBoxCheckInput();
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

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)dataGridView1.DataSource;
            string str = table.Rows[0][0].ToString();
            int count = table.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                str = table.Rows[i][0].ToString();
                str = table.Rows[i][1].ToString();
                str = table.Rows[i][2].ToString();

                str = table.Rows[i][3].ToString();

            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[0].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;


        }

        private void dataGridView1_DataSourceChanged_1(object sender, EventArgs e)
        {
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[columnUsedIndex].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[columnUsedIndex].DefaultCellStyle.BackColor = Color.Lavender;



            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[columnChannelIndex].ReadOnly = true;
            dataGridView1.Columns[columnChannelIndex].DefaultCellStyle.BackColor = Color.Lavender;
            dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
            dataGridView1.Columns[columnAddressIndex].DefaultCellStyle.BackColor = Color.Lavender;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2)  // 2代表第二列
            {
                //e.Cancel = true;
            }
        }

        enum COLUMN_DO { USED, VARNAME, CHANNELNAME, ADDRESS, NOTE };
        
        void getDataFromUI()
        {
            var listDO = UserControlBase.dataManage.doList;
            int row = 0;
            foreach (DataRow dr in dtData.Rows)
            {
                listDO[row].used = bool.Parse(dr[(int)COLUMN_DO.USED].ToString());
                listDO[row].varName = dr[(int)COLUMN_DO.VARNAME].ToString();
                listDO[row].channelName = dr[(int)COLUMN_DO.CHANNELNAME].ToString();
                listDO[row].address = dr[(int)COLUMN_DO.ADDRESS].ToString();
                listDO[row].note = dr[(int)COLUMN_DO.NOTE].ToString();

                row++;
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
            getDataFromUI();
            //utility.PrintInfo("DO数据生效!");
            setButtonEnable(false);
            setModifgFlag(false);
            setTreeNodeStatusDelegate(ConstVariable.DO, ConstVariable.DO);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshData();
            setButtonEnable(false);
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


        System.Text.RegularExpressions.Regex regStr = new System.Text.RegularExpressions.Regex(@"^[\w]{1,32}$");
        System.Text.RegularExpressions.Regex regStrNote = new System.Text.RegularExpressions.Regex(@"^(.{0,32})$");
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;
            if (row < 0 || column < 0)
            {
                return;
            }

            var cell = sender as DataGridView;
            var value = dataGridView1.CurrentCell.Value.ToString();
            var listDO = UserControlBase.dataManage.doList;
            if(cell == null || listDO.Count == 0)
            {
                return;
            }

            if(column == columnVarIndex)
            {
                checkTextInput();
            }
            else if(column == columnNoteIndex)
            {
                //注释
                if (!checkNoteStrValid(value.ToString(), e.RowIndex))
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

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitInfo = this.dataGridView1.HitTest(e.X, e.Y);
            if (hitInfo.Type == DataGridViewHitTestType.TopLeftHeader)
            {
                dataGridView1.ClearSelection();
            }
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//取消方向键对控件的焦点的控件，用自己自定义的函数处理各个方向键的处理函数
        //{
        //    switch (keyData)
        //    {
        //        case Keys.Up:
        //            //UpKey();
        //            if(text_Temp.Visible)
        //            {
        //                text_Temp.SelectionStart = text_Temp.SelectionStart - 1;
        //            }
        //            return true;//不继续处理
        //        case Keys.Down:
        //            //DownKey();
        //            return true;
        //        case Keys.Left:
        //            //LeftKey();
        //            return true;
        //        case Keys.Right:
        //            //RightKey();
        //            return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
    }
}
