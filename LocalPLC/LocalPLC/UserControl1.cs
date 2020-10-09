using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using ADELib;
using LocalPLC.ModbusMaster;

namespace LocalPLC
{
    [ComVisible(true)]
    [Guid("4D23925D-E5C1-40A8-9D69-AAD815FDCECE")]
    [ProgId("LocalPLC.CONTROLBAR.PROGID")]
    public partial class UserControl1 : UserControl,IAdeAddIn,IAdeProjectObserver
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private static ADELib.Application multiprogApp = null;

        public empty e1;
        public ModbusClient.modbusclient mct;
        
        private void UserControl1_Load(object sender, EventArgs e)
        {
            e1 = new empty();
            mct = new ModbusClient.modbusclient();
        }

        private static int adviceProjectCookie = 0;

        void IAdeAddIn.OnConnection(object Application, AdeConnectMode ConnectMode, object AddInInst, ref Array Custom)
        {
            multiprogApp = Application as ADELib.Application;
            adviceProjectCookie = multiprogApp.AdviseProjectObserver(this);
        }

        void IAdeAddIn.OnDisconnection(AdeDisconnectMode RemoveMode, ref Array Custom)
        {
           
        }

        void IAdeAddIn.OnAddInsUpdate(ref Array Custom)
        {
            
        }

        void IAdeAddIn.OnStartupComplete(ref Array Custom)
        {
          
        }

        void IAdeAddIn.OnBeginShutdown(ref Array Custom)
        {
            multiprogApp.UnadviseProjectObserver(adviceProjectCookie);

        }

        void IAdeProjectObserver.BeforeProjectOpen(string Name, ref bool Cancel)
        {
            
        }

        void IAdeProjectObserver.AfterProjectOpen(string Name)
        {
            MessageBox.Show(Name + "has opened");
        }

        void IAdeProjectObserver.BeforeProjectClose(string Name, ref bool Cancel)
        {
            
        }

        void IAdeProjectObserver.AfterProjectClose(string Name)
        {
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string name = e.Node.Text.ToString();
            if (name == "Modbus")
            {
                e1.Show();
                ModbusWindow.Controls.Clear();
                ModbusWindow.Controls.Add(e1);

            }
            else if (name == "MobusTCP-Client")
            {
                mct.Show();
                ModbusWindow.Controls.Clear();
                ModbusWindow.Controls.Add(mct);
            }
            else if(name == "ModbusRTU-Master")
            {
                modbusmastermain modmaster = new modbusmastermain();
                modmaster.Show();
                ModbusWindow.Controls.Clear();
                ModbusWindow.Controls.Add(modmaster);
            }
        }
    }
}
