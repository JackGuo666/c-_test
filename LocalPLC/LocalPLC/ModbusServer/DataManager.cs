using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalPLC.ModbusServer
{
    public class DataServer
    {
        public DataServer()
        {
            //coilCount = 0;
        }

        public int coilCount = 0;
        public string coilIoAddrStart = "1";
        public string coilIoAddrEnd;
        public string coilIoVarNameIn;
        public string coilIoVarNameOut;
        public int iocoilstart;

        public int holdingCount = 0;
        public string holdingIoAddrStart = "40001";
        public string holdingIoAddrEnd;
        public string holdingVarNameIn;
        public string holdingVarNameOut;
        public int mholdingstart;

        public int decreteCount = 0;
        public string decreteIoAddrStart = "10001";
        public string decreteIoAddrEnd;
        public string decreteVarNameIn;
        public string decreteVarNameOut;
        public string mdiscretestart;

        public int statusCount = 0;
        public string statusIoAddrStart = "30001";
        public string statusIoAddrEnd;
        public string statusVarNameIn;
        public string statusVarNameOut;
        public string mstatusstart;

        public string IOAddrRange;
        public int IOAddrLength;
        public int shmrange = 0;
        public int shmlength;
        public int transformMode;  //0 TCP 1 UDP
        public int deviceAddr;
        public int transform;
        public int transformport = -1;
        public string transformportdescribe = "";
        public bool ipfixed = false;
        //public int connectnum = 1;
        public int slavetansformMode; //0 RTU 1ASCII
        public int port = 0;
        public int packet_interval = 0;
        public int maxconnectnumber = 1;
        public int ipconnect; //0 false 1true
        public bool isready = true; // 冲突检测，是否可以生成配置文件
        public int ip0=255, ip1=255 , ip2=255 , ip3=255;
        public int ip10=255, ip11=255, ip12=255, ip13=255;
        public int ip20=255, ip21=255, ip22=255, ip23=255;
        public int ip30=255, ip31=255, ip32=255, ip33=255;

    }

    public class ModbusServerData
    {
        public int ID;
        public int serverstartaddr;
        //public int TCPnum = 0;
        //public int Rtunum = 0;
        //public DeviceData device { get; set; }
        public DataServer dataDevice_ = new DataServer();
        public ModbusServerData()
        {

        }
    }
}

namespace LocalPLC.ModbusServer
{
    public class DataManager
    {
        private static DataManager instance = null;
        public int TCPnum = 0;
        public int Rtunum = 0;
        //public int serverstartaddr;
        public List<ModbusServer.ModbusServerData> listServer = new List<ModbusServer.ModbusServerData>();

        //public ModbusSlave.DataSlave data_ = new ModbusSlave.DataSlave();
        public DataManager()
        {

        }

        public static DataManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }
    }
}