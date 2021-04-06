using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPLC.motion
{
    public class AxisBasePara
    {
        //轴基本参数
        public string axisName = "";
        public int axisType;
        public int meaUnit;
        public string hardwareInterface;
        public int signalType;
        public string pulseoutput = "";
        public string dirOutput = "";
    }

    public class PulseEquivalent
    {
        public UInt32 pulsePerRevolutionMotor = 1000;         //电机每转脉冲数
        public UInt32 offsetPerReolutionMotor = 1;          //电机每转的负载位移
    }

    public class LimitSignal
    {
        public bool hardLimitChecked = false;
        public string hardUpLimitInput = "";
        public int hardUpLimitInputLevel = 1;
        public string hardDownLimitInput = "";
       public int hardDownLimitInputLevel = 1;

        public bool softLimitChecked = false;
        public int softUpLimitInputOffset = 2147483647;
        public int softDownLimitOffset = -2147483648;
    }


    public class DynamicPara
    {
        //最大速度
        public int maxSpeed = 1;
        //加速度
        public int acceleratedSpeed = 1;
        //减速度
        public int decelerationSpeed = 1;
        //跃度Jerk
        public int jerk = 1;
        //急停减速度
        public int emeStopDeceleration = 1;
    }

    public class BackOriginal
    {
        //原点输入信号
        public string orginInputSignal = "";

        //选择电平
        public int selectLevel = 0;
        //Z脉冲信号
        public string ZPulseSignal = "";
    }

    public class ReverseCompensation
    {
        public int reverseCompensation = 1;
    }
    //反向间隙补偿


    public class AxisMotionPara
    {
        //脉冲当量
        public PulseEquivalent pulseEquivalent = new PulseEquivalent();
        public LimitSignal limitSignal = new LimitSignal();
        public DynamicPara dynamicPara = new DynamicPara();
        public BackOriginal backOriginal = new BackOriginal();
        public ReverseCompensation reverseCompensation = new ReverseCompensation();
    }

    public class Axis
    {
        public string name = "";
        public string key = "";
        public string axisKey = "";

        public AxisBasePara axisBasePara = new AxisBasePara();
        public AxisMotionPara axisMotionPara = new AxisMotionPara();
    }

    public class DataManageBase
    {
        //public AxisBasePara axisBasePara = new AxisBasePara();
        public DataManageBase()
        {
            axisList.Clear();
        }

        public void clear()
        {
            axisList.Clear();
        }

        public void delete(Axis axis)
        {
            for (int i = axisList.Count - 1; i >= 0; i--)
            {
                if (axisList[i] == axis)
                {
                    axisList.RemoveAt(i);
                }
            }
        }

        public List<Axis> axisList = new List<Axis>();

    }
}
