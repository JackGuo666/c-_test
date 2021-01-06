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

    public class ModuleElem
    {
        public string baseName;
        public string moduleID;
    }
    public  class Connector
    {
        public string connectorId;

        public List<ModuleElem> moduleList = new List<ModuleElem>();
    }
    public class DeviceInfoElem
    {
        public string vendorId;

        public string name;
        public string desc;
        public string vendor;
        public string defaultInstanceName;

        public Connector connector = new Connector();
    }


    #endregion

    public class DataManageBase
    {
        //DI DO
        public Dictionary<string, BitfieldType> dicBitfield = new Dictionary<string, BitfieldType>();

        //字段里的内容
        public Dictionary<string, EnumType> dicEnum = new Dictionary<string, EnumType>();

        //struct
        public Dictionary<string, StructType> dicStruct = new Dictionary<string, StructType>();
    }
}
