using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocalPLC.Base;

namespace LocalPLC.ModbusMaster
{
    public partial class modbusmasterDeviceform : Form
    {
        public modbusmasterDeviceform()
        {
            InitializeComponent();

           
        }
        public static ModbusServer.ServerIndex serverdata { get; set; } = new ModbusServer.ServerIndex();
        public static UserControlBase UC { get; set; } = new UserControlBase();
        private ModbusMasterData masterData_;
        private int masterStartAddr_ = 0;
        private ModbusMasterManage mastermanage;
        private int MID;
        public void getMasterData(ref ModbusMasterData data, int masterStartAddr,ModbusMasterManage a,int masterid)
        {
            masterData_ = data;
            masterData_.ID = data.ID;
            masterStartAddr_ = masterStartAddr;
            mastermanage = a;
            MID = masterid;
        }

        public enum COLUMNNAME :  int
            { ID, NAME, SLAVE_ADDR, PERMIT_TIMEOUT_COUNT, RECONNECT_INTERVAL
                                        , RESET_VARIABLE, CHANNEL};
        private string[] columnName = {"ID", "name"};
        private void modbusmasterform_Load(object sender, EventArgs e)
        {
            LocalPLC.Base.xml.DataManageBase baseData = null;
            UserControl1.UC.getDataManager(ref baseData);
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            comboBox_transform_channel.Items.Clear();
            foreach (string serialname in baseData.serialDic.Keys)
            {
                comboBox_transform_channel.Items.Add(serialname);

            }
            comboBox_transform_channel.SelectedIndex = -1;
            textBox_packet_interval.Text = masterData_.packet_interval.ToString();
            try
            {
                DataGridViewTextBoxColumn cellColumnID = new DataGridViewTextBoxColumn();
                cellColumnID.Name = "ID";
                DataGridViewTextBoxColumn cellColumnName = new DataGridViewTextBoxColumn();
                cellColumnName.Name = "名称";
                DataGridViewTextBoxColumn cellColumnSlaveAddr = new DataGridViewTextBoxColumn();
                cellColumnSlaveAddr.Name = "从站地址";
                //DataGridViewTextBoxColumn cellColumnTimeout = new DataGridViewTextBoxColumn();
                //cellColumnTimeout.Name = "响应超时(ms)";
                DataGridViewTextBoxColumn cellColumnTimeoutCount = new DataGridViewTextBoxColumn();
                cellColumnTimeoutCount.Name = "允许的超时次数";
                DataGridViewTextBoxColumn cellColumnReconnectInvertal = new DataGridViewTextBoxColumn();
                cellColumnReconnectInvertal.Name = "重连间隔";
                DataGridViewTextBoxColumn cellColumnResetVariable = new DataGridViewTextBoxColumn();
                cellColumnResetVariable.Name = "复位变量";
                DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
                buttonColumn.Name = "通道";
                
                //列标题自适应
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


                dataGridView1.Columns.Add(cellColumnID);
                dataGridView1.Columns.Add(cellColumnName);
                dataGridView1.Columns.Add(cellColumnSlaveAddr);
                //dataGridView1.Columns.Add(cellColumnTimeout);
                dataGridView1.Columns.Add(cellColumnTimeoutCount);
                dataGridView1.Columns.Add(cellColumnReconnectInvertal);
                dataGridView1.Columns.Add(cellColumnResetVariable);
                dataGridView1.Columns.Add(buttonColumn);

                dataGridView1.RowCount = 1 + masterData_.modbusDeviceList.Count;
               
                int i = 0;
                foreach (DeviceData devData in masterData_.modbusDeviceList)
                {
                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = devData.ID;

                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value = devData.nameDev;

                    //
                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value = devData.slaveAddr;

                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value = devData.reponseTimeout;

                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value = devData.permitTimeoutCount;

                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value = devData.reconnectInterval;

                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value = devData.resetVaraible;

                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.CHANNEL].Value = "..."/* + i.ToString()*/;

                    i++;
                }

                //dataGridView1.RowCount = /*8*/ 1;
                dataGridView1.AutoSize = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                //this.comboBox_transform_channel.Items.Add("COM1");
                this.comboBox_transform_channel.Text = masterData_.transformChannel;

                this.textBox_reponse_timeout.Text = masterData_.responseTimeout.ToString();   //ms

                if (masterData_.transformMode == 0)
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else if (masterData_.transformMode == 1)
                {
                    radioButton2.Checked = true;
                    radioButton1.Checked = false;
                }
            }

            catch (Exception t)
            {
                MessageBox.Show(t.Message);
                return;
            }
        }
        private int[] temrow = new int[16] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        private void button_add_Click(object sender, EventArgs e)
        {
            if (utility.masterDeviceCountMax <= dataGridView1.RowCount)
            {
                string err = string.Format("设备最大个数是{0}", utility.masterDeviceCountMax);
                utility.PrintError(err);
                return;
            }

            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;

            DeviceData data = new DeviceData();

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //dataGridView1.Rows[i].Cells["ID"].Value = row;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = row;
                data.ID = row;
                int flag = 0;
               
                    for (int j = 0; j < dataGridView1.Rows.Count-1; j++)
                    {
                    string a = dataGridView1.Rows[j].Cells["名称"].Value.ToString();
                        if (dataGridView1.Rows[j].Cells["名称"].Value.ToString() == "设备" + i.ToString())
                        {
                            flag++;
                        }
                    }
                
               
                if (flag == 0)
                {
                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value = "设备" + i.ToString();
                    data.nameDev = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value.ToString();
                }
                else
                {
                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value = "";
                    data.nameDev = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value.ToString();
                }
                //
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value = "";
                data.slaveAddr = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value.ToString();

                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value = 1000;
                //data.reponseTimeout = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value.ToString());

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value = 5;
                data.permitTimeoutCount = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value.ToString());

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value = 1000;
                data.reconnectInterval = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value.ToString());

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value = "";
                data.resetVaraible =  dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value.ToString();
                string devkey = null;
                if (row < 10)
                {
                    devkey = 0 + row.ToString();
                }
                else if (row >= 10 && row <= 16)
                {
                    devkey = row.ToString();
                }
                data.resetkey[0] = masterData_.ID.ToString();
                data.resetkey[1] = devkey;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.CHANNEL].Value = "..."/* + i.ToString()*/;
                //data.


                //dataGridView1.Cell.Value = "Button " + i.ToString();

                //data_.modbusDeviceList.Add(data);
                masterData_.addDevice(ref data);


            }
            for (int j = 0; j < 16; j++)
            {
                if (temrow[j] == -1)
                {
                    temrow[j] = row;
                    break;
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            // int row = dataGridView1.SelectedRows[0];
            try
            {
                int n = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("请选择一整行进行删除");
                    return;
                }
                else
                {
                   // CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                   // cm.SuspendBinding(); //挂起数据绑定
                    dataGridView1.Rows[n].Visible = false;
                    //cm.ResumeBinding(); //恢复数据绑定
                }
                //for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                //{
                //    int index = dataGridView1.SelectedRows[i].Index;

                //    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                //    //masterData_.modbusDeviceList.RemoveAt(index);
                //    var device = masterData_.modbusDeviceList[index];
                //    masterData_.removeDevice(ref device);

                //}
            }
            catch
            {
                return;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Index == (int)COLUMNNAME.CHANNEL)
            {
                DataGridViewDisableButtonCell buttonCell =
                    (DataGridViewDisableButtonCell)dataGridView1.
                    Rows[e.RowIndex].Cells[(int)COLUMNNAME.CHANNEL];

                if (buttonCell.Enabled)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    //    Cells[e.ColumnIndex].Value.ToString() +
                    //    " is enabled");

                    //刷新地址
                    masterData_.refreshAddr();
                    modbusmasterchannel form = new modbusmasterchannel();
                    DeviceData data = masterData_.modbusDeviceList.ElementAt(e.RowIndex);
                    form.getDeviceData(ref data, masterStartAddr_, ref masterData_,ref mastermanage,MID,e.RowIndex);
                    form.devicenumber(data.ID);
                    form.devaddr(dataGridView1.Rows[e.RowIndex].Cells["从站地址"].Value.ToString());
                    form.devname(dataGridView1.Rows[e.RowIndex].Cells["名称"].Value.ToString());
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
        public bool isNumber(string message)
        {
            try
            {
                int a = Convert.ToInt32(message);
                return true;
            }
            catch
            {
                MessageBox.Show("请输入范围内的数字");
                return false;
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object obj = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string str = "";
            if (obj == null)
            {

            }
            else
            {
                str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (str.Equals(""))
                {

                }
            }



            if (e.ColumnIndex == (int)COLUMNNAME.ID)
            {
                //masterData_.modbusDeviceList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.NAME)
            {
                //masterData_.modbusDeviceList.ElementAt(e.RowIndex).nameDev = str;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (str == dataGridView1.Rows[i].Cells[1].Value.ToString() && i != e.RowIndex)
                    {
                        MessageBox.Show("设备名有重复，请重新设置！");
                        //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.SLAVE_ADDR)
            {
                bool number;
                try
                {
                    number = isNumber(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                catch
                {
                    number = false;
                }
                //if (number == true && (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) >= 1 && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) <= 100))
                //{
                //    int flag = 0;
                //    for (int i = 0; i < masterData_.modbusDeviceList.Count; i++)
                //    {
                //        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == masterData_.modbusDeviceList[i].slaveAddr && e.RowIndex!=i)
                //        {
                //            flag++;
                //        }
                //    }
                //    if (flag == 0)
                //    {
                //        //masterData_.modbusDeviceList.ElementAt(e.RowIndex).slaveAddr = str;
                //    }
                //    else
                //    {
                //        MessageBox.Show("从站地址有重复，请重新设置");
                //        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                //    }
            //}
                    if(number == true && (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) >= 1 && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) <= 247))
                {

                }
                    
                    else
                    {
                        MessageBox.Show("请输入1-247的数字作为从站地址");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                    }
                
               
            }
            //else if(e.ColumnIndex == (int)COLUMNNAME.REPONSE_TIMEOUT)
            //{   
            //    int.TryParse(str, out masterData_.modbusDeviceList.ElementAt(e.RowIndex).reponseTimeout);
            //}
            else if(e.ColumnIndex == (int)COLUMNNAME.PERMIT_TIMEOUT_COUNT)
            {
                try
                {
                    int value = Convert.ToInt32(str);
                    if (value >= 0 && value <= 10)
                    {
                        //int.TryParse(str, out masterData_.modbusDeviceList.ElementAt(e.RowIndex).permitTimeoutCount);
                    }
                    else
                    {
                        MessageBox.Show("请输入0-10的数字");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 10;
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入0-10的数字");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 10;
                    return;
                }
                //int.TryParse(str, out masterData_.modbusDeviceList.ElementAt(e.RowIndex).permitTimeoutCount);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.RECONNECT_INTERVAL)
            {
                try
                {
                    int value = Convert.ToInt32(str);
                    if (value >= 100 && value <= 10000)
                    {
                        //int.TryParse(str, out masterData_.modbusDeviceList.ElementAt(e.RowIndex).reconnectInterval);
                    }
                    else
                    {
                        MessageBox.Show("请输入300-10000的数字");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1000;
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入300-10000的数字");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1000;
                    return;
                }
                //int.TryParse(str, out masterData_.modbusDeviceList.ElementAt(e.RowIndex).reconnectInterval);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.RESET_VARIABLE)
            {
                int flag = 0;
                for (int i = 0; i < mastermanage.modbusMastrList.Count; i++)
                {
                    for (int j = 0; j < mastermanage.modbusMastrList[i].modbusDeviceList.Count; j++)
                    {
                        if(str == mastermanage.modbusMastrList[i].modbusDeviceList[j].resetVaraible &&(i != MID || j != e.RowIndex))
                        {
                            flag++;
                        }
                        
                        for(int k =0;k< mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList.Count;k++)
                        {
                            if (str == mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList[k].trigger || 
                                str == mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList[k].error && j != e.RowIndex)
                            {
                                flag++;
                            }
                        }
                    }
                }
                for (int l = 0; l < dataGridView1.Rows.Count; l++)
                {
                    if (dataGridView1.Rows[l].Cells[5].Value.ToString() == str && l != e.RowIndex && str != "")
                    {
                        flag++;
                    }
                }
                if (flag == 0)
                {
                    //masterData_.modbusDeviceList[e.RowIndex].resetVaraible = str;
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = dataGridView1.Rows[0].Cells[0].Style.BackColor;
                }
                else
                {
                    MessageBox.Show("复位变量名有重复，请检查后重新输入");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                }
            //masterData_.modbusDeviceList.ElementAt(e.RowIndex).resetVaraible = str;
            }
            
            
            else if(e.ColumnIndex == (int)COLUMNNAME.CHANNEL)
            {

            }
        }

        private void comboBox_transform_channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocalPLC.ModbusServer.DataManager servermanage = null;
            serverdata.getservermanage(ref servermanage);
            LocalPLC.Base.xml.DataManageBase baseData = null;
            UC.getDataManager(ref baseData);
            try
            {
                for (int i = 0; i < mastermanage.modbusMastrList.Count; i++)
                {
                    if (comboBox_transform_channel.SelectedItem.ToString() == mastermanage.modbusMastrList[i].transformChannel && i != MID)
                    {
                        comboBox_transform_channel.SelectedIndex = -1;
                        MessageBox.Show("该传输通道已被占用，请选择其他通道");
                    }
                }
                for (int j = 0;j<servermanage.listServer.Count;j++)
                {
                    if(servermanage.listServer[j].dataDevice_.transformportdescribe == comboBox_transform_channel.SelectedItem.ToString())
                    {
                        comboBox_transform_channel.SelectedIndex = -1;
                        MessageBox.Show("该传输通道已被server占用，请选择其他通道");
                    }
                }
                if (baseData.serialDic[comboBox_transform_channel.SelectedItem.ToString()].dataBit != 8 && radioButton1.Checked == true)
                {
                    MessageBox.Show("RTU模式下，串口的数据位不能为7");
                    comboBox_transform_channel.SelectedIndex = -1;
                }
            }
            catch
            {
                return;
            }
        }

        private void textBox_reponse_timeout_TextChanged(object sender, EventArgs e)
        {

            string str = textBox_reponse_timeout.Text;
            //try
            //{
            //    int value = Convert.ToInt32(str);
            //    if(value < 100 || value >10000)
            //    {
            //        MessageBox.Show("请输入100-10000的数字");
            //        textBox_reponse_timeout.Text = "1000";
            //    }
            //}
            //catch
            //{

            //}
            int value;
            if(!int.TryParse(str.ToString(), out value))
            {
                
                textBox_reponse_timeout.Text = "1000";
            }
           else
            {
                if(Convert.ToInt32(str)<100|| Convert.ToInt32(str)>10000)
                {
                    MessageBox.Show("请输入100-10000的数字");
                    textBox_reponse_timeout.Text = "1000";
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if(radioButton1.Checked == true)
            //{
            //    masterData_.transformMode = 0;
            //}
            //else 
            //{
            //    masterData_.transformMode = 1;
            //}

            LocalPLC.Base.xml.DataManageBase baseData = null;
            UC.getDataManager(ref baseData);
            try
            {
                if (baseData.serialDic[comboBox_transform_channel.SelectedItem.ToString()].dataBit != 8 && radioButton1.Checked == true)
                {
                    MessageBox.Show("RTU模式下，串口的数据位不能为7");
                    comboBox_transform_channel.SelectedIndex = -1;
                }
            }
            catch
            {

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                masterData_.transformMode = 1;
            }
            else
            {
                masterData_.transformMode = 0;
            }
        }

        private void modbusmasterDeviceform_FormClosing(object sender, FormClosingEventArgs e)
        {
            int length = 0;
            for (int i = 0; i < masterData_.modbusDeviceList.Count; i++)
            {
                length += masterData_.modbusDeviceList[i].curDeviceLength;
            }
            if (length >= 2000)
            {
                MessageBox.Show("master" + MID.ToString() + "长度超过2000，请重新设置");
                utility.PrintError("master" + MID.ToString() + "长度超过2000，请重新设置");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            //int flag = 0;
            //for (int j =0;j<dataGridView1.Rows.Count;j++)
            //{
            //    if(dataGridView1.Rows[j].Cells["从站地址"].Value.ToString() == "")
            //    {
            //        flag++;
            //    }
            //}
            //if(flag>0)
            //{
            //    MessageBox.Show("从站地址还未设置，请设置");
            //    e.Cancel = true;
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 15; i >= 0; i--)
            {
                if (temrow[i] != -1)
                {
                    dataGridView1.Rows.RemoveAt(temrow[i]);
                    masterData_.modbusDeviceList.RemoveAt(temrow[i]);
                }
            }
            this.Close();
        }
        private void refreshtemrow()
        {
            for (int i = 0; i < 16; i++)
            {
                temrow[i] = -1;
            }
        }
        public void refresh()
        {
            for (int i = 0; i < masterData_.modbusDeviceList.Count; i++)
            {
                masterData_.modbusDeviceList[i].ID = i;
                if (i > 0)
                {
                    masterData_.modbusDeviceList[i].curDeviceAddr = masterData_.modbusDeviceList[i - 1].curDeviceAddr + masterData_.modbusDeviceList[i - 1].curDeviceLength;
                }
                else if (i == 0)
                {
                    masterData_.modbusDeviceList[i].curDeviceAddr = masterData_.curMasterStartAddr;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox_transform_channel.SelectedIndex == -1)
            {
                MessageBox.Show("传输通道未配置！");
                return;
            }
            
                int flag = 0;
                int flag1 = 0;
                for (int j = dataGridView1.Rows.Count-1; j >= 0; j--)
                {
                    try
                    {
                        string text = dataGridView1.Rows[j].Cells["从站地址"].Value.ToString();
                    }
                    catch
                    {
                        dataGridView1.Rows[j].Cells["从站地址"].Value = "";
                    }
                if (dataGridView1.Rows[j].Cells["从站地址"].Value.ToString() == "" || dataGridView1.Rows[j].Cells["从站地址"].Value == null 
                        && dataGridView1.Rows[j].Visible == true )
                    {
                        flag++;
                    }
                    if (dataGridView1.Rows[j].Cells["名称"].Value.ToString() == "")
                    {
                        flag1++;
                    }
                }
                if (flag > 0)
                {
                    MessageBox.Show("从站地址还未设置，请设置");
                    return;
                }
                else if (flag1 > 0)
                {
                    MessageBox.Show("请输入设备名称");
                    return;
                }
                else
                {
                    refreshtemrow();
                    for (int m = dataGridView1.Rows.Count - 1; m >= 0; m--)
                    {
                        if (dataGridView1.Rows[m].Visible == false)
                        {
                            dataGridView1.Rows.RemoveAt(m);
                            masterData_.modbusDeviceList.RemoveAt(m);
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                dataGridView1.Rows[i].Cells[0].Value = i;
                                masterData_.modbusDeviceList[i].ID = i;
                            }
                            masterData_.refreshAddr();
                        }
                    }
                    if (radioButton1.Checked == true)
                    {
                        masterData_.transformMode = 0;
                    }
                    else
                    {
                        masterData_.transformMode = 1;
                    }

                    masterData_.transformChannel = comboBox_transform_channel.SelectedItem.ToString();
                    masterData_.packet_interval = Convert.ToInt32( textBox_packet_interval.Text);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        masterData_.transformChannel = comboBox_transform_channel.Text;
                        masterData_.modbusDeviceList[i].ID = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        masterData_.modbusDeviceList[i].nameDev = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        masterData_.modbusDeviceList[i].slaveAddr = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        int.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out masterData_.modbusDeviceList[i].permitTimeoutCount);
                        int.TryParse(dataGridView1.Rows[i].Cells[4].Value.ToString(), out masterData_.modbusDeviceList[i].reconnectInterval);
                        masterData_.modbusDeviceList[i].resetVaraible = dataGridView1.Rows[i].Cells[5].Value.ToString();

                    }


                }
            this.Close();
        }

        private void textBox_packet_interval_Leave(object sender, EventArgs e)
        {
            int value;
            try
            {
                value = Convert.ToInt32(textBox_packet_interval.Text);
            }
            catch
            {
                MessageBox.Show("请输入0-1000的数字");
                textBox_packet_interval.Text = masterData_.packet_interval.ToString();
                return;
            }
            if (value < 0 || value > 1000)
            {
                MessageBox.Show("请输入0-1000的数字");
                textBox_packet_interval.Text = masterData_.packet_interval.ToString();
                return;
            }
        }
    }
}
