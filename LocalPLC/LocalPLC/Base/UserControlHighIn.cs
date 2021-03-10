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
        public UserControlHighIn()
        {
            InitializeComponent();

            typeDescDic.Clear();
            typeDescDic.Add(((int)TYPE.NOTUSED), "未配置");
            typeDescDic.Add(((int)TYPE.SINGLEPULSE), "单相");
            typeDescDic.Add(((int)TYPE.DOUBLEPULSE), "双相");
            typeDescDic.Add(((int)TYPE.FREQUENCY), "频率计");

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

            // 禁止用户改变列头的高度  
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
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

                                hscData.address = "";
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




        void setButtonConfig(bool enable, int column, int row)
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

                //根据高速DI使用情况，HSC轴是否可以配置调整
                foreach(var di in UserControlBase.dataManage.diList)
                {
                    if(di.channelName == "DI01")
                    {
                        if(di.used)
                        {
                            //DI00使用，HSC4不可以配置
                            //DataGridViewDisableButtonColumn button = (DataGridViewDisableButtonColumn)dataGridView1.Columns[0];
                            //DataGridViewButtonCell vCell = (DataGridViewButtonCell)dataGridView1[0, 4];
                            //if (vCell is DataGridViewDisableButtonCell)
                            //{
                            //    ((DataGridViewDisableButtonCell)vCell).Enabled = false;
                            //    dataGridView1.Invalidate();
                            //}

                            setButtonConfig(false, 0, 4);

                        }
                        else
                        {
                            setButtonConfig(true, 0, 4);
                        }
                    }
                    else if(di.channelName == "DI03")
                    {
                        if(di.used)
                        {
                            setButtonConfig(false, 0, 5);
                        }
                        else
                        {
                            setButtonConfig(true, 0, 5);
                        }
                    }
                    else if(di.channelName == "DI05")
                    {
                        if (di.used)
                        {
                            setButtonConfig(false, 0, 6);
                        }
                        else
                        {
                            setButtonConfig(true, 0, 6);
                        }
                    }
                    else if (di.channelName == "DI07")
                    {


                        if (di.used)
                        {
                            setButtonConfig(false, 0, 7);
                        }
                        else
                        {
                            setButtonConfig(true, 0, 7);
                        }
                    }
                }
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
                if (dataGridView1.CurrentCell.ColumnIndex == columnNoteIndex + 1)
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
            ////绑定事件DataBindingComplete 之后设置才有效果
            //dataGridView1.Columns[columnUsedIndex].ReadOnly = true;
            ////背景设置灰色只读
            //dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;


            //dataGridView1.Columns[columnVarIndex].ReadOnly = true;
            //dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
            //dataGridView1.Columns[columnTypeIndex].ReadOnly = true;
        }
    }
}
