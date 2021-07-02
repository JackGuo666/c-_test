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
using System.IO;
using LocalPLC.Interface;

namespace LocalPLC.Base
{
    public partial class UserControlBase : UserControl, ICheckVarName, IGetModifyFlag
    {
        public List<UserControl> PlcTypeArr = new List<UserControl>();
        LocalPLC24P curPlcType = null;
        public IWeapon curWeaponType = null;
        //动态创建串口网口控件
        public Dictionary<string, UserControlCom> comDic = new Dictionary<string, UserControlCom>();
        public Dictionary<string, UserControlEth> ethDic = new Dictionary<string, UserControlEth>();

        public TreeView treeView_ = null;

        public Control parent_ = null;
        //DataManageBase数据管理
        public static DataManageBase dataManage = new DataManageBase();
        public string localPLCType_ = "";
        public UserControlBase() 
        {
            InitializeComponent();

        }

        #region
        //接口
        bool modifyFlag = false;
        public bool getModifyFlag()
        {
            foreach(var control in splitContainer2.Panel2.Controls)
            {
                IGetModifyFlag getModify = control as IGetModifyFlag;
                if(getModify != null)
                {
                    getModify.getModifyFlag();
                }
            }

            //curWeaponType.getModifyFlag();

            return false;
        }
        #endregion

        public void getDataManager(ref DataManageBase retDataManageBase)
        {
            retDataManageBase = dataManage;
        }

       public DataManageBase getReDataManager()
        {
            return dataManage;
        }

        public bool modifyDINameByKey(string key, string modifyName)
        {
            var diList = dataManage.diList;
            foreach (var di in diList)
            {

                if(di.channelName == key)
                {
                    if(di.varName != modifyName)
                    {
                        di.varName = modifyName;
                        refreshDIUserBaseUI();
                        return true;
                    }
                }
            }

            return false;
        }

        public bool modifyDONameByKey(string key, string modifyName)
        {
            var doList = dataManage.doList;
            foreach (var dout in doList)
            {

                if (dout.channelName == key)
                {
                    if(dout.varName == modifyName)
                    {
                        //相等不用修改
                        return false;
                    }

                    dout.varName = modifyName;
                    refreshDOUserBaseUI();
                    return true;
                }
            }

            return false;
        }

        public bool checkVarName(String varName)
        {
            return true;
        }

        

        public void refreshUserBaseUI()
        {
            if(curWeaponType != null)
            {
                curWeaponType.refreshData();
            }

        }

        public void refreshDIUserBaseUI()
        {
            if (curWeaponType != null)
            {
                curWeaponType.refreshDIData();
            }

        }

        public void refreshDOUserBaseUI()
        {
            if (curWeaponType != null)
            {
                curWeaponType.refreshDOData();
            }

        }

        //重新加载工程，清空界面
        public void clearUI()
        {
            PlcTypeArr.Clear();
            splitContainer2.Panel1.Controls.Clear();
            splitContainer2.Panel2.Controls.Clear();

            treeView2.Nodes.Clear();
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
                uint.TryParse(filterName, out diData.filterTime);
                string channelName = e.GetAttribute("channelname");
                diData.channelName = channelName;
                string address = e.GetAttribute("address");
                diData.address = address;
                string note = e.GetAttribute("note");
                diData.note = note;
                string hscUsed = e.GetAttribute("hscused");
                diData.hscUsed = hscUsed;
                string motionUsed = e.GetAttribute("motionused");
                diData.motionUsed = motionUsed;


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
                doData.hspUsed = e.GetAttribute("hspused");

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

                //串口模式
                string rsMode = e.GetAttribute("rsmode");
                int.TryParse(rsMode, out serialData.rsMode);

                //极化电阻
                string polR = e.GetAttribute("polr");
                int.TryParse(polR, out serialData.polR);

                //终端电阻
                serialData.terminalResis = e.GetAttribute("terminalresis");
                //数据位是否enable
                serialData.databitEnable = e.GetAttribute("databitenable");

                dataManage.serialDic.Add(serialData.name, serialData);
            }
        }


        public void loadXmlEthernet(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                ETHERNETData ethernetData = new ETHERNETData();
                string name = e.Name;
                ethernetData.name =  e.GetAttribute("name");
                string strIpMode =  e.GetAttribute("ipmode");
                int.TryParse(strIpMode, out ethernetData.ipMode);
                ethernetData.ipAddress = e.GetAttribute("ipaddress");
                ethernetData.maskAddress = e.GetAttribute("maskaddress");
                ethernetData.gatewayAddress = e.GetAttribute("gatewayaddress");
                string strCheckSNTP = e.GetAttribute("checksntp");
                int.TryParse(strCheckSNTP, out ethernetData.checkSNTP);
                ethernetData.sntpServerIp = e.GetAttribute("sntpserverip");
                //读取时间戳
                string strTimestamp = e.GetAttribute("timestamp");
                Int64.TryParse(strTimestamp, out ethernetData.timestamp);

                dataManage.ethernetDic.Add(ethernetData.name, ethernetData);
            }
        }


        public void loadXmlHsp(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                HSPData hspData = new HSPData();
                string name = e.Name;
                hspData.name = e.GetAttribute("name");
                bool.TryParse(e.GetAttribute("used"), out hspData.used);
                hspData.address = e.GetAttribute("address");

                int.TryParse(e.GetAttribute("type"), out hspData.type);
                int.TryParse(e.GetAttribute("timebase"), out hspData.timeBase);
                int.TryParse(e.GetAttribute("preset"), out hspData.preset);
                bool.TryParse(e.GetAttribute("doubleword"), out hspData.doubleWord);
                int.TryParse(e.GetAttribute("signalfrequency"), out hspData.signalFrequency);
                int.TryParse(e.GetAttribute("outputmode"), out hspData.outputMode);
                hspData.pulsePort = e.GetAttribute("pulseport");
                hspData.directionPort = e.GetAttribute("directionport");
                hspData.note = e.GetAttribute("note");
                //轴对象使用
                bool.TryParse(e.GetAttribute("usedaxis"), out hspData.usedAxis);

                dataManage.hspList.Add(hspData);
            }
        }

        public void loadXmlHsc(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                HSCData hscData = new HSCData();

                hscData.name = e.GetAttribute("name");
                bool.TryParse(e.GetAttribute("used"), out hscData.used);
                hscData.address = e.GetAttribute("address");
                int.TryParse(e.GetAttribute("type"), out hscData.type);
                //输入模式
                int.TryParse(e.GetAttribute("inputmode"), out hscData.inputMode);
                //opr mode
                hscData.opr_mode = e.GetAttribute("oprmode");

                bool.TryParse(e.GetAttribute("doubleword"), out hscData.doubleWord);
                int.TryParse(e.GetAttribute("preset"), out hscData.preset);
                int.TryParse(e.GetAttribute("thresholds0"), out hscData.thresholdS0);
                int.TryParse(e.GetAttribute("thresholds1"), out hscData.thresholdS1);
                hscData.eventName0 = e.GetAttribute("eventname0");
                hscData.eventName1 = e.GetAttribute("eventname1");
                hscData.eventName0 = e.GetAttribute("eventname0");
                hscData.eventID0 = e.GetAttribute("eventid0");
                hscData.eventID1 = e.GetAttribute("eventid1");
                int.TryParse(e.GetAttribute("trigger0"), out hscData.trigger0);
                int.TryParse(e.GetAttribute("trigger1"), out hscData.trigger1);
                //脉冲
                bool.TryParse(e.GetAttribute("pulseinputchecked"), out hscData.pulseChecked);
                hscData.pulsePort = e.GetAttribute("pulseinputport");
                //方向
                bool.TryParse(e.GetAttribute("dirinputcheck"), out hscData.dirChecked);
                hscData.dirPort = e.GetAttribute("dirinputport");
                //预留
                bool.TryParse(e.GetAttribute("presetinputchecked"), out hscData.presetChecked);
                hscData.presetPort = e.GetAttribute("presetinputport");
                //捕获
                bool.TryParse(e.GetAttribute("caputreinputchecked"), out hscData.captureChecked);
                hscData.capturePort = e.GetAttribute("caputreinput");

                //频率计
                bool.TryParse(e.GetAttribute("frequencydoubleword"), out hscData.frequencyDoubleWord);
                //时间窗口
                int.TryParse(e.GetAttribute("timewindow"), out hscData.timeWindow);
                bool.TryParse(e.GetAttribute("frequencypulseinputchecked"), out hscData.pulseFrequencyChecked);
                hscData.pulseFrequencyInputPort = e.GetAttribute("frequencypulseinputport");

                //note
                hscData.note = e.GetAttribute("note");

                dataManage.hscList.Add(hscData);
            }

        }

        public void saveXml(ref XmlElement elem, ref XmlDocument doc)
        {
            XmlElement elemDI = doc.CreateElement("DI");
            elemDI.SetAttribute("name", "DI");
            elem.AppendChild(elemDI);


            if(curWeaponType == null)
            {
                MessageBox.Show("配置文件格式错误!");
                return;
            }

            //不从界面获取值了
            ////DI、DO、HOUT、HIN数据datatable到data manage
            //curWeaponType.getDataFromUI();

            foreach (var di in dataManage.diList)
            {
                XmlElement elem_di = doc.CreateElement("elem");
                elem_di.SetAttribute("used", di.used.ToString());
                elem_di.SetAttribute("varname", di.varName);
                elem_di.SetAttribute("fitertime", di.filterTime.ToString());
                elem_di.SetAttribute("channelname", di.channelName.ToString());
                elem_di.SetAttribute("address", di.address);
                elem_di.SetAttribute("note", di.note);
                elem_di.SetAttribute("hscused", di.hscUsed);
                elem_di.SetAttribute("motionused", di.motionUsed);

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
                elem_dout.SetAttribute("hspused", dout.hspUsed);

                elemDO.AppendChild(elem_dout);
            }

            XmlElement elemSerial = doc.CreateElement("Serial");
            elemSerial.SetAttribute("name", "Serial");
            elem.AppendChild(elemSerial);
            foreach (var comUI in comDic)
            {
                //comUI.Value.getDataFromUI();
                if(dataManage.serialDic.ContainsKey(comUI.Key))
                {
                    dataManage.serialDic[comUI.Key] = comUI.Value.serialValueData_;
                    XmlElement elem_serialChid = doc.CreateElement("elem");
                    elem_serialChid.SetAttribute("name", dataManage.serialDic[comUI.Key].name);
                    elem_serialChid.SetAttribute("baud", dataManage.serialDic[comUI.Key].baud.ToString());
                    elem_serialChid.SetAttribute("parity", dataManage.serialDic[comUI.Key].Parity.ToString());
                    elem_serialChid.SetAttribute("databit", dataManage.serialDic[comUI.Key].dataBit.ToString());
                    elem_serialChid.SetAttribute("stopbit", dataManage.serialDic[comUI.Key].stopBit.ToString());
                    elem_serialChid.SetAttribute("rsmode", dataManage.serialDic[comUI.Key].rsMode.ToString());
                    elem_serialChid.SetAttribute("polr", dataManage.serialDic[comUI.Key].polR.ToString());
                    elem_serialChid.SetAttribute("terminalresis", dataManage.serialDic[comUI.Key].terminalResis.ToString());
                    //数据位是否启用
                    elem_serialChid.SetAttribute("databitenable", dataManage.serialDic[comUI.Key].databitEnable);

                    elemSerial.AppendChild(elem_serialChid);
                }
            }



            XmlElement elemEthnet = doc.CreateElement("Ethnet");
            elemEthnet.SetAttribute("name", "Ethnet");
            elem.AppendChild(elemEthnet);
            foreach (var etherUI in ethDic)
            {
                //bool ret = etherUI.Value.getDataFromUI();
                //if(!ret)
                //{
                //    break;
                //}
                if (dataManage.ethernetDic.ContainsKey(etherUI.Key))
                {
                    dataManage.ethernetDic[etherUI.Key] = etherUI.Value.ethernetValueData_;
                    XmlElement ethernetChild = doc.CreateElement("elem");
                    ethernetChild.SetAttribute("name", dataManage.ethernetDic[etherUI.Key].name);
                    ethernetChild.SetAttribute("ipmode", dataManage.ethernetDic[etherUI.Key].ipMode.ToString());
                    ethernetChild.SetAttribute("ipaddress", dataManage.ethernetDic[etherUI.Key].ipAddress.ToString());
                    ethernetChild.SetAttribute("maskaddress", dataManage.ethernetDic[etherUI.Key].maskAddress.ToString());
                    ethernetChild.SetAttribute("gatewayaddress", dataManage.ethernetDic[etherUI.Key].gatewayAddress.ToString());
                    ethernetChild.SetAttribute("checksntp", dataManage.ethernetDic[etherUI.Key].checkSNTP.ToString());
                    ethernetChild.SetAttribute("sntpserverip", dataManage.ethernetDic[etherUI.Key].sntpServerIp.ToString());
                    ethernetChild.SetAttribute("timestamp", dataManage.ethernetDic[etherUI.Key].timestamp.ToString());

                    elemEthnet.AppendChild(ethernetChild);
                }
            }


            XmlElement elemHSC = doc.CreateElement("HSC");
            elemHSC.SetAttribute("name", "HSC");
            elem.AppendChild(elemHSC);
            foreach(var hsc in dataManage.hscList)
            {

                XmlElement hscChild = doc.CreateElement("elem");
                hscChild.SetAttribute("used", hsc.used.ToString());
                hscChild.SetAttribute("name", hsc.name);
                hscChild.SetAttribute("address", hsc.address);
                hscChild.SetAttribute("type", hsc.type.ToString());
                //输入模式
                hscChild.SetAttribute("inputmode", hsc.inputMode.ToString());

                //ope mode
                hscChild.SetAttribute("oprmode", hsc.opr_mode.ToString());

                //双字
                hscChild.SetAttribute("doubleword", hsc.doubleWord.ToString());
                hscChild.SetAttribute("preset", hsc.preset.ToString());
                //阈值
                hscChild.SetAttribute("thresholds0", hsc.thresholdS0.ToString());
                hscChild.SetAttribute("thresholds1", hsc.thresholdS1.ToString());
                //事件名
                hscChild.SetAttribute("eventname0", hsc.eventName0);
                hscChild.SetAttribute("eventname1", hsc.eventName1);
                //事件ID
                hscChild.SetAttribute("eventid0", hsc.eventID0);
                hscChild.SetAttribute("eventid1", hsc.eventID1);
                //触发器
                hscChild.SetAttribute("trigger0", hsc.trigger0.ToString());
                hscChild.SetAttribute("trigger1", hsc.trigger1.ToString());

                //脉冲输入
                hscChild.SetAttribute("pulseinputchecked", hsc.pulseChecked.ToString());
                hscChild.SetAttribute("pulseinputport", hsc.pulsePort.ToString());
                //方向输入
                hscChild.SetAttribute("dirinputcheck", hsc.dirChecked.ToString());
                hscChild.SetAttribute("dirinputport", hsc.dirPort);
                //预设输入
                hscChild.SetAttribute("presetinputchecked", hsc.presetChecked.ToString());
                hscChild.SetAttribute("presetinputport", hsc.presetPort);
                //捕捉收入
                hscChild.SetAttribute("caputreinputchecked", hsc.captureChecked.ToString());
                hscChild.SetAttribute("caputreinput", hsc.capturePort);

                //频率计
                hscChild.SetAttribute("frequencydoubleword", hsc.frequencyDoubleWord.ToString());
                hscChild.SetAttribute("timewindow", hsc.timeWindow.ToString());
                hscChild.SetAttribute("frequencypulseinputchecked", hsc.pulseFrequencyChecked.ToString());
                hscChild.SetAttribute("frequencypulseinputport", hsc.pulseFrequencyInputPort);

                hscChild.SetAttribute("note", hsc.note);

                elemHSC.AppendChild(hscChild);
            }

            XmlElement elemHSP = doc.CreateElement("HSP");
            elemHSP.SetAttribute("name", "HSP");
            elem.AppendChild(elemHSP);
            foreach(var hsp in dataManage.hspList)
            {
                XmlElement hspChild = doc.CreateElement("elem");

                hspChild.SetAttribute("used", hsp.used.ToString());
                hspChild.SetAttribute("name", hsp.name);
                hspChild.SetAttribute("address", hsp.address);
                hspChild.SetAttribute("type", hsp.type.ToString());

                //PWM
                hspChild.SetAttribute("timebase", hsp.timeBase.ToString());
                hspChild.SetAttribute("preset", hsp.preset.ToString());
                //PLS
                hspChild.SetAttribute("doubleword", hsp.doubleWord.ToString());
                //frequency
                hspChild.SetAttribute("signalfrequency", hsp.signalFrequency.ToString());
                //PTO
                hspChild.SetAttribute("outputmode", hsp.outputMode.ToString());
                hspChild.SetAttribute("pulseport", hsp.pulsePort.ToString());
                hspChild.SetAttribute("directionport", hsp.directionPort.ToString());
                hspChild.SetAttribute("note", hsp.note);
                hspChild.SetAttribute("usedaxis", hsp.usedAxis.ToString());

                elemHSP.AppendChild(hspChild);
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

        public string loadControler()
        {
            string path = UserControl1.multiprogApp.Path;
            path += "\\LocalPLC\\controler.xml";

            XmlDocument xDoc = new XmlDocument();
            this.treeView1.Nodes.Clear();
            string returnType = "";
            if (File.Exists(path))
            {
                xDoc.Load(path);

                //根节点
                XmlNode node = xDoc.SelectSingleNode("Controler");
                XmlNodeList nodeList = node.ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement eChild = (XmlElement)xn;
                    string childname = eChild.Name;
                    string name = eChild.GetAttribute("name");
                    int defaultType;
                    string test = eChild.GetAttribute("default");
                    int.TryParse(test, out defaultType);
                    if(defaultType > 0)
                    {
                        returnType = name;
                    }

                    this.treeView1.Nodes.Add(name);
                }
            }


            return returnType;
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
            if (tnParent.Text.Trim(new char[] { '*'}) == strValue) return tnParent;

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
                    data.name = elem.baseName;
                    data.terminalResis = elem.terminalResis;
                    data.databitEnable = elem.databitEnable;

                    UserControlCom com = new UserControlCom(this, elem.baseName, data, false);
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

                 UserControlCom com = new UserControlCom(this, elem.Key, elem.Value, !dataManage.newControlerFlag);
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
                    ETHERNETData data = new ETHERNETData();
                    data.name = elem.baseName;
                    UserControlEth eth = new UserControlEth(this, elem.baseName, data, dataManage.newControlerFlag);
                    ethDic.Add(elem.baseName, eth);
                    UserControlBase.dataManage.ethernetDic.Add(elem.baseName, data);
                }
            }
        }


        void createEthernetUserControlConfigured()
        {
            ethDic.Clear();

            foreach (var elem in dataManage.ethernetDic)
            {
                UserControlEth eth = new UserControlEth(this, elem.Key, elem.Value, dataManage.newControlerFlag);
                 ethDic.Add(elem.Key, eth);
            }
        }

        public void createControlerConfigured(string PLCType)
        {
            localPLCType_ = PLCType;

            dataManage.newControlerFlag = false;

            LocalPLC.Base.xml.ClassParseBaseXml ttt = new ClassParseBaseXml(PLCType, dataManage);

            string tmp = string.Format("LocalPLC.Base.{0}", PLCType);
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
            user1.Name = PLCType;
            //user1.Dock = DockStyle.Fill;
            //user1.Size = new System.Drawing.Size(41, 12);
            user1.TabIndex = 0;


            //
            var topNode = treeView_.TopNode;
            var commNode = FindNode(topNode, "通信线路");

            delSubNodes(commNode);
            addSerialNode(commNode);
            createSerialUserControlConfigured();
            //createEthernetUserControl();
            createEthernetUserControlConfigured();

            topNode.Text = PLCType;
            //////

            
        }

        public void createControler(string defaultPLCType)
        {
            dataManage.newControlerFlag = true;

            localPLCType_ = defaultPLCType;
            LocalPLC.Base.xml.ClassParseBaseXml ttt = new ClassParseBaseXml(localPLCType_, dataManage);

            var topNode = treeView_.TopNode;
            var commNode = FindNode(topNode, "通信线路");
            delSubNodes(commNode);
            addSerialNode(commNode);
            createSerialUserControl();
            createEthernetUserControl();

            topNode.Text = localPLCType_;

            string tmp = string.Format("LocalPLC.Base.{0}", localPLCType_);
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


            if(type == null)
            {
                return;
            }

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
