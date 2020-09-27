using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace demo_xml_rw
{
    public class StudentData
    {
        public StudentData()
        {

        }

        public StudentData(Boolean bValue, string str2, string str3)
        {
            value = bValue;
            test2 = str2;
            test3 = str3;
        }

        public Boolean value { get; set; }
        public string test2 { get; set; }
        public string test3 { get; set; }
    }

    public class RWXml
    {
        //生成文档
        public void WriteXml()
        {
            Random rd = new Random();
            
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration declaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xDoc.AppendChild(declaration);

            //创建根节点
            XmlElement elem =  xDoc.CreateElement("students");
            xDoc.AppendChild(elem);

            for(int i = 0; i < 10; i++)
            {
                XmlElement elem1 = xDoc.CreateElement("strudent");
                elem1.SetAttribute("name", "张三");
                elem1.SetAttribute("class", "三年一班" + i.ToString());
                elem1.SetAttribute("sex", "性别");
                elem.AppendChild(elem1);

                XmlElement elem1_1 = xDoc.CreateElement("语文成绩");
                
                elem1_1.InnerText = rd.Next(60, 100).ToString(); ;
                elem1.AppendChild(elem1_1);

                XmlElement elem1_2 = xDoc.CreateElement("数学成绩");
                elem1_2.InnerText = rd.Next(60, 100).ToString();
                elem1.AppendChild(elem1_2);

                XmlElement elem1_3 = xDoc.CreateElement("英语成绩");
                elem1_3.InnerText = rd.Next(60, 100).ToString();
                elem1.AppendChild(elem1_3);
            }
            

            xDoc.Save("students.xml");
            int 您好 = 5;
            int b = 您好;
        }

        public void ReadXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("students.xml");

            //根节点
           XmlNode node =   xDoc.SelectSingleNode("students");
           XmlNodeList nodeList =  node.ChildNodes;
            foreach(XmlNode xn in nodeList)
            {
                XmlElement e = (XmlElement) xn;
                string name = e.GetAttribute("name");
                Console.WriteLine(name);

                XmlNodeList childList = xn.ChildNodes;
                foreach(XmlNode nChild in childList)
                {
                    //student子节点
                    XmlElement eChild = (XmlElement)nChild;
                    string childname = eChild.Name;
                    if(childname == "语文成绩")
                    {

                        //语文成绩结构
                        StudentData st = new StudentData();
                        st.test2 = eChild.InnerText;
                    }
                    else if(childname == "数学成绩")
                    {
                        //数学成绩结构
                    }
                    else if(childname == "英语成绩")
                    {
                        //英语成绩结构
                    }
                }    
            }

        }
    }


}
