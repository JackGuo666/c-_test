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
        public string coilIoAddrStart = "";
        public string coilIoAddrEnd;
        public string coilIoVarNameIn;
        public string coilIoVarNameOut;
        public string mcoilstart;

        public int holdingCount;
        public string holdingIoAddrStart;
        public string holdingIoAddrEnd;
        public string holdingVarNameIn;
        public string holdingVarNameOut;
        public string mholdingstart;

        public int decreteCount;
        public string decreteIoAddrStart;
        public string decreteIoAddrEnd;
        public string decreteVarNameIn;
        public string decreteVarNameOut;
        public string mdiscretestart;

        public int statusCount;
        public string statusIoAddrStart;
        public string statusIoAddrEnd;
        public string statusVarNameIn;
        public string statusVarNameOut;
        public string mstatusstart;

        public string IOAddrRange;
        public int IOAddrLength;

        public int transformMode;  //0 TCP 1 UDP
        public int deviceAddr;

        public int port = 502;
        public int maxconnectnumber;
        public int ipconnect; //0 false 1true
        public int ip0, ip1, ip2, ip3;
    }

    public class ModbusServerData
    {
        public int ID;
        public int serverstartaddr;
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
        //public int serverstartaddr;
        public List<ModbusServer.ModbusServerData> listServer = new List<ModbusServer.ModbusServerData>();

        //public ModbusSlave.DataSlave data_ = new ModbusSlave.DataSlave();
        private DataManager()
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