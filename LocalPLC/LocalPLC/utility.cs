using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADELib;
using LocalPLC.ModbusMaster;

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
        //每个单位间隔1000字节
        public static int modbusMudule= 1000;
        public static void PrintError(string str)
        {
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").Activate();
        }

        /*
         IOGroups添加IOGroup
         */
        public static void addIOGroups()
        {
            IoGroups iog = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups;


            var list = UserControl1.modmaster.masterManage.modbusMastrList;
            foreach(var master in list)
            {
                string str = string.Format("master_in{0}", master.ID);

                iog.Create(str, AdeIoGroupAccessType.adeIgatInput,
            modbusMudule, "driver1", "<默认>", "", master.curMasterStartAddr, "test", AdeIoGroupDataType.adeIgdtByte,
            1, 1, 1, 1);
                str = string.Format("master_out{0}", master.ID);
                iog.Create(str, AdeIoGroupAccessType.adeIgatOutput,
                            modbusMudule, "driver1", "<默认>", "", master.curMasterStartAddr, "test", AdeIoGroupDataType.adeIgdtByte,
                            1, 1, 1, 1);
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
        }
    }
}
