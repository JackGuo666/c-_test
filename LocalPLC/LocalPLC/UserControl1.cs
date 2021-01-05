using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using System.Runtime.InteropServices;
using ADELib;
using LocalPLC.ModbusMaster;
using LocalPLC.ModbusSlave;
using LocalPLC.ModbusServer;
using LocalPLC.ModbusClient;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LocalPLC.Base;

namespace LocalPLC
{
    [ComVisible(true)]
    [Guid("4D23925D-E5C1-40A8-9D69-AAD815FDCECE")]
    [ProgId("LocalPLC.CONTROLBAR.PROGID")]
    public partial class UserControl1 : UserControl,IAdeAddIn,IAdeProjectObserver,IAdeCompileExtension,IAdeVariableObserver2
    {
        public UserControl1()
        {
            InitializeComponent();
            i++;
        }

        public static ADELib.Application multiprogApp = null;
        public static string projectPath = "test";
        private static string projectName = null;
        static int i = 0; 

        public empty e1;
        public static ModbusClient.Clientindex mci = new Clientindex();
        public static ModbusClient.modbusclient mct = new modbusclient();
        public  static modbusmastermain modmaster = new modbusmastermain();
		public  static modbusslavemain modslave = new modbusslavemain();
	
		public static ModbusServer.ServerIndex msi = new ServerIndex();

        static UserControlBase UC = new UserControlBase();
        private void UserControl1_Load(object sender, EventArgs e)
        {
            e1 = new empty();
            //mci = new ModbusClient.Clientindex();
            //mct = new ModbusClient.modbusclient();
            //msi = new ModbusServer.ServerIndex();

            UC.Parent = this;
            UC.getTreeView(treeView1);

            return;
         }


        /// <summary>
        /// 设置TreeView选中节点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="selectStr">选中节点文本</param>
        private void SelectTreeView(TreeView treeView, string selectStr)
        {
            treeView.Focus();
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                {
                    for(int k = 0; k < treeView.Nodes[i].Nodes.Count; k++)
                    { 
                        if (treeView.Nodes[i].Nodes[j].Nodes[k].Text == selectStr)
                        {
                            treeView1.SelectedNode = treeView.Nodes[i].Nodes[j].Nodes[k];//选中
                                                                                //treeView.Nodes[i].Nodes[j].Checked = true;
                            treeView.Nodes[i].Nodes[j].Expand();//展开父级
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="tv">TreeView</param>
        /// <param name="tnc">tc=tv.Nodes</param>
        /// <param name="nds">node.Text</param>
        private void FindEvery(TreeView tv, TreeNodeCollection tnc, string nds)
        {
            if (tnc.Count != 0)
            {
                for (int i = 0; i < tnc.Count; i++)
                {
                    if (tnc[i].Text == nds)
                    {
                        tv.SelectedNode = tnc[i];
                        tv.SelectedNode.Expand();//展开找到的节点
                        //tv.SelectedNode.BackColor = System.Drawing.Color.LightGray;//谁知道在Node失去选中状态时，如何取消掉这个BackColor的，请留言评论
                        return;//找到一个就返回，没有return则继续查找 直到遍历所有节点
                    }

                    System.Diagnostics.Debug.WriteLine(tnc[i].Text);

                    FindEvery(tv, tnc[i].Nodes, nds);
                }
            }
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
                    treeView1.SelectedNode = tnRet;
                    treeView1.SelectedNode.Expand();//展开找到的节点
                    break;
                }
            }
            return tnRet;
        }


        //接受代理传来参数的方法
        public void DoSomething(string s1)
        {
            //SelectTreeView(treeView1, s1);
            treeView1.Focus();
            //FindEvery(treeView1, treeView1.Nodes, s1);
            TreeNode tnRet = null;
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tnRet = FindNode(tn, s1);
                if (tnRet != null)
                    return;
            }
        }

        //void loadXml(ref )
        private static int adviceProjectCookie = 0;

        void IAdeAddIn.OnConnection(object Application, AdeConnectMode ConnectMode, object AddInInst, ref Array Custom)
        {
            multiprogApp = Application as ADELib.Application;
            adviceProjectCookie = multiprogApp.AdviseProjectObserver(this);
            object[] SafeArrayOfObjectType = new object[1];
            SafeArrayOfObjectType[0] = AdeObjectType.adeOtResource;
            multiprogApp.AdviseVariableObserver2(this, (int)AdeVariableAction.adeVaChange,SafeArrayOfObjectType);

			multiprogApp.AdviseCompileExtension(this, AdeObjectType.adeOtProject);
            
        }

        void IAdeAddIn.OnDisconnection(AdeDisconnectMode RemoveMode, ref Array Custom)
        {
           
        }

        void IAdeAddIn.OnAddInsUpdate(ref Array Custom)
        {
            
        }

        void IAdeAddIn.OnStartupComplete(ref Array Custom)
        {
          
        }

        void IAdeAddIn.OnBeginShutdown(ref Array Custom)
        {
            multiprogApp.UnadviseProjectObserver(adviceProjectCookie);

        }

        void IAdeProjectObserver.BeforeProjectOpen(string Name, ref bool Cancel)
        {
            
        }

        void IAdeProjectObserver.AfterProjectOpen(string Name)
        {
            //MessageBox.Show(Name + "has opened");
            
			//获得当前工程路径
            XmlDocument xDoc = new XmlDocument();
            string projectPath = multiprogApp.ActiveProject.Path;
            string projectName = multiprogApp.ActiveProject.Name;
            string path = projectPath + "\\" + projectName + "\\" + "config_project.xml";

            clean();
            try
            {
                xDoc.Load(path);

                //根节点
                XmlNode node = xDoc.SelectSingleNode("root");
                XmlNodeList nodeList = node.ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement elem = (XmlElement)xn;
                    //根节点下面分支
                    string name = xn.Name;
                    Console.WriteLine(name);

                    if (name == "modbus")
                    {
                        XmlNodeList childList = xn.ChildNodes;
                        foreach (XmlNode nChild in childList)
                        {
                            //student子节点
                            XmlElement eChild = (XmlElement)nChild;
                            string childname = eChild.Name;
                            if (childname == "modbusmaster")
                            {
                                //语文成绩结构
                                //StudentData st = new StudentData();
                                //st.test2 = eChild.InnerText;
                                //modmaster = new modbusmastermain();


                                modmaster.loadXml(nChild);
                            }
                            else if (childname == "modbusslave")
                            {
                                //数学成绩结构
                                modslave.loadXml(nChild);
                            }
                            else if (childname == "modbusclient")
                            {
                                
                                mci.loadXml(nChild);
                            }
                            else if (childname == "modbusserver")
                            {
                                msi.loadXml(nChild);
                            }
                        }



                    }
                }
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return;
            }
        }

        void IAdeProjectObserver.BeforeProjectClose(string Name, ref bool Cancel)
        {
            
        }

        void IAdeProjectObserver.AfterProjectClose(string Name)
        {
            
        }
      
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string name = e.Node.Text.ToString();
            if (name == "Modbus")
            {
                //e1.Show();
                //ModbusWindow.Controls.Clear();
                //ModbusWindow.Controls.Add(e1);


            }
            //else if (name == "MobusTCP-Client")
            //{
            //    if(mct == null)
            //    {
            //        //测试函数 暂定位置
            //        saveXml();
            //        return;
            //    }


            //    saveXml();

            //    e1.Show();
            //    ModbusWindow.Controls.Clear();
            //    ModbusWindow.Controls.Add(e1);

            //}
            else if (name == "ModbusTCP-Client")
			{
                if (mct == null)
                {
                    //测试函数 暂定位置
                    saveXml();
                    return;
                }

                mci.initForm();
                saveXml();
                mci.Show();			
				ModbusWindow.Controls.Clear();
				ModbusWindow.Controls.Add(mci);
			}
			else if(name == "ModbusRTU-Master")
            {
                
                if(modmaster == null)
                {
                    return;
                }

                //刷新master地址
                modmaster.masterManage.getMasterStartAddr();
                modmaster.masterManage.refresh();

                modmaster.initForm();
                saveXml();
                modmaster.Show();
                ModbusWindow.Controls.Clear();
                modmaster.Dock = DockStyle.Fill;
                ModbusWindow.Controls.Add(modmaster);
            }
            else if (name == "ModbusTCP-Server")
		{
                if (msi == null)
                {
                    return;
                }
                msi.initForm();
                saveXml();
                msi.Show();
					   ModbusWindow.Controls.Clear();
		               ModbusWindow.Controls.Add(msi);
		}
			
			else if(name == "ModbusRTU-Slave")
		{
                if (modslave == null)
                {
                    return;
                }
                modslave.initForm();
                saveXml();
                modslave.Show(); 
						ModbusWindow.Controls.Clear();
                modslave.Dock = DockStyle.Fill;
                ModbusWindow.Controls.Add(modslave);
		}
        else if(name == "基本配置")
            {
                UC.Show();
                ModbusWindow.Controls.Clear();
                UC.Dock = DockStyle.Fill;
                ////UC.Size = new Size(472, 336);
                ModbusWindow.Controls.Add(UC);
            }
			else if(name == ConstVariable.DO)
            {
                if(!ModbusWindow.Controls.Contains(UC))
                {
                    UC.Show();
                    ModbusWindow.Controls.Clear();
                    UC.Dock = DockStyle.Fill;
                    ////UC.Size = new Size(472, 336);
                    ModbusWindow.Controls.Add(UC);
                }

                UC.setDOShow(name);
            }
            else if(name == ConstVariable.DI)
            {
                UC.setDIShow(name);
            }
            else if(name == "本体COM1")
            {
                UC.setCOMShow(name);
            }
            else if(name == "本体ETH1")
            {
                UC.setETHShow(name);
            }
            else if(name == "高速计数器")
            {
                UC.setHighInput(name);
            }
            else if(name == "高速输出")
            {
                UC.setHighOutput(name);
            }
            else if(name == "正交编码器")
            {
                UC.setQuadShow(name);
            }
            else if(name == "双相脉冲计数")
            {
                UC.setBiDirPulseShow(name);
            }
            else if(name == "单脉冲计数")
            {
                UC.setSinglePulseShow(name);
            }
            else if(name == "PTO")
            {
                UC.setPTOShow(name);
            }
            else if(name == "PWM")
            {
                UC.setPWMShow(name);
            }
            else if(name == "AI")
            {
                //暂时
                UC.setExtendAIShow(name);
            }
            else if(name == "AO")
            {
                UC.setExtendAOShow(name);
            }
    }
        private void defaultjson(JsonTextWriter writer)//基础配置
        {
            
            writer.WritePropertyName("general");
            writer.WriteStartObject(); //{ general 节点
            writer.WritePropertyName("device_info");
            writer.WriteStartObject(); //{ device_info 节点
            writer.WritePropertyName("device_type");
            writer.WriteValue("DC-14DI-10DO-4HI-2HO-2SERIAL-1ETH-1CAN");
            writer.WriteEndObject(); //} device_info 节点
            //硬件配置
            writer.WritePropertyName("hardware");
            writer.WriteStartObject(); //{ hardware 节点
            //串口配置
            writer.WritePropertyName("serial");
            writer.WriteStartObject(); //{ serial 节点
            writer.WritePropertyName("portnum");
            writer.WriteValue(2);
            writer.WritePropertyName("portcfg");
            writer.WriteStartArray(); //[ 串口portcfg 数组
            writer.WriteStartObject(); //{ 串口portcfg 数组下节点1
            writer.WritePropertyName("namestr");
            writer.WriteValue("ser_port0");
            writer.WritePropertyName("baudrate");
            writer.WriteValue(115200);
            writer.WritePropertyName("parity");
            writer.WriteValue("none");
            writer.WritePropertyName("data_bits");
            writer.WriteValue(8);
            writer.WritePropertyName("stop_bits");
            writer.WriteValue(0);
            writer.WritePropertyName("medium");
            writer.WriteValue("rs232");
            writer.WriteEndObject(); //} portcfg 数组下节点1
            writer.WriteStartObject(); //{ portcfg 数组下节点2
            writer.WritePropertyName("namestr");
            writer.WriteValue("ser_port1");
            writer.WritePropertyName("baudrate");
            writer.WriteValue(115200);
            writer.WritePropertyName("parity");
            writer.WriteValue("none");
            writer.WritePropertyName("data_bits");
            writer.WriteValue(8);
            writer.WritePropertyName("stop_bits");
            writer.WriteValue(0);
            writer.WritePropertyName("medium");
            writer.WriteValue("rs485");
            writer.WriteEndObject(); //} portcfg 数组下节点2
            writer.WriteEndArray(); //] portcfg 数组
            writer.WriteEndObject(); //} serial 节点
            //设备所有的网口个数
            writer.WritePropertyName("ethnet");
            writer.WriteStartObject(); //{ ethnet节点
            writer.WritePropertyName("portnum");
            writer.WriteValue(1);
            writer.WritePropertyName("portcfg");
            writer.WriteStartArray(); //[ 网口portcfg 数组
            writer.WriteStartObject(); //{ 网口portcfg 数组下节点1
            writer.WritePropertyName("mac");
            writer.WriteValue("aa-aa-aa-aa-aa-aa");
            writer.WritePropertyName("is_dhpc");
            writer.WriteValue("false");
            writer.WritePropertyName("ip");
            writer.WriteValue("192.168.1.10");
            writer.WritePropertyName("netmask");
            writer.WriteValue("255.255.255.0");
            writer.WritePropertyName("gateway");
            writer.WriteValue("192.168.1.1");
            writer.WritePropertyName("dns");
            writer.WriteValue("192.168.1.200");
            writer.WritePropertyName("sntp_server_ip");
            writer.WriteValue("192.168.1.13");
            writer.WritePropertyName("sntp_en");
            writer.WriteValue("false");
            writer.WriteEndObject(); //} 网口portcfg 数组下节点1
            writer.WriteEndArray(); //] 网口portcfg 数组
            writer.WriteEndObject(); //} ethnet 节点
            //can
            writer.WritePropertyName("can");
            writer.WriteStartObject(); //{ can节点
            writer.WritePropertyName("pornum");
            writer.WriteValue(1);
            writer.WritePropertyName("portcfg");
            writer.WriteStartArray(); //[ can节点下portcfg数组
            writer.WriteStartObject(); //{ can节点下portcfg数组下节点1
            writer.WritePropertyName("namestr");
            writer.WriteValue("can_port0");
            writer.WritePropertyName("auto_baud");//是否启动自动波特率
            writer.WriteValue(false);
            writer.WritePropertyName("baudrate");
            writer.WriteValue(100000);
            writer.WriteEndObject(); //} can节点下portcfg数组下节点1
            writer.WriteEndArray(); //] can节点下portcfg数组
            //HSC高速脉冲输入组
            writer.WritePropertyName("HSC");
            writer.WriteStartObject(); //{ HSC节点
            writer.WritePropertyName("grp_total");
            writer.WriteValue(3);
            writer.WritePropertyName("conf");
            writer.WriteStartArray(); //[ HSC下的conf数组
            writer.WriteStartObject(); //{ HSC下的conf数组下节点1组合输入模式
            writer.WritePropertyName("opr_mode");
            writer.WriteValue("grouped");
            writer.WritePropertyName("cnt_mode");
            writer.WriteValue("quad");//正交编码
            writer.WritePropertyName("grp_no");
            writer.WriteValue(0);
            writer.WritePropertyName("grp_member");
            writer.WriteStartArray(); //[ grp_member 高速脉冲输入数组
            writer.WriteValue("DI00");
            writer.WriteValue("DI01");
            writer.WriteEndArray(); //] grp_member 高速脉冲输入数组
            writer.WriteEndObject(); //} HSC下的conf数组下节点1组合输入模式
            writer.WriteStartObject(); //{ HSC下的conf数组下节点2单路脉冲计数模式1
            writer.WritePropertyName("opr_mode");
            writer.WriteValue("single");
            writer.WritePropertyName("cnt_mode");
            writer.WriteValue("");
            writer.WritePropertyName("grp_no");//单路脉冲计数模式下，该项无意义
            writer.WriteValue(128);
            writer.WritePropertyName("grp_member");
            writer.WriteStartArray(); //[ grp_member 单路脉冲计数组
            writer.WriteValue("DI02");
            writer.WriteEndArray(); //] grp_member 单路脉冲计数组
            writer.WriteEndObject(); //} HSC下的conf数组下节点2单路脉冲计数模式1
            writer.WriteStartObject(); //{ HSC下的conf数组下节点3单路脉冲计数模式2
            writer.WritePropertyName("opr_mode");
            writer.WriteValue("simple");
            writer.WritePropertyName("cnt_mode");
            writer.WriteValue("");
            writer.WritePropertyName("grp_no");//单路脉冲计数模式下，该项无意义
            writer.WriteValue(128);
            writer.WritePropertyName("grp_member");
            writer.WriteStartArray(); //[ grp_member 单路脉冲计数组
            writer.WriteValue("DI03");
            writer.WriteEndArray(); //] grp_member 单路脉冲计数组
            writer.WriteEndObject(); //} HSC下的conf数组下节点3单路脉冲计数模式2
            writer.WriteEndArray();//] HSC下的conf数组
            writer.WriteEndObject();//} HSC节点
            //PTO高速脉冲输出组
            writer.WritePropertyName("PTO");
            writer.WriteStartObject(); //{ PTO节点
            writer.WritePropertyName("grp_total");
            writer.WriteValue(1);
            writer.WritePropertyName("conf");
            writer.WriteStartArray(); //[ PTO下conf数组
            writer.WriteStartObject(); //{ PTO conf数组下节点组合脉冲输入模式
            writer.WritePropertyName("opr_mode");
            writer.WriteValue("grouped");
            writer.WritePropertyName("output_mode");
            writer.WriteValue("quad");
            writer.WritePropertyName("grp_no");
            writer.WriteValue(0);
            writer.WritePropertyName("grp_member");
            writer.WriteStartArray(); //[ grp_member 单路脉冲计数组
            writer.WriteValue("DO00");
            writer.WriteValue("DO01");
            writer.WriteEndArray(); //] grp_member 单路脉冲计数组
            writer.WriteEndObject(); //} PTO conf数组下节点组合脉冲输入模式
            writer.WriteEndArray(); //]PTO下conf数组
            writer.WriteEndObject(); //} PTO节点
            //DI
            writer.WritePropertyName("DI");
            writer.WriteStartObject(); //{ DI节点
            writer.WritePropertyName("conf");
            writer.WriteStartArray(); //[ DI下conf数组
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置1
            writer.WritePropertyName("member");
            writer.WriteValue("DI04");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置1
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置2
            writer.WritePropertyName("member");
            writer.WriteValue("DI05");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置2
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置3
            writer.WritePropertyName("member");
            writer.WriteValue("DI06");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置3
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置4
            writer.WritePropertyName("member");
            writer.WriteValue("DI07");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置4
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置5
            writer.WritePropertyName("member");
            writer.WriteValue("DI08");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置5
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置6
            writer.WritePropertyName("member");
            writer.WriteValue("DI09");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置6
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置7
            writer.WritePropertyName("member");
            writer.WriteValue("DI010");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置7
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置8
            writer.WritePropertyName("member");
            writer.WriteValue("DI11");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置8
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置9
            writer.WritePropertyName("member");
            writer.WriteValue("DI12");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置9
            writer.WriteStartObject(); //{ DI conf数组下节点DI输入配置10
            writer.WritePropertyName("member");
            writer.WriteValue("DI13");
            writer.WritePropertyName("filter_time");
            writer.WriteValue(10);
            writer.WriteEndObject(); //} DI conf数组下节点DI输入配置10
            writer.WriteEndArray(); //]DI下conf数组
            writer.WriteEndObject(); //} DI节点
            //DO
            writer.WritePropertyName("DO");
            writer.WriteStartObject(); //{ DO节点
            writer.WritePropertyName("conf");
            writer.WriteStartArray(); //[ DO下conf数组
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置1
            writer.WritePropertyName("member");
            writer.WriteValue("DO02");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置1
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置2
            writer.WritePropertyName("member");
            writer.WriteValue("DO03");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置2
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置3
            writer.WritePropertyName("member");
            writer.WriteValue("DO04");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置3
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置4
            writer.WritePropertyName("member");
            writer.WriteValue("DO05");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置4
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置5
            writer.WritePropertyName("member");
            writer.WriteValue("DO06");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置5
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置6
            writer.WritePropertyName("member");
            writer.WriteValue("DO07");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置6
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置7
            writer.WritePropertyName("member");
            writer.WriteValue("DO08");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置7
            writer.WriteStartObject(); //{ DO conf数组下节点DO输入配置8
            writer.WritePropertyName("member");
            writer.WriteValue("DO09");
            writer.WriteEndObject(); //} DO conf数组下节点DO输入配置8
            writer.WriteEndArray(); //]DO下conf数组
            writer.WriteEndObject(); //} DO节点
            writer.WriteEndObject(); //} hardware节点
            writer.WriteEndObject(); //} general节点
            writer.WriteEndObject(); //} 总节点
        }

        private void saveJson()
        {
            try
            {
                string name = multiprogApp.ActiveProject.Name;
            }
            catch (Exception e)
            {
                utility.PrintError("请先打开工程!");
                return;
            }
            string projectPath = multiprogApp.ActiveProject.Path;
            string projectName = multiprogApp.ActiveProject.Name;
            string path = projectPath + "\\" + projectName + "\\" + "myconfig.json";

            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);//字符串转换为json

            // writer.WriteStartObject();  
            //=====================================
            //第一级节点
            
            writer.WriteStartObject();//   {  （Json数据的大括号左边 ）
            defaultjson(writer);
            writer.WritePropertyName("modbus");
            writer.WriteStartObject();// { modbus大括号左边
            //writer.WriteValue("1");
            modslave.saveJson(writer);
            msi.saveJson(writer);
            modmaster.saveJson(writer);
            mci.saveJson(writer);
            //============================
            ////第一级节点
            //writer.WritePropertyName("img");

            //writer.WriteStartObject();//{

            //writer.WritePropertyName("ig1");

            //writer.WriteValue("3");

            //writer.WritePropertyName("ig2");

            //writer.WriteValue("3");

            //writer.WritePropertyName("ig3");

            //writer.WriteValue("3");

            //writer.WritePropertyName("ig4");

            //writer.WriteValue("3");

            //writer.WriteEndObject();//} 


            //=====================================
            writer.WriteEndObject();//} modbus大括号右边
            writer.WriteEndObject();//}json数据大括号右边
            //writer.WriteEndObject(); //} 总节点
            string str = "myconfig.json";
            string strpath = projectPath + @"\" + str;//System.IO.Directory.GetCurrentDirectory() + @"\" + Name;
            StreamWriter wtyeu = new StreamWriter(path);
            wtyeu.Write(sw);
            wtyeu.Flush();
            wtyeu.Close();
            
            
        }
        private void saveXml()
        {
            try 
            {
                string name = multiprogApp.ActiveProject.Name;
            }
            catch (Exception e)
            {
                utility.PrintError("请先打开工程!");
                return;
            }


            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration declaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");//xml文件抬头节点
            xDoc.AppendChild(declaration);

            //创建根节点
            XmlElement elemRoot = xDoc.CreateElement("root");
            xDoc.AppendChild(elemRoot);
            XmlElement elem = xDoc.CreateElement("modbus");
            elemRoot.AppendChild(elem);

            modmaster.saveXml(ref elem, ref xDoc);
            modslave.saveXml(ref elem, ref xDoc);
            mci.saveXml(ref elem, ref xDoc);
            msi.saveXml(ref elem, ref xDoc);
            //
            //

            //xDoc.Save("students.xml");

            string projectPath = multiprogApp.ActiveProject.Path;
            string projectName = multiprogApp.ActiveProject.Name;

            xDoc.Save(projectPath + "\\" + projectName + "\\config_project.xml");
            int 您好 = 5;
            int b = 您好;
        }


        void clean()
        {
            if(modmaster != null)
            {
                modmaster.deleteTableRow();
                modmaster.masterManage.modbusMastrList.Clear();
            }
            
            if(modslave != null)
            {
                modslave.deleteTableRow();
                modslave.slaveDataManager.listSlave.Clear();
            }
            if (mci != null)
            {
                mci.deleteTableRow();
                mci.clientManage.modbusClientList.Clear();
                
            }

            if (msi != null)
            {
                msi.deleteTableRow();
                msi.serverDataManager.listServer.Clear();
            }
            //client server clean
        }
        private void ModbusWindow_Enter(object sender, EventArgs e)
        {        
		}
private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            if(e.Node.Text == "Modbus")
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);

                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    CurrentNode.ContextMenuStrip = contextMenuStrip1;
                    string name = treeView1.SelectedNode.Text.ToString();//存储节点的文本
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                }
            }

        }
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            saveXml();
            saveJson();

            utility.addIOGroups();

            //utility.addServerIOGroups();
            //utility.addVarType();
            utility.addVarType1();
            utility.addVariables();
            multiprogApp.ActiveProject.Compile(AdeCompileType.adeCtBuild);
        }
        public  int a = 0;
        void IAdeCompileExtension.OnCompile(object Object, AdeCompileType CompileType, ref bool Errors)
        {

            if(!multiprogApp.IsProjectOpen() || msi.serverDataManager.listServer.Count == 0)
            {
                return;
            }
            int coillength = msi.serverDataManager.listServer[0].dataDevice_.coilCount;
            int coilstart = Convert.ToInt32(msi.serverDataManager.listServer[0].dataDevice_.coilIoAddrStart);
            int coilIOstart = msi.serverDataManager.listServer[0].serverstartaddr;
            int holdinglength = msi.serverDataManager.listServer[0].dataDevice_.holdingCount;
            int holdingstart = Convert.ToInt32(msi.serverDataManager.listServer[0].dataDevice_.holdingIoAddrStart);
            int holdingIOstart = msi.serverDataManager.listServer[0].serverstartaddr+100;
            int decretelength = msi.serverDataManager.listServer[0].dataDevice_.decreteCount;
            int decretestart = Convert.ToInt32(msi.serverDataManager.listServer[0].dataDevice_.decreteIoAddrStart);
            int decreteIOstart = msi.serverDataManager.listServer[0].serverstartaddr+500;
            int statuslength = msi.serverDataManager.listServer[0].dataDevice_.statusCount;
            int statusstart = Convert.ToInt32(msi.serverDataManager.listServer[0].dataDevice_.statusIoAddrStart);
            int statusIOstart = msi.serverDataManager.listServer[0].serverstartaddr + 600;
            if (multiprogApp != null && multiprogApp.IsProjectOpen())
            {
                Hardware physicalHardware = multiprogApp.ActiveProject.Hardware;
                foreach (Configuration configuration in physicalHardware.Configurations)
                {
                    foreach (Resource resource in configuration.Resources)
                    {
                        var groups = resource.Variables.Groups;
                       
                        foreach (VariableGroup servergroup in groups)
                        {
                            if (servergroup.Name == "Server")//变量组是否为Server组
                            {
                                foreach(ADELib.Variable variable in servergroup.Variables)//遍历组中变量，根据变量在modbus地址这一项中的值，修改IO地址
                                {
                                    object mda = variable.GetAttribute(19);
                                    int modbusaddr = Convert.ToInt32(mda);

                                    for (int i = 0; i < coillength; i++)
                                    {
                                        if (modbusaddr == coilstart + i)
                                        {
                                            int a = i / 8;
                                            int b = i % 8;
                                            variable.IecAddress ="%MX" +(coilIOstart+a).ToString()+"."+b.ToString();
                                            
                                        }
                                    }
                                    for (int j = 0;j < holdinglength;j++)
                                    {
                                        if (modbusaddr == holdingstart + j)
                                        {

                                            variable.IecAddress = "%MW" + (holdingIOstart + j).ToString();
                                        }
                                    }
                                    for (int k = 0; k < decretelength; k++)
                                    {
                                        if (modbusaddr == decretestart + k)
                                        {
                                            int c = k / 8;
                                            int d = k % 8;
                                            variable.IecAddress = "%IX" + (decreteIOstart + c).ToString()+"."+d.ToString();
                                        }
                                    }
                                    for (int l = 0; l < statuslength; l++)
                                    {
                                        if (modbusaddr == statusstart + l)
                                        {

                                            variable.IecAddress = "%IW" + (statusIOstart + l).ToString();
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }



            //var configurations = multiprogApp.ActiveProject.Hardware.Configurations;
            //// create the configuration tree items for all configurations
            //foreach (Configuration configuration in configurations)
            //{

            //    foreach (Resource resource in configuration.Resources)
            //    {
            //        Variables variables = resource.Variables;
            //        foreach (ADELib.Variable variable in variables)
            //        {
            //            //地址修改
            //            //variable.IecAddress = "%IX1000.0";
            //            //modbus地址修改
            //            object var = variable.GetAttribute(19);
            //            if(var != null)
            //            {
            //                string str = var.ToString();
            //            }
                        
            //            variable.IecAddress = "%IX1000.0";


            //        }
            //    }
            //}


                    //utility.addIOGroups();

                    //utility.addVariables();
                    //IoGroups iog = multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups;

                    //iog.Create("master1_in", AdeIoGroupAccessType.adeIgatInput,
                    //            1000, "driver1", "<默认>", "", 1000, "test", AdeIoGroupDataType.adeIgdtByte, 
                    //            1, 1, 1, 1);
                    //iog.Create("master1_out", AdeIoGroupAccessType.adeIgatOutput,
                    //            1000, "driver1", "<默认>", "", 1000, "test", AdeIoGroupDataType.adeIgdtByte,
                    //            1, 1, 1, 1);

                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);

                    //var variables_ = multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).Variables;
                    //var text = modmaster.masterManage.modbusMastrList[0].modbusDeviceList[0].modbusChannelList[0].nameChannel;
                    //variables_.Create(text, "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                    //                         "Inserted from AIFDemo", "12", "%MW1.1003", false);

                    //modmaster.masterManage.modbusMastrList[0].modbusDeviceList[0].modbusChannelList[0].

                    //return;
                    //IoGroups iog = multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups;
                    //var count = iog.Count;
                    //foreach (IoGroup io in iog)
                    //{

                    //    var name = io.Name;

                    //    //io.Delete();
                    //}

                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);

                    //var configurations = multiprogApp.ActiveProject.Hardware.Configurations;
                    //// create the configuration tree items for all configurations
                    //foreach (Configuration configuration in configurations)
                    //{

                    //    foreach (Resource resource in configuration.Resources)
                    //    {

                    //        resource.IoGroups.Create("master1", AdeIoGroupAccessType.adeIgatInput,
                    //            1000, "driver1", "");

                    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(resource.IoGroups);

                    //        return;
                    //        // get the variables collection with the specified logical name
                    //        AdeObjectType objectType = AdeObjectType.adeOtVariables;
                    //        object variablesObject =
                    //            multiprogApp.ActiveProject.GetObjectByLogicalName(resource.Variables.LogicalName, ref objectType);
                    //        // is the returned object really of type "Variables"?
                    //        if (objectType == AdeObjectType.adeOtVariables)
                    //        {
                    //            Variables variables = variablesObject as Variables;


                    //            foreach (ADELib.Variable variable in variables)
                    //            {
                    //                //地址修改
                    //                //variable.IecAddress = "%IX1000.0";
                    //                //modbus地址修改
                    //                variable.SetAttribute(89, 40001);



                    //            }
                    //        }

                    //        //// add only the PG instance if there are variables available or FB instances with variables
                    //        //foreach (Task task in resource.Tasks)
                    //        //{
                    //        //    foreach (ProgramInstance programInstance in task.ProgramInstances)
                    //        //    {
                    //        //        // get the variables of this PG instance
                    //        //        Variables programInstanceVariables = programInstance.Variables;
                    //        //        // variables available? (variables at program instances are optional!)
                    //        //        if (programInstanceVariables != null)
                    //        //        {
                    //        //            object variablesObject =
                    //        //                mpApplication.ActiveProject.GetObjectByLogicalName(programInstanceVariables.LogicalName, ref objectType);
                    //        //        }

                    //        //        // add only FB instances if there are variables available
                    //        //        foreach (FbInstance fbInstance in programInstance.FbInstances)
                    //        //        {
                    //        //            // get the variables of this FB instance
                    //        //            Variables fbInstanceVariables = fbInstance.Variables;
                    //        //            // variables available? (variables at FB instances are optional!)
                    //        //            if (fbInstanceVariables != null)
                    //        //            {
                    //        //                object variablesObject =
                    //        //                    mpApplication.ActiveProject.GetObjectByLogicalName(fbInstanceVariables.LogicalName, ref objectType);
                    //        //            }
                    //        //        }

                    //        //        // add the program instance tree item (and its sub items) only if there are any sub items
                    //        //        ICollection<ITreeItem> programInstanceSubItems = programInstanceItem.SubItems as ICollection<ITreeItem>;
                    //        //        if ((programInstanceSubItems != null) && (programInstanceSubItems.Count > 0))
                    //        //        {
                    //        //            resourceItem.AddSubItem(programInstanceItem);
                    //        //        }
                    //        //    }
                    //        //}
                    //    }
                    //}
                }

        void IAdeVariableObserver2.BeforeInsert(AdeObjectType ObjectType, ref Variable Variable, ref bool Cancel)
        {
            throw new NotImplementedException();
        }

        void IAdeVariableObserver2.AfterInsert(AdeObjectType ObjectType, ref Variable Variable)
        {
            throw new NotImplementedException();
        }

        void IAdeVariableObserver2.BeforeDelete(AdeObjectType ObjectType, Variable Variable, ref bool Cancel)
        {
            throw new NotImplementedException();
        }
        private static int adviceProjectCookie1 = 0;

        public object[] SafeArrayOfObjectType { get; private set; }

        void IAdeVariableObserver2.AfterDelete(AdeObjectType ObjectType, Variable Variable)
        {
            //multiprogApp = Application as ADELib.Application;
            adviceProjectCookie1 = multiprogApp.AdviseVariableObserver2(this,4);
        }

        void IAdeVariableObserver2.BeforeChange(AdeObjectType ObjectType, Variable OldVariable, ref Variable NewVariable, ref bool Cancel)
        {
            
        }

        void IAdeVariableObserver2.AfterChange(AdeObjectType ObjectType, Variable OldVariable, ref Variable NewVariable)
        {
            //MessageBox.Show("已修改");
           
        }

        void IAdeVariableObserver2.OnErrorCodeChanged(AdeObjectType ObjectType, ref Variable Variable)
        {
            string name = Variable.Name;
        }

        void IAdeVariableObserver2.BeforeMove(AdeObjectType ObjectType, Variable OldVariable, VariableGroup NewVariableGroup, ref bool Cancel)
        {
            
        }

        void IAdeVariableObserver2.AfterMove(AdeObjectType ObjectType, Variable NewVariable, VariableGroup OldVariableGroup)
        {
            
        }

        public static void addIOGroups()
        {
            IoGroups iog = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups;
            
            List<IoGroup> listGroup = new List<IoGroup>();

            Dictionary<int, IoGroup> dic = new Dictionary<int, IoGroup>();
            foreach(IoGroup group in iog)
            {
                if(group.Name.Contains("mastter"))
                {
                    listGroup.Add(group);
                }
            }



            var list = UserControl1.modmaster.masterManage.modbusMastrList;
            foreach (var master in list)
            {
                string str = string.Format("master_in{0}", master.ID);

                iog.Create(str, AdeIoGroupAccessType.adeIgatInput,
            utility.modbusMudule, "driver1", "<默认>", "", master.curMasterStartAddr, "test", AdeIoGroupDataType.adeIgdtByte,
            1, 1, 1, 1);
                str = string.Format("master_out{0}", master.ID);
                iog.Create(str, AdeIoGroupAccessType.adeIgatOutput,
                            utility.modbusMudule, "driver1", "<默认>", "", master.curMasterStartAddr, "test", AdeIoGroupDataType.adeIgdtByte,
                            1, 1, 1, 1);
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
        }

        public static void deleteIOGroups()
        {

        }
    }

    public class MyRichTextBox : TextBox
    {
        private bool _state_textBox1_selectAll = false;//开关值

        DataGridView grid_ = null;
        public MyRichTextBox()
        {
            //this.Enter += new System.EventHandler(this.textBox1_Enter);
            //this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseUp);


        }

        public void setParent(DataGridView grid)
        {
            grid_ = grid;
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            this._state_textBox1_selectAll = true;//当获取焦点后，设置开关
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this._state_textBox1_selectAll)//如果开关被设置，则进行SelectAll
            {
                this._state_textBox1_selectAll = false;//先清除开关，以免影响MouseUp
                this.SelectAll();
                this.Focus();
                this.ScrollToCaret();

            }
        }



        private GetComposition getImmStr = new GetComposition();
        string immStr;
        private const int WM_IME_COMPOSITION = 0x010F;
        const int WM_IME_CHAR = 0x0286;

        [DllImport("User.dll", EntryPoint = "SendMessage")]

        private static extern int SendMessage(

                    int hWnd,   // handle to destination window

                    int Msg,    // message

                    int wParam, // first message parameter

                    int lParam // second message parameter

);

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)                               //判断系统消息的ID号     
            {
                case 513:
                    //MessageBox.Show("恭喜你点击了左键！");
                    //m.Result = (IntPtr)0;                  //为了响应消息处理而向 Windows 返回的值        
                    base.WndProc(ref m);
                    break;
                case 516:
                    MessageBox.Show("恭喜你点击了右键！");
                    m.Result = (IntPtr)0;                //为了响应消息处理而向 Windows 返回的值          
                    break;

                case 0x10E:
                    //截获输入法结果
                    immStr = getImmStr.CurrentCompStr(this.Handle);
                    //Debug.WriteLine(immStr);

                    string str = immStr;

                    //base.WndProc(ref m);

                    this.Text += str;

                    if(grid_ != null)
                    {
                        grid_.CurrentCell.Value = this.Text;
                    }

                    this.SelectionStart = this.TextLength;
                    //this.ScrollToCaret();
                    //Select(Text.Length, 0);

                    this.Invalidate();

                    break;
                case WM_IME_COMPOSITION:
                    int a = 5;
                    a = 6;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }

    public class GetComposition
    {

        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
        [DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
        private static extern int ImmGetCompositionStringW(IntPtr hIMC, int dwIndex, byte[] lpBuf, int dwBufLen);



        public string CurrentCompStr(IntPtr handle)
        {
            IntPtr hIMC = ImmGetContext(handle);
            try
            {
                int strLen = ImmGetCompositionStringW(hIMC, 0x0800, null, 0);

                if (strLen > 0)
                {
                    byte[] buffer = new byte[strLen];
                    ImmGetCompositionStringW(hIMC, 0x0800, buffer, strLen);




                    return Encoding.Unicode.GetString(buffer);
                }
                else
                {
                    return string.Empty;
                }
            }
            finally
            {
                ImmReleaseContext(handle, hIMC);
            }
        }
    }

    public enum emWinMsg
    {
        eWM_NULL = 0x0000,
        eWM_CREATE = 0x0001,
        eWM_DESTROY = 0x0002,
        eWM_MOVE = 0x0003,
        eWM_SIZE = 0x0005,
        eWM_ACTIVATE = 0x0006,
        eWM_SETFOCUS = 0x0007,
        eWM_KILLFOCUS = 0x0008,
        eWM_ENABLE = 0x000A,
        eWM_SETREDRAW = 0x000B,
        eWM_SETTEXT = 0x000C,
        eWM_GETTEXT = 0x000D,
        eWM_GETTEXTLENGTH = 0x000E,
        eWM_PAINT = 0x000F,
        eWM_CLOSE = 0x0010,
        eWM_QUERYENDSESSION = 0x0011,
        eWM_QUIT = 0x0012,
        eWM_QUERYOPEN = 0x0013,
        eWM_ERASEBKGND = 0x0014,
        eWM_SYSCOLORCHANGE = 0x0015,
        eWM_ENDSESSION = 0x0016,
        eWM_SHOWWINDOW = 0x0018,
        eWM_CTLCOLOR = 0x0019,
        eWM_WININICHANGE = 0x001A,
        eWM_SETTINGCHANGE = 0x001A,
        eWM_DEVMODECHANGE = 0x001B,
        eWM_ACTIVATEAPP = 0x001C,
        eWM_FONTCHANGE = 0x001D,
        eWM_TIMECHANGE = 0x001E,
        eWM_CANCELMODE = 0x001F,
        eWM_SETCURSOR = 0x0020,
        eWM_MOUSEACTIVATE = 0x0021,
        eWM_CHILDACTIVATE = 0x0022,
        eWM_QUEUESYNC = 0x0023,
        eWM_GETMINMAXINFO = 0x0024,
        eWM_PAINTICON = 0x0026,
        eWM_ICONERASEBKGND = 0x0027,
        eWM_NEXTDLGCTL = 0x0028,
        eWM_SPOOLERSTATUS = 0x002A,
        eWM_DRAWITEM = 0x002B,
        eWM_MEASUREITEM = 0x002C,
        eWM_DELETEITEM = 0x002D,
        eWM_VKEYTOITEM = 0x002E,
        eWM_CHARTOITEM = 0x002F,
        eWM_SETFONT = 0x0030,
        eWM_GETFONT = 0x0031,
        eWM_SETHOTKEY = 0x0032,
        eWM_GETHOTKEY = 0x0033,
        eWM_QUERYDRAGICON = 0x0037,
        eWM_COMPAREITEM = 0x0039,
        eWM_GETOBJECT = 0x003D,
        eWM_COMPACTING = 0x0041,
        eWM_COMMNOTIFY = 0x0044,
        eWM_WINDOWPOSCHANGING = 0x0046,
        eWM_WINDOWPOSCHANGED = 0x0047,
        eWM_POWER = 0x0048,
        eWM_COPYDATA = 0x004A,
        eWM_CANCELJOURNAL = 0x004B,
        eWM_NOTIFY = 0x004E,
        eWM_INPUTLANGCHANGEREQUEST = 0x0050,
        eWM_INPUTLANGCHANGE = 0x0051,
        eWM_TCARD = 0x0052,
        eWM_HELP = 0x0053,
        eWM_USERCHANGED = 0x0054,
        eWM_NOTIFYFORMAT = 0x0055,
        eWM_CONTEXTMENU = 0x007B,
        eWM_STYLECHANGING = 0x007C,
        eWM_STYLECHANGED = 0x007D,
        eWM_DISPLAYCHANGE = 0x007E,
        eWM_GETICON = 0x007F,
        eWM_SETICON = 0x0080,
        eWM_NCCREATE = 0x0081,
        eWM_NCDESTROY = 0x0082,
        eWM_NCCALCSIZE = 0x0083,
        eWM_NCHITTEST = 0x0084,
        eWM_NCPAINT = 0x0085,
        eWM_NCACTIVATE = 0x0086,
        eWM_GETDLGCODE = 0x0087,
        eWM_SYNCPAINT = 0x0088,
        eWM_NCMOUSEMOVE = 0x00A0,
        eWM_NCLBUTTONDOWN = 0x00A1,
        eWM_NCLBUTTONUP = 0x00A2,
        eWM_NCLBUTTONDBLCLK = 0x00A3,
        eWM_NCRBUTTONDOWN = 0x00A4,
        eWM_NCRBUTTONUP = 0x00A5,
        eWM_NCRBUTTONDBLCLK = 0x00A6,
        eWM_NCMBUTTONDOWN = 0x00A7,
        eWM_NCMBUTTONUP = 0x00A8,
        eWM_NCMBUTTONDBLCLK = 0x00A9,
        eWM_KEYDOWN = 0x0100,
        eWM_KEYUP = 0x0101,
        eWM_CHAR = 0x0102,
        eWM_DEADCHAR = 0x0103,
        eWM_SYSKEYDOWN = 0x0104,
        eWM_SYSKEYUP = 0x0105,
        eWM_SYSCHAR = 0x0106,
        eWM_SYSDEADCHAR = 0x0107,
        eWM_KEYLAST = 0x0108,
        eWM_IME_STARTCOMPOSITION = 0x010D,
        eWM_IME_ENDCOMPOSITION = 0x010E,
        eWM_IME_COMPOSITION = 0x010F,
        eWM_IME_KEYLAST = 0x010F,
        eWM_INITDIALOG = 0x0110,
        eWM_COMMAND = 0x0111,
        eWM_SYSCOMMAND = 0x0112,
        eWM_TIMER = 0x0113,
        eWM_HSCROLL = 0x0114,
        eWM_VSCROLL = 0x0115,
        eWM_INITMENU = 0x0116,
        eWM_INITMENUPOPUP = 0x0117,
        eWM_MENUSELECT = 0x011F,
        eWM_MENUCHAR = 0x0120,
        eWM_ENTERIDLE = 0x0121,
        eWM_MENURBUTTONUP = 0x0122,
        eWM_MENUDRAG = 0x0123,
        eWM_MENUGETOBJECT = 0x0124,
        eWM_UNINITMENUPOPUP = 0x0125,
        eWM_MENUCOMMAND = 0x0126,
        eWM_CTLCOLORWinMsgBOX = 0x0132,
        eWM_CTLCOLOREDIT = 0x0133,
        eWM_CTLCOLORLISTBOX = 0x0134,
        eWM_CTLCOLORBTN = 0x0135,
        eWM_CTLCOLORDLG = 0x0136,
        eWM_CTLCOLORSCROLLBAR = 0x0137,
        eWM_CTLCOLORSTATIC = 0x0138,
        eWM_MOUSEMOVE = 0x0200,
        eWM_LBUTTONDOWN = 0x0201,
        eWM_LBUTTONUP = 0x0202,
        eWM_LBUTTONDBLCLK = 0x0203,
        eWM_RBUTTONDOWN = 0x0204,
        eWM_RBUTTONUP = 0x0205,
        eWM_RBUTTONDBLCLK = 0x0206,
        eWM_MBUTTONDOWN = 0x0207,
        eWM_MBUTTONUP = 0x0208,
        eWM_MBUTTONDBLCLK = 0x0209,
        eWM_MOUSEWHEEL = 0x020A,
        eWM_PARENTNOTIFY = 0x0210,
        eWM_ENTERMENULOOP = 0x0211,
        eWM_EXITMENULOOP = 0x0212,
        eWM_NEXTMENU = 0x0213,
        eWM_SIZING = 0x0214,
        eWM_CAPTURECHANGED = 0x0215,
        eWM_MOVING = 0x0216,
        eWM_DEVICECHANGE = 0x0219,
        eWM_MDICREATE = 0x0220,
        eWM_MDIDESTROY = 0x0221,
        eWM_MDIACTIVATE = 0x0222,
        eWM_MDIRESTORE = 0x0223,
        eWM_MDINEXT = 0x0224,
        eWM_MDIMAXIMIZE = 0x0225,
        eWM_MDITILE = 0x0226,
        eWM_MDICASCADE = 0x0227,
        eWM_MDIICONARRANGE = 0x0228,
        eWM_MDIGETACTIVE = 0x0229,
        eWM_MDISETMENU = 0x0230,
        eWM_ENTERSIZEMOVE = 0x0231,
        eWM_EXITSIZEMOVE = 0x0232,
        eWM_DROPFILES = 0x0233,
        eWM_MDIREFRESHMENU = 0x0234,
        eWM_IME_SETCONTEXT = 0x0281,
        eWM_IME_NOTIFY = 0x0282,
        eWM_IME_CONTROL = 0x0283,
        eWM_IME_COMPOSITIONFULL = 0x0284,
        eWM_IME_SELECT = 0x0285,
        eWM_IME_CHAR = 0x0286,
        eWM_IME_REQUEST = 0x0288,
        eWM_IME_KEYDOWN = 0x0290,
        eWM_IME_KEYUP = 0x0291,
        eWM_MOUSEHOVER = 0x02A1,
        eWM_MOUSELEAVE = 0x02A3,
        eWM_CUT = 0x0300,
        eWM_COPY = 0x0301,
        eWM_PASTE = 0x0302,
        eWM_CLEAR = 0x0303,
        eWM_UNDO = 0x0304,
        eWM_RENDERFORMAT = 0x0305,
        eWM_RENDERALLFORMATS = 0x0306,
        eWM_DESTROYCLIPBOARD = 0x0307,
        eWM_DRAWCLIPBOARD = 0x0308,
        eWM_PAINTCLIPBOARD = 0x0309,
        eWM_VSCROLLCLIPBOARD = 0x030A,
        eWM_SIZECLIPBOARD = 0x030B,
        eWM_ASKCBFORMATNAME = 0x030C,
        eWM_CHANGECBCHAIN = 0x030D,
        eWM_HSCROLLCLIPBOARD = 0x030E,
        eWM_QUERYNEWPALETTE = 0x030F,
        eWM_PALETTEISCHANGING = 0x0310,
        eWM_PALETTECHANGED = 0x0311,
        eWM_HOTKEY = 0x0312,
        eWM_PRINT = 0x0317,
        eWM_PRINTCLIENT = 0x0318,
        eWM_HANDHELDFIRST = 0x0358,
        eWM_HANDHELDLAST = 0x035F,
        eWM_AFXFIRST = 0x0360,
        eWM_AFXLAST = 0x037F,
        eWM_PENWINFIRST = 0x0380,
        eWM_PENWINLAST = 0x038F,
        eWM_APP = 0x8000,
        eWM_USER = 0x0400,
        eWM_REFLECT = eWM_USER + 0x1c00
    }
}
