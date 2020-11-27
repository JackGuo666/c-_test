using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADELib;
using LocalPLC.ModbusMaster;
using System.IO;

namespace LocalPLC
{

    enum ArrayDataType{ DataBit, DataWord}

    class SplicedDataType
    {
        public static string splicedDataTypeArray(string name, ArrayDataType type, int count)
        {
            string strArray = "";
            if (type == ArrayDataType.DataBit)
            {
                //for(int i = 0; i < 6; i++)
                {
                    strArray += "\r\nTYPE\r\n" + name + " : ARRAY[0.." + count.ToString() + "] OF BYTE;";
                    strArray += "\r\nEND_TYPE\r\n";
                }

            }

            return strArray;
        }
    }

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

            int Count = iog.Count;

            List<IoGroup> ll = new List<IoGroup>();
            foreach(IoGroup io in iog)
            {
                ll.Add(io);
            }

            for(int i = 0; i < ll.Count; i++)
            {
                ll[i].Delete();
            }

            var list = UserControl1.modmaster.masterManage.modbusMastrList;
            foreach (var master in list)
            {
                string str = string.Format("master_in{0}", master.ID);

                iog.Create(str, AdeIoGroupAccessType.adeIgatInput,
            utility.modbusMudule, "driver1", "<默认>", "", master.curMasterStartAddr, "test", AdeIoGroupDataType.adeIgdtByte,
            1, 1, 1, 1);
                str = string.Format("master_out{0}", master.ID);
                iog.Create(str, AdeIoGroupAccessType.adeIgatOutput,
                            utility.modbusMudule, "driver1", "<默认>", "", master.curMasterStartAddr, "test", AdeIoGroupDataType.adeIgdtByte,
                            1, 1, 1, 1);
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(iog);
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
                    
                    foreach(Variable var in variables)
                    {
                        //var.Delete();
                    }

                    var variable = variables.Create("test", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                                     "Inserted from AIFDemo", "12", "", false);
                    //variable = variables.Create("test1", "INT", AdeVariableBlockType.adeVarBlockVarGlobal,
                    //              "Inserted from AIFDemo", "12", "%IW1000", false);
                    variable.SetAttribute(90, "1");

                    object t = variable.GetAttribute(90);
                    string strTemp = t.ToString();


                    System.Runtime.InteropServices.Marshal.ReleaseComObject(variable);

                }


                string str = LocalPLC.UserControl1.multiprogApp.ActiveProject.Path;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(variablesObject);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(resource);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(physicalHardware);


            }
        }

        static public void addVarType()
        {


            if(!UserControl1.multiprogApp.IsProjectOpen())
            {
                return;
            }
            string fullName = UserControl1.multiprogApp.ActiveProject.Path + "\\" + UserControl1.multiprogApp.ActiveProject.Name + "\\DT\\datatype\\datatype.TYB";
            ;
            string path = UserControl1.multiprogApp.ActiveProject.FullName;

            FileStream fs1 = new FileStream(fullName, FileMode.Open, FileAccess.Read);
            StreamReader sr1 = new StreamReader(fs1, Encoding.Default);
            string s;
            s = sr1.ReadLine();
            string strSave = "";

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

            //sw.WriteLine("TYPE\r\nnimade: ARRAY[0..20] OF BYTE;\r\nEND_TYPE");
            foreach (var master in LocalPLC.UserControl1.modmaster.masterManage.modbusMastrList)
            {
                foreach (var device in master.modbusDeviceList)
                {
                    foreach (var channel in device.modbusChannelList)
                    {
                        strSave += SplicedDataType.splicedDataTypeArray(channel.nameChannel, ArrayDataType.DataBit, channel.readLength);
                    }
                }
            }
            sw.WriteLine(strSave);
            sw.Close();
            fs.Dispose();
            fs.Close();


            

            //UserControl1.multiprogApp.ActiveProject.Close();
            //UserControl1.multiprogApp.OpenProject(path, AdeConfirmRule.adeCrConfirm);

            UserControl1.multiprogApp.ActiveProject.Compile(AdeCompileType.adeCtBuild);
        }

    }
}
