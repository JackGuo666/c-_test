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
            if(UserControl1.multiprogApp.IsProjectOpen() == false)
            {
                return;
            }

            dataManage_ = dataManage;
            //清除之前加载的数据
            dataManage_.clear();


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
                    else if(name == "Strings")
                    {

                    }
                    else if(name == "Files")
                    {

                    }
                    else if(name == "Device")
                    {
                        parseDevice(xn);
                    }
                    else if(name == "Modules")
                    {
                        parseModules(xn);
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

        private void parseDevice(XmlNode xNode)
        {
            XmlNodeList nodeList = xNode.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode xn in nodeList)
            {
                XmlElement elem = (XmlElement)xn;
                //根节点下面分支
                string name = xn.Name;

                if ("DeviceIdentification" == name)
                {
                    DeviceIdentificationElem deviceIdentificationElem = dataManage_.deviceInfoElem.deviceIdentificationElem;
                    foreach (XmlNode xnChild in xn.ChildNodes)
                    {
                        XmlElement elemChild = (XmlElement)xnChild;
                        //根节点下面分支
                        string nameChild = xnChild.Name;
                        if(nameChild == "Type")
                        {
                            deviceIdentificationElem.type = elemChild.InnerText;
                        }
                        else if(nameChild == "Id")
                        {
                            deviceIdentificationElem.ID = elemChild.InnerText;
                        }
                        else if(nameChild == "Version")
                        {
                            deviceIdentificationElem.version = elemChild.InnerText;
                        }
                    }
                }
                else if("DeviceInfo" == name)
                {
                    DeviceInfoElem devicInfoElem = dataManage_.deviceInfoElem;
                    foreach (XmlNode xnChild in xn.ChildNodes)
                    {
                        XmlElement elemChild = (XmlElement)xnChild;
                        //根节点下面分支
                        string nameChild = xnChild.Name;

                        if (nameChild == "Name")
                        {
                            devicInfoElem.name = elemChild.InnerText;
                        }
                        else if(nameChild == "Description")
                        {
                            devicInfoElem.desc = elemChild.InnerText;
                        }
                        else if(nameChild == "DefaultInstanceName")
                        {
                            devicInfoElem.defaultInstanceName = elemChild.InnerText;
                        }
                        else if(nameChild == "Vendor")
                        {
                            devicInfoElem.vendor = elemChild.InnerText;
                        }
                        else if(nameChild == "Connector")
                        {
                            //connector链表
                            devicInfoElem.connector.connectorId = elemChild.GetAttribute("connectorId");

                            foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                XmlElement elemChild1 = (XmlElement)xnChild1;

                                string nameChild1 = xnChild1.Name;

                                if (nameChild1 == "Module")
                                {
                                    DeviceModuleElem moduleElem = new DeviceModuleElem();
                                    moduleElem.baseName = elemChild1.GetAttribute("basename");
                                    foreach (XmlNode xnChild11 in xnChild1.ChildNodes)
                                    {
                                        XmlElement elemChild11 = (XmlElement)xnChild11;
                                        string nameChild11 = xnChild11.Name;
                                        if(nameChild11 == "LocalModuleId")
                                        {
                                            moduleElem.moduleID = elemChild11.InnerText;
                                            devicInfoElem.connector.moduleList.Add(moduleElem);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void parseModules(XmlNode xNode)
        {
            XmlNodeList nodeList = xNode.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode xn in nodeList)
            {
                XmlElement elem = (XmlElement)xn;
                string name = xn.Name;
                if(name == "Module")
                {
                    ModuleElemModules moduleElemModules = new ModuleElemModules();
                    foreach (XmlNode xnChild in xn.ChildNodes)
                    {
                        XmlElement elemChild = (XmlElement)xnChild;
                        string nameChild = xnChild.Name;
                        if(nameChild == "ModuleId")
                        {
                            moduleElemModules.moduleID = elemChild.InnerText;
                        }
                        else if(nameChild == "DeviceInfo")
                        {
                            foreach(XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                XmlElement elemChild1 = (XmlElement)xnChild1;
                                string nameChild1 = xnChild1.Name;
                                if(nameChild1 == "Name")
                                {
                                    moduleElemModules.deviceInfoModules.name = elemChild1.InnerText; ;
                                }
                                else if(nameChild1 == "Description")
                                {
                                    moduleElemModules.deviceInfoModules.desc = elemChild1.InnerText;
                                }
                                else if(nameChild1 == "Vendor")
                                {
                                    moduleElemModules.deviceInfoModules.vendor = elemChild1.InnerText;
                                }
                            }
                        }
                        else if(nameChild == "Connector")
                        {
                            moduleElemModules.connectModules.connectorID = elemChild.GetAttribute("connectorId");
                            Parameter parameter = new Parameter();
                            foreach(XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                XmlElement elemChild1 = (XmlElement)xnChild1;
                                string nameChild1 = xnChild1.Name;
                                if(nameChild1 == "Name")
                                {
                                    parameter.name = elemChild1.InnerText;
                                }
                                else if(nameChild1 == "Parameter")
                                {
                                    parameter.paraID = elemChild1.GetAttribute("ParameterId");
                                    parameter.type = elemChild1.GetAttribute("type");
                                    parameter.parameterName = elemChild1.GetAttribute("ParameterName");

                                    Parameter parameterTmp = new Parameter();
                                    parameterTmp.name = parameter.name;
                                    parameterTmp.paraID = parameter.paraID;
                                    parameterTmp.type = parameter.type;
                                    parameterTmp.parameterName = parameter.parameterName;

                                    moduleElemModules.connectModules.list.Add(parameterTmp);
                                }
                            }

                        }
                    }

                    dataManage_.modules.list.Add(moduleElemModules);
                }
            }
        }
    }


}
