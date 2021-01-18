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
using System.Xml;

namespace LocalPLC.Base
{
    public partial class UserControlBase : UserControl
    {
        public List<UserControl> PlcTypeArr = new List<UserControl>();
        LocalPLC24P curPlcType = null;
        public IWeapon curWeaponType = null;
        //动态创建串口网口控件
        public Dictionary<string, UserControlCom> comDic = new Dictionary<string, UserControlCom>();
        public Dictionary<string, UserControlEth> ethDic = new Dictionary<string, UserControlEth>();

        TreeView treeView_ = null;

        public Control parent_ = null;
        //DataManageBase数据管理
        public static DataManageBase dataManage = new DataManageBase();
        public UserControlBase() 
        {
            InitializeComponent();
        }


        //重新加载工程，清空界面
        public void clearUI()
        {
            PlcTypeArr.Clear();
            splitContainer2.Panel1.Controls.Clear();
            splitContainer2.Panel2.Controls.Clear();
        }

        public void loadXmlDI(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                DIData diData = new DIData();
                string name = e.Name;
                string used = e.GetAttribute("used");
                bool.TryParse(used, out diData.used);
                string varName = e.GetAttribute("varname");
                diData.varName = varName;
                string filterName = e.GetAttribute("fitertime");
                int.TryParse(filterName, out diData.filterTime);
                string channelName = e.GetAttribute("channelname");
                diData.channelName = channelName;
                string address = e.GetAttribute("address");
                diData.address = address;
                string note = e.GetAttribute("note");
                diData.note = note;

                UserControlBase.dataManage.diList.Add(diData);

            }
        }

        public void loadXmlDO(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合

            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                DOData doData = new DOData();
                string name = e.Name;
                string used = e.GetAttribute("used");
                bool.TryParse(used, out doData.used);
                string varName = e.GetAttribute("varname");
                doData.varName = varName;
                string channelName = e.GetAttribute("channelname");
                doData.channelName = channelName;
                string address = e.GetAttribute("address");
                doData.address = address;
                string note = e.GetAttribute("note");
                doData.note = note;

                UserControlBase.dataManage.doList.Add(doData);

            }
        }


        public void loadXmlSerial(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                SERIALData serialData = new SERIALData();
                string name = e.Name;
                serialData.name = e.GetAttribute("name");
                string baud = e.GetAttribute("baud");
                int.TryParse(baud, out serialData.baud);

                string parity = e.GetAttribute("parity");
                int.TryParse(parity, out serialData.Parity);

                string databit = e.GetAttribute("databit");
                int.TryParse(databit, out serialData.dataBit);

                string stopbit = e.GetAttribute("stopbit");
                int.TryParse(stopbit, out serialData.stopBit);

                //极化电阻
                string polR = e.GetAttribute("polr");
                int.TryParse(polR, out serialData.polR);

                dataManage.serialDic.Add(serialData.name, serialData);
            }
        }

        public void saveXml(ref XmlElement elem, ref XmlDocument doc)
        {
            XmlElement elemDI = doc.CreateElement("DI");
            elemDI.SetAttribute("name", "DI");
            elem.AppendChild(elemDI);

            //DI数据datatable到data manage
            curWeaponType.getDataFromUI();
            foreach(var di in dataManage.diList)
            {
                XmlElement elem_di = doc.CreateElement("elem");
                elem_di.SetAttribute("used", di.used.ToString());
                elem_di.SetAttribute("varname", di.varName);
                elem_di.SetAttribute("fitertime", di.filterTime.ToString());
                elem_di.SetAttribute("channelname", di.channelName.ToString());
                elem_di.SetAttribute("address", di.address);
                elem_di.SetAttribute("note", di.note);

                elemDI.AppendChild(elem_di);
            }

            XmlElement elemDO = doc.CreateElement("DO");
            elemDO.SetAttribute("name", "DO");
            elem.AppendChild(elemDO);
            foreach (var dout in dataManage.doList)
            {
                XmlElement elem_dout = doc.CreateElement("elem");
                elem_dout.SetAttribute("used", dout.used.ToString());
                elem_dout.SetAttribute("varname", dout.varName);
                elem_dout.SetAttribute("channelname", dout.channelName.ToString());
                elem_dout.SetAttribute("address", dout.address);
                elem_dout.SetAttribute("note", dout.note);

                elemDO.AppendChild(elem_dout);
            }

            XmlElement elemSerial = doc.CreateElement("Serial");
            elemSerial.SetAttribute("name", "Serial");
            elem.AppendChild(elemSerial);
            foreach (var comUI in comDic)
            {
                comUI.Value.getDataFromUI();
                if(dataManage.serialDic.ContainsKey(comUI.Key))
                {
                    dataManage.serialDic[comUI.Key] = comUI.Value.serialValueData_;
                    XmlElement elem_serialChid = doc.CreateElement("elem");
                    elem_serialChid.SetAttribute("name", dataManage.serialDic[comUI.Key].name);
                    elem_serialChid.SetAttribute("baud", dataManage.serialDic[comUI.Key].baud.ToString());
                    elem_serialChid.SetAttribute("parity", dataManage.serialDic[comUI.Key].Parity.ToString());
                    elem_serialChid.SetAttribute("databit", dataManage.serialDic[comUI.Key].dataBit.ToString());
                    elem_serialChid.SetAttribute("stopbit", dataManage.serialDic[comUI.Key].stopBit.ToString());
                    elem_serialChid.SetAttribute("polr", dataManage.serialDic[comUI.Key].polR.ToString());

                    elemSerial.AppendChild(elem_serialChid);
                }
            }



            XmlElement elemEthnet = doc.CreateElement("Ethnet");
            elemEthnet.SetAttribute("name", "Ethnet");
            elem.AppendChild(elemEthnet);
            foreach (var etherUI in ethDic)
            {
                etherUI.Value.getDataFromUI();
                if (dataManage.ethernetDic.ContainsKey(etherUI.Key))
                {
                    dataManage.ethernetDic[etherUI.Key] = etherUI.Value.ethernetValueData;
                    XmlElement ethernetChild = doc.CreateElement("elem");
                    ethernetChild.SetAttribute("name", dataManage.ethernetDic[etherUI.Key].name);
                    ethernetChild.SetAttribute("ipmode", dataManage.ethernetDic[etherUI.Key].ipMode.ToString());
                    ethernetChild.SetAttribute("maskaddress", dataManage.ethernetDic[etherUI.Key].maskAddress.ToString());
                    ethernetChild.SetAttribute("gatewayaddress", dataManage.ethernetDic[etherUI.Key].gatewayAddress.ToString());
                    ethernetChild.SetAttribute("checksntp", dataManage.ethernetDic[etherUI.Key].checkSNTP.ToString());
                    ethernetChild.SetAttribute("sntpserverip", dataManage.ethernetDic[etherUI.Key].sntpServerIp.ToString());

                    elemEthnet.AppendChild(ethernetChild);
                }
            }
        }

        public void getTreeView(TreeView view)
        {
            treeView_ = view;
        }

        public void getParent(Control parent)
        {
            parent_ = parent;
        }

        private void UserControlBase_Load(object sender, EventArgs e)
        {
            //加载工程信息
        }


        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DataObject data = new DataObject();
            data.SetData("Test", e.Item);

            //开始拖放操作
            this.DoDragDrop(data, DragDropEffects.Copy);
        }

        private void splitContainer1_Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Test"))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }



        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strValue) return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNode(tn, strValue);
                if (tnRet != null)
                {
                    //treeView1.SelectedNode = tnRet;
                    //treeView1.SelectedNode.Expand();//展开找到的节点
                    break;
                }
            }
            return tnRet;
        }

        //删除 parent 下所有的子节点
        private void delSubNodes(TreeNode parent)
        {
            if (parent == null)
            {
                return;
            }
            //判断选定的节点是否存在下一级节点
            if (parent.Nodes.Count == 0)
            {
            }
            else
            {
                for (int i = parent.Nodes.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(" 删除： {0}", parent.Nodes[i].Text);
                    parent.Nodes.RemoveAt(i);
                }
            }
        }

        void addSerialNode(TreeNode tn)
        {
            var moduleList = UserControlBase.dataManage.deviceInfoElem.connector.moduleList;
            foreach(var elem in moduleList)
            {
                if(elem.moduleID == "SERIAL_LINE")
                {
                    TreeNode serialNode = new TreeNode();
                    serialNode.Text = elem.baseName;
                    serialNode.Tag = elem.moduleID;
                    serialNode.SelectedImageIndex = 12;
                    serialNode.ImageIndex = 12;
                    tn.Nodes.Add(serialNode);
                }
                else if(elem.moduleID == "ETHERNET")
                {
                    TreeNode ethernetNode = new TreeNode();
                    ethernetNode.Text = elem.baseName;
                    ethernetNode.Tag = elem.moduleID;
                    ethernetNode.SelectedImageIndex = 13;
                    ethernetNode.ImageIndex = 13;
                    tn.Nodes.Add(ethernetNode);
                }
            }
        }

        //动态创建串口界面
        void createSerialUserControl()
        {
            //清空之前加载的串口控件数组
            comDic.Clear();
            List<DeviceModuleElem> list = dataManage.deviceInfoElem.connector.moduleList;
            foreach(DeviceModuleElem elem in list)
            {
                if(elem.moduleID == "SERIAL_LINE")
                {
                    SERIALData data = new SERIALData();
                    UserControlCom com = new UserControlCom(elem.baseName, data, false);
                    comDic.Add(elem.baseName, com);
                    dataManage.serialDic.Add(elem.baseName, data);
                }
            }
        }

        void createSerialUserControlConfigured()
        {
            //清空之前加载的串口控件数组
            comDic.Clear();
            //var list = dataManage.serialDic.ToList()
            foreach (var elem in dataManage.serialDic)
            {

                 UserControlCom com = new UserControlCom(elem.Key, elem.Value, !dataManage.newControlerFlag);
                 comDic.Add(elem.Key, com);

            }
        }

        void createEthernetUserControl()
        {
            ethDic.Clear();
            List<DeviceModuleElem> list = dataManage.deviceInfoElem.connector.moduleList;
            foreach (DeviceModuleElem elem in list)
            {
                if (elem.moduleID == "ETHERNET")
                {
                    UserControlEth eth = new UserControlEth(elem.baseName);
                    ethDic.Add(elem.baseName, eth);
                }
            }
        }

        public void createControlerConfigured(string PLCType)
        {
            dataManage.newControlerFlag = false;

            LocalPLC.Base.xml.ClassParseBaseXml ttt = new ClassParseBaseXml(PLCType, dataManage);

            string tmp = string.Format("LocalPLC.Base.{0}", PLCType);
            Type type = Type.GetType(/*"LocalPLC.Base.PlcType"*/ tmp);
            //object obj = type.Assembly.CreateInstance(type);
            UserControl user1 = (UserControl)Activator.CreateInstance(type, splitContainer2, this, dataManage);



            //
            var topNode = treeView_.TopNode;
            var commNode = FindNode(topNode, "通信线路");

            delSubNodes(commNode);
            addSerialNode(commNode);
            createSerialUserControlConfigured();
            createEthernetUserControl();

            topNode.Text = PLCType;
            //////

            //PlcType user1 = new PlcType(splitContainer2, this, dataManage);
            curPlcType = (LocalPLC24P)user1;
            curWeaponType = user1 as IWeapon;
            user1.Parent = this;
            PlcTypeArr.Add(user1);
            splitContainer2.Panel1.Controls.Add(user1);
            user1.Location = new System.Drawing.Point(PlcTypeArr.Count * user1.Width, 0);
            user1.Name = PLCType;
            //user1.Dock = DockStyle.Fill;
            //user1.Size = new System.Drawing.Size(41, 12);
            user1.TabIndex = 0;
        }

        public void createControler(string defaultPLCType)
        {
            dataManage.newControlerFlag = true;

            string localPLCType = defaultPLCType;
            LocalPLC.Base.xml.ClassParseBaseXml ttt = new ClassParseBaseXml(localPLCType, dataManage);




            var topNode = treeView_.TopNode;
            var commNode = FindNode(topNode, "通信线路");
            delSubNodes(commNode);
            addSerialNode(commNode);
            createSerialUserControl();
            createEthernetUserControl();

            topNode.Text = localPLCType;

            string tmp = string.Format("LocalPLC.Base.{0}", localPLCType);
            Type type = Type.GetType(/*"LocalPLC.Base.PlcType"*/ tmp);
            //object obj = type.Assembly.CreateInstance(type);
            UserControl user1 = (UserControl)Activator.CreateInstance(type, splitContainer2, this, dataManage);


            //PlcType user1 = new PlcType(splitContainer2, this, dataManage);
            curPlcType = (LocalPLC24P)user1;
            curWeaponType = user1 as IWeapon;
            user1.Parent = this;
            PlcTypeArr.Add(user1);
            splitContainer2.Panel1.Controls.Add(user1);
            user1.Location = new System.Drawing.Point(PlcTypeArr.Count * user1.Width, 0);
            user1.Name = defaultPLCType;
            //user1.Dock = DockStyle.Fill;
            //user1.Size = new System.Drawing.Size(41, 12);
            user1.TabIndex = 0;
        }


        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            if(!LocalPLC.UserControl1.multiprogApp.IsProjectOpen())
            {
                MessageBox.Show("请先打开工程!");
                return;
            }

            dataManage.newControlerFlag = true;
            object item = e.Data.GetData("Test");

            foreach(Control control in splitContainer2.Panel1.Controls)
            {
                if(control.Tag.ToString() == 0.ToString())
                {
                    splitContainer2.Panel1.Controls.Remove(control);
                    PlcTypeArr.Clear();
                }
            }

            TreeNode node = (TreeNode)item;

            string localPLCType = node.Text.ToString();
            LocalPLC.Base.xml.ClassParseBaseXml ttt = new ClassParseBaseXml(localPLCType, dataManage);


            

            var topNode = treeView_.TopNode;
            var commNode = FindNode(topNode, "通信线路");
            
            delSubNodes(commNode);
            addSerialNode(commNode);
            createSerialUserControl();
            createEthernetUserControl();

            topNode.Text = localPLCType;

            string tmp = string.Format("LocalPLC.Base.{0}", localPLCType);
            Type type = Type.GetType(/*"LocalPLC.Base.PlcType"*/ tmp);
            //object obj = type.Assembly.CreateInstance(type);
            UserControl user1 = (UserControl)Activator.CreateInstance(type, splitContainer2, this, dataManage);


            //PlcType user1 = new PlcType(splitContainer2, this, dataManage);
            curPlcType = (LocalPLC24P)user1;
            curWeaponType = user1 as IWeapon;
            user1.Parent = this;
            PlcTypeArr.Add(user1);
            splitContainer2.Panel1.Controls.Add(user1);
            user1.Location = new System.Drawing.Point(PlcTypeArr.Count * user1.Width, 0);
            user1.Name = node.Name;
            //user1.Dock = DockStyle.Fill;
            //user1.Size = new System.Drawing.Size(41, 12);
            user1.TabIndex = 0;
        }

        //树节点选中DO，PLC显示DO信息
        public void setDOShow(string name)
        {
            if(curPlcType == null)
            {

            }
            else
            {
                curWeaponType.setDOInfo(name);
                //curPlcType.setDOInfo(name);
            }
        }

        //树节点选中DO，PLC显示DO信息
        public void setDIShow(string name)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curWeaponType.setDIInfo(name);
                //curPlcType.setDIInfo(name);
            }
        }

        
        public void setCOMShow(string com)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                //curPlcType.setCOMInfo(com);
                curWeaponType.setCOMInfo(com);
            }

        }

        public void setETHShow(string eth)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                //curPlcType.setETHInfo(eth);
                curWeaponType.setETHInfo(eth);
            }

        }

        public void setQuadShow(string eth)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setQuadInfo(eth);
            }
        }

        public void setBiDirPulseShow(string bi)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setBiDirPulseInfo(bi);
            }
        }

        public void setSinglePulseShow(string pulse)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setSinglePulseInfo(pulse);
            }
        }

        public void setPTOShow(string pulse)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setPTOInfo(pulse);
            }
        }

        public void setPWMShow(string pulse)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setPWMInfo(pulse);
            }
        }

        public void setExtendAIShow(string ai)
        {
            if (curPlcType == null)
            {
                
            }
            else
            {
                curPlcType.setExtendAIInfo(ai);
            }
        }

        public void setExtendAOShow(string ao)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setExtendAOInfo(ao);
            }
        }

        public void setHighInput(string hi)
        {
            if(curPlcType == null)
            {

            }
            else
            {
                curWeaponType.setHighInputInfo(hi);
                //curPlcType.setHighInputInfo(hi);
            }
        }

         public void setHighOutput(string hout)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curWeaponType.setHighOutputInfo(hout);
                //curPlcType.setHighOutputInfo(hout);
            }
        }
    }
}
