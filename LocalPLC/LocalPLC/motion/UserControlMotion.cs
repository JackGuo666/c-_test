using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace LocalPLC.motion
{
    public class UserControlMotion
    {
        //运控参数数据管理
        public LocalPLC.motion.DataManageBase motionDataManage = new LocalPLC.motion.DataManageBase();
        TreeView treeView_ = null;
        public Control parent_ = null;
        public TreeNode axisNode_ = null;
        public UserControlMotionBasePara basePara = new UserControlMotionBasePara();

        public UserControlMotionPara motionPara = new UserControlMotionPara();

        #region
        ///function
        ///

        public UserControlMotion()
        {

        }


        public void clear()
        {
            motionDataManage.clear();
        }

        //清空树节点
        public void clearUI()
        {
            axisNode_.Nodes.Clear();
            
            //foreach(TreeNode node in axisNode_.Nodes)
            //{
            //    if(node.Tag.ToString() != "ADDAXIS")
            //    {
            //        axisNode_.Nodes.Remove(node);
            //    }
            //}
        }

        public void getTreeView(TreeView treeView)
        {
            treeView_ = treeView;
        }


        public void getTreeNode(TreeNode axisNode)
        {
            axisNode_ = axisNode;
        }

        public void getParent(UserControl parent)
        {
            parent_ = parent;
        }

        public void refreshNodeKey(List<LocalPLC.motion.Axis> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                list[i].key = string.Format("axis_{0}", i);
            }
        }
        public string addAxisName()
        {
            int count = motionDataManage.axisList.Count;
            string name = string.Format("轴{0}", count);
            bool flag = true;
            do
            {
                int interCount = 0;
                foreach (var axis in motionDataManage.axisList)
                {
                    if (axis.name == name)
                    {
                        count++;
                    }
                    else
                    {
                        interCount++;
                    }
                }



                if(interCount == motionDataManage.axisList.Count)
                {
                    flag = false;
                }
                else
                {
                    name = string.Format("轴{0}", count);
                }
            }
            while (flag);


            return name;
        }


        public void loadBaseData(XmlNode xn, Axis axis)
        {
            XmlNodeList childList = xn.ChildNodes;
            XmlElement childElement = (XmlElement)xn;
            foreach (XmlNode nChild in childList)
            {
                childElement = (XmlElement)nChild;
                string name = childElement.Name;
                if(name == "basepara")
                {
                    axis.axisBasePara.axisName = childElement.GetAttribute("axisname");
                    string temp = childElement.GetAttribute("axistype");
                    int.TryParse(temp, out axis.axisBasePara.axisType);
                    temp = childElement.GetAttribute("meaunit");
                    int.TryParse(temp, out axis.axisBasePara.meaUnit);
                    axis.axisBasePara.hardwareInterface = childElement.GetAttribute("hardwareinterface");

                    temp = childElement.GetAttribute("signaltype");
                    int.TryParse(temp, out axis.axisBasePara.signalType);


                    axis.axisBasePara.pulseoutput = childElement.GetAttribute("pulseoutput");
                    axis.axisBasePara.dirOutput = childElement.GetAttribute("diroutput");
                }
                else if(name == "motionpara")
                {
                    //脉冲当量
                    //电机每转脉冲数
                    string temp = childElement.GetAttribute("pulseperrevolutionmotor");
                    UInt32.TryParse(temp, out axis.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor);
                    //电机每转的负载位移
                    temp = childElement.GetAttribute("offsetperreolutionmotor");
                    UInt32.TryParse(temp, out axis.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor);

                    //限位信号
                    //启动硬限位
                    temp = childElement.GetAttribute("hardlimitchecked");
                    bool.TryParse(temp, out axis.axisMotionPara.limitSignal.hardLimitChecked);

                    axis.axisMotionPara.limitSignal.hardUpLimitInput = childElement.GetAttribute("harduplimitinput");

                    temp = childElement.GetAttribute("harduplimitinputlevel");
                    int.TryParse(temp, out axis.axisMotionPara.limitSignal.hardUpLimitInputLevel);

                    axis.axisMotionPara.limitSignal.hardDownLimitInput = childElement.GetAttribute("harddownlimitinput");
                    temp = childElement.GetAttribute("harddownlimitinputlevel");
                    int.TryParse(temp, out axis.axisMotionPara.limitSignal.hardDownLimitInputLevel);

                    //启动软限位
                    temp = childElement.GetAttribute("softlimitchecked");
                    bool.TryParse(temp, out axis.axisMotionPara.limitSignal.softLimitChecked);
                    //软件上限位位置
                    temp = childElement.GetAttribute("softuplimitinputoffset");
                    int.TryParse(temp, out axis.axisMotionPara.limitSignal.softUpLimitInputOffset);
                    //软件下限位位置
                    temp = childElement.GetAttribute("softdownlimitoffset");
                    int.TryParse(temp, out axis.axisMotionPara.limitSignal.softDownLimitOffset);

                    //动态参数
                    //最大速度
                    temp = childElement.GetAttribute("maxspeed");
                    UInt32.TryParse(temp, out axis.axisMotionPara.dynamicPara.maxSpeed);
                    //加速度
                    temp = childElement.GetAttribute("acceleratedspeed");
                    UInt32.TryParse(temp, out axis.axisMotionPara.dynamicPara.acceleratedSpeed);
                    //减速度
                    temp = childElement.GetAttribute("decelerationspeed");
                    UInt32.TryParse(temp, out axis.axisMotionPara.dynamicPara.decelerationSpeed);
                    //jerk
                    temp = childElement.GetAttribute("jerk");
                    UInt32.TryParse(temp, out axis.axisMotionPara.dynamicPara.jerk);
                    //急停减速度
                    temp = childElement.GetAttribute("emestopdeceleration");
                    UInt32.TryParse(temp, out axis.axisMotionPara.dynamicPara.emeStopDeceleration);





                    //回原点
                    axis.axisMotionPara.backOriginal.orginInputSignal = childElement.GetAttribute("orgininputsignal");
                    //选择电平
                    temp = childElement.GetAttribute("selectlevel");
                    int.TryParse(temp, out axis.axisMotionPara.backOriginal.selectLevel);
                    //Z脉冲信号
                    axis.axisMotionPara.backOriginal.ZPulseSignal = childElement.GetAttribute("zpulsesignal");

                    temp = childElement.GetAttribute("reversecompensation");
                    UInt32.TryParse(temp, out axis.axisMotionPara.reverseCompensation.reverseCompensation);
                }
            }
        }
        public void loadXml(XmlNode xn)
        {
            XmlNodeList childList = xn.ChildNodes;
            XmlElement childElement = (XmlElement)xn;
            var localPLCType = childElement.GetAttribute("Type");
            foreach (XmlNode nChild in childList)
            {
                XmlElement eChild = (XmlElement)nChild;
                string childname = eChild.Name;
                if(childname == "axis")
                {
                    childElement = (XmlElement)eChild;

                    foreach(XmlNode nChildAxis in eChild.ChildNodes)
                    {
                        XmlElement eChildAxis = (XmlElement)nChildAxis;
                        string name = eChildAxis.Name;


                        Axis axis = new Axis();
                        axis.name = eChildAxis.GetAttribute("name");
                        axis.key = eChildAxis.GetAttribute("key");
                        axis.axisKey = eChildAxis.GetAttribute("axiskey");
                        loadBaseData(nChildAxis, axis);

                        motionDataManage.axisList.Add(axis);
                    }
                }
            }
        }

        public void saveMotionXml(ref XmlElement elem, ref XmlDocument doc)
        {
            //运控
            XmlElement elemAxis = doc.CreateElement("axis");
            elemAxis.SetAttribute("name", "axis");
            elem.AppendChild(elemAxis);
            foreach (var axis in motionDataManage.axisList)
            {
                //axis元素
                XmlElement elemChild = doc.CreateElement("axiselem");
                elemChild.SetAttribute("name", axis.name);
                elemChild.SetAttribute("key", axis.key);
                //PTO轴
                elemChild.SetAttribute("axiskey", axis.axisKey);
                elemAxis.AppendChild(elemChild);

                //axis基本参数
                XmlElement elemChildPara = doc.CreateElement("basepara");
                //轴名称
                elemChildPara.SetAttribute("axisname", axis.axisBasePara.axisName);
                //轴类型
                elemChildPara.SetAttribute("axistype", axis.axisBasePara.axisType.ToString());
                //测量单位
                elemChildPara.SetAttribute("meaunit", axis.axisBasePara.meaUnit.ToString());
                //硬件接口
                elemChildPara.SetAttribute("hardwareinterface", axis.axisBasePara.hardwareInterface);
                //信号类型
                elemChildPara.SetAttribute("signaltype", axis.axisBasePara.signalType.ToString());
                //脉冲输出
                elemChildPara.SetAttribute("pulseoutput", axis.axisBasePara.pulseoutput);
                //方向输出
                elemChildPara.SetAttribute("diroutput", axis.axisBasePara.dirOutput);

                elemChild.AppendChild(elemChildPara);

                //axis运动参数
                XmlElement elemMotionPara = doc.CreateElement("motionpara");
                //电机每转脉冲数
                elemMotionPara.SetAttribute("pulseperrevolutionmotor", axis.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString());

                //电机每转的负载位移
                elemMotionPara.SetAttribute("offsetperreolutionmotor", axis.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString());

                //限位信号
                //启动硬限位
                elemMotionPara.SetAttribute("hardlimitchecked", axis.axisMotionPara.limitSignal.hardLimitChecked.ToString());
                //硬件上限位输入
                elemMotionPara.SetAttribute("harduplimitinput", axis.axisMotionPara.limitSignal.hardUpLimitInput);
                elemMotionPara.SetAttribute("harduplimitinputlevel", axis.axisMotionPara.limitSignal.hardUpLimitInputLevel.ToString());
                //硬件下限位输入
                elemMotionPara.SetAttribute("harddownlimitinput", axis.axisMotionPara.limitSignal.hardDownLimitInput);
                elemMotionPara.SetAttribute("harddownlimitinputlevel", axis.axisMotionPara.limitSignal.hardDownLimitInputLevel.ToString());

                //启动软限位
                elemMotionPara.SetAttribute("softlimitchecked", axis.axisMotionPara.limitSignal.softLimitChecked.ToString());
                //软件上限位位置
                elemMotionPara.SetAttribute("softuplimitinputoffset", axis.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString());
                //软件下限位位置
                elemMotionPara.SetAttribute("softdownlimitoffset", axis.axisMotionPara.limitSignal.softDownLimitOffset.ToString());

                //动态参数
                elemMotionPara.SetAttribute("maxspeed", axis.axisMotionPara.dynamicPara.maxSpeed.ToString());
                elemMotionPara.SetAttribute("acceleratedspeed", axis.axisMotionPara.dynamicPara.acceleratedSpeed.ToString());
                elemMotionPara.SetAttribute("decelerationspeed", axis.axisMotionPara.dynamicPara.decelerationSpeed.ToString());
                elemMotionPara.SetAttribute("jerk", axis.axisMotionPara.dynamicPara.jerk.ToString());
                elemMotionPara.SetAttribute("emestopdeceleration", axis.axisMotionPara.dynamicPara.emeStopDeceleration.ToString());

                //回原点
                elemMotionPara.SetAttribute("orgininputsignal", axis.axisMotionPara.backOriginal.orginInputSignal);

                elemMotionPara.SetAttribute("selectlevel", axis.axisMotionPara.backOriginal.selectLevel.ToString());
                elemMotionPara.SetAttribute("zpulsesignal", axis.axisMotionPara.backOriginal.ZPulseSignal.ToString());
                elemMotionPara.SetAttribute("reversecompensation", axis.axisMotionPara.reverseCompensation.reverseCompensation.ToString());

                elemChild.AppendChild(elemMotionPara);
            }
        }

        public void deleteAxisData(Axis axis)
        {
            motionDataManage.delete(axis);
        }

        void createNode(ref TreeNode retNode, string name, string tag, TreeNode parent, int index)
        {
            TreeNode basePara = new TreeNode(name, index, index);
            basePara.Tag = tag;
            retNode = basePara;
            parent.Nodes.Add(basePara);
        }

        public void createAxisTree()
        {
            createAddAxisNode();
            var list = motionDataManage.axisList;

            foreach(var axisElem in list)
            {
                TreeNode axisPara = new TreeNode(axisElem.axisBasePara.axisName, 1, 1);
                axisPara.Tag = axisElem;
                axisPara.Text = axisElem.name;
                axisNode_.Nodes.Add(axisPara);


                TreeNode basePara = new TreeNode("基本参数", 4, 4);
                basePara.Tag = "MOTION_BASE_PARA";
                axisPara.Nodes.Add(basePara);

                TreeNode motionMotionPara = new TreeNode("运动参数", 5, 5);
                motionMotionPara.Tag = "MOTION_MOTION_PARA";
                axisPara.Nodes.Add(motionMotionPara);

                //运控参数下的分支
                //脉冲当量
                TreeNode motionPulseEquivalent = null;
                TreeNode motionLimitSignal = null;
                TreeNode motionDynamicParameter = null;
                TreeNode motionBackOrigin = null;
                TreeNode motionReverseCompensation = null;
                createNode(ref motionPulseEquivalent, "脉冲当量", "MOTION_PULSE_EQUIVALENT", motionMotionPara, 6);
                createNode(ref motionLimitSignal, "限位信号", "MOTION_LIMIT_SIGNAL", motionMotionPara, 7);
                createNode(ref motionDynamicParameter, "动态参数", "MOTION_DYNAMIC_PARA", motionMotionPara, 8);
                createNode(ref motionBackOrigin, "回原点", "MOTION_BACK_ORIGIN", motionMotionPara, 9);
                createNode(ref motionReverseCompensation, "反向间隙补偿", "MOTION_REVERSE_COMPENSATION", motionMotionPara, 9);
            }

        }
        
        void createAddAxisNode()
        {
            //添加轴对象
            TreeNode addAxis = new TreeNode("添加轴对象", 2, 2);
            addAxis.Tag = "ADDAXIS";
            axisNode_.Nodes.Add(addAxis);
        }
        #endregion
    }
}
