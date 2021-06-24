using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonSerializerAndDeSerializer;
using DebugLib.Protocols;
using System.Net.NetworkInformation;
//using LocalPLC.loadingscreen;

namespace LocalPLC.Debug
{
    public partial class UserControlDebugIP : UserControl
    {
        enum COLUMN_NAME {IP, SUBMASK, GATEWAY, MAC, DEVID,  FIRMVER, DHCP, ACK, FILCKER/*, PERSIST*/}
        Timer scanDevTimer = null;

        public UserControlDebugIP()
        {
            InitializeComponent();
            SetupDataGridView();

            createTimer();
        }

        #region delegate
        public delegate void AppendDevInofDelegate(ReciveModel model);
        #endregion


        #region timer
        void createTimer()
        {
            //测试定时器
            scanDevTimer = new Timer(); //新建一个Timer对象
            scanDevTimer.Interval = 30000;//设定多少秒后行动，单位是毫秒
            scanDevTimer.Tick += new EventHandler(scanDeviceTimer);//到时所有执行的动作
            scanDevTimer.Stop();//启动计时
        }

        public void startTimer()
        {
            scanDevTimer.Start();
        }


        public void stopTimer()
        {
            scanDevTimer.Stop();
        }
        #endregion




        void scanDeviceTimer(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
        protected void AppendLog(ReciveModel model)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new AppendDevInofDelegate(AppendLog), new object[] { model });
                return;
            }
            else
            {
                if(addRowCheck(model))
                {
                    PopulateDataGridView(model);
                }
            }
        }

        public void IncommingData(byte[] data, int len)
        {
            string str = System.Text.Encoding.Default.GetString(data);
            Console.WriteLine(str);
        }

        public void OutgoingDataToUi(ReciveModel model)
        {
            AppendLog(model);
        }

        public void OutgoingData(byte[] data)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if(dataGridView1.CurrentRow == null)
            //{
            //    return;
            //}

            //var row = dataGridView1.CurrentRow.Index;



            SendModel model = new SendModel();
            model.cmd = DebugCommand.CommandScan;
            model.dev_mac = "";
            model.dhcp = false;
            model.ip = "";
            model.subnet_mask = "";
            model.gateway = "";

            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            model.timestamp = Convert.ToInt64(ts.TotalSeconds);
            //model.persist = false;

            foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
            {
                var command = new DebugCommand(model) { };
                var result = driver.Key.ExecuteGeneric(driver.Value, command);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var row = dataGridView1.CurrentRow.Index;
            string mac = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.MAC].Value.ToString();
            string dhcp = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.DHCP].Value.ToString();
            
            string ip = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.IP].Value.ToString();
            string subnetMask = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.SUBMASK].Value.ToString();
            string gateway = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.GATEWAY].Value.ToString();
            //string persist = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.PERSIST].Value.ToString();

            SendModel model = new SendModel();
            model.cmd = DebugCommand.CommandSetIP;
            model.dev_mac = mac;
            bool temp = false;
            bool.TryParse(dhcp, out temp);
            model.dhcp = temp;

            model.ip = ip;
            model.subnet_mask = subnetMask;
            model.gateway = gateway;

            //temp = false;
            //bool.TryParse(persist, out temp);
            //model.persist = temp;


            FormSetIP setIpFrom = new FormSetIP(LocalPLC.UserControl1.ucDebug, model);
            setIpFrom.StartPosition = FormStartPosition.CenterScreen;

            var result = setIpFrom.ShowDialog();
            if(result == DialogResult.OK)
            {

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var row = dataGridView1.CurrentRow.Index;
            //mac
            string mac = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.MAC].Value.ToString();
            string flicker = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.FILCKER].Value.ToString();

            SendModel model = new SendModel();
            if(flicker.ToUpper() == "false".ToUpper())
            {
                model.cmd = DebugCommand.CommandFlickerOn;
                button3.Text = "取消闪烁LED灯";
                dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.FILCKER].Value = true.ToString();
            }
            else
            {
                model.cmd = DebugCommand.CommandFlickerOff;
                button3.Text = "启动闪烁LED灯";
                dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.FILCKER].Value = false.ToString();
            }

            model.dev_mac = mac;
            model.dhcp = false;
            model.ip = "";
            model.subnet_mask = "";
            model.gateway = "";
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            model.timestamp = Convert.ToInt64(ts.TotalSeconds);
            //model.persist = false;
            foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
            {
                var command = new DebugCommand(model) { };
                var result = driver.Key.ExecuteGeneric(driver.Value, command);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var row = dataGridView1.CurrentRow.Index;
            //mac
            string mac = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.MAC].Value.ToString();
            string dhcp = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.DHCP].Value.ToString();

            SendModel model = new SendModel();
            model.cmd = DebugCommand.CommandRestart;
            model.dev_mac = mac;
            if(dhcp.ToUpper() == true.ToString().ToUpper())
            {
                model.dhcp = true;
            }
            else
            {
                model.dhcp = false;
            }

            string ip = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.IP].Value.ToString();
            string subnetMask = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.SUBMASK].Value.ToString();
            string gateway = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.GATEWAY].Value.ToString();
            model.ip = ip;
            model.subnet_mask = subnetMask;
            model.gateway = gateway;
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            model.timestamp = Convert.ToInt64(ts.TotalSeconds);


            foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
            {
                var command = new DebugCommand(model) { };
                var result = driver.Key.ExecuteGeneric(driver.Value, command);
            }

        }




        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            //FormSimpleLoading loadingfrm = new FormSimpleLoading(this);
            ////loadingfrm.StartPosition = FormStartPosition.CenterScreen;
            ////将Loaing窗口，注入到 SplashScreenManager 来管理
            //SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            //loading.ShowLoading();
            //try catch 包起来，防止出错
            try
            {
                var row = dataGridView1.CurrentRow.Index;
                //mac
                string ip = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.IP].Value.ToString();
                //------------使用ping类------
                string host = ip;
                Ping p1 = new Ping();
                PingReply reply = p1.Send(host); //发送主机名或Ip地址
                StringBuilder sbuilder;
                if (reply.Status == IPStatus.Success)
                {
                    sbuilder = new StringBuilder();
                    sbuilder.AppendLine(string.Format("Address: {0} ", reply.Address.ToString()));
                    sbuilder.AppendLine(string.Format("RoundTrip time: {0} ", reply.RoundtripTime));
                    sbuilder.AppendLine(string.Format("Time to live: {0} ", reply.Options.Ttl));
                    sbuilder.AppendLine(string.Format("Don't fragment: {0} ", reply.Options.DontFragment));
                    sbuilder.AppendLine(string.Format("Buffer size: {0} ", reply.Buffer.Length));
                    Console.WriteLine(sbuilder.ToString());
                    MessageBox.Show(sbuilder.ToString());
                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    Console.WriteLine("超时");
                    MessageBox.Show("超时");
                }
                else
                {
                    Console.WriteLine("失败");
                    MessageBox.Show("失败");
                }
            }
            catch (Exception)
            { /*可选处理异常*/ }
            //finally { loading.CloseWaitForm(); }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;  //设置列标题不换行
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 9;

            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[(int)COLUMN_NAME.IP].Name = "IP";
            dataGridView1.Columns[(int)COLUMN_NAME.MAC].Name = "Mac";
            dataGridView1.Columns[(int)COLUMN_NAME.SUBMASK].Name = "子网掩码";
            dataGridView1.Columns[(int)COLUMN_NAME.GATEWAY].Name = "网关";
            dataGridView1.Columns[(int)COLUMN_NAME.DEVID].Name = "设备ID";
            dataGridView1.Columns[(int)COLUMN_NAME.FIRMVER].Name = "固件版本";
            dataGridView1.Columns[(int)COLUMN_NAME.DHCP].Name = "DHCP";
            dataGridView1.Columns[(int)COLUMN_NAME.ACK].Name = "ACK";

            dataGridView1.Columns[(int)COLUMN_NAME.DHCP].Visible = true;
            dataGridView1.Columns[(int)COLUMN_NAME.ACK].Visible = true;
            dataGridView1.Columns[(int)COLUMN_NAME.FILCKER].Visible = false;
            //dataGridView1.Columns[(int)COLUMN_NAME.PERSIST].Visible = false;

            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.ReadOnly = true;

        }


        /// <summary>
        /// 时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime GetDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }

        private bool addRowCheck(ReciveModel model)
        {
            var count = dataGridView1.Rows.Count;

            var dateTime =  GetDateTime(model.timestamp).ToString();

            bool isAdd = true;

            for (int i = 0; i < count; i++)
                {
                    var row = dataGridView1.Rows[i];
                    if (model.dev_mac == row.Cells[(int)COLUMN_NAME.MAC].Value.ToString())
                    {
                        row.Cells[(int)COLUMN_NAME.IP].Value = model.ip;
                        row.Cells[(int)COLUMN_NAME.MAC].Value = model.dev_mac;
                        row.Cells[(int)COLUMN_NAME.SUBMASK].Value = model.subnet_mask;
                        row.Cells[(int)COLUMN_NAME.GATEWAY].Value = model.gateway;
                        row.Cells[(int)COLUMN_NAME.DEVID].Value = model.dev_id;

                        row.Cells[(int)COLUMN_NAME.FIRMVER].Value = model.firm_ver;
                        row.Cells[(int)COLUMN_NAME.DHCP].Value = model.dhcp;
                        row.Cells[(int)COLUMN_NAME.ACK].Value = model.ack;

                    isAdd = false;
                    }
                }

            return isAdd;
        }

        private void PopulateDataGridView(ReciveModel model)
        {

            string[] row0 = { model.ip, model.subnet_mask,
            model.gateway, model.dev_mac, model.dev_id, model.firm_ver, model.dhcp.ToString(), model.ack, false.ToString(),  false.ToString()};


            dataGridView1.Rows.Add(row0);
            MessageBox.Show("TEST");
        }

    }
}
