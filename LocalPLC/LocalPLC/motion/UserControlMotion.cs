using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LocalPLC.motion
{
    public class UserControlMotion
    {
        //运控参数数据管理
        public LocalPLC.motion.DataManageBase motionDataManage = new LocalPLC.motion.DataManageBase();


        #region
        ///function
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
                elemMotionPara.SetAttribute("harduplimitinputlevel", axis.axisMotionPara.limitSignal.hardDownLimitInputLevel.ToString());

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
        #endregion
    }
}
