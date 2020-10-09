using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace demo_xml_rw
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            RWXml xml = new RWXml();
            xml.WriteXml();

            xml.ReadXml();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



        }
    }
}
