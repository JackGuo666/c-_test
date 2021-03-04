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
    public partial class UserControlDO : UserControl
    {
        public UserControlDO(string name)
        {
            InitializeComponent();
            //this.DoubleBuffered = true;
            //this.DoubleBuffered = true;//设置本窗体
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            BindData();

            // 设置下拉列表框不可见
            text_Temp.Visible = false;
            text_Temp.TextChanged += new System.EventHandler(textBox1_TextChanged);
            text_Temp.Visible = false;
            text_Temp.WordWrap = false;

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

                    diData.varName = "";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = text_Temp.Text;
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
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string varName = dataGridView1.CurrentCell.Value.ToString();

                    text_Temp.Text = this.dataGridView1.CurrentCell.Value.ToString();


                    text_Temp.Left = rect.Left;
                    text_Temp.Top = rect.Top;
                    text_Temp.Width = rect.Width;
                    text_Temp.Height = rect.Height;
                    text_Temp.Visible = true;
                    text_Temp.Focus();
                    text_Temp.Select(text_Temp.SelectionStart, 0);
                    text_Temp.SelectionStart = text_Temp.TextLength;
                    text_Temp.ScrollToCaret();
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
            dataGridView1.Columns[columnAddressIndex].ReadOnly = true;
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
    }
}
