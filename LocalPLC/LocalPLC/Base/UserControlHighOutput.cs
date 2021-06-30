using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalPLC.Base.xml;
using System.Windows.Forms.VisualStyles;
using LocalPLC.Interface;

namespace LocalPLC.Base
{
    public partial class UserControlHighOutput : UserControl, IGetModifyFlag
    {

        public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
        {
            public DataGridViewDisableButtonColumn()
            {
                this.CellTemplate = new DataGridViewDisableButtonCell();
            }
        }

        public class DataGridViewDisableButtonCell : DataGridViewButtonCell
        {
            private bool enabledValue;
            public bool Enabled
            {
                get
                {
                    return enabledValue;
                }
                set
                {
                    enabledValue = value;
                }
            }

            // Override the Clone method so that the Enabled property is copied.
            public override object Clone()
            {
                DataGridViewDisableButtonCell cell =
                    (DataGridViewDisableButtonCell)base.Clone();
                cell.Enabled = this.Enabled;
                return cell;
            }

            // By default, enable the button cell.
            public DataGridViewDisableButtonCell()
            {
                this.enabledValue = true;
            }

            protected override void Paint(Graphics graphics,
                Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                DataGridViewElementStates elementState, object value,
                object formattedValue, string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                // The button cell is disabled, so paint the border,
                // background, and disabled button for the cell.
                if (!this.enabledValue)
                {
                    // Draw the cell background, if specified.
                    if ((paintParts & DataGridViewPaintParts.Background) ==
                        DataGridViewPaintParts.Background)
                    {
                        SolidBrush cellBackground =
                            new SolidBrush(cellStyle.BackColor);
                        graphics.FillRectangle(cellBackground, cellBounds);
                        cellBackground.Dispose();
                    }

                    // Draw the cell borders, if specified.
                    if ((paintParts & DataGridViewPaintParts.Border) ==
                        DataGridViewPaintParts.Border)
                    {
                        PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                            advancedBorderStyle);
                    }

                    // Calculate the area in which to draw the button.
                    Rectangle buttonArea = cellBounds;
                    Rectangle buttonAdjustment =
                        this.BorderWidths(advancedBorderStyle);
                    buttonArea.X += buttonAdjustment.X;
                    buttonArea.Y += buttonAdjustment.Y;
                    buttonArea.Height -= buttonAdjustment.Height;
                    buttonArea.Width -= buttonAdjustment.Width;

                    // Draw the disabled button.
                    ButtonRenderer.DrawButton(graphics, buttonArea,
                        PushButtonState.Disabled);


                    // Draw the disabled button text.
                    if (this.FormattedValue is String)
                    {
                        TextRenderer.DrawText(graphics,
                            (string)this.FormattedValue,
                            this.DataGridView.Font,
                            buttonArea, SystemColors.GrayText);
                    }
                }
                else
                {
                    // The button cell is enabled, so let the base class
                    // handle the painting.
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                        elementState, value, formattedValue, errorText,
                        cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }
        public enum TYPE { NOTUSED, PLS, PWM, FREQUENCY, PTO}
        ToolTip tip = new ToolTip();

        System.Text.RegularExpressions.Regex regStr = new System.Text.RegularExpressions.Regex(@"^[\w]{1,32}$");
        System.Text.RegularExpressions.Regex regStrNote = new System.Text.RegularExpressions.Regex(@"^(.{0,32})$");

        public UserControlHighOutput(UserControl1 us)
        {
            InitializeComponent();

            setTreeNodeStatusDelegate = new setTreeNodeStatusEventHandler(us.setTreeNodeStatus);

            typeDescDic.Clear();

            typeDescDic.Add(((int)TYPE.NOTUSED), "未配置");
            typeDescDic.Add(((int)TYPE.PLS), "PLS");
            typeDescDic.Add(((int)TYPE.PWM), "PWM");
            typeDescDic.Add(((int)TYPE.FREQUENCY), "FREQUENCY");
            typeDescDic.Add(((int)TYPE.PTO), "PTO");

            BindData();


            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);
            text_Temp.Visible = false;
            text_Temp.WordWrap = false;
            //不显示滚动条
            text_Temp.ScrollBars = RichTextBoxScrollBars.None;

            text_Temp.MaxLength = 30;


            dataGridView1.Controls.Add(text_Temp);

            //禁止用户改变DataGridView1の所有行的行高  
            dataGridView1.AllowUserToResizeRows = false;
            //列太多，去掉最后一行填充表格
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            // 禁止用户改变列头的高度  
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        #region
        public DataTable dtData = null;
        public const int columnUsedIndex = 0;
        public const int columnVarIndex = 1;
        public const int columnAddressIndex = 2;
        public const int columnTypeIndex = 3;
        public const int columnNoteIndex = 4;

        private RichTextBox text_Temp = new RichTextBox();


        Dictionary<int, string> typeDescDic = new Dictionary<int, string>();
        #endregion

        #region
        //代理
        public delegate void setTreeNodeStatusEventHandler(string tag, string name);
        setTreeNodeStatusEventHandler setTreeNodeStatusDelegate = null;
        #endregion

        #region
        bool modifiedFlag = false;
        void setModifgFlag(bool flag)
        {
            modifiedFlag = flag;
            if (flag)
            {
                setTreeNodeStatusDelegate("HSP", "高速输出");
            }
        }

        //接口实现
        public bool getModifyFlag()
        {
            if (!checkDataGridView())
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
                    button1_Click(null, null);
                }
                else
                {
                    // 不保存
                    button2_Click(null, null);
                }
            }

            return modifiedFlag;
        }

        bool checkDataGridView()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[columnVarIndex + 1].Style.BackColor == Color.Red)
                {
                    return false;
                }
            }

            return true;
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

        #endregion

        public void initData()
        {
            var list = UserControlBase.dataManage.deviceInfoElem.connector.moduleList;
            foreach(var elem in list)
            {
                if(elem.moduleID == "HSP_FUNC")
                {
                    var modulesList = UserControlBase.dataManage.modules.list;
                    foreach(var elemModule in modulesList)
                    {
                        if(elemModule.moduleID == elem.moduleID)
                        {
                            foreach(var innerElem in elemModule.connectModules.list)
                            {

                                xml.HSPData hspData = new HSPData();

                                DataRow drData;
                                drData = dtData.NewRow();
                                hspData.used = false;
                                drData[columnUsedIndex] = hspData.used;

                                hspData.name = innerElem.parameterName;
                                drData[columnVarIndex] = hspData.name;

                                hspData.address = hspData.name;
                                drData[columnAddressIndex] = hspData.address;

                                hspData.type = 0;
                                if(typeDescDic.ContainsKey(hspData.type))
                                {
                                    drData[columnTypeIndex] = typeDescDic[hspData.type];
                                }
                                else
                                {
                                    drData[columnTypeIndex] = "";
                                }

                                hspData.note = "";
                                drData[columnNoteIndex] = hspData.note;  //注释

                                dtData.Rows.Add(drData);
                                UserControlBase.dataManage.hspList.Add(hspData);
                            }
                        }
                    }
                    this.dataGridView1.DataSource = dtData;
                }
            }

        }


        /// <summary>
        /// 加载工程文件，刷新界面显示
        /// </summary>
        public void refreshData()
        {
            dtData.Clear();
            foreach (var hspData in UserControlBase.dataManage.hspList)
            {
                DataRow drData;
                drData = dtData.NewRow();

                drData[columnUsedIndex] = hspData.used;
                drData[columnVarIndex] = hspData.name;
                drData[columnAddressIndex] = hspData.address;
                
                if(typeDescDic.ContainsKey(hspData.type))
                {
                    drData[columnTypeIndex] = typeDescDic[hspData.type];
                }
                else
                {
                    drData[columnTypeIndex] = "";
                }

                drData[columnNoteIndex] = hspData.note;

                dtData.Rows.Add(drData);
            }
        }

        private void BindData()
        {
            //view绑定datatable
            dtData = new DataTable();
            dtData.Columns.Add("已配置", typeof(bool));
            dtData.Columns.Add("变量名", typeof(string));
            dtData.Columns.Add("地址", typeof(string));
            dtData.Columns.Add("类型");
            //dtData.Columns.Add("配置");
            dtData.Columns.Add("注释");


            //DataRow drData;
            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "HSP0";
            //drData[2] = "未配置";
            //drData[3] = "";  //类型
            ////drData[4] = 0; //


            ////drData[5] = "注释1";
            //dtData.Rows.Add(drData);
            //drData = dtData.NewRow();
            //drData[0] = 2;
            //drData[1] = "HSP1";
            //drData[2] = "未配置";
            //drData[3] = "";
            ////drData[4] = 0;


            ////drData[5] = "注释2";
            //dtData.Rows.Add(drData);
            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "HSP2";
            //drData[2] = "未配置";
            //drData[3] = "";
            ////drData[4] = 0;

            //dtData.Rows.Add(drData);

            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "HSP3";
            //drData[2] = "未配置";
            //drData[3] = "";
            ////drData[4] = 0;
            //dtData.Rows.Add(drData);


            this.dataGridView1.DataSource = dtData;

            DataGridViewDisableButtonColumn uninstallButtonColumn = new DataGridViewDisableButtonColumn();
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


            int columnIndex = 4;
            if (dataGridView1.Columns["配置"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, uninstallButtonColumn);
            }

        }

        #region
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentCell == null)
            {
                return;
            }

            if (dataGridView1.CurrentCell.Value.ToString() != text_Temp.Text)
            {
                dataGridView1.CurrentCell.Value = text_Temp.Text;
            }

        }

        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["配置"].Index && e.RowIndex >= 0)
            {
                if (dataGridView1.CurrentCell is DataGridViewDisableButtonCell)
                {
                    if (((DataGridViewDisableButtonCell)dataGridView1.CurrentCell).Enabled == false)
                    {
                        return;
                    }
                }

                //Do something with your button.
                FormHighOutput color = new FormHighOutput(typeDescDic, UserControlBase.dataManage.hspList[e.RowIndex]);
                color.StartPosition = FormStartPosition.CenterScreen;
                if (color.ShowDialog() == DialogResult.Yes)
                {
                    setModifgFlag(true);
                    setModifgFlag(false);

                    //button1_Click(null, null);
                }

                var row = e.RowIndex;
                var col = e.ColumnIndex;

                var type = UserControlBase.dataManage.hspList[e.RowIndex].type;
                if(typeDescDic.ContainsKey(type))
                {
                    //dtData.Rows[row][col] = typeDescDic[type];
                    dtData.Rows[row][columnTypeIndex] = typeDescDic[type];
                    dtData.Rows[row][columnUsedIndex] = UserControlBase.dataManage.hspList[e.RowIndex].used;
                }

                refreshHSPConfigButton();

                //数据刷新到DI DO datarow里,动态更新
                UserControl1.UC.refreshDOUserBaseUI();
            }
        }

        void setButtonEnable(bool enable, int column, int row)
        {
            //DI00使用，HSC4不可以配置
            DataGridViewDisableButtonColumn button = (DataGridViewDisableButtonColumn)dataGridView1.Columns[0];
            DataGridViewButtonCell vCell = (DataGridViewButtonCell)dataGridView1[column, row];
            if (vCell is DataGridViewDisableButtonCell)
            {
                ((DataGridViewDisableButtonCell)vCell).Enabled = enable;
                
                dataGridView1.Invalidate();
            }
        }

        void refreshHSPConfigButton()
        {
            foreach (var dout in UserControlBase.dataManage.doList)
            {
                //dout.used
                if (dout.channelName == "DO00")
                {
                    if(dout.used)
                    {
                        if(dout.hspUsed != "HSP0")
                        {
                            setButtonEnable(false, 0, 0);
                        }
                    }
                    else
                    {
                        if(dout.hspUsed == "")
                        {
                            setButtonEnable(true, 0, 0);
                        }
                    }

                }
                else if(dout.channelName == "DO01")
                {
                    if(dout.used)
                    {
                        if(dout.hspUsed != "HSP1")
                        {
                            setButtonEnable(false, 0, 1);
                        }
                    }
                    else
                    {
                        if(dout.hspUsed == "")
                        {
                            setButtonEnable(true, 0, 1);
                        }
                    }


                }
                else if(dout.channelName == "DO02")
                {
                    if(dout.used)
                    {
                        if(dout.hspUsed != "HSP2")
                        {
                            setButtonEnable(false, 0, 2);
                        }
                    }
                    else
                    {
                        if(dout.hspUsed == "")
                        {
                            setButtonEnable(true, 0, 2);
                        }
                    }

                }
                else if(dout.channelName == "DO03")
                {
                    if(dout.used)
                    {
                        if(dout.hspUsed != "HSP3")
                        {
                            setButtonEnable(false, 0, 3);
                        }
                    }
                    else
                    {
                        if(dout.hspUsed == "")
                        {
                            setButtonEnable(true, 0, 3);
                        }
                    }

                }
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
                if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex + 1)
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
                    this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
                    this.text_Temp.ScrollToCaret();


                    firstClickTextBoxCheckInput();
                }

                else if(dataGridView1.CurrentCell.ColumnIndex == columnVarIndex + 1)
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
                    this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
                    this.text_Temp.ScrollToCaret();

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

            //text_Temp.Focus();
            //text_Temp.AutoSize = false;
            text_Temp.SelectAll();
            this.text_Temp.SelectionStart = this.text_Temp.Text.Length;
            //this.text_Temp.ScrollToCaret();

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


            if (dataGridView1.CurrentCell.ColumnIndex == columnVarIndex + 1)
            {
                if (text_Temp.Text.Length == 0)
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
                        string channel = dataGridView1.Rows[rowIndex].Cells[columnAddressIndex + 1].Value.ToString();
                        string name = dataGridView1.Rows[rowIndex].Cells[columnVarIndex + 1].Value.ToString();
                        foreach (DataRow row in dtData.Rows)
                        {
                            string curUiVarName = row[columnVarIndex].ToString();
                            string curUiChannelName = row[columnAddressIndex].ToString();
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
                            ret = UserControl1.UC.getReDataManager().checkVarNameDO(name, channel) ||
                                UserControl1.UC.getReDataManager().checkVarNameDI(name, channel);
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
            else if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex + 1)
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

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {
                return;
            }

            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex + 1)
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
            dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
            dataGridView1.Columns[columnAddressIndex].DefaultCellStyle.BackColor = Color.Lavender;
            dataGridView1.Columns[columnTypeIndex].ReadOnly = true;
            dataGridView1.Columns[columnTypeIndex].DefaultCellStyle.BackColor = Color.Lavender;
        }


        public void getDataFromUI()
        {
            int row = 0;
            var hspList = UserControlBase.dataManage.hspList;
            foreach (DataRow dr in dtData.Rows)
            {
                bool.TryParse(dr[(int)UserControlHighOutput.columnUsedIndex].ToString(), out hspList[row].used);
                hspList[row].name = dr[UserControlHighOutput.columnVarIndex].ToString();
                hspList[row].address = dr[UserControlHighOutput.columnAddressIndex].ToString();
                hspList[row].note = dr[UserControlHighOutput.columnNoteIndex].ToString();

                row++;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
            setModifgFlag(false);
            getDataFromUI();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            refreshData();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;

            setModifgFlag(false);
        }

        private void UserControlHighOutput_Load(object sender, EventArgs e)
        {
            button_valid.Enabled = false;
            button_cancel.Enabled = false;

            refreshHSPConfigButton();
        }

        bool checkVarNameStrValid(string name, int row, out string temp)
        {
            if (!regStr.IsMatch(name))
            {
                dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;

                temp = string.Format("{0} 格式无效", dataGridView1.Rows[row].Cells[columnVarIndex + 1].Value.ToString());

                return true;
            }
            else
            {
                char[] c = name.ToCharArray();
                if (c[0] >= '0' && c[0] <= '9')
                {
                    dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                    dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;

                    temp = string.Format("{0} 格式无效,第一个字符不可以为数字!", dataGridView1.Rows[row].Cells[columnVarIndex + 1].Value.ToString());

                    return true;
                }
                else
                {
                    //setCellColor(Color.White, "");
                    temp = "";

                    return false;
                }

            }
        }

        void checkCurUICellColor(string name, string channel, ref bool ret)
        {

            int i = 0;
            foreach (DataRow row in dtData.Rows)
            {
                string curUiVarName = row[columnVarIndex].ToString();
                string curUiAddressName = row[columnAddressIndex].ToString();
                if (channel != curUiAddressName)
                {
                    if (name == curUiVarName)
                    {
                        ret = true;
                    }
                }

                i++;
            }
        }


        void checkTextInput()
        {
            if (dataGridView1.CurrentCell == null)
            {
                return;
            }

            //先判断当前字符串格式是否有效
            //判断输入是否有效
            string temp = "";
            if (checkVarNameStrValid(dataGridView1.CurrentCell.Value.ToString(), dataGridView1.CurrentCell.RowIndex, out temp))
            {
                setCellColor(Color.Red, string.Format("{0} 格式无效", dataGridView1.CurrentCell.Value.ToString()));
                if (dataGridView1.CurrentCell.Value.ToString().Length != 0)
                {
                    MessageBox.Show(temp);
                }

                return;
            }
            else
            {
                //当前输入有效
                setCellColor(Color.White, "");

            }

            ////其他模块判断
            //bool rett = UserControl1.UC.getReDataManager().checkVarNameDO("DO05", "TEST");
            //if (rett)
            //{
            //    dataGridView1.CurrentCell.Style.BackColor = Color.Red;
            //    dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
            //    return;
            //}


            if (dataGridView1.CurrentCell.ColumnIndex == columnVarIndex + 1)
            {
                //先判断本界面



                var address = UserControlBase.dataManage.hscList[dataGridView1.CurrentCell.RowIndex].address;

                int i = 0;
                bool buttonStatus = false;
                bool ret = false;
                string curUiVarName = "";
                string curUiAddressName = "";
                foreach (DataRow row in dtData.Rows)
                {
                    ret = false;
                    curUiVarName = row[columnVarIndex].ToString();
                    curUiAddressName = row[columnAddressIndex].ToString();
                    if (curUiVarName.Length == 0)
                    {
                        i++;
                        continue;
                    }

                    //判断不是currentcell字段字符是否有效
                    string tmp = "";
                    if (checkVarNameStrValid(curUiVarName, i, out tmp))
                    {
                        i++;
                        continue;
                    }

                    checkCurUICellColor(curUiVarName, curUiAddressName, ref ret);

                    if (ret)
                    {
                        dataGridView1.Rows[i].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;

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
                        ret = UserControl1.UC.getReDataManager().checkVarNameDO(curUiVarName, address)
                            || UserControl1.UC.getReDataManager().checkVarNameDI(curUiVarName, address);
                        if (ret)
                        {
                            dataGridView1.Rows[i].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                            dataGridView1.Rows[i].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;

                            if (dataGridView1.CurrentCell.RowIndex == i)
                            {
                                setCellColor(Color.Red, string.Format("{0}已被使用", curUiVarName));
                                //dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                                dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                                dataGridView1.CurrentCell.Style.SelectionBackColor = Color.Red;
                            }
                            buttonStatus = true;
                        }
                        else
                        {
                            if (dataGridView1.CurrentCell.RowIndex == i)
                            {
                                dataGridView1.CurrentCell.Style.BackColor = Color.White;
                                dataGridView1.CurrentCell.Style.SelectionBackColor = Color.White;
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[columnVarIndex + 1].Style.BackColor = Color.White;
                            }

                            //dataGridView1.Rows[i].Cells[columnVarIndex].Style.SelectionBackColor = Color.White;



                        }

                        //bool retT = false;
                        ////其他模块判断
                        //var curUiVarNameT = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[columnVarIndex + 1].Value.ToString();
                        //ret = UserControl1.UC.getReDataManager().checkVarNameDO(curUiVarNameT, address) ||
                        //    UserControl1.UC.getReDataManager().checkVarNameDI(curUiVarNameT, address);
                        //if (ret)
                        //{
                        //    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                        //    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;
                        //    setCellColor(Color.Red, string.Format("{0}已被使用", curUiVarName));

                        //    buttonStatus = true;
                        //    return;
                        //}
                        //else
                        //{
                        //    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[columnVarIndex + 1].Style.BackColor = Color.White;
                        //    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.White;



                        //}
                    }
                    i++;
                }




                if (!buttonStatus)
                {
                    //数据没有重复
                    setModifgFlag(true);
                    setButtonEnable(true);
                }

            }

            if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex + 1)
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
                    setModifgFlag(true);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;
            if (row < 0 || column < 0)
            {
                return;
            }

            if (column == columnVarIndex + 1)
            {
                checkTextInput();
            }
            else if (column == columnNoteIndex + 1)
            {
                //注释
                setButtonEnable(true);
                setModifgFlag(true);
            }
        }
    }
}
