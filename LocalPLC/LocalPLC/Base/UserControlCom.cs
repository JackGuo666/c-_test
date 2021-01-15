using System;
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
using LocalPLC.Base;

namespace LocalPLC.Base
{
    public partial class UserControlCom : UserControl
    {
        SERIALData serialValueData = new SERIALData();
        public UserControlCom(string com)
        {
            InitializeComponent();
            //串口名
            com_ = com;
            Init();


            //数据管理里的串口数组
            UserControlBase.dataManage.serialDic.Clear();
            UserControlBase.dataManage.serialDic.Add(com_, serialValueData);

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
                                        int.TryParse(serialData.defaultValue, out serialValueData.baud);

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
                                        int.TryParse(serialData.defaultValue, out serialValueData.Parity);
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
                                        int.TryParse(serialData.defaultValue, out serialValueData.rsMode);
                                        //在enumType里找到对应的描述
                                        string[] strArrMedium = serialData.type.Split(new Char[] { ':' });
                                        if (strArrMedium.Length == 2)
                                        {
                                            string strMedium = strArrMedium.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strMedium))
                                            {
                                                var mediumList = UserControlBase.dataManage.dicEnum[strMedium].list;
                                                foreach(var medium in mediumList)
                                                {
                                                    if(medium.value == 1.ToString())
                                                    {
                                                        radioButton1.Checked = true;
                                                        radioButton2.Checked = false;
                                                        
                                                    }
                                                    else if(medium.value == 0.ToString())
                                                    {
                                                        radioButton1.Checked = false;
                                                        radioButton2.Checked = true;
                                                        if (UserControlBase.dataManage.dicEnum.ContainsKey("Polarization"))
                                                        {
                                                            var polList = UserControlBase.dataManage.dicEnum["Polarization"].list;
                                                            foreach (var pol in polList)
                                                            {
                                                                if (pol.value == medium.value)
                                                                {
                                                                    textBox1.Text = pol.name;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else if(serialData.name == "Polarization")
                                    {
                                        //serialValueData.polR = serialData.defaultValue;
                                        int.TryParse(serialData.defaultValue, out serialValueData.polR);

                                        //在enumType里找到对应的描述
                                        string[] strArrPolarization = serialData.type.Split(new Char[] { ':' });
                                        if (strArrPolarization.Length == 2)
                                        {
                                            string strPolarization = strArrPolarization.ElementAt(1);
                                            if (UserControlBase.dataManage.dicEnum.ContainsKey(strPolarization))
                                            {
                                                var polList = UserControlBase.dataManage.dicEnum[strPolarization].list;
                                                foreach (var pol in polList)
                                                {
                                                    if (pol.value == serialValueData.polR.ToString())
                                                    {
                                                        textBox1.Text = pol.name;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    else if(serialData.name == "Data bits")
                                    {
                                        //数据位
                                        //serialValueData.dataBit = serialData.defaultValue;
                                        int.TryParse(serialData.defaultValue, out serialValueData.dataBit);
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
                                        int.TryParse(serialData.defaultValue, out serialValueData.stopBit);

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
                                    if (combo.Value.ToString() == serialValueData.baud.ToString())
                                    {
                                        comboBox_Baud.SelectedItem = combo;
                                    }
                                }

                                var itemsParity = comboBox_Parity.Items;
                                foreach (var item in itemsParity)
                                {
                                    ComboboxItem combo = (ComboboxItem)item;
                                    if(combo.Value.ToString() ==serialValueData.Parity.ToString())
                                    {
                                        comboBox_Parity.SelectedItem = combo;
                                    }
                                }

                                ComboBox.ObjectCollection collection = comboBox_Databit.Items;
                                foreach(var dataBit in collection)
                                {
                                    ComboboxItem combo = (ComboboxItem)dataBit;
                                    if(combo.Value == combo.Value)
                                    {
                                        comboBox_Databit.SelectedItem = combo;
                                    }
                                }

                                ComboBox.ObjectCollection collectionStopBit = comboBox_StopBit.Items;
                                foreach (var stopBit in collectionStopBit)
                                {
                                    ComboboxItem combo = (ComboboxItem)stopBit;
                                    if (combo.Value == combo.Value)
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
