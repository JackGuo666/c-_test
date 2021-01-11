﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalPLC.Base.xml;

namespace LocalPLC.Base
{
    public partial class UserControlBase : UserControl
    {
        public List<UserControl> PlcTypeArr = new List<UserControl>();
        PlcType curPlcType = null;
        IWeapon curWeaponType = null;


        TreeView treeView_ = null;
        //DataManageBase数据管理
        public static DataManageBase dataManage = new DataManageBase();
        public UserControlBase() 
        {
            InitializeComponent();
        }

        public void getTreeView(TreeView view)
        {
            treeView_ = view;
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
            List<DeviceModuleElem> list = dataManage.deviceInfoElem.connector.moduleList;
            foreach(DeviceModuleElem elem in list)
            {
                if(elem.moduleID == "SERIAL_LINE")
                {
                    UserControlCom com = new UserControlCom(elem.baseName);
                    comDic.Add(elem.baseName, com);
                }
            }
        }
        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            object item = e.Data.GetData("Test");

            TreeNode node = (TreeNode)item;

            string localPLCType = node.Text.ToString();
            LocalPLC.Base.xml.ClassParseBaseXml ttt = new ClassParseBaseXml(localPLCType, dataManage);


            

            var topNode = treeView_.TopNode;
            var commNode = FindNode(topNode, "通信线路");
            //delSubNodes(commNode);
            addSerialNode(commNode);
            createSerialUserControl();

            topNode.Text = localPLCType;

            string tmp = string.Format("LocalPLC.Base.{0}", "PlcType");
            Type type = Type.GetType(/*"LocalPLC.Base.PlcType"*/ tmp);
            //object obj = type.Assembly.CreateInstance(type);
            UserControl user1 = (UserControl)Activator.CreateInstance(type, splitContainer2, this, dataManage);


            //PlcType user1 = new PlcType(splitContainer2, this, dataManage);
            curPlcType = (PlcType)user1;
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

        public Dictionary<string, UserControlCom> comDic = new Dictionary<string, UserControlCom>();
        public void setCOMShow(string com)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setCOMInfo(com);
            }

        }

        public void setETHShow(string eth)
        {
            if (curPlcType == null)
            {

            }
            else
            {
                curPlcType.setETHInfo(eth);
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
