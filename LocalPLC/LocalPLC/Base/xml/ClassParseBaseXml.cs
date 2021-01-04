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
        public ClassParseBaseXml(string type)
        {
            if(UserControl1.multiprogApp.ActiveProject == null)
            {
                return;
            }

            string projectPath = UserControl1.multiprogApp.ActiveProject.Path;
            string projectName = UserControl1.multiprogApp.ActiveProject.Name;
            string path = projectPath + "\\" + projectName + "\\" + "LocalPLC586.xml";
            try
            {
                xDoc.Load(path);

                //根节点
                XmlNode node = xDoc.SelectSingleNode("DeviceDescription");
                XmlNodeList nodeList = node.ChildNodes;
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
    }
}
