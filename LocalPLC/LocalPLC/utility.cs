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
            Variables vars = LocalPLC.UserControl1.multiprogApp.ActiveProject.Hardware.Configurations.Item(1).Resources.Item(1).Variables;
            var name = vars.Item(1).Name;


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
    }
}
