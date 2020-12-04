using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalPLC.ModbusSlave
{
    public class DataSlave
    {
        //coil起始地址
        int coilStartAddr = 0;

        //decrete起始地址
        int decreteAddr = 0;

        //holding起始地址
        int holdingAddr = 0;

        //input起始地址
        int statusAddr = 0;

        public DataSlave(int curSlaveStartAddr)
        {
            //coilCount = 0;
            curSlaveStartAddr_ = curSlaveStartAddr;

            //线圈起始地址
            coilStartAddr = curSlaveStartAddr;
            decreteAddr = curSlaveStartAddr + utility.modbusMudule / 10 * 1;
            holdingAddr = curSlaveStartAddr + utility.modbusMudule / 10 * (1 + 1);
            statusAddr = curSlaveStartAddr + utility.modbusMudule / 10 * (1 + 1 + 4);
        }

        private int curSlaveStartAddr_ = 0;

        public int coilCount;
        public string coilModbusAddrStart = "";
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
        //该设备起始地址
        public int curSlaveStartAddr = 0;
        public int ID;
        //public DeviceData device { get; set; }
        public DataSlave dataDevice_  = null;
        public ModbusSlaveData()
        {
            dataDevice_ = new DataSlave(curSlaveStartAddr);
        }
    }
}




namespace LocalPLC.ModbusSlave
{
    public class DataManager
    {
        private static DataManager instance = null;
        public List<ModbusSlave.ModbusSlaveData> listSlave = new List<ModbusSlave.ModbusSlaveData>();
        public int slaveStartAddr = 0;

        //public ModbusSlave.DataSlave data_ = new ModbusSlave.DataSlave();
        private DataManager()
        {
            
        }


        public int getSlaveStartAddr()
        {
            int clientCount = UserControl1.mci.clientManage.modbusClientList.Count;
            int serverCount = UserControl1.msi.serverDataManager.listServer.Count;
            int masterCount = UserControl1.modmaster.masterManage.modbusMastrList.Count;

            slaveStartAddr = (clientCount + serverCount + masterCount) * utility.modbusMudule + utility.modbusAddr;

            //刷新list每项的首地址
            for(int i = 0; i < listSlave.Count; i++)
            {
                listSlave[i].curSlaveStartAddr = utility.modbusMudule * i + slaveStartAddr;
            }

            return slaveStartAddr;
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
