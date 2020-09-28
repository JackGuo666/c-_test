using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//include
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace demo_json1
{

    class Person
    {
        public string name { get; set; }
        public int age { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0} \nAge:{1}", name, age);
        }
    }

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartObject();  //   {  （Json数据的大括号左边 ）

            //第一级节点
            writer.WritePropertyName("test_key");
            writer.WriteValue("1");

            //第一级节点
            writer.WritePropertyName("wen");

            writer.WriteStartArray();//   [      (Json数据的大括号左边) 

            writer.WriteStartObject();//   {

            writer.WritePropertyName("ahref");

            writer.WriteValue("1");

            writer.WritePropertyName("imgpath");

            writer.WriteValue("2");

            writer.WritePropertyName("duanluo");

            writer.WriteValue("3");

            writer.WriteEndObject();//} 

            writer.WriteStartObject();//{

            writer.WritePropertyName("ahref");

            writer.WriteValue("1");

            writer.WritePropertyName("imgpath");

            writer.WriteValue("2");

            writer.WritePropertyName("duanluo");

            writer.WriteValue("3");

            writer.WriteEndObject();//    } （一组json数据结束标记）

            writer.WriteStartObject();//{

            writer.WritePropertyName("ahref");

            writer.WriteValue("1");

            writer.WritePropertyName("imgpath");

            writer.WriteValue("2");

            writer.WritePropertyName("duanluo");

            writer.WriteValue("3");

            writer.WriteEndObject();//        }  （一组json数据结束标记）

            writer.WriteEndArray();//    ]   （多组json数据结束标记）

            //第一级节点
            writer.WritePropertyName("img");

            writer.WriteStartObject();//{

            writer.WritePropertyName("ig1");

            writer.WriteValue("3");

            writer.WritePropertyName("ig2");

            writer.WriteValue("3");

            writer.WritePropertyName("ig3");

            writer.WriteValue("3");

            writer.WritePropertyName("ig4");

            writer.WriteValue("3");

            writer.WriteEndObject();//} 

            writer.WriteEndObject();//}

            string str = "test.json";
            StreamWriter wtyeu = new StreamWriter(str);
            wtyeu.Write(sw);
            wtyeu.Flush();
            wtyeu.Close();


            StreamReader file = new StreamReader(str);

            JsonTextReader reader = new JsonTextReader(file);

            JObject obj = (JObject)JToken.ReadFrom(reader);

            //根节点

            //第一级节点
            JToken token1 = obj["test_key"];
            JToken token = obj["wen"];

            foreach (JObject e in token)
            {
                string strTemp = e["ahref"].ToString();
                strTemp = e["imgpath"].ToString();
                strTemp = e["duanluo"].ToString();
                JToken jk = e.GetValue("ahref_test");
                if(jk == null)
                {

                }
            }

            reader.Close();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
