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

namespace LocalPLC
{
    [ComVisible(true)]
    [Guid("4D23925D-E5C1-40A8-9D69-AAD815FDCECE")]
    [ProgId("LocalPLC.CONTROLBAR.PROGID")]
    public partial class UserControl1 : UserControl,IAdeAddIn,IAdeProjectObserver
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
        
        private void UserControl1_Load(object sender, EventArgs e)
        {
            e1 = new empty();
            mci = new ModbusClient.Clientindex();
            mct = new ModbusClient.modbusclient();
            msi = new ModbusServer.ServerIndex();

            return;
            
            XmlDocument xDoc = new XmlDocument();
            //string projectPath = multiprogApp.ActiveProject.Path;
            //string projectName = multiprogApp.ActiveProject.Name;
            xDoc.Load("students.xml");//加载xml文件


            //根节点
            XmlNode node = xDoc.SelectSingleNode("root");  //获得根节点root，SelectSingleNode表示获得第一个姓名匹配的节点，此处为根节点
            XmlNodeList nodeList = node.ChildNodes; //获取根节点下所有的子节点 
            foreach (XmlNode xn in nodeList)//遍历各个子节点
            {
                XmlElement elem = (XmlElement)xn;
                //根节点下面分支
                string name = xn.Name;
                Console.WriteLine(name);

                if(name == "modbus")                //modbus节点下
                {
                    XmlNodeList childList = xn.ChildNodes;//读取modbus节点下所有子节点
                    foreach (XmlNode nChild in childList) //遍历modbus下全部子节点
                    {
                        //student子节点
                        XmlElement eChild = (XmlElement)nChild;
                        string childname = eChild.Name;
                        if (childname == "modbusmaster")
                        {

                            //语文成绩结构
                            //StudentData st = new StudentData();
                            //st.test2 = eChild.InnerText;
                            if(modmaster == null)
                            {
                                modmaster = new modbusmastermain();
                            }
                            

                            modmaster.loadXml(nChild);
                        }
                        else if (childname == "modbusslave")
                        {
                            //数学成绩结构
                        }
                        else if (childname == "modbusclient")
                        {
                            //
                        }
                        else if(childname == "modbusserver")
                        {

                        }
                    }


                
                }
            }
    }

        //void loadXml(ref )
        private static int adviceProjectCookie = 0;

        void IAdeAddIn.OnConnection(object Application, AdeConnectMode ConnectMode, object AddInInst, ref Array Custom)
        {
            multiprogApp = Application as ADELib.Application;
            adviceProjectCookie = multiprogApp.AdviseProjectObserver(this);
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
            MessageBox.Show(Name + "has opened");
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
                modmaster.initForm();
                saveXml();
                modmaster.Show();
                ModbusWindow.Controls.Clear();


                ModbusWindow.Controls.Add(modmaster);
            }
            else if (name == "ModbusTCP-Server")
		{
                       msi.initForm(); 
		               msi.Show();
					   ModbusWindow.Controls.Clear();
		               ModbusWindow.Controls.Add(msi);
		}
			
			else if(name == "ModbusRTU-Slave")
		{
		                modslave.initForm();
						modslave.Show(); 
						ModbusWindow.Controls.Clear(); 
						ModbusWindow.Controls.Add(modslave);
		}
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

            // writer.WriteStartObject();  //   {  （Json数据的大括号左边 ）
            //=====================================
            //第一级节点
            writer.WriteStartObject();
            writer.WritePropertyName("modbus");
            writer.WriteStartObject();
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
            writer.WriteEndObject();//}
            writer.WriteEndObject();//}
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
        }

        
    }
}
