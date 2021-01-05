using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LocalPLC.Base.xml
{
    public class BitfieldElem
    {
        public string name;
    }

    public class BitfieldType
    {
        public string tagName;

        public List<BitfieldElem> list= new List<BitfieldElem>();
    }


    public class DataManageBase
    {
        //DI DO
        public Dictionary<string, BitfieldType> dicBiffield = new Dictionary<string, BitfieldType>();
    }
}
