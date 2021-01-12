﻿using System;
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

        public List<BitfieldElem> list= new List<BitfieldElem>();
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
        public int defaultValue;
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
    }

    public class DeviceModuleElem
    {
        public string baseName;
        public string moduleID;
    }
    public  class Connector
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
        public int filterTime = 3;
        public string channelName = "";
        public string address = "";
        public string note = "";
    }

    public class SERIALData
    {
        public string name = "";
        public int baud = 19200;
        public int Parity = 2;
        public int rsMode = 1;//Medium //232 0    485 1
        public int modPol = 0; 
        public int dataBit = 8;
        public int stopBit = 1;

        public int polR = 1;
    }

    public class ETHERNETData
    {

    }

    public class DataManageBase
    {
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
    }
}
