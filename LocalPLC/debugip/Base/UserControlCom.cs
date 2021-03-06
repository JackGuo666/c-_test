﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using LocalPLC.Base.xml;
using LocalPLC.Interface;

namespace LocalPLC.Base
{
    public partial class UserControlCom : UserControl, IGetModifyFlag
    {
        public SERIALData serialValueData_ = null;
        bool configured_ = false;
        //string polKey = "ModPol";
        List<EnumElem> mediumList_ = null;
        List<EnumElem> polList_ = null;
        bool initDone = false;
        
        string HAS_RS232 = "0";
        string HAS_RS485 = "1";
        string HAS_BOTH = "2";

        string DATABIE_DISENABLE = "0";
        string DATABIE_ENABLE = "1";
        enum TERMINALRESIS { HAS_RS232, HAS_RS485, HAS_BOTH}
        public UserControlCom(UserControlBase ub, string com, SERIALData serialValueData, bool configured = false)
        {
            InitializeComponent();

            UserControl1 us = ub.parent_ as UserControl1;

            setTreeNodeStatusDelegate = new setTreeNodeStatusEventHandler(us.setTreeComEthNodeStats);
            serialValueData_ = serialValueData;
            //串口名
            com_ = com;
            //configured为true，串口数据加载config_project
            //configured为false，串口数据加载控制器模板数据
            configured_ = configured;



            if(serialValueData_.terminalResis == HAS_BOTH)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else if(serialValueData_.terminalResis == HAS_RS485)
            {
                radioButton2.Enabled = false;
            }


            if(serialValueData.databitEnable == DATABIE_DISENABLE)
            {
                comboBox_Databit.Enabled = false;
            }
            else if(serialValueData_.databitEnable == DATABIE_ENABLE)
            {
                comboBox_Databit.Enabled = true;
            }




            initDone = false;
            if (configured_)
            {
                Init();
                setDataToUI();
            }
            else
            {
                Init();
            }



            initDone = true;
            //数据管理里的串口数组
            //UserControlBase.dataManage.serialDic.Add(com_, serialValueData);


            setButtonEnable(false);
        }


        #region 
        //代理
        public delegate void setTreeNodeStatusEventHandler(string tag, string name);
        setTreeNodeStatusEventHandler setTreeNodeStatusDelegate = null;
        #endregion

        #region
        //接口
        bool modifiedFlag = false;
        void setModifgFlag(bool flag)
        {
            modifiedFlag = flag;
            if(flag)
            {
                setTreeNodeStatusDelegate("SERIAL_LINE", com_);
                setTreeNodeStatusDelegate("COMMUNICATION_LINE", "通信线路");
            }
        }

        //接口实现
        public bool getModifyFlag()
        {
            //if (!checkDataGridView())
            //{

            //}

            if (modifiedFlag)
            {
                if (MessageBox.Show("是否保存修改数据?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    // 保存
                    button_valid_Click(null, null);
                }
                else
                {
                    // 不保存
                    button_cancel_Click(null, null);
                }
            }

            return modifiedFlag;
        }

            #endregion

            private void setDataToUI()
        {
            textBox_Com.Text = com_;

            var itemsBaud = comboBox_Baud.Items;
            foreach (var item in itemsBaud)
            {
                ComboboxItem combo = (ComboboxItem)item;
                if (combo.Value.ToString() == serialValueData_.baud.ToString())
                {
                    comboBox_Baud.SelectedItem = combo;
                }
            }

            var itemsParity = comboBox_Parity.Items;
            foreach (var item in itemsParity)
            {
                ComboboxItem combo = (ComboboxItem)item;
                if (combo.Value.ToString() == serialValueData_.Parity.ToString())
                {
                    comboBox_Parity.SelectedItem = combo;
                }
            }

            ComboBox.ObjectCollection collection = comboBox_Databit.Items;
            foreach (var dataBit in collection)
            {
                ComboboxItem combo = (ComboboxItem)dataBit;
                if (combo.Value.ToString() == serialValueData_.dataBit.ToString())
                {
                    comboBox_Databit.SelectedItem = combo;
                }
            }

            ComboBox.ObjectCollection collectionStopBit = comboBox_StopBit.Items;
            foreach (var stopBit in collectionStopBit)
            {
                ComboboxItem combo = (ComboboxItem)stopBit;
                if (combo.Value.ToString() == serialValueData_.stopBit.ToString())
                {
                    comboBox_StopBit.SelectedItem = combo;
                }
            }

            if(serialValueData_.rsMode == (int)SerialMode.RS232)
            {
                radioButton2.Checked = true;
            }
            else if(serialValueData_.rsMode == (int)SerialMode.RS485)
            {
                radioButton1.Checked = true;
            }

            foreach(var combo in comboBox1.Items)
            {
                var item = combo as ComboboxItem;
                if(item.Value.ToString() == serialValueData_.polR.ToString())
                {
                    comboBox1.SelectedItem = item;
                }
            }


            if(comboBox_Databit.SelectedItem.ToString() == "7")
            {
                comboBox_Parity.Enabled = false;
            }
        }

        enum SerialMode { RS232, RS485};
        enum SerialPol { RS232POL, RS485POL };
        public void getDataFromUI()
        {
            serialValueData_.name = textBox_Com.Text.ToString();
            string strBaud = ((ComboboxItem)comboBox_Baud.SelectedItem).Value.ToString();
            int.TryParse(strBaud, out serialValueData_.baud);

            string strParity = ((ComboboxItem)comboBox_Parity.SelectedItem).Value.ToString();
            int.TryParse(strParity, out serialValueData_.Parity);

            string strDatabit = ((ComboboxItem)comboBox_Databit.SelectedItem).Value.ToString();
            int.TryParse(strDatabit, out serialValueData_.dataBit);

            string strStopbit = ((ComboboxItem)comboBox_StopBit.SelectedItem).Value.ToString();
            int.TryParse(strStopbit, out serialValueData_.stopBit);


            if(radioButton1.Checked)
            {
                serialValueData_.rsMode = (int)SerialMode.RS485;
                //极化电阻
                var list = UserControlBase.dataManage.dicEnum["SerialBus.ModPol"].list;
                foreach (var combo in comboBox1.Items)
                {
                    var item = combo as ComboboxItem;
                    if (item.Text == comboBox1.SelectedItem.ToString())
                    {
                        int.TryParse(item.Value.ToString(), out serialValueData_.polR);
                    }
                }

            }
            else if(radioButton2.Checked)
            {
                serialValueData_.rsMode = (int)SerialMode.RS232;
                //极化电阻
                var list = UserControlBase.dataManage.dicEnum["SerialBus.ModPol"].list;

                foreach(var combo in comboBox1.Items)
                {
                    var item = combo as ComboboxItem;
                    if(item.Text == comboBox1.SelectedItem.ToString())
                    {
                        int.TryParse(item.Value.ToString(), out serialValueData_.polR);
                    }
                }
            }


        }

        public string com_;
        private ArrayList baudList = new ArrayList();
        private ArrayList parityList = new ArrayList();
        private ArrayList databitList = new ArrayList();
        private string type = "";
        private void Init()
        {
            var list = UserControlBase.dataManage.modules.list;
            foreach(var elem in list)
            {
                if(elem.moduleID == "SERIAL_LINE")
                {
                    //遍历module的类型type
                    foreach(var para in elem.connectModules.list)
                    {
                        type = para.type;
                        string[] strArr = type.Split(new Char[] { ':' });
                        if(strArr.Length == 2)
                        {
                            //串口type localTypes
                            string localType = strArr.ElementAt(0);
                            string serialBusType = strArr.ElementAt(1);
                            if(UserControlBase.dataManage.dicStruct.ContainsKey(serialBusType))
                            {
                                var serialStructType = UserControlBase.dataManage.dicStruct[serialBusType];
                                //SERIALData serialValueData = new SERIALData();

                                foreach (var serialData in serialStructType.list)
                                {
                                    if(serialData.name == "Baudrate")
                                    {
                                        //StructType里波特率默认值
                                        if (!configured_)
                                        {
                                            //从控制器模板读取，设置默认值
                                            int.TryParse(serialData.defaultValue, out serialValueData_.baud);
                                        }

                                        string[] strArrBaud = serialData.type.Split(new Char[] { ':' });
                                        if (strArrBaud.Length == 2)
                                        {
                                            //找到baud的类型combobox数据
                                            string baudType = strArrBaud.ElementAt(1);
                                            if(UserControlBase.dataManage.dicEnum.ContainsKey(baudType))
                                            {                             
                                                //初始化波特率
                                                var baudList = UserControlBase.dataManage.dicEnum[baudType].list;
                                                foreach (var elemBaud in baudList)
                                                {
                                                    ComboboxItem item = new ComboboxItem();
                                                    item.Value = elemBaud.value;
                                                    item.Text = elemBaud.value;

                                                    comboBox_Baud.Items.Add(item);
                                                }
                                            }
                                        }
                                    }
                                    else if(serialData.name == "Parity")
                                    {
                                        //StructType里波特率默认值
                                        //serialValueData.Parity = serialData.defaultValue;
                                        if (!configured_)
                                        {
                                            int.TryParse(serialData.defaultValue, out serialValueData_.Parity);
                                        }
                                        //在enumType里找到对应的描述
                                        string[] strArrParity = serialData.type.Split(new Char[] { ':' });

                                        if (strArrParity.Length == 2)
                                        {
                                            string strParity = strArrParity.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strParity))
                                            {
                                                //初始化波特率
                                                var parityList = UserControlBase.dataManage.dicEnum[strParity].list;
                                                foreach (var parity in parityList)
                                                {
                                                    ComboboxItem item = new ComboboxItem();
                                                    item.Value = parity.value; //值
                                                    item.Text = parity.name;  //描述
                                                    comboBox_Parity.Items.Add(item);
                                                }
                                            }
                                        }

                                    }
                                    else if(serialData.name == "Medium")
                                    {
                                        //StructType里串口默认值串口0-rs232    1-rs485
                                        if (!configured_)
                                        {
                                            int.TryParse(serialData.defaultValue, out serialValueData_.rsMode);
                                        }
                                        else
                                        {
                                            //已配置，不要设置默认值
                                            continue;
                                        }
                                        //在enumType里找到对应的描述
                                        string[] strArrMedium = serialData.type.Split(new Char[] { ':' });
                                        if (strArrMedium.Length == 2)
                                        {
                                            string strMedium = strArrMedium.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strMedium))
                                            {
                                                var mediumList = UserControlBase.dataManage.dicEnum[strMedium].list;
                                                mediumList_ = mediumList;
                                                foreach (var medium in mediumList)
                                                {
                                                    if(medium.value == /*1.ToString()*/ serialData.defaultValue)
                                                    {
                                                        radioButton1.Checked = true;
                                                        radioButton2.Checked = false;
                                                        
                                                    }
                                                    else if(medium.value == /*0.ToString()*/ serialData.defaultValue)
                                                    {
                                                        radioButton1.Checked = false;
                                                        radioButton2.Checked = true;
                                                        //if (UserControlBase.dataManage.dicEnum.ContainsKey("Polarization"))
                                                        //{
                                                        //    var polList = UserControlBase.dataManage.dicEnum["Polarization"].list;
                                                        //    foreach (var pol in polList)
                                                        //    {
                                                        //        if (pol.value == medium.value)
                                                        //        {
                                                        //            textBox_Pol.Text = pol.name;
                                                        //        }
                                                        //    }
                                                        //}
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else if(serialData.name == "Polarization")
                                    {
                                        //serialValueData.polR = serialData.defaultValue;
                                        if (!configured_)
                                        {
                                            int.TryParse(serialData.defaultValue, out serialValueData_.polR);
                                        }
                                        else
                                        {
                                            //已配置，不要设置默认值
                                            //continue;
                                        }

                                        //在enumType里找到对应的描述
                                        string[] strArrPolarization = serialData.type.Split(new Char[] { ':' });
                                        if (strArrPolarization.Length == 2)
                                        {
                                            string strPolarization = strArrPolarization.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strPolarization))
                                            {
                                                var polList = UserControlBase.dataManage.dicEnum[strPolarization].list;
                                                polList_ = polList;

                                                foreach (var pol in polList)
                                                {
                                                    ComboboxItem item = new ComboboxItem();
                                                    item.Value = pol.value;
                                                    item.Text = pol.name;
                                                    comboBox1.Items.Add(item);
                                                }

                                                foreach (var item in comboBox1.Items)
                                                {
                                                    ComboboxItem cur = (item as ComboboxItem);
                                                    if ((item as ComboboxItem).Value.ToString() == serialValueData_.polR.ToString())
                                                    {
                                                        comboBox1.SelectedItem = cur;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    else if(serialData.name == "Data bits")
                                    {
                                        //数据位
                                        //serialValueData.dataBit = serialData.defaultValue;
                                        if (!configured_)
                                        {
                                            int.TryParse(serialData.defaultValue, out serialValueData_.dataBit);
                                        }
                                        //在enumType里找到对应的描述
                                        string[] strArrDataBit = serialData.type.Split(new Char[] { ':' });
                                        if (strArrDataBit.Length == 2)
                                        {
                                            string strDataBit = strArrDataBit.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strDataBit))
                                            {
                                                var databitList = UserControlBase.dataManage.dicEnum[strDataBit].list;
                                                foreach (var dataBit in databitList)
                                                {
                                                    ComboboxItem item = new ComboboxItem();
                                                    item.Value = dataBit.value; //值
                                                    item.Text = dataBit.name;  //描述
                                                    comboBox_Databit.Items.Add(item);
                                                }
                                            }
                                        }
                                    }
                                    else if(serialData.name == "Stop bits")
                                    {
                                        //数据位
                                        //serialValueData.stopBit = serialData.defaultValue;
                                        if (!configured_)
                                        {
                                            int.TryParse(serialData.defaultValue, out serialValueData_.stopBit);
                                        }

                                        //在enumType里找到对应的描述
                                        string[] strArrStopBit = serialData.type.Split(new Char[] { ':' });
                                        if (strArrStopBit.Length == 2)
                                        {
                                            string strStopBit = strArrStopBit.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strStopBit))
                                            {
                                                var stopBitList = UserControlBase.dataManage.dicEnum[strStopBit].list;
                                                foreach (var dataBit in stopBitList)
                                                {
                                                    ComboboxItem item = new ComboboxItem();
                                                    item.Value = dataBit.value;
                                                    item.Text = dataBit.name;
                                                    comboBox_StopBit.Items.Add(item);
                                                }
                                            }
                                        }
                                    }
                                }

                                
                                
                                var itemsBaud = comboBox_Baud.Items;
                                foreach (var item in itemsBaud)
                                {
                                    ComboboxItem combo = (ComboboxItem)item;
                                    if (combo.Value.ToString() == serialValueData_.baud.ToString())
                                    {
                                        comboBox_Baud.SelectedItem = combo;
                                    }
                                }

                                var itemsParity = comboBox_Parity.Items;
                                foreach (var item in itemsParity)
                                {
                                    ComboboxItem combo = (ComboboxItem)item;
                                    if(combo.Value.ToString() ==serialValueData_.Parity.ToString())
                                    {
                                        comboBox_Parity.SelectedItem = combo;
                                    }
                                }

                                ComboBox.ObjectCollection collection = comboBox_Databit.Items;
                                foreach(var dataBit in collection)
                                {
                                    ComboboxItem combo = (ComboboxItem)dataBit;
                                    if(combo.Value.ToString() == /*combo.Value*/ serialValueData_.dataBit.ToString())
                                    {
                                        comboBox_Databit.SelectedItem = combo;
                                    }
                                }

                                ComboBox.ObjectCollection collectionStopBit = comboBox_StopBit.Items;
                                foreach (var stopBit in collectionStopBit)
                                {
                                    ComboboxItem combo = (ComboboxItem)stopBit;
                                    if (combo.Value.ToString() == /*combo.Value*/ serialValueData_.stopBit.ToString())
                                    {
                                        comboBox_StopBit.SelectedItem = combo;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }

            //baudList.Clear();
            //baudList.Add(1200);
            //baudList.Add(2400);
            //baudList.Add(4800);
            //baudList.Add(9600);
            //baudList.Add(19200);
            //baudList.Add(38400);
            //baudList.Add(57600);
            //baudList.Add(115200);

            //parityList.Clear();
            //parityList.Add("无");
            //parityList.Add("偶数");
            //parityList.Add("奇数");

            textBox_Com.Text = com_;

            //foreach(var baud in baudList)
            //{
            //    comboBox_Baud.Items.Add(baud);
            //}

            //comboBox_Baud.Text = 19200.ToString();



            //foreach (var parity in parityList)
            //{
            //    comboBox_Parity.Items.Add(parity);
            //}
            //comboBox_Parity.SelectedIndex = 1;

            ////数据位
            //comboBox_Databit.Items.Add(7);
            //comboBox_Databit.Items.Add(8);
            //comboBox_Databit.SelectedIndex = 1;


        }

        public void refreshData()
        {
            //serialDic
            if (UserControlBase.dataManage.serialDic.ContainsKey(com_))
            {
                var serial = UserControlBase.dataManage.serialDic[com_];
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //rs232
            if (radioButton1.Checked)
            {
                //serialValueData_.rsMode = (int)SerialMode.RS485;
                //极化电阻
                var list = UserControlBase.dataManage.dicEnum["SerialBus.ModPol"].list;
                foreach (var value in list)
                {
                    int temp = (int)SerialMode.RS485;
                    if (value.value == temp.ToString())
                    {
                        //serialValueData_.polR = serialValueData_.rsMode;
                        comboBox1.Text = value.name;
                        comboBox1.Enabled = true;
                    }
                }

            }
            else if (radioButton2.Checked)
            {
                //serialValueData_.rsMode = (int)SerialMode.RS232;
                //极化电阻
                var list = UserControlBase.dataManage.dicEnum["SerialBus.ModPol"].list;
                foreach (var value in list)
                {
                    int temp = (int)SerialMode.RS232;

                    if (value.value.ToString() == temp.ToString())
                    {
                        //serialValueData_.polR = serialValueData_.rsMode;
                        comboBox1.Text = value.name;
                        comboBox1.Enabled = false;
                    }
                }
            }

            if (initDone)
            {
                setButtonEnable(true);
                setModifgFlag(true);
            }
        }

        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                //serialValueData_.rsMode = (int)SerialMode.RS485;
                //极化电阻
                var list = UserControlBase.dataManage.dicEnum["SerialBus.ModPol"].list;
                foreach (var value in list)
                {
                    int temp = (int)SerialMode.RS485;
                    if (value.value == temp.ToString())
                    {
                        //serialValueData_.polR = serialValueData_.rsMode;
                        comboBox1.Text = value.name;
                        comboBox1.Enabled = true;
                    }
                }

            }
            else if (radioButton2.Checked)
            {
                //serialValueData_.rsMode = (int)SerialMode.RS232;
                //极化电阻
                var list = UserControlBase.dataManage.dicEnum["SerialBus.ModPol"].list;
                foreach (var value in list)
                {
                    int temp = (int)SerialMode.RS232;

                    if (value.value.ToString() == temp.ToString())
                    {
                        //serialValueData_.polR = serialValueData_.rsMode;
                        comboBox1.Text = value.name;
                        comboBox1.Enabled = false;
                    }
                }
            }

            if (initDone)
            {
                setButtonEnable(true);
                setModifgFlag(true);
            }
        }

        private void comboBox_Baud_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_Parity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_Databit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_StopBit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            getDataFromUI();
            setButtonEnable(false);
            setModifgFlag(false);
            setTreeNodeStatusDelegate("SERIAL_LINE", com_);
        }

        private void comboBox_Baud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(serialValueData_.baud.ToString() != comboBox_Baud.SelectedItem.ToString())
            {
                if (initDone)
                {
                    setButtonEnable(true);
                    setModifgFlag(true);
                }
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            setDataToUI();
            setButtonEnable(false);
            setModifgFlag(false);
        }

        private void comboBox_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ttt = comboBox_Parity.SelectedItem as ComboboxItem;
            if(ttt != null)
            {
                if (serialValueData_.Parity.ToString() != ttt.Value.ToString())
                {
                    if(initDone)
                    {
                        setButtonEnable(true);
                        setModifgFlag(true);
                    }

                }
            }


        }

        private void comboBox_StopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ttt = comboBox_StopBit.SelectedItem as ComboboxItem;
            if (ttt != null)
            {
                if (serialValueData_.stopBit.ToString() != ttt.Value.ToString())
                {
                    if (initDone)
                    {
                        setButtonEnable(true);
                        setModifgFlag(true);
                    }
                }
            }
        }

        private void comboBox_Databit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ttt = comboBox_Databit.SelectedItem as ComboboxItem;
            if (ttt != null)
            {
                //if(serialValueData_.dataBit.ToString() != ttt.Value.ToString())
                {
                    if (initDone)
                    {
                        if(ttt.ToString() == "7")
                        {
                            comboBox_Parity.Enabled = false;
                            comboBox_Parity.SelectedIndex = 0;
                        }
                        else
                        {
                            comboBox_Parity.Enabled = true;
                        }

                        setButtonEnable(true);
                        setModifgFlag(true);
                    }
                    else
                    {
                        if (ttt.ToString() == "7")
                        {
                            comboBox_Parity.Enabled = false;
                        }
                    }
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ttt = comboBox1.SelectedItem as ComboboxItem;
            if(ttt != null)
            {
                if(serialValueData_.polR.ToString() != ttt.Value.ToString())
                {
                    if(initDone)
                    {
                        setButtonEnable(true);
                        setModifgFlag(true);
                    }
                }
            }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
