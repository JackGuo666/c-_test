using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ADELib;
using LocalPLC;
using System.IO;


namespace LocalPLC.Base.xml
{

    public class ClassParseBaseXml: Object
    {
        private XmlDocument xDoc = new XmlDocument();
        DataManageBase dataManage_ = null;
        public ClassParseBaseXml(string type, DataManageBase dataManage)
        {
            if(UserControl1.multiprogApp.ActiveProject == null)
            {
                return;
            }

            dataManage_ = dataManage;

            string path = UserControl1.multiprogApp.Path;
            string projectPath = UserControl1.multiprogApp.ActiveProject.Path;
            string projectName = UserControl1.multiprogApp.ActiveProject.Name;
            path +=  "\\LocalPLC\\LocalPLC586.xml";
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                //xmlFilePath:xml文件路径
                XmlReader reader = XmlReader.Create(path, settings);

                xDoc.Load(reader);

                //根节点
                XmlNode node = xDoc.SelectSingleNode("DeviceDescription");
                XmlNodeList nodeList = node.ChildNodes;
                foreach(XmlNode xn in nodeList)
                {
                    XmlElement elem = (XmlElement)xn;
                    //根节点下面分支
                    string name = xn.Name;
                    string temp = elem.GetAttribute("namespace");
                    if (name == "Types")
                    {
                         parseType(xn);
                    }
                }

            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message);
                return;
            }
        }
        //string projectPath = multiprogApp.ActiveProject.Path;
        //string projectName = multiprogApp.ActiveProject.Name;
        //string path = projectPath + "\\" + projectName + "\\" + "config_project.xml";

        private void parseType(XmlNode xNode)
        {
            XmlNodeList nodeList = xNode.ChildNodes;//创建xn的所有子节点的集合
            foreach(XmlNode xn in nodeList)
            {
                XmlElement elem = (XmlElement)xn;
                //根节点下面分支
                string name = xn.Name;
                
                if ("BitfieldType" == name)
                {
                    BitfieldType bitfieldType = new BitfieldType();
                    bitfieldType.tagName = elem.GetAttribute("name");
                    foreach(XmlNode xnChild in xn.ChildNodes)
                    {
                        XmlElement elemChild = (XmlElement)xnChild;
                        //根节点下面分支
                        string nameChild = xnChild.Name;
                        if(nameChild == "Component")
                        {
                            foreach(XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                XmlElement elemChild1 = (XmlElement)xnChild1;
                                
                                string nameChild1 = xnChild1.Name;
                                if(nameChild1 == "Default")
                                {

                                }
                                else if(nameChild1 == "VisibleName")
                                {
                                    BitfieldElem bitfieldElem = new BitfieldElem();
                                    bitfieldElem.name = elemChild1.GetAttribute("name");
                                    
                                    bitfieldType.list.Add(bitfieldElem);
                                }
                            }
                        }
                    }

                    dataManage_.dicBitfield.Add(bitfieldType.tagName, bitfieldType);
                }
                else if("EnumType" == name)
                {
                    EnumType enumType = new EnumType();
                    enumType.tagName = elem.GetAttribute("name");
                    foreach (XmlNode xnChild in xn.ChildNodes)
                    {
                        XmlElement elemChild = (XmlElement)xnChild;
                        //根节点下面分支
                        string nameChild = xnChild.Name;
                        if(nameChild == "Enum")
                        {
                            EnumElem enumElem = new EnumElem();
                            foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                XmlElement elemChild1 = (XmlElement)xnChild1;

                                string nameChild1 = xnChild1.Name;
                                if (nameChild1 == "Value")
                                {
                                    enumElem.value = elemChild1.InnerText;
                                }
                                else if (nameChild1 == "VisibleName")
                                {
                                    enumElem.name = xnChild1.InnerText;
                                }
                            }

                            enumType.list.Add(enumElem);
                        }
                    }
                    dataManage_.dicEnum.Add(enumType.tagName, enumType);
                }
                else if("StructType" == name)
                {
                    StructType structType = new StructType();
                    structType.tagName = elem.GetAttribute("name");
                    foreach (XmlNode xnChild in xn.ChildNodes)
                    {
                        XmlElement elemChild = (XmlElement)xnChild;
                        //根节点下面分支
                        string nameChild = xnChild.Name;
                        if (nameChild == "Component")
                        {
                            StructElem structElem = new StructElem();
                            foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                
                                structElem.type = elemChild.GetAttribute("type");
                                XmlElement elemChild1 = (XmlElement)xnChild1;

                                string nameChild1 = xnChild1.Name;
                                if (nameChild1 == "Default")
                                {
                                     int.TryParse(elemChild1.InnerText, out structElem.defaultValue);
                                }
                                else if (nameChild1 == "VisibleName")
                                {
                                    structElem.name = elemChild1.InnerText;
                                }
                                else if(nameChild1 == "Description")
                                {

                                }
                                else if(nameChild1 == "custom")
                                {

                                }

                            }


                            structType.list.Add(structElem);
                        }


                    }
                    dataManage_.dicStruct.Add(structType.tagName, structType);
                }
            }

        }

    }


}
