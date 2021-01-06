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
using LocalPLC.Base;

namespace LocalPLC.Base
{
    public partial class UserControlCom : UserControl
    {
        public UserControlCom(string com)
        {
            InitializeComponent();

            Init();

            com_ = com;
        }

        public string com_;
        private ArrayList baudList = new ArrayList();
        private ArrayList parityList = new ArrayList();
        private ArrayList databitList = new ArrayList();
        private void Init()
        {
            var list = UserControlBase.dataManage.modules.list;
            foreach(var elem in list)
            {
                if(elem.moduleID == "SERIAL_LINE")
                {
                    //elem.connectModules.list
                }
            }

            baudList.Clear();
            baudList.Add(1200);
            baudList.Add(2400);
            baudList.Add(4800);
            baudList.Add(9600);
            baudList.Add(19200);
            baudList.Add(38400);
            baudList.Add(57600);
            baudList.Add(115200);

            parityList.Clear();
            parityList.Add("无");
            parityList.Add("偶数");
            parityList.Add("奇数");

            textBox_Com.Text = "COM1";

            foreach(var baud in baudList)
            {
                comboBox_Baud.Items.Add(baud);
            }

            comboBox_Baud.Text = 19200.ToString();

            foreach (var parity in parityList)
            {
                comboBox_Parity.Items.Add(parity);
            }
            comboBox_Parity.SelectedIndex = 1;

            //数据位
            comboBox_Databit.Items.Add(7);
            comboBox_Databit.Items.Add(8);
            comboBox_Databit.SelectedIndex = 1;


        }
    }
}
