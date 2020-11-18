using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADELib;

namespace LocalPLC
{
    class utility
    {
        //根据串口个数确定master个数，一个master有8个device，一个device有8个channel
        public static int masterCount = 5;
        //modbus master的设备最大个数
        public static int masterDeviceCountMax = 8;
        public static int masterDeviceChannleCountMax = 8;
        //modbus总起始地址
        public static int modbusAddr = 1000;
        public static void PrintError(string str)
        {
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").Activate();
        }
    }
}
