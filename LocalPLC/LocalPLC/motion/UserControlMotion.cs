using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LocalPLC.motion
{
    public class UserControlMotion
    {
        //运控参数数据管理
        public LocalPLC.motion.DataManageBase motionDataManage = new LocalPLC.motion.DataManageBase();
        TreeView treeView_ = null;
        public Control parent_ = null;
        public TreeNode axisNode_ = null;
        public TreeNode commandNode_ = null;
        public UserControlMotionBasePara basePara = new UserControlMotionBasePara();

        public UserControlMotionPara motionPara = new UserControlMotionPara();

        public UserControlCommandTable commandPara = new UserControlCommandTable();
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
            commandNode_.Nodes.Clear();
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


        public void getAxisTreeNode(TreeNode axisNode)
        {
            axisNode_ = axisNode;
        }

        public void getCommandTreeNode(TreeNode commandNode)
        {
            commandNode_ = commandNode;
        }

        public void getParent(UserControl parent)
        {
            parent_ = parent;
        }

        public void refreshAxisNodeKey(List<LocalPLC.motion.Axis> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                list[i].key = string.Format("axis_{0}", i);
            }
        }

        public void refreshCommandNodeKey(List<LocalPLC.motion.CommandTable> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].commandKey = string.Format("command_{0}", i);
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

        public string addCommandName()
        {
            int count = motionDataManage.commandTableList.Count;
            string name = string.Format("命令表{0}", count);
            bool flag = true;

            do
            {
                int interCount = 0;
                foreach(var command in motionDataManage.commandTableList)
                {
                    if (command.name == name)
                    {
                        count++;
                    }
                    else
                    {
                        interCount++;
                    }
                }

                if (interCount == motionDataManage.commandTableList.Count)
                {
                    flag = false;
                }
                else
                {
                    name = string.Format("命令表{0}", count);
                }
            }
            while(flag);

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
                else if(childname == "commandtable")
                {
                    childElement = (XmlElement)eChild;
                    foreach (XmlNode nChildCommand in eChild.ChildNodes)
                    {
                        XmlElement eChildCommand = (XmlElement)nChildCommand;


                        CommandTable command = new CommandTable();
                        command.clear();

                        command.name = eChildCommand.GetAttribute("name");
                        command.commandKey = eChildCommand.GetAttribute("key");
                        //command.step
                        foreach(XmlNode nChildElem in eChildCommand)
                        {
                            XmlElement eChildElem = (XmlElement)nChildElem;
                            Step step = new Step();
                            step.step = eChildElem.GetAttribute("step");
                            step.type = eChildElem.GetAttribute("type");
                            step.pos = eChildElem.GetAttribute("pos");
                            step.dis = eChildElem.GetAttribute("dis");
                            step.speed = eChildElem.GetAttribute("speed");
                            step.acc = eChildElem.GetAttribute("acc");
                            step.dec = eChildElem.GetAttribute("dec");
                            step.nextStep = eChildElem.GetAttribute("nextstep");
                            step.jerk = eChildElem.GetAttribute("jerk");
                            step.eventVar = eChildElem.GetAttribute("eventvar");
                            step.delay = eChildElem.GetAttribute("delay");
                            step.note = eChildElem.GetAttribute("note");

                            command.stepList.Add(step);
                        }

                        motionDataManage.commandTableList.Add(command);
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



            XmlElement elemCommandTable = doc.CreateElement("commandtable");
            elem.AppendChild(elemCommandTable);
            //utility.PrintInfo(motionDataManage.commandTableList.Count.ToString());
            foreach(var command in motionDataManage.commandTableList)
            {
                XmlElement elemChild = doc.CreateElement("commandtableelem");
                elemChild.SetAttribute("name", command.name);
                elemChild.SetAttribute("key", command.commandKey);
                foreach(var step in command.stepList)
                {
                    XmlElement elemCommand = doc.CreateElement("elem");

                    elemCommand.SetAttribute("step", step.step);
                    elemCommand.SetAttribute("type", step.type);
                    elemCommand.SetAttribute("pos", step.pos);
                    elemCommand.SetAttribute("dis", step.dis);
                    elemCommand.SetAttribute("speed", step.speed);
                    elemCommand.SetAttribute("acc", step.acc);
                    elemCommand.SetAttribute("dec", step.dec);
                    elemCommand.SetAttribute("nextstep", step.nextStep);
                    elemCommand.SetAttribute("jerk", step.jerk);
                    elemCommand.SetAttribute("eventvar", step.eventVar);
                    elemCommand.SetAttribute("delay", step.delay);
                    elemCommand.SetAttribute("note", step.note);

                    elemChild.AppendChild(elemCommand);

                }

                elemCommandTable.AppendChild(elemChild);
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



        public void createCommandTableTree()
        {
            createAddCommandNode();

            var list = motionDataManage.commandTableList;
            foreach(var command in list)
            {
                TreeNode commandPara = new TreeNode(command.name, 3, 3);
                commandPara.Tag = command;
                commandNode_.Nodes.Add(commandPara);
            }
        }


        void createAddCommandNode()
        {
            //添加命令表对象
            TreeNode addCommandTable = new TreeNode("添加命令表对象", 2, 2);
            addCommandTable.Tag = "ADDCOMMANDTABLE";
            commandNode_.Nodes.Add(addCommandTable);
        }
        #endregion


        # region json
        public void saveJson(JsonTextWriter writer)
        {
            if(motionDataManage.axisList.Count > 0)
            {
                writer.WritePropertyName("motion");
                writer.WriteStartObject();//添加{  节点

                writer.WritePropertyName("grp_total");
                writer.WriteValue(motionDataManage.axisList.Count);


                writer.WritePropertyName("axis_conf");
                writer.WriteStartArray(); //[ 数组


                int i = 0;
                foreach (var axis in motionDataManage.axisList)
                {
                    //============开始============
                    writer.WriteStartObject();//添加{  节点
                    writer.WritePropertyName("axis_id");
                    writer.WriteValue(i); 
                    writer.WritePropertyName("axis_object");
                    writer.WriteValue(axis.axisBasePara.hardwareInterface.ToLower());
                    writer.WritePropertyName("axis_name");
                    writer.WriteValue(axis.name);



                    writer.WritePropertyName("basic_para_cfg");
                    writer.WriteStartObject();//添加{  节点
                    /* 轴类型:0,总线轴; 1,虚拟轴; 2,脉冲轴 */
                    writer.WritePropertyName("axis_type");
                    writer.WriteValue(axis.axisBasePara.axisType);
                    /* 组合脉冲输出组号，对应实际的硬件资源   有问题?*/
                    writer.WritePropertyName("pluse_grp_no");
                    writer.WriteValue(i);
                    /* 测量单位:0, mm; 1, °; 2, plus */
                    writer.WritePropertyName("measure_unit");
                    writer.WriteValue(axis.axisBasePara.meaUnit);
                    /*循环周期*/
                    writer.WritePropertyName("cycle_time");
                    writer.WriteValue(1);
                    /*单元名字*/
                    writer.WritePropertyName("unit_name");
                    writer.WriteValue("tbd");
                    /* 电机逻辑位置指令模值*/
                    writer.WritePropertyName("pos_logic_modval");
                    writer.WriteValue(0);
                    /* 激活轴：true, 激活轴； false, 不激活轴*/
                    writer.WritePropertyName("axis_enable");
                    writer.WriteValue(true);
                    /* 轴运行模式：0, FIFO轴； 1: 实时轴*/
                    writer.WritePropertyName("axis_mode");
                    writer.WriteValue(0);
                    /* 轴表格补偿模式使能： 0，不使能；非0值，使能, 且为补偿用的表格ID号*/
                    writer.WritePropertyName("table_com");
                    writer.WriteValue(0);
                    /*轴输入信号类型：1，从本地IO上读取输入信息；2.从伺服的pdo中读取信息；3从总线上挂载的IO模块上读取输入信息 */
                    writer.WritePropertyName("axisInput_type");
                    writer.WriteValue(0);
                    /*轴正零位输入信号索引 目前正负零位信号实际是一个*/
                    writer.WritePropertyName("pos_home_input_idx");
                    writer.WriteValue("DI00");
                    /*轴负零位输入信号索引 目前正负零位信号实际是一个*/
                    writer.WritePropertyName("neg_home_input_idx");
                    writer.WriteValue("DI00");
                    /*轴正限位输入信号索引 */
                    writer.WritePropertyName("pos_limit_input_idx");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.hardUpLimitInput);
                    /*轴负限位输入信号索引 */
                    writer.WritePropertyName("neg_limit_input_idx");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.hardDownLimitInput);

                    /*轴的Z脉冲索引 */
                    writer.WritePropertyName("z_pluse_idx");
                    writer.WriteValue(axis.axisMotionPara.backOriginal.orginInputSignal);
                    /* 硬件限位使能*/
                    writer.WritePropertyName("hard_limit_enable");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.hardLimitChecked);
                    /* 软件限位使能*/
                    writer.WritePropertyName("soft_limit_enable");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.softLimitChecked);
                    /* 原点输入信号使能*/
                    writer.WritePropertyName("zero_signal_enable");
                    writer.WriteValue(false);
                    ///* 检测伺服报警信号使能*/
                    writer.WritePropertyName("servo_alarm_signal_enable");
                    writer.WriteValue(false);
                    /* 电机极性*/
                    writer.WritePropertyName("reverse_dir");
                    writer.WriteValue(0);
                    /* 轴正向限位信号极性*/
                    writer.WritePropertyName("pos_limit_signal_lev");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.hardUpLimitInputLevel);
                    /* 轴负向限位信号极性*/
                    writer.WritePropertyName("neg_limit_signal_lev");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.hardDownLimitInputLevel);
                    /* 正零位信号极性*/
                    writer.WritePropertyName("pos_zero_signal_lev");
                    writer.WriteValue(0);
                    /* 负零位信号极性*/
                    writer.WritePropertyName("neg_zero_signal_lev");
                    writer.WriteValue(0);
                    /* 调试信息输出*/
                    writer.WritePropertyName("debug_out");
                    writer.WriteValue(false);
                    /* 错误信息输出*/
                    writer.WritePropertyName("error_out");
                    writer.WriteValue(false);

                    writer.WriteEndObject();//添加}  节点


                    //kinematic_para_cfg
                    writer.WritePropertyName("kinematic_para_cfg");
                    writer.WriteStartObject();//添加{  节点

                    /*脉冲当量*/
                    writer.WritePropertyName("pulse_equivalent");
                    writer.WriteValue(axis.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor);
                    /*电机每转的负载位移*/
                    writer.WritePropertyName("displace_per_rev");
                    writer.WriteValue(axis.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor);
                    /* 最大速度*/
                    writer.WritePropertyName("velo_maximum");
                    writer.WriteValue(axis.axisMotionPara.dynamicPara.maxSpeed);
                    /* 高速*/
                    writer.WritePropertyName("velol_fast");
                    writer.WriteValue(0);
                    /*加速度*/
                    writer.WritePropertyName("ace");
                    writer.WriteValue(axis.axisMotionPara.dynamicPara.acceleratedSpeed);
                    /*减速度*/
                    writer.WritePropertyName("dec");
                    writer.WriteValue(axis.axisMotionPara.dynamicPara.decelerationSpeed);
                    /*跃度*/
                    writer.WritePropertyName("jerk");
                    writer.WriteValue(axis.axisMotionPara.dynamicPara.jerk);
                    /* 急停减速度*/
                    writer.WritePropertyName("estop_dec");
                    writer.WriteValue(axis.axisMotionPara.dynamicPara.emeStopDeceleration);
                    /* 反向间隙补偿*/
                    writer.WritePropertyName("backlash_compensation");
                    writer.WriteValue(axis.axisMotionPara.reverseCompensation.reverseCompensation);
                    /* 电机控制时间*/
                    writer.WritePropertyName("mot_cntrl_time");
                    writer.WriteValue(0);
                    /* 正向软限位*/
                    writer.WritePropertyName("puls_dist_pos");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.softUpLimitInputOffset);
                    /* 负向软限位*/
                    writer.WritePropertyName("puls_dist_neg");
                    writer.WriteValue(axis.axisMotionPara.limitSignal.softDownLimitOffset);
                    /*延时*/
                    writer.WritePropertyName("delay");
                    writer.WriteValue(0);
                    /* 回零过程中寻找零位参考点的速度*/
                    writer.WritePropertyName("velo_ref_search");
                    writer.WriteValue(0);
                    /* 回零过程中寻找同步脉冲的速度*/
                    writer.WritePropertyName("velo_sync_search");
                    writer.WriteValue(0);
                    /* JOG模式中的低速度 */
                    writer.WritePropertyName("velo_slow_manual");
                    writer.WriteValue(0);
                    /* JOG模式中的高速度 */
                    writer.WritePropertyName("velol_fast_manual");
                    writer.WriteValue(0);
                    /*保留*/
                    writer.WritePropertyName("override_type");
                    writer.WriteValue("tbd");
                    /*保留*/
                    writer.WritePropertyName("velo_jump_factor");
                    writer.WriteValue(0);
                    /*保留*/
                    writer.WritePropertyName("reduction_feedback");
                    writer.WriteValue("tbd");
                    /*保留*/
                    writer.WritePropertyName("tolerance_ball_auxaxis");
                    writer.WriteValue("0");
                    /*保留*/
                    writer.WritePropertyName("max_pos_deviation_auxaxis");
                    writer.WriteValue(0);
                    /*保留*/
                    writer.WritePropertyName("fast_acc");
                    writer.WriteValue(0);
                    /*保留*/
                    writer.WritePropertyName("fast_dcc");
                    writer.WriteValue(0);
                    /*保留*/
                    writer.WritePropertyName("fast_jerk");
                    writer.WriteValue(0);

                    writer.WriteEndObject();//添加}  节点



                    //============结束============
                    writer.WriteEndObject();//添加}  节点


                    i++;
                }

                writer.WriteEndArray(); //] 数组


                writer.WriteEndObject(); // }
            }


        }
        #endregion
    }
}
