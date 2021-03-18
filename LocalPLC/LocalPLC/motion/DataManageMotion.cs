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
        int pulsePerRevolutionMotor = 1000;         //电机每转脉冲数
        int offsetPerReolutionMotor = 1;          //电机每转的负载位移
    }

    public class LimitSignal
    {
        bool hardLimitChecked = false;
        string hardUpLimitInput = "";
        int hardUpLimitInputLevel = 0;
        string hardDownLimitInput = "";
        int hardDownLimitInputLevel = 0;

        bool softLimitChecked = false;
        int softUpLimitInputOffset = 1;
        int softDownLimitOffset = 1;
    }


    public class DynamicPara
    {
        //最大速度
        int maxSpeed;
        //加速度
        int acceleratedSpeed;
        //减速度
        int decelerationSpeed;
        //跃度Jerk
        int jerk;
        //急停减速度
        int emeStopDeceleration;
    }

    public class BackOriginal
    {
        //原点输入信号
        string orginInputSignal = "";

        //选择电平
        int selectLevel = 0;
        //Z脉冲信号
        string ZPulseSignal;
    }

    public class ReverseCompensation
    {
        int reverseCompensation = 1;
    }
    //反向间隙补偿


    public class AxisMotionPara
    {
        //脉冲当量
    }

    public class Axis
    {
        public string name = "";

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

        public List<Axis> axisList = new List<Axis>();

    }
}
