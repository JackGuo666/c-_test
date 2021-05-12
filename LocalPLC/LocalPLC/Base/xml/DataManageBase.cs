using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LocalPLC.Base.xml
{
    #region
    public class BitfieldElem
    {
        public string name;

    }

    public class BitfieldType
    {
        public string tagName;

        public List<BitfieldElem> list = new List<BitfieldElem>();
    }
    #endregion


    #region
    public class EnumElem
    {
        public string name;
        public string value;
    }

    public class EnumType
    {
        public string tagName;

        public List<EnumElem> list = new List<EnumElem>();
    }
    #endregion

    #region


    public class StructElem
    {
        public string name;
        public string value;
        public /*int*/ string defaultValue;
        public string type;
    }


    public class StructType
    {
        public string tagName;

        public List<StructElem> list = new List<StructElem>();
    }
    #endregion

    #region device
    public class DeviceIdentificationElem
    {
        public string type;
        public string ID;
        public string version;


        //IO address
        public string ioAddrStart = "";
        public string ioAddrEnd = "";
    }

    public class DeviceModuleElem
    {
        public string baseName;
        //串口特别字段
        public string terminalResis = "2";
        public string databitEnable = "1";
        public string moduleID;
    }
    public class Connector
    {
        public void clear()
        {
            moduleList.Clear();
        }

        public string connectorId;

        public List<DeviceModuleElem> moduleList = new List<DeviceModuleElem>();
    }
    public class DeviceInfoElem
    {
        public void clear()
        {
            connector.clear();
        }

        public string vendorId;

        public string name;
        public string desc;
        public string vendor;
        public string defaultInstanceName;


        public DeviceIdentificationElem deviceIdentificationElem = new DeviceIdentificationElem();

        public Connector connector = new Connector();
    }


    #endregion

    #region
    public class DeviceInfoModules
    {
        public string name;
        public string desc;
        public string vendor;
    }

    public class Parameter
    {
        public string name;
        public string paraID;
        public string type;
        public string parameterName;

        public string defaultValue = "";
    }

    public class ConnectorModules
    {
        public string connectorID;
        //public string name;
        //public string paraID;
        //public string type;
        //public string parameterName;
        public List<Parameter> list = new List<Parameter>();
    }



    public class ModuleElemModules
    {
        public string moduleID;
        public DeviceInfoModules deviceInfoModules = new DeviceInfoModules();
        public ConnectorModules connectModules = new ConnectorModules();
    }

    public class Modules
    {
        public void clear()
        {
            list.Clear();
        }
        public List<ModuleElemModules> list = new List<ModuleElemModules>();
    }

    #endregion

    public class DOData
    {
        public bool used = false;
        public string varName = "";
        public string channelName = "";
        public string address = "";
        public string note = "";
    }


    public class DIData
    {
        public bool used = false;
        public string varName = "";
        public  uint filterTime = 3;
        public string channelName = "";
        public string address = "";
        public string note = "";
        public string hscUsed = "";
    }

    public class SERIALData
    {
        public string name = "";
        public int baud = 19200;
        public int Parity = 2;
        public int rsMode = 1;//Medium //232 0    485 1
        //public int modPol = 0; 
        public int dataBit = 8;
        public int stopBit = 1;

        public int polR = 1;

        //终端电阻
        public string terminalResis = "2";
        //数据位是否eanble
        public string databitEnable = "1";
    }

    public class ETHERNETData
    {
        public string name = "";    //网口名
        public int ipMode = 0;          //0-固定IP地址  1-DHCP分配IP地址
        public string ipAddress = "0.0.0.0";
        public string maskAddress = "0.0.0.0";
        public string gatewayAddress = "0.0.0.0";

        //SNTP服务器
        public int checkSNTP = 0;
        public string sntpServerIp = "0.0.0.0";
    }

    public class HSPConfigData
    {

    }

    public class HSCData
    {
        public bool used = false;
        public string name = "";
        public string address = "";
        public int type = 0;    //0-未配置 1-单相 2-双相 3-频率计
        //输入模式
        public int inputMode = 0;   //0-脉冲方向 1-积分X1 2-积分X2 2-积分X4

        public string opr_mode = "";

        public bool doubleWord = false;
        //预设
        public int preset = 0;
        //阈值
        public int thresholdS0 = 1; 
        public int thresholdS1 = 2;
        //事件名
        public string eventName0 = "";
        public string eventName1 = "";
        //事件ID
        public string eventID0 = "EVENT_1";
        public string eventID1 = "EVENT_2";

        //触发器
        public int trigger0 = 0;    //0-未使用 1-下降沿 2-上升沿 3-上升/下降沿
        public int trigger1 = 0;

        //脉冲输入
        public bool pulseChecked = true;
        public bool dirChecked = true;
        public bool presetChecked = false;
        public bool captureChecked = false;

        public string pulsePort = "";
        public string dirPort = "";
        public string presetPort = "";
        public string capturePort = "";

        //频率计
        public bool pulseFrequencyChecked = true;
        public string pulseFrequencyInputPort = "";


        //时间窗口
        public int timeWindow = 0;  //0-100ms 1-1s 2 - none
        public bool frequencyDoubleWord = false;

        public string note = "";
    }

    public class HSPData
    {
        public bool used = false;
        public string name = "";
        public string address = "";

        //config
        //public int pulseType = 0; //
        public string pulsePort = ""; //脉冲
        public string directionPort = "";
        public int timeBase = 3;    //0-0.1毫秒 1-1毫秒 2-10毫秒 3-1秒
        public int preset = 1;

        public int signalFrequency = 1000;
        public bool doubleWord = false;

        //PTO
        public int outputMode = 0;         //0-PULSE_DIC 1-CW/CCW 2-AB_DIRECTION

        public int type = 0;    //0-未配置 1-PLS 2-PWM 3-Frequency 4-PTO

        public string note = "";
    }

    public class DataManageBase
    {
        public bool newControlerFlag = false;

        public void clear()
        {
            dicBitfield.Clear();
            dicEnum.Clear();
            dicStruct.Clear();
            deviceInfoElem.clear();
            modules.clear();
            doList.Clear();
            diList.Clear();
            serialDic.Clear();
            ethernetDic.Clear();

            hscList.Clear();
            hspList.Clear();
        }

        //DI DO
        public Dictionary<string, BitfieldType> dicBitfield = new Dictionary<string, BitfieldType>();

        //字段里的内容
        public Dictionary<string, EnumType> dicEnum = new Dictionary<string, EnumType>();

        //struct
        public Dictionary<string, StructType> dicStruct = new Dictionary<string, StructType>();

        public DeviceInfoElem deviceInfoElem = new DeviceInfoElem();

        //
        public Modules modules = new Modules();



        //do数据结构
        public List<DOData> doList = new List<DOData>();

        //di数据结构
        public List<DIData> diList = new List<DIData>();

        public Dictionary<string, SERIALData> serialDic = new Dictionary<string, SERIALData>();
        public Dictionary<string, ETHERNETData> ethernetDic = new Dictionary<string, ETHERNETData>();

        public List<HSPData> hspList = new List<HSPData>();
        public List<HSCData> hscList = new List<HSCData>();



        public int getNotUsedDiCount()
        {
            int count = 0;
            foreach(var di in diList)
            {
                if(!di.used)
                {
                    count++;
                }
            }

            return count;
        }


        public int getUsedHscCount()
        {
            int count = 0;
            foreach(var hsc in hscList)
            {
                if(hsc.used)
                {
                    count++;
                }
            }

            return count;
        }

        public DOData getDoData(string channelName)
        {
            foreach (var dout in doList)
            {
                if (channelName == dout.channelName)
                {
                    return dout;
                }
            }

            return null;
        }

        public bool setDoutUsed(string channelName, bool used)
        {
            bool ret = false;
            foreach (var dout in doList)
            {
                if (channelName == dout.channelName)
                {
                    dout.used = used;
                    ret = true;
                    return ret;
                }
            }

            return ret;
        }

        public bool checkVarNameDI(string name, string channel)
        {
            foreach (var di in diList)
            {
                if (channel != di.channelName)
                {
                    if (di.varName == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool checkVarNameDO(string name, string channel)
        {
            foreach (var dout in doList)
            {
                if (channel != dout.channelName)
                {
                    if (dout.varName == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }



        public string getHscVarInitValue(HSCData hsc, int axis)
        {
            int timeBase = 0;
            if (hsc.timeWindow == 0)
            {
                timeBase = 100;
            }
            else if (hsc.timeWindow == 1)
            {
                timeBase = 1000;
            }
            else if (hsc.timeWindow == 2)
            {
                timeBase = 0;
            }

            string initValue = string.Format("( axis_no := {0}, preset_val := {1}, threshold0 := {2}, threshold1 := {3}, time_base := {4} )",
                                            axis, hsc.preset, hsc.thresholdS0, hsc.thresholdS1, timeBase);
            return initValue;
        }
    }
}
