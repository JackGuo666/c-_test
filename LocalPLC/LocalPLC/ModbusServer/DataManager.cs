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

        public int coilCount;
        public string coilIoAddrStart = "1";
        public string coilIoAddrEnd;
        public string coilIoVarNameIn;
        public string coilIoVarNameOut;
        public int iocoilstart;

        public int holdingCount;
        public string holdingIoAddrStart = "40001";
        public string holdingIoAddrEnd;
        public string holdingVarNameIn;
        public string holdingVarNameOut;
        public int mholdingstart;

        public int decreteCount;
        public string decreteIoAddrStart = "10001";
        public string decreteIoAddrEnd;
        public string decreteVarNameIn;
        public string decreteVarNameOut;
        public string mdiscretestart;

        public int statusCount;
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
        public int deviceAddr = 0;
        public int transform;
        public int transformport;
        public bool ipfixed = false;
        //public int connectnum = 1;
        public int slavetansformMode; //0 RTU 1ASCII
        public int port = 502;
        public int maxconnectnumber = 1;
        public int ipconnect; //0 false 1true
        public bool isready = true; // 冲突检测，是否可以生成配置文件
        public int ip0, ip1 , ip2 , ip3;
        public int ip10, ip11, ip12, ip13;
        public int ip20, ip21, ip22, ip23;
        public int ip30, ip31, ip32, ip33;

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