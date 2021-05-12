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

namespace LocalPLC.Base
{
    public partial class UserControlHighOutput : UserControl
    {
        public enum TYPE { NOTUSED, PLS, PWM, FREQUENCY, PTO}
        public UserControlHighOutput()
        {
            InitializeComponent();

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


            int columnIndex = 4;
            if (dataGridView1.Columns["配置"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, uninstallButtonColumn);
            }

        }

        #region
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = text_Temp.Text;
        }

        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["配置"].Index && e.RowIndex >= 0)
            {
                //Do something with your button.
                FormHighOutput color = new FormHighOutput(typeDescDic, UserControlBase.dataManage.hspList[e.RowIndex]);
                color.StartPosition = FormStartPosition.CenterScreen;
                color.ShowDialog();

                var row = e.RowIndex;
                var col = e.ColumnIndex;

                var type = UserControlBase.dataManage.hspList[e.RowIndex].type;
                if(typeDescDic.ContainsKey(type))
                {
                    //dtData.Rows[row][col] = typeDescDic[type];
                    dtData.Rows[row][columnTypeIndex] = typeDescDic[type];
                    dtData.Rows[row][columnUsedIndex] = UserControlBase.dataManage.hspList[e.RowIndex].used;
                }


                //数据刷新到DI DO datarow里,动态更新
                UserControl1.UC.refreshDOUserBaseUI();
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
            //绑定事件DataBindingComplete 之后设置才有效果
            dataGridView1.Columns[columnUsedIndex].ReadOnly = true;
            //背景设置灰色只读
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Lavender;


            dataGridView1.Columns[columnVarIndex].ReadOnly = true;
            dataGridView1.Columns[columnTypeIndex].ReadOnly = true;
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
            getDataFromUI();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}
