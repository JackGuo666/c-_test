using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LocalPLC.Base
{
    public partial class UserControlHighIn : UserControl
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



        public enum TYPE { NOTUSED, SINGLEPULSE, DOUBLEPULSE, FREQUENCY}


        bool init = false;
        System.Text.RegularExpressions.Regex regStr = new System.Text.RegularExpressions.Regex(@"^[\w]{1,32}$");
        System.Text.RegularExpressions.Regex regStrNote = new System.Text.RegularExpressions.Regex(@"^(.{0,32})$");
        ToolTip tip = new ToolTip();
        public UserControlHighIn()
        {
            InitializeComponent();

            init = true;

            typeDescDic.Clear();
            typeDescDic.Add(((int)TYPE.NOTUSED), "未配置");
            typeDescDic.Add(((int)TYPE.SINGLEPULSE), "单相");
            typeDescDic.Add(((int)TYPE.DOUBLEPULSE), "双相");
            typeDescDic.Add(((int)TYPE.FREQUENCY), "频率计");

            BindData();

            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);
            text_Temp.Visible = false;
            text_Temp.WordWrap = false;
            text_Temp.ScrollBars = RichTextBoxScrollBars.None;
            text_Temp.MaxLength = 30;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

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


            init = false;

            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        # region
        DataTable dtData = null;
        const int columnUsedIndex = 0;
        const int columnVarIndex = 1;
        const int columnAddressIndex = 2;
        const int columnTypeIndex = 3;
        const int columnNoteIndex = 4;

        private RichTextBox text_Temp = new RichTextBox();


        Dictionary<int, string> typeDescDic = new Dictionary<int, string>();
        #endregion


        public void initData()
        {
            var list = UserControlBase.dataManage.deviceInfoElem.connector.moduleList;
            foreach(var elem in list)
            {
                if(elem.moduleID == "HSC_FUNC")
                {
                    var moduleList = UserControlBase.dataManage.modules.list;
                    foreach(var elemModule in moduleList)
                    {
                        if(elemModule.moduleID == elem.moduleID)
                        {
                            foreach (var innerElem in elemModule.connectModules.list)
                            {
                                xml.HSCData hscData = new xml.HSCData();

                                DataRow drData;
                                drData = dtData.NewRow();

                                hscData.used = false;
                                drData[columnUsedIndex] = hscData.used;


                                hscData.name = innerElem.parameterName;
                                drData[columnVarIndex] = hscData.name;

                                hscData.address = hscData.name;
                                drData[columnAddressIndex] = hscData.address;

                                hscData.type = (int)TYPE.NOTUSED;
                                if (typeDescDic.ContainsKey(hscData.type))
                                {
                                    drData[columnTypeIndex] = typeDescDic[hscData.type];
                                }
                                else
                                {
                                    drData[columnTypeIndex] = "";
                                }

                                hscData.note = "";
                                drData[columnNoteIndex] = hscData.note; //

                                dtData.Rows.Add(drData);
                                UserControlBase.dataManage.hscList.Add(hscData);
                            }
                        }    
                    }
                    this.dataGridView1.DataSource = dtData;
                }
            }
        }


        public void refreshData()
        {
            dtData.Clear();
            foreach (var hscData in UserControlBase.dataManage.hscList)
            {
                DataRow drData;
                drData = dtData.NewRow();

                drData[columnUsedIndex] = hscData.used;
                drData[columnVarIndex] = hscData.name;
                drData[columnAddressIndex] = hscData.address;
                if (typeDescDic.ContainsKey(hscData.type))
                {
                    drData[columnTypeIndex] = typeDescDic[hscData.type];
                }
                else
                {
                    drData[columnTypeIndex] = "";
                }
                drData[columnNoteIndex] = hscData.note; //

                dtData.Rows.Add(drData);
            }


            //refreshHSCConfigButton();
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
            //drData[1] = "HSC0";
            //drData[2] = "未配置";
            //drData[3] = "";  //类型
            ////drData[4] = 0; //


            ////drData[5] = "注释1";
            //dtData.Rows.Add(drData);
            //drData = dtData.NewRow();
            //drData[0] = 2;
            //drData[1] = "HSC1";
            //drData[2] = "未配置";
            //drData[3] = "";
            ////drData[4] = 0;


            ////drData[5] = "注释2";
            //dtData.Rows.Add(drData);
            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "HSC2";
            //drData[2] = "未配置";
            //drData[3] = "";
            ////drData[4] = 0;

            //dtData.Rows.Add(drData);

            //drData = dtData.NewRow();
            //drData[0] = 1;
            //drData[1] = "HSC3";
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
        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if(e.RowIndex < 0)
            //{
            //    return;
            //}

            //if (e.ColumnIndex == 0)
            //{
            //    e.Handled = true;

            //    using (SolidBrush brush = new SolidBrush(Color.Red))
            //    {
            //        e.Graphics.FillRectangle(brush, e.CellBounds);
            //    }
            //    ControlPaint.DrawBorder(e.Graphics, e.CellBounds, Color.Yellow, ButtonBorderStyle.Outset);
            //}
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

        public void refreshHSCConfigButton()
        {
            foreach (var hsc in UserControlBase.dataManage.hscList)
            {
                if (hsc.address == "HSC6")
                {
                    bool di05Flag = false;
                    foreach (var di in UserControlBase.dataManage.diList)
                    {
                        if (di.channelName == "DI05" && di.used && (di.hscUsed == "HSC0" || di.hscUsed == "HSC2"))
                        {
                            di05Flag = true;
                        }
                    }

                    if (di05Flag)
                    {
                        setButtonEnable(false, 0, 6);
                    }
                    else
                    {
                        setButtonEnable(true, 0, 6);
                    }
                }

                if (hsc.address == "HSC7")
                {
                    bool di07Flag = false;
                    foreach (var di in UserControlBase.dataManage.diList)
                    {
                        if (di.channelName == "DI07" && di.used && (di.hscUsed == "HSC1" || di.hscUsed == "HSC3"))
                        {
                            di07Flag = true;
                        }
                    }

                    if (di07Flag)
                    {
                        setButtonEnable(false, 0, 7);
                    }
                    else
                    {
                        setButtonEnable(true, 0, 7);
                    }
                }

                else if (hsc.address == "HSC2")
                {

                    bool di04Flag = false;
                    bool di05Flag = false;
                    foreach (var di in UserControlBase.dataManage.diList)
                    {
                        if (di.channelName == "DI04" && di.used && di.hscUsed == "HSC0")
                        {
                            di04Flag = true;
                        }

                        if (di.channelName == "DI05" && di.used && (di.hscUsed == "HSC0" || di.hscUsed == "HSC6"))
                        {
                            di05Flag = true;
                        }
                    }

                    if (di04Flag /*|| (di04Flag == false && di05Flag == true)*/)
                    {
                        setButtonEnable(false, 0, 2);
                    }
                    else
                    {
                        setButtonEnable(true, 0, 2);
                    }
                }
                else if (hsc.address == "HSC3")
                {
                    bool di06Flag = false;
                    bool di07Flag = false;
                    foreach (var di in UserControlBase.dataManage.diList)
                    {
                        if (di.channelName == "DI06" && di.used && di.hscUsed == "HSC1")
                        {
                            di06Flag = true;
                        }

                        if (di.channelName == "DI07" && di.used && (di.hscUsed == "HSC1" || di.hscUsed == "HSC7"))
                        {
                            di07Flag = true;
                        }
                    }


                    if (di06Flag/* || (di07Flag == false && di06Flag == true)*/)
                    {
                        setButtonEnable(false, 0, 3);
                    }
                    else
                    {
                        setButtonEnable(true, 0, 3);
                    }
                }

                else if (hsc.address == "HSC4")
                {
                    bool di01Flag = false;
                    foreach (var di in UserControlBase.dataManage.diList)
                    {
                        if (di.channelName == "DI01" && di.used && di.hscUsed == "HSC0")
                        {
                            //被HSC0占用
                            di01Flag = true;
                        }
                    }

                    if (di01Flag)
                    {
                        setButtonEnable(false, 0, 4);
                    }
                    else
                    {
                        setButtonEnable(true, 0, 4);
                    }
                }
                else if (hsc.address == "HSC5")
                {
                    bool di03Flag = false;
                    foreach (var di in UserControlBase.dataManage.diList)
                    {
                        if (di.channelName == "DI03" && di.used && di.hscUsed == "HSC1")
                        {
                            //被HSC0占用
                            di03Flag = true;
                        }
                    }
                    if (di03Flag)
                    {
                        setButtonEnable(false, 0, 5);
                    }
                    else
                    {
                        setButtonEnable(true, 0, 5);
                    }
                }
            }


            //数据刷新到DI DO datarow里,动态更新
            UserControl1.UC.refreshDIUserBaseUI();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            if(e.RowIndex < 0)
            {
                return;
            }


            if (e.ColumnIndex == dataGridView1.Columns["配置"].Index)
            {
                if (dataGridView1.CurrentCell is DataGridViewDisableButtonCell)
                {
                    if( ((DataGridViewDisableButtonCell)dataGridView1.CurrentCell).Enabled == false)
                    {
                        return;
                    }
                }

                //Do something with your button. 
                 FormHighInput color = new FormHighInput(typeDescDic, UserControlBase.dataManage.hscList[e.RowIndex]);
                color.StartPosition = FormStartPosition.CenterScreen;
                color.ShowDialog();


                var row = e.RowIndex;
                var col = e.ColumnIndex;




                var type = UserControlBase.dataManage.hscList[e.RowIndex].type;
                if(typeDescDic.ContainsKey(type))
                {
                    //dtData.Rows[row][col] = typeDescDic[type];
                    dtData.Rows[row][columnTypeIndex] = typeDescDic[type];

                    dtData.Rows[row][columnUsedIndex] = UserControlBase.dataManage.hscList[e.RowIndex].used;
                }

                refreshHSCConfigButton();
                
            }

            foreach(var test in UserControlBase.dataManage.diList)
            {
                if(test.used)
                {
                    utility.PrintError(test.channelName);
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(init)
            {
                return;
            }
            if (dataGridView1.CurrentCell.Value.ToString() != text_Temp.Text)
            {
                dataGridView1.CurrentCell.Value = text_Temp.Text;
               // text_Temp.Focus();
            }

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
                if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex + 1)
                {
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
                else if(this.dataGridView1.CurrentCell.ColumnIndex == columnVarIndex + 1)
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
            dataGridView1.Columns[columnTypeIndex].ReadOnly = true;
        }

        private void UserControlHighIn_Load(object sender, EventArgs e)
        {
            refreshHSCConfigButton();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            text_Temp.Hide();
            dataGridView1.CurrentCell = null;
            getDataFromUI();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        void getDataFromUI()
        {
            int row = 0;
            var hscList = UserControlBase.dataManage.hscList;
            foreach (DataRow dr in dtData.Rows)
            {
                bool.TryParse(dr[columnUsedIndex].ToString(), out hscList[row].used);
                hscList[row].name = dr[columnVarIndex].ToString();
                hscList[row].address = dr[columnAddressIndex].ToString();
                //type
                hscList[row].note = dr[columnNoteIndex].ToString();

                row++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshData();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
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
                            ret = UserControl1.UC.getReDataManager().checkVarNameDO(name, channel);
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

        bool checkVarNameStrValid(string name, int row)
        {
            if (!regStr.IsMatch(name))
            {
                dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;


                return true;
            }
            else
            {
                char[] c = name.ToCharArray();
                if (c[0] >= '0' && c[0] <= '9')
                {
                    dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
                    dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;

                    return true;
                }
                else
                {
                    //setCellColor(Color.White, "");

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

        bool checkVarCurUI(string name, int row)
        {
            var curUiVarName = dataGridView1.Rows[row].Cells[columnVarIndex + 1].Value.ToString();
            var address = dataGridView1.Rows[row].Cells[columnAddressIndex + 1].Value.ToString();
            ////其他模块判断
            bool ret = UserControl1.UC.getReDataManager().checkVarNameDO(curUiVarName, address);
            Color color = Color.Red;
            if(!ret)
            {
                color = Color.White;
            }
            //if(ret)
            {
                //dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.BackColor = color;
                //dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.SelectionBackColor = color;
                dataGridView1.CurrentCell.Style.BackColor = color;
                dataGridView1.CurrentCell.Style.SelectionBackColor = color;

                return true;
            }

            //return ret;

            //int i = 0;
            //foreach (DataRow item in dtData.Rows)
            //{
            //    if(i != row)
            //    {
            //        var curUiVarName = item[columnVarIndex].ToString();
            //        var curUiAddressName = item[columnAddressIndex].ToString();
            //        if(name == curUiVarName)
            //        {
            //            dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.BackColor = Color.Red;
            //            dataGridView1.Rows[row].Cells[columnVarIndex + 1].Style.SelectionBackColor = Color.Red;

            //            return true;
            //        }
            //    }

            //    i++;
            //}

            //return false;
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
            else if(checkVarCurUI(dataGridView1.CurrentCell.Value.ToString(), dataGridView1.CurrentCell.RowIndex))
            {
                setCellColor(Color.Red, string.Format("{0} 格式无效", dataGridView1.CurrentCell.Value.ToString()));
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
                    if (checkVarNameStrValid(curUiVarName, i))
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
                        ret = UserControl1.UC.getReDataManager().checkVarNameDO(curUiVarName, address);
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

            if(column == columnVarIndex + 1)
            {
                checkTextInput();
            }
        }
    }
}
