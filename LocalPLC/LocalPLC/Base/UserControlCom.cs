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
        public UserControlCom(string com)
        {
            InitializeComponent();
            //串口名
            com_ = com;
            Init();


            //数据管理里的串口数组
            //UserControlBase.dataManage.serialDic

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
                                SERIALData serialValueData = new SERIALData();

                                foreach (var serialData in serialStructType.list)
                                {
                                    if(serialData.name == "Baudrate")
                                    {
                                        //StructType里波特率默认值
                                        serialValueData.baud = serialData.defaultValue;
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
                                        serialValueData.Parity = serialData.defaultValue;
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
                                        serialValueData.rsMode = serialData.defaultValue;
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

                                                    }
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

            textBox_Com.Text = "COM1";

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

            //数据位
            comboBox_Databit.Items.Add(7);
            comboBox_Databit.Items.Add(8);
            comboBox_Databit.SelectedIndex = 1;


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
