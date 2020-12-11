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

        //接受代理传来参数的方法
        public void DoSomething(string s1)
        {
            SelectTreeView(treeView1, s1);
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
            writer.WriteValue(0);
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
            writer.WriteValue(0);
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
            //writer.WriteEndObject(); //} 总节点
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
            writer.WriteEndObject(); //} 总节点
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
            utility.addVarType();
            utility.addVariables();
        }
        public  int a = 0;
        void IAdeCompileExtension.OnCompile(object Object, AdeCompileType CompileType, ref bool Errors)
        {

            if(!multiprogApp.IsProjectOpen())
            {
                return;
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
}
