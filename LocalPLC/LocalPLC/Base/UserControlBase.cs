using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.Base
{
    public partial class UserControlBase : UserControl
    {
        List<PlcType> PlcTypeArr = new List<PlcType>();
        PlcType curPlcType = null;

        public UserControlBase()
        {
            InitializeComponent();
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


        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            object item = e.Data.GetData("Test");

            TreeNode node = (TreeNode)item;
            
            PlcType user1 = new PlcType(splitContainer2, this);
            curPlcType = user1;
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
                curPlcType.setDOInfo(name);
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
                curPlcType.setDIInfo(name);
            }
        }

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
    }
}
