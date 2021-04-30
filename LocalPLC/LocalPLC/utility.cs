using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADELib;
using LocalPLC.ModbusMaster;
using System.IO;
using System.Xml;

namespace LocalPLC
{
   class ConstVariable
    {
        public static string DO = "DO";
        public static string DI = "DI";
        //IO地址
        public const int DOADDRESSIO = 8001;
        public const int DIADDRESSIO = 8001;
    }
}

namespace LocalPLC
{    enum ArrayDataType{ DataBit, DataWord}

    class SplicedDataType
    {
        public static HashSet<int> hashSetBit = new HashSet<int>() { 0x01, 0x02, 0x05,
        0x0F};
        public static HashSet<int> hashSetWord = new HashSet<int>() { 0x03, 0x04, 0x06,
        0x10};
        
        public static string splicedDataTypeArray(string name, ArrayDataType type, int count)
        {
            string strArray = "";
            if (type == ArrayDataType.DataBit)
            {
                if (!utility.varTypeDicBit.ContainsKey(count))
                {
                    string varTypeName = string.Format("ARRAY_bit_{0}", name);
                    strArray += "\r\nTYPE\r\n" + varTypeName + " : ARRAY[0.." + (count - 1).ToString() + "] OF BOOL;";
                    strArray += "\r\nEND_TYPE\r\n";
                    
                    {
                        
                        utility.varTypeDicBit.Add(count, varTypeName);
                        //utility.varTypeDicBit1.Add(count, varTypeName);
                    }
                }

            }
            else if(type == ArrayDataType.DataWord)
            {
                if (!utility.varTypeDicWord.ContainsKey(count))
                {
                    string varTypeName = string.Format("ARRAY_word_{0}", name);

                    strArray += "\r\nTYPE\r\n" + varTypeName + " : ARRAY[0.." + (count - 1).ToString() + "] OF WORD;";
                    strArray += "\r\nEND_TYPE\r\n";

                    utility.varTypeDicWord.Add(count, varTypeName);
                    //utility.varTypeDicWord2.Add(count, varTypeName);
                }
            }

            return strArray;
        }
        public static string splicedDataTypeArray1(string name, ArrayDataType type, int count)
        {
            string strArray = "";
            if (type == ArrayDataType.DataBit)
            {
                if (!utility.varTypeDicBit1.ContainsKey(count))
                {
                    string varTypeName = string.Format("ARRAY_bit_{0}", name);
                    strArray += "\r\nTYPE\r\n" + varTypeName + " : ARRAY[0.." + (count - 1).ToString() + "] OF BOOL;";
                    strArray += "\r\nEND_TYPE\r\n";

                    

                        
                        utility.varTypeDicBit1.Add(count, varTypeName);
                    
                }

            }
            else if (type == ArrayDataType.DataWord)
            {
                if (!utility.varTypeDicWord2.ContainsKey(count))
                {
                    string varTypeName = string.Format("ARRAY_word_{0}", name);

                    strArray += "\r\nTYPE\r\n" + varTypeName + " : ARRAY[0.." + (count - 1).ToString() + "] OF WORD;";
                    strArray += "\r\nEND_TYPE\r\n";
                    utility.varTypeDicWord2.Add(count, varTypeName);
                }
                   

                              
            }

            return strArray;
        }
    }

    class utility
    {
        //根据串口个数确定master个数，一个master有16个device，一个device有16个channel
        public static int masterCount = 5;
        public static int clientCount = 5;
        //modbus master的设备最大个数
        public static int masterDeviceCountMax = 16;
        public static int masterDeviceChannleCountMax = 16;
        //modbus总起始地址
        public static int modbusAddr = 10000;
        //每个单位间隔1000字节
        public static int modbusMudule= 2000;
        public static void PrintBuild(string str)
        {
            //for (int i = 0; i < LocalPLC.UserControl1.multiprogApp.OutputWindows.Count; i++)
            //{
            //    var name = LocalPLC.UserControl1.multiprogApp.OutputWindows.Item(i + 1).Name;
            //    PrintError(name);
            //}

            //foreach(var name in LocalPLC.UserControl1.multiprogApp.OutputWindows)
            //{
            //   var ttt =  (name as OutputWindow).Name;
            //}

            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Build").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Build").Activate();
        }

        public static void PrintError(string str)
        {
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Errors").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Errors").Activate();
            
        }


        public static void PrintInfo(string str)
        {
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").Activate();
        }


        public static void PrintPLCError(string str)
        {
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("PLC Errors").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("PLC Errors").Activate();
        }

        public static void Print(string str)
        {
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Print").AddEntry(str, AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
            // show the output window and activate the "Infos" tab
            LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Print").Activate();
        }

        /*
         IOGroups添加IOGroup
         */
        public static bool addIOGroups()
        {
            try
            {
                if(LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("资源里没有数据,请配置好资源后，再重新导入数据!");
                    return false; ;
                }

                //GC.Collect();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups);

                IoGroups iog = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups;

                

                //int Count = UserControl1.iog.Count;

                //List<IoGroup> ll = new List<IoGroup>();
                

                for(int i = iog.Count; i > 0; i--)
                {
                    iog.Item(i).Delete();
                }

                //foreach (IoGroup io in iog)
                //{
                //    //ll.Add(io);
                //    io.Delete();
                //}

                //for (int i = 0; i < ll.Count; i++)
                //{
                //    ll[i].Delete();
                //}
                List<LocalPLC.ModbusMaster.ModbusMasterData> listmaster = UserControl1.modmaster.masterManage.modbusMastrList;
                int masternumber = 0;
                //var list = UserControl1.modmaster.masterManage.modbusMastrList;
                foreach (var master in listmaster)
                {
                    List<LocalPLC.ModbusMaster.DeviceData> listmasterdev = UserControl1.modmaster.masterManage.modbusMastrList[masternumber].modbusDeviceList;
                    for (int i =0;i<listmasterdev.Count;i++)
                    {
                        string str = string.Format("master_in{0}_dev{1}", master.ID,master.modbusDeviceList[i].ID);
                        //int a = listmasterdev[i].curDeviceLength;
                        //int b = listmasterdev[i].curDeviceAddr;
                        iog.Create(str, AdeIoGroupAccessType.adeIgatInput,
                            listmasterdev[i].curDeviceLength, "SystemIODriver", "<默认>", "", listmasterdev[i].curDeviceAddr, "test", AdeIoGroupDataType.adeIgdtByte,
                            1, 1, 1, 1);
                        str = string.Format("master_out{0}_dev{1}", master.ID, master.modbusDeviceList[i].ID);
                        iog.Create(str, AdeIoGroupAccessType.adeIgatOutput,
                            listmasterdev[i].curDeviceLength, "SystemIODriver", "<默认>", "", listmasterdev[i].curDeviceAddr, "test", AdeIoGroupDataType.adeIgdtByte,
                            1, 1, 1, 1);
                    }
                    masternumber++;                   
                }
                List<LocalPLC.ModbusClient.ModbusClientData> listClient = UserControl1.mci.clientManage.modbusClientList;
                int clientnumber = 0;
                foreach (LocalPLC.ModbusClient.ModbusClientData client in listClient)
                {
                    List<LocalPLC.ModbusClient.DeviceData> listclientdev = UserControl1.mci.clientManage.modbusClientList[clientnumber].modbusDeviceList;
                    for (int j = 0; j < listclientdev.Count; j++)
                    {     
                        string str = string.Format("client_in{0}_dev{1}", client.ID, client.modbusDeviceList[j].ID);
                        //int aaa = listclientdev[j].devstartaddr;
                        //int bbb = listclientdev[j].devlength;
                        iog.Create(str, AdeIoGroupAccessType.adeIgatInput, listclientdev[j].devlength, "SystemIODriver", "<默认>", "", listclientdev[j].devstartaddr, "test", AdeIoGroupDataType.adeIgdtByte
                            , 1, 1, 1, 1);
                        str = string.Format("client_out{0}_dev{1}", client.ID, client.modbusDeviceList[j].ID);
                        iog.Create(str, AdeIoGroupAccessType.adeIgatOutput,
                                   listclientdev[j].devlength, "SystemIODriver", "<默认>", "", listclientdev[j].devstartaddr, "test", AdeIoGroupDataType.adeIgdtByte,
                                    1, 1, 1, 1);
                    }
                    clientnumber++;
                }
                string str1 = "server_in";
                if (UserControl1.msi.serverDataManager.listServer.Count > 0)
                {
                    iog.Create(str1, AdeIoGroupAccessType.adeIgatInput,
                    utility.modbusMudule, "SystemIODriver", "<默认>", "", UserControl1.msi.serverDataManager.listServer[0].serverstartaddr, "test", AdeIoGroupDataType.adeIgdtByte,
                    1, 1, 1, 1);
                    str1 = "server_out";
                    iog.Create(str1, AdeIoGroupAccessType.adeIgatOutput,
                    utility.modbusMudule, "SystemIODriver", "<默认>", "", UserControl1.msi.serverDataManager.listServer[0].serverstartaddr, "test", AdeIoGroupDataType.adeIgdtByte,
                    1, 1, 1, 1);
                }


                //add by gw in 20210201 for 添加io group地址
                string ioStartAddr = LocalPLC.Base.UserControlBase.dataManage.deviceInfoElem.deviceIdentificationElem.ioAddrStart;
                string ioEndAddr = LocalPLC.Base.UserControlBase.dataManage.deviceInfoElem.deviceIdentificationElem.ioAddrEnd;
                int nIoStartAddr = 0;
                int nIoEndAddr = 0;
                if (int.TryParse(ioStartAddr, out nIoStartAddr) == false)
                {
                    System.Windows.Forms.MessageBox.Show("io组起始地址无效!");
                }

                if (int.TryParse(ioEndAddr, out nIoEndAddr) == false)
                {
                    System.Windows.Forms.MessageBox.Show("io组结束地址无效!");
                }

                int count = (nIoEndAddr - nIoStartAddr) + 1;

                string strBase = "Base_DI_in";
                iog.Create(strBase, AdeIoGroupAccessType.adeIgatInput, count, "SystemIODriver", "<默认>", "", nIoStartAddr, "test", AdeIoGroupDataType.adeIgdtByte
                    , 1, 1, 1, 1);
                strBase = "Base_DI_out";
                iog.Create(strBase, AdeIoGroupAccessType.adeIgatOutput, count, "SystemIODriver", "<默认>", "", nIoStartAddr, "test", AdeIoGroupDataType.adeIgdtByte
                   , 1, 1, 1, 1);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("编译有错误，导入数据失败，请修改错误后,手动编译然后再导入数据!");
                System.Windows.Forms.MessageBox.Show(e.ToString());

                //add by gw in 20210201 for释放异常情况IO Groups
                //GC.Collect();

                return false;
            }

            return true;
            
        }
        public static void addServerIOGroups()
        {
            IoGroups iog = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).IoGroups;

            int Count = iog.Count;

            List<IoGroup> ll = new List<IoGroup>();
            foreach (IoGroup io in iog)
            {
                ll.Add(io);
            }

            for (int i = 0; i < ll.Count; i++)
            {
                ll[i].Delete();
            }

            string str = "server_in";
            int sss = UserControl1.msi.serverDataManager.listServer[0].serverstartaddr;
            iog.Create(str, AdeIoGroupAccessType.adeIgatInput,
            utility.modbusMudule, "SystemIODriver", "<默认>", "", UserControl1.msi.serverDataManager.listServer[0].serverstartaddr, "test", AdeIoGroupDataType.adeIgdtByte,
            1, 1, 1, 1);
            str = "server_out";
            iog.Create(str, AdeIoGroupAccessType.adeIgatOutput,
            utility.modbusMudule, "SystemIODriver", "<默认>", "", UserControl1.msi.serverDataManager.listServer[0].serverstartaddr, "test", AdeIoGroupDataType.adeIgdtByte,
            1, 1, 1, 1);
        }
        /* 动态添加创建数组刷新
         //C: \Users\Public\Documents\MULTIPROG\Projects\ttt13579\DT\datatype
            // 从文件中读取并显示每行
            string fullName = UserControl1.multiprogApp.ActiveProject.Path + "\\" + UserControl1.multiprogApp.ActiveProject.Name + "\\DT\\datatype\\datatype.TYB";
            ;
            string path = UserControl1.multiprogApp.ActiveProject.FullName;
            FileStream fs = new FileStream(fullName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            
            sw.WriteLine("TYPE\r\nnimade: ARRAY[0..20] OF BYTE;\r\nEND_TYPE");
            sw.Close();
            fs.Close();

            //UserControl1.multiprogApp.ActiveProject.Close();
            //UserControl1.multiprogApp.OpenProject(path, AdeConfirmRule.adeCrConfirm);
            UserControl1.multiprogApp.ActiveProject.Compile(AdeCompileType.adeCtBuild);
         */


        /* master变量组里添加变量
        var groups = resource.Variables.Groups;
                        foreach(VariableGroup ttt in groups)
                        {
                            var name = ttt.Name;
                            if(name == "master")
                            {
                                ttt.Variables.Create("test", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                                     "Inserted from AIFDemo", "12", "%IX0.0", false);
                            }
}
        */

        public static void addVariables()
        {
            try
            {
                if (LocalPLC.UserControl1.multiprogApp != null && LocalPLC.UserControl1.multiprogApp.IsProjectOpen())
                {
                    Hardware physicalHardware = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware;

                    // Because of VB all indices are starting with 1 !!!!!
                    Resource resource = physicalHardware.Configurations.Item(1).Resources.Item(1);

                    // get the variables collection with the specified logical name
                    AdeObjectType objectType = AdeObjectType.adeOtVariables;
                    object variablesObject =
                        LocalPLC.UserControl1.multiprogApp.ActiveProject.GetObjectByLogicalName(resource.Variables.LogicalName, ref objectType);
                    // is the returned object really of type "Variables"?
                    if (objectType == AdeObjectType.adeOtVariables)
                    {
                        Variables variables = variablesObject as Variables;

                        foreach (Variable var in variables)
                        {
                            //var.Delete();
                        }

                        //master组添加变量
                        var groups = resource.Variables.Groups;
                        foreach (VariableGroup ttt in groups)
                        {
                            
                            var name = ttt.Name;
                            if (name == "Master")
                            {
                                foreach (Variable variable in ttt.Variables)
                                {
                                    variable.Delete();
                                }
                                foreach (var master in UserControl1.modmaster.masterManage.modbusMastrList)
                                {
                                    foreach (var device in master.modbusDeviceList)
                                    {
                                        if (device.resetVaraible != "")
                                        {
                                            var resetvariable = ttt.Variables.Create(device.resetVaraible, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "复位变量", "", "%QB" + device.curDeviceAddr.ToString());
                                            string a = device.resetkey[0];
                                            string b = device.resetkey[1];
                                            resetvariable.SetAttribute(20, device.resetkey[0] + "m" + device.resetkey[1]);

                                        }
                                        foreach (var channel in device.modbusChannelList)
                                        {
                                            string adress = string.Format("%IB{0}", channel.curChannelAddr + 3);  //2 一个触发变量 一个错误变量
                                            string adresswrite = string.Format("%QB{0}", channel.curChannelAddr + 3);
                                            string adressword = string.Format("%IW{0}", channel.curChannelAddr + 3);  //2 一个触发变量 一个错误变量
                                            string adresswriteword = string.Format("%QW{0}", channel.curChannelAddr + 3);
                                            if (utility.varTypeDicBit.ContainsKey(channel.readLength) && (channel.msgType == 1|| channel.msgType == 2
                                                || channel.msgType == 5 || channel.msgType == 15))
                                            {
                                                string varType = utility.varTypeDicBit[channel.readLength];
                                                //string adress = string.Format("%IX{0}.0", channel.curChannelAddr + 3);  //2 一个触发变量 一个错误变量,错误变量为1个word
                                                if (channel.trigger != "")
                                                {
                                                    var triggeroffset = ttt.Variables.Create(channel.trigger, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "触发变量", "", "%QB" + channel.curChannelAddr.ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    triggeroffset.SetAttribute(20, channel.offsetkey[0] + "m" + channel.offsetkey[1] + channel.offsetkey[2] + "m" + channel.offsetkey1);
                                                }
                                                if (channel.error != "")
                                                {
                                                    var erroroffset = ttt.Variables.Create(channel.error, "WORD", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "错误变量", "", "%IW" + (channel.curChannelAddr + 1).ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    erroroffset.SetAttribute(20, channel.offsetkey[0] + "m" + channel.offsetkey[1] + channel.offsetkey[2] + "m" + channel.offsetkey2);
                                                }
                                                if (channel.msgType >= 1 && channel.msgType <= 4)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    "Inserted from AIFDemo", "", adress, false);
                                                }
                                                else if (channel.msgType >= 5)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    "Inserted from AIFDemo", "", adresswrite, false);
                                                }
                                            }
                                            else if (utility.varTypeDicWord.ContainsKey(channel.readLength) && (channel.msgType == 3 || channel.msgType == 4 ||
                                                channel.msgType == 6 || channel.msgType == 16))
                                            {
                                                string varType = utility.varTypeDicWord[channel.readLength];
                                                //string adress = string.Format("%IW{0}", channel.curChannelAddr + 3);  //2 一个触发变量 一个错误变量
                                                if (channel.trigger != "")
                                                {
                                                    var triggeroffset = ttt.Variables.Create(channel.trigger, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "触发变量", "", "%QB" + channel.curChannelAddr.ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    triggeroffset.SetAttribute(20, channel.offsetkey[0] + "m" + channel.offsetkey[1] + channel.offsetkey[2] + "m" + channel.offsetkey1);
                                                }
                                                if (channel.error != "")
                                                {
                                                    var erroroffset = ttt.Variables.Create(channel.error, "WORD", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "错误变量", "", "%IW" + (channel.curChannelAddr + 1).ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    erroroffset.SetAttribute(20, channel.offsetkey[0] + "m" + channel.offsetkey[1] + channel.offsetkey[2] + "m" + channel.offsetkey2);
                                                }
                                                if (channel.msgType >= 1 && channel.msgType <= 4)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    "Inserted from AIFDemo", "", adressword, false);
                                                }
                                                else if (channel.msgType >= 5)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    "Inserted from AIFDemo", "", adresswriteword, false);
                                                }
                                            }

                                        }
                                    }
                                }

                                //ttt.Variables.Create("test", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                                //     "Inserted from AIFDemo", "12", "%IX0.0", false);
                            }
                            if (name == "Client")
                            {
                                foreach (Variable variable in ttt.Variables)
                                {
                                    variable.Delete();
                                }
                                foreach (var client in UserControl1.mci.clientManage.modbusClientList)
                                {
                                    foreach (var device in client.modbusDeviceList)
                                    {
                                        if (device.resetVaraible != "")
                                        {
                                            var resetvariable = ttt.Variables.Create(device.resetVaraible, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                         "复位变量", "", "%QB" + device.devstartaddr.ToString());
                                            string a = device.resetkey[0];
                                            string b = device.resetkey[1];
                                            resetvariable.SetAttribute(20, device.resetkey[0] + "c" + device.resetkey[1]);
                                        }
                                        foreach (var channel in device.modbusChannelList)
                                        {
                                            string adress = string.Format("%IB{0}", channel.channelstartaddr + 3);  //2 一个触发变量 一个错误变量
                                            string adresswrite = string.Format("%QB{0}", channel.channelstartaddr + 3);
                                            string adressword = string.Format("%IW{0}", channel.channelstartaddr + 3);  //2 一个触发变量 一个错误变量
                                            string adresswriteword = string.Format("%QW{0}", channel.channelstartaddr + 3);
                                            if (utility.varTypeDicBit1.ContainsKey(channel.Length)&&(channel.msgType == 1 || channel.msgType == 2
                                                || channel.msgType == 5 || channel.msgType == 15))
                                            {
                                                string varType = utility.varTypeDicBit1[channel.Length];
                                                //string varTypeword = utility.varTypeDicWord2[channel.Length];
                                                //string adress = string.Format("%IX]B{0}.0", channel.channelstartaddr + 3);  //2 一个触发变量 一个错误变量
                                                //string adresswrite = string.Format("%QB{0}.0", channel.channelstartaddr + 3);
                                                if (channel.trigger_offset != "")
                                                {
                                                    var triggeroffset = ttt.Variables.Create(channel.trigger_offset, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "触发变量", "", "%QB" + channel.channelstartaddr.ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    triggeroffset.SetAttribute(20, channel.offsetkey[0] + "c" + channel.offsetkey[1] + channel.offsetkey[2] + "c" + channel.offsetkey1);
                                                }
                                                if (channel.error_offset != "")
                                                {
                                                    var erroroffset = ttt.Variables.Create(channel.error_offset, "WORD", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "错误变量", "", "%IW" + (channel.channelstartaddr + 1).ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    erroroffset.SetAttribute(20, channel.offsetkey[0] + "c" + channel.offsetkey[1] + channel.offsetkey[2] + "c" + channel.offsetkey2);
                                                }
                                                if (channel.msgType >= 1 && channel.msgType <= 4)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                      "Inserted from AIFDemo", "", adress, false);
                                                }
                                                else if (channel.msgType >= 5)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                      "Inserted from AIFDemo", "", adresswrite, false);
                                                }
                                            }
                                            else if (utility.varTypeDicWord2.ContainsKey(channel.Length) && (channel.msgType == 3 || channel.msgType == 4 ||
                                                channel.msgType == 6 || channel.msgType == 16))
                                            {
                                                string varType = utility.varTypeDicWord2[channel.Length];
                                                //string adress = string.Format("%IW{0}", channel.channelstartaddr + 3);  //2 一个触发变量 一个错误变量
                                                if (channel.trigger_offset != "")
                                                {
                                                    var triggeroffset = ttt.Variables.Create(channel.trigger_offset, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "触发变量", "", "%QB" + channel.channelstartaddr.ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    triggeroffset.SetAttribute(20, channel.offsetkey[0] + "c" + channel.offsetkey[1] + channel.offsetkey[2] + "c" + channel.offsetkey1);
                                                }
                                                if (channel.error_offset != "")
                                                {
                                                    var erroroffset = ttt.Variables.Create(channel.error_offset, "WORD", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                        "错误变量", "", "%IW" + (channel.channelstartaddr + 1).ToString());
                                                    string a = channel.offsetkey[0];
                                                    string b = channel.offsetkey[1];
                                                    string c = channel.offsetkey[2];
                                                    string d = channel.offsetkey1;
                                                    erroroffset.SetAttribute(20, channel.offsetkey[0] + "c" + channel.offsetkey[1] + channel.offsetkey[2] + "c" + channel.offsetkey2);
                                                }

                                                //ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                //    "Inserted from AIFDemo", "", adress, false);
                                                if (channel.msgType >= 1 && channel.msgType <= 4)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                      "Inserted from AIFDemo", "", adressword, false);
                                                }
                                                else if (channel.msgType >= 5)
                                                {
                                                    ttt.Variables.Create(channel.nameChannel, varType, AdeVariableBlockType.adeVarBlockVarGlobal,
                                                      "Inserted from AIFDemo", "", adresswriteword, false);
                                                }
                                            }
                                           
                                        }
                                    }
                                }

                                //ttt.Variables.Create("test", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                                //     "Inserted from AIFDemo", "12", "%IX0.0", false);
                            }




                            //add by gw in 20210201 for添加DI变量
                            if(name == "Base_DI")
                            {
                                //删除变量组下的变量
                                foreach (Variable variable in ttt.Variables)
                                {
                                    variable.Delete();
                                }

                                foreach(var di in LocalPLC.Base.UserControlBase.dataManage.diList)
                                {
                                    if(di.used)
                                    {
                                        if (di.varName == "")
                                        {
                                            di.varName = di.channelName;
                                        }

                                        continue;
                                    }

                                    string varName = "";
                                    if(di.varName == "")
                                    {
                                        varName = di.channelName;
                                        di.varName = di.channelName;
                                    }
                                    else
                                    {
                                        varName = di.varName;
                                    }
                                    var resetvariable = ttt.Variables.Create(varName, "BOOL", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    di.note, "", di.address);
                                    resetvariable.SetAttribute(20, di.channelName);
                                }
                            }

                            if(name == "Hsc")
                            {
                                //删除变量组下的变量
                                foreach (Variable variable in ttt.Variables)
                                {
                                    variable.Delete();
                                }



                                int i = 0;
                                foreach(var hsc in LocalPLC.Base.UserControlBase.dataManage.hscList)
                                {
                                    if(hsc.used)
                                    {
                                        string initValue = LocalPLC.Base.UserControlBase.dataManage.getHscVarInitValue(hsc, i);
                                        var hscVariable = ttt.Variables.Create(hsc.name, "HSC_REF_t", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    "hsc变量", initValue);
                                    }

                                    i++;
                                }
                            }

                            if(name == "Base_DO")
                            {
                                //删除变量组下的变量
                                foreach (Variable variable in ttt.Variables)
                                {
                                    variable.Delete();
                                }

                                foreach (var dout in LocalPLC.Base.UserControlBase.dataManage.doList)
                                {

                                    if(dout.used)
                                    {
                                        continue;
                                    }

                                    string varName = "";
                                    if (dout.varName == "")
                                    {
                                        varName = dout.channelName;
                                        dout.varName = dout.channelName;
                                    }
                                    else
                                    {
                                        varName = dout.varName;
                                    }

                                    var resetvariable = ttt.Variables.Create(varName, "BOOL", AdeVariableBlockType.adeVarBlockVarGlobal,
                                                    dout.note, "", dout.address);
                                    resetvariable.SetAttribute(20, dout.channelName);
                                }
                            }
                        }

                        //var variable = variables.Create("test", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                        //                 "Inserted from AIFDemo", "12", "", false);
                        ////variable = variables.Create("test1", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                        ////              "Inserted from AIFDemo", "12", "%IW1000", false);
                        //variable.SetAttribute(90, "1");

                        //object t = variable.GetAttribute(90);
                        //string strTemp = t.ToString();


                        //System.Runtime.InteropServices.Marshal.ReleaseComObject(variable);

                    }


                    string str = LocalPLC.UserControl1.multiprogApp.ActiveProject.Path;

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(variablesObject);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(resource);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(physicalHardware);


                }
            }
            catch(Exception e)
            {
                //System.Windows.Forms.MessageBox.Show("变量内存冲突!");
                System.Windows.Forms.MessageBox.Show(e.ToString());

            }


        }
        public static void addserverVariables()
        {
            //VariableGroup variablegroup;
            if (LocalPLC.UserControl1.multiprogApp != null && LocalPLC.UserControl1.multiprogApp.IsProjectOpen())
            {
                Hardware physicalHardware = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware;
                // Because of VB all indices are starting with 1 !!!!!
                Resource resource = physicalHardware.Configurations.Item(1).Resources.Item(1);

                // get the variables collection with the specified logical name
                AdeObjectType objectType = AdeObjectType.adeOtVariables;
                object variablesObject =
                    LocalPLC.UserControl1.multiprogApp.ActiveProject.GetObjectByLogicalName(resource.Variables.LogicalName, ref objectType);
                // is the returned object really of type "Variables"?
                if (objectType == AdeObjectType.adeOtVariables)
                {
                    Variables variables = variablesObject as Variables;
                    //server组添加变量
                    var groups = resource.Variables.Groups;
                    foreach(VariableGroup ttt in groups)
                    {
                        if (ttt.Name == "Server" )
                        {
                            foreach (Variable var in ttt.Variables)
                            {
                                var.Delete();
                            }
                            for (int i =0;i< UserControl1.msi.serverDataManager.listServer.Count;i++)
                            {
                                for (int j =0;j< UserControl1.msi.serverDataManager.listServer[i].dataDevice_.coilCount;j++)
                                {
                                    int a = 0;int b = 0;
                                    b = j / 8; 
                                    a = j % 8;
                                    ttt.Variables.Create("Server" + i + "_coil" + j, "BOOL", AdeVariableBlockType.adeVarBlockVarGlobal, "modbus server 变量", "0", 
                                        "%MX"+ (Convert.ToInt32(UserControl1.msi.serverDataManager.listServer[i].dataDevice_.coilIoAddrStart)+b).ToString()+"."+a.ToString(), false);
                                }
                                for (int k = 0; k < UserControl1.msi.serverDataManager.listServer[i].dataDevice_.holdingCount; k++)
                                {
                                    int c = 0;
                                    c = k * 2;
                                    ttt.Variables.Create("Server" + i + "_holding" + k, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal, "modbus server 变量", "0",
                                        "%MB" + (Convert.ToInt32(UserControl1.msi.serverDataManager.listServer[i].dataDevice_.holdingIoAddrStart)+c).ToString(), false);
                                }
                                for(int l = 0;l < UserControl1.msi.serverDataManager.listServer[i].dataDevice_.decreteCount;l++)
                                {
                                    int d = 0;int e = 0;
                                    e = l / 8;
                                    d = l % 8;
                                    ttt.Variables.Create("Server" + i + "_decrete" + l, "BOOL", AdeVariableBlockType.adeVarBlockVarGlobal, "modbus server 变量", "0",
                                        "%IX" + (Convert.ToInt32(UserControl1.msi.serverDataManager.listServer[i].dataDevice_.coilIoAddrStart) + e).ToString() + "." + d.ToString(), false);
                                }
                                for (int m = 0; m < UserControl1.msi.serverDataManager.listServer[i].dataDevice_.statusCount; m++)
                                {
                                    int f = 0;
                                    f = m * 2;
                                    ttt.Variables.Create("Server" + i + "_status" + m, "BYTE", AdeVariableBlockType.adeVarBlockVarGlobal, "modbus server 变量", "0",
                                        "%IB" + (Convert.ToInt32(UserControl1.msi.serverDataManager.listServer[i].dataDevice_.statusIoAddrStart) + m).ToString(), false);
                                }
                            }
                        }
                        
                    }

                }
            }
        }
        public static void checkvariables()
        {
            //addVariables();
            //if (LocalPLC.UserControl1.multiprogApp != null && LocalPLC.UserControl1.multiprogApp.IsProjectOpen())
            //{
            //    Hardware physicalHardware = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware;
            //    foreach (Configuration configuration in physicalHardware.Configurations)
            //    {
            //        foreach (Resource resource in configuration.Resources)
            //        {
            //            var groups = resource.Variables.Groups;
            //            foreach (VariableGroup clientgroup in groups)
            //            {
            //                if (clientgroup.Name == "Client")
            //                {

            //                    string resetkey = null;
            //                    foreach (Variable variable in clientgroup.Variables)
            //                    {

            //                        object resetkey1 = variable.GetAttribute(20);
            //                        if (resetkey1 != null)
            //                        {
            //                            resetkey = resetkey1.ToString();
            //                            string[] key1 = resetkey.Split('c');
            //                            string key2 = key1[0];
            //                            if (resetkey != "c" && resetkey != "" &&
            //                            OldVariable.Name == UserControl1.mci.clientManage.modbusClientList[Convert.ToInt32(key1[0])].modbusDeviceList[Convert.ToInt32(key1[1])].resetVaraible)
            //                            {



            //                                UserControl1.mci.clientManage.modbusClientList[Convert.ToInt32(key1[0])].modbusDeviceList[Convert.ToInt32(key1[1])].resetVaraible
            //                                    = variable.Name;
            //                            }
            //                        }




            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public static Dictionary<int, string> varTypeDicBit = new Dictionary<int, string>();
        public static Dictionary<int, string> varTypeDicBit1 = new Dictionary<int, string>();
        public static Dictionary<int, string> varTypeDicWord = new Dictionary<int, string>();
        public static Dictionary<int, string> varTypeDicWord2 = new Dictionary<int, string>();
        static public void addVarType1()
        {
            varTypeDicBit.Clear();
            varTypeDicBit1.Clear();
            varTypeDicWord.Clear();
            varTypeDicWord2.Clear();
            if (!UserControl1.multiprogApp.IsProjectOpen())
            {
                return;
            }
            string fullName1 = UserControl1.multiprogApp.ActiveProject.Path + "\\" + UserControl1.multiprogApp.ActiveProject.Name + "\\dt\\Task_Info\\Task_Info.TYB";
            string path = UserControl1.multiprogApp.ActiveProject.FullName;
            FileStream fs2 = new FileStream(fullName1, FileMode.Open, FileAccess.Read);
            StreamReader sr2 = new StreamReader(fs2, Encoding.Default);
            string s2;
            s2 = sr2.ReadLine();
            
            string strSave2 = "";
            string strSave3 = "";
            string compare1 = "TYPE";
            string compare2 = "Task_Info_eCLR :";
            string compare3 = "STRUCT";
            string compare4 = "TaskStack : INT;";
            string compare5 = "END_STRUCT;";
            string compare6 = "END_TYPE";

            int count2 = 0;

            while (s2 != null)
            {
                strSave2 += s2 + "\r\n";



                s2 = s2.Trim();
                if (compare1 == s2)
                {
                    count2 = 0;
                    count2++;
                }
                else if (compare2 == s2)
                {
                    count2++;
                }
                else if (compare3 == s2)
                {
                    count2++;
                }
                else if (compare4 == s2)
                {
                    count2++;
                }
                else if (compare5 == s2)
                {
                    count2++;
                }
                else if (compare6 == s2)
                {
                    count2++;

                    if (count2 == 6)
                    {
                        int b = 5;
                        b = 6;
                        break;
                    }
                }



                s2 = sr2.ReadLine();
            }
            fs2.Dispose();
            fs2.Close();
            sr2.Close();


            FileStream fs3 = new FileStream(fullName1, FileMode.OpenOrCreate);
            fs3.SetLength(0);
            StreamWriter sw2 = new StreamWriter(fs3, Encoding.Default);

            foreach (var master in LocalPLC.UserControl1.modmaster.masterManage.modbusMastrList)
            {
                foreach (var device in master.modbusDeviceList)
                {
                    foreach (var channel in device.modbusChannelList)
                    {
                        if (SplicedDataType.hashSetBit.Contains(channel.msgType))
                        {
                            strSave2 += SplicedDataType.splicedDataTypeArray("master"+master.ID.ToString()+"dev"+device.ID.ToString()+"cha"+channel.ID.ToString(), ArrayDataType.DataBit, channel.readLength);
                        }
                        else if (SplicedDataType.hashSetWord.Contains(channel.msgType))
                        {
                            strSave2 += SplicedDataType.splicedDataTypeArray("master" + master.ID.ToString() + "dev" + device.ID.ToString() + "cha" + channel.ID.ToString(), ArrayDataType.DataWord, channel.readLength);
                        }

                    }
                }
            }
            foreach (var client in LocalPLC.UserControl1.mci.clientManage.modbusClientList)
            {
                foreach (var device in client.modbusDeviceList)
                {
                    foreach (var channel in device.modbusChannelList)
                    {
                        if (SplicedDataType.hashSetBit.Contains(channel.msgType))
                        {
                            strSave3 += SplicedDataType.splicedDataTypeArray1("client" + client.ID.ToString() + "dev" + device.ID.ToString() + "cha" + channel.ID.ToString(), ArrayDataType.DataBit, channel.Length);
                        }
                        else if (SplicedDataType.hashSetWord.Contains(channel.msgType))
                        {
                            strSave3 += SplicedDataType.splicedDataTypeArray1("client" + client.ID.ToString() + "dev" + device.ID.ToString() + "cha" + channel.ID.ToString(), ArrayDataType.DataWord, channel.Length);
                        }

                    }
                }
            }
            sw2.WriteLine(strSave2);
            sw2.WriteLine(strSave3);
            sw2.Close();
            fs3.Dispose();
            fs3.Close();
            UserControl1.multiprogApp.ActiveProject.Compile(AdeCompileType.adeCtBuild);
        }
        static public void addVarType()
        {
            varTypeDicBit.Clear();
            varTypeDicBit1.Clear();
            varTypeDicWord.Clear();
            varTypeDicWord2.Clear();

            if (!UserControl1.multiprogApp.IsProjectOpen())
            {
                return;
            }
            string fullName = UserControl1.multiprogApp.ActiveProject.Path + "\\" + UserControl1.multiprogApp.ActiveProject.Name + "\\DT\\datatype\\datatype.TYB";
            
            
            string path = UserControl1.multiprogApp.ActiveProject.FullName;
            

            FileStream fs1 = new FileStream(fullName, FileMode.Open, FileAccess.Read);
            StreamReader sr1 = new StreamReader(fs1, Encoding.Default);
            

            string s;
            
            s = sr1.ReadLine();
           
            string strSave = "";
            string strSave1 = "";
            string compare1 = "TYPE";
            string compare2 = "Task_Info_eCLR :";
            string compare3 = "STRUCT";
            string compare4 = "TaskStack : INT;";
            string compare5 = "END_STRUCT;";
            string compare6 = "END_TYPE";

            int count = 0;
           
            while (s != null)
            {
                strSave += s + "\r\n";



                s = s.Trim();
                if (compare1 == s)
                {
                    count = 0;
                    count++;
                }
                else if (compare2 == s)
                {
                    count++;
                }
                else if (compare3 == s)
                {
                    count++;
                }
                else if (compare4 == s)
                {
                    count++;
                }
                else if (compare5 == s)
                {
                    count++;
                }
                else if (compare6 == s)
                {
                    count++;

                    if (count == 6)
                    {
                        int a = 5;
                        a = 6;
                        break;
                    }
                }



                s = sr1.ReadLine();
            }
            
            fs1.Dispose();
            fs1.Close();
            sr1.Close();
            

            FileStream fs = new FileStream(fullName, FileMode.OpenOrCreate);
            fs.SetLength(0);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //StreamWriter sw1 = new StreamWriter(fs1, Encoding.Default);

            //sw.WriteLine("TYPE\r\nnimade: ARRAY[0..20] OF BYTE;\r\nEND_TYPE");

            foreach (var master in LocalPLC.UserControl1.modmaster.masterManage.modbusMastrList)
            {
                foreach (var device in master.modbusDeviceList)
                {
                    foreach (var channel in device.modbusChannelList)
                    {
                        if (SplicedDataType.hashSetBit.Contains(channel.msgType))
                        {
                            string name = channel.readLength.ToString() + "m";
                            strSave += SplicedDataType.splicedDataTypeArray(name, ArrayDataType.DataBit, channel.readLength);
                        }
                        else if (SplicedDataType.hashSetWord.Contains(channel.msgType))
                        {
                            string name = channel.readLength.ToString() + "m";
                            strSave += SplicedDataType.splicedDataTypeArray(name, ArrayDataType.DataWord, channel.readLength);
                        }

                    }
                }
            }
            foreach (var client in LocalPLC.UserControl1.mci.clientManage.modbusClientList)
            {
                foreach (var device in client.modbusDeviceList)
                {
                    foreach (var channel in device.modbusChannelList)
                    {
                        if (SplicedDataType.hashSetBit.Contains(channel.msgType))
                        //if(channel.msgType == 1 || channel.msgType == 2 || channel.msgType == 5 || channel.msgType == 15)
                        {
                            //string name = "client" + client.ID.ToString() + "dev" + device.ID.ToString() + "cha" + channel.ID.ToString();
                            string name = channel.Length.ToString()+"c";
                            strSave1 += SplicedDataType.splicedDataTypeArray1(name, ArrayDataType.DataBit, channel.Length);
                            
                            
                        }
                        else if (SplicedDataType.hashSetWord.Contains(channel.msgType))
                        {
                            string name = channel.Length.ToString()+"c";
                            strSave1 += SplicedDataType.splicedDataTypeArray1(name, ArrayDataType.DataWord, channel.Length);
                               
                        }

                    }
                }
            }
            sw.WriteLine(strSave);
            sw.WriteLine(strSave1);
            sw.Close();
            fs.Dispose();
            fs.Close();

            


            //UserControl1.multiprogApp.ActiveProject.Close();
            //UserControl1.multiprogApp.OpenProject(path, AdeConfirmRule.adeCrConfirm);

            //UserControl1.multiprogApp.ActiveProject.Compile(AdeCompileType.adeCtBuild);
        }

        public static void setDebugIP(string ip)
        {
            if (LocalPLC.UserControl1.multiprogApp != null && LocalPLC.UserControl1.multiprogApp.IsProjectOpen())
            {



                Hardware physicalHardware = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware;
                foreach (Configuration configuration in physicalHardware.Configurations)
                {
                    foreach (Resource resource in configuration.Resources)
                    {



                        Resource res = null;
                        Configuration cfg = null;

                        cfg = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1);
                        res = cfg.Resources.Item(1);

                        //C\配置\R\资源\资源.SET
                        string res_set_file = LocalPLC.UserControl1.multiprogApp.ActiveProject.Path + "\\" +
                                              LocalPLC.UserControl1.multiprogApp.ActiveProject.Name + "\\" +
                                              "\\C\\" +
                                              cfg.Name + "\\R\\" +
                                              res.Name + "\\" +
                                              res.Name + ".SET";

                        FileStream fs = new FileStream(res_set_file, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);

                        //开始写入
                        string IP = ip;
                        sw.WriteLine("RESOURCE");
                        sw.WriteLine(@"	COMPORT: DLL .\plc\socomm.dll -ip" +
                                         IP +
                                         " -p41100 -TO2000");
                        //sw.WriteLine(@"	COMPORT: DLL .\plc\socomm.dll -ip 192.168.1.10 -p41100 -TO2000");
                        sw.WriteLine("END_RESOURCE");
                        //清空缓冲区
                        sw.Flush();

                        //关闭流
                        sw.Close();
                        fs.Close();

                        res_set_file = LocalPLC.UserControl1.multiprogApp.ActiveProject.Path + "\\" +
                                              LocalPLC.UserControl1.multiprogApp.ActiveProject.Name + "\\" +
                                              "C\\" +
                                              cfg.Name + "\\R\\" +
                                              res.Name + "\\eCLRIpAddressAssignments.set";

                        System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();

                        if (File.Exists(res_set_file))
                        {
                            xmlDocument.Load(res_set_file);

                            XmlNode node = xmlDocument.SelectSingleNode(@"ArrayOfIpAddressAssignment/IpAddressAssignment");//获取bookstore节点的所有子节点
                            XmlElement xe = (XmlElement)node;//将子节点类型转换为XmlElement类型
                            xe.SetAttribute("IpAddress", IP);
                            xmlDocument.Save(res_set_file);
                        }




                        //res.ShowControlDialog(false);
                        //res.ShowControlDialog(true);
                    }
                }
            }
        }

        //日志接口
        public static void WriteLogs(string fileName, string type, string content)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + fileName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + type + "-->" + content);
                    //  sw.WriteLine("----------------------------------------");
                    sw.Close();
                }
            }
        }

        public static void getDataTypeLength()
        {
            if (LocalPLC.UserControl1.multiprogApp != null && LocalPLC.UserControl1.multiprogApp.IsProjectOpen())
            {
                foreach (DataType dataType in LocalPLC.UserControl1.multiprogApp.ActiveProject.DataTypes)
                {
                    string name = dataType.Name;
                    Resource res = null;
                    Configuration cfg = null;

                    cfg = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1);
                    res = cfg.Resources.Item(1);

                    int count = dataType.GetSizeOnPlc(cfg.PlcType, res.ProcessorType);
                }
            }
        }
    }
}
