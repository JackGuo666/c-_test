using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalPLC.ModbusSlave
{
    public class DataSlave
    {
        public DataSlave()
        {
            //coilCount = 0;
        }

        public int coilCount;
        public string coilIoAddrStart = "";
        public string coilIoAddrEnd = "";
        public string coilIoVarNameIn;
        public string coilIoVarNameOut;


        public int holdingCount;
        public string holdingIoAddrStart = "";
        public string holdingIoAddrEnd = "";
        public string holdingVarNameIn;
        public string holdingVarNameOut;

        public int decreteCount;
        public string decreteIoAddrStart = "";
        public string decreteIoAddrEnd = "";
        public string decreteVarNameIn;
        public string decreteVarNameOut;

        public int statusCount;
        public string statusIoAddrStart = "";
        public string statusIoAddrEnd = "";
        public string statusVarNameIn;
        public string statusVarNameOut;

        public string IOAddrRange;
        public int IOAddrLength;

        public int transformMode;  //0 RTU 1 ASCII
        public int deviceAddr;
    }



    public class ModbusSlaveData
    {

        public int ID;
        //public DeviceData device { get; set; }
        public DataSlave dataDevice_  = new DataSlave();
        public ModbusSlaveData()
        {

        }
    }
}




namespace LocalPLC.ModbusSlave
{
    public class DataManager
    {
        private static DataManager instance = null;
        public List<ModbusSlave.ModbusSlaveData> listSlave = new List<ModbusSlave.ModbusSlaveData>();

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
