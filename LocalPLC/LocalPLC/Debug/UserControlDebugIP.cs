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
using LocalPLC.loadingscreen;
using System.Net;
using System.Drawing.Drawing2D;

namespace LocalPLC.Debug
{
    public partial class UserControlDebugIP : UserControl
    {
        enum COLUMN_NAME {STATUS, IP, SUBMASK, GATEWAY, MAC, DEVID,  FIRMVER, DHCP, ACK, FILCKER/*, PERSIST*/}
        Timer scanDevTimer = null;

        public DataTable GetTable()
        {
            // Here we create a DataTable with four columns.  
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("ImageName", typeof(string));

            dataGridView1.AllowUserToAddRows = false;

            // Here we add 5 DataRows.  
            table.Rows.Add(2, "Pankaj", "PankajImg");
            table.Rows.Add(3, "Manoj", "ManojImg");
            table.Rows.Add(4, "DigVijay", "DigVijayImg");
            table.Rows.Add(5, "Sumit", "SumitImg");
            table.Rows.Add(6, "Varun", "VarunImg");
            return table;
        }

        public UserControlDebugIP()
        {
            InitializeComponent();
            SetupDataGridView();
            //dataGridView1.DataSource = GetTable();
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
            stopTimer();

            LocalPLC.UserControl1.ucDebug.checkNetCount();
            //if(dataGridView1.CurrentRow == null)
            //{
            //    return;
            //}

            //var row = dataGridView1.CurrentRow.Index;

            dataGridView1.Rows.Clear();
            button3.Text = "启动闪烁LED灯";

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

            try
            {
                bool error = false;
                foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
                {
                    var command = new DebugCommand(model) { };
                    var result = driver.Key.ExecuteGeneric(driver.Value, command);
                    LoggerService.Log.Info(model.cmd + model.dev_mac + " ret:" + result.Status.ToString());
                    if (result.Status == DebugLib.CommResponse.Critical)
                    {
                        error = true;
                    }
                }

                if(error)
                {
                    MessageBox.Show("IP地址修改出现问题，扫描服务重新启动，请重新操作!");
                    LocalPLC.UserControl1.ucDebug.unitClientSoket();
                    LocalPLC.UserControl1.ucDebug.initClient();
                }


            }
            catch(Exception except)
            {
                LoggerService.Log.Fatal(model.cmd, except);
            }

            startTimer();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            stopTimer();

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

            startTimer();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            stopTimer();

            var row = dataGridView1.CurrentRow.Index;
            //mac
            string mac = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.MAC].Value.ToString();
            string flicker = dataGridView1.CurrentRow.Cells[(int)COLUMN_NAME.FILCKER].Value.ToString();

            SendModel model = new SendModel();
            if(button3.Text == "启动闪烁LED灯")
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

            try
            {
                bool error = false;
                foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
                {
                    var command = new DebugCommand(model) { };
                    var result = driver.Key.ExecuteGeneric(driver.Value, command);

                    if (result.Status == DebugLib.CommResponse.Critical)
                    {
                        error = true;
                    }

                    LoggerService.Log.Info(model.cmd + model.dev_mac + " ret:" + result.Status.ToString());
                }

                if (error)
                {
                    MessageBox.Show("IP地址修改出现问题，扫描服务重新启动，请重新操作!");
                    LocalPLC.UserControl1.ucDebug.unitClientSoket();
                    LocalPLC.UserControl1.ucDebug.initClient();
                }
            }
            catch(Exception except)
            {
                LoggerService.Log.Fatal(model.cmd + model.dev_mac, except);
            }

            startTimer();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }


            stopTimer();

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

            try
            {
                bool error = false;
                foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
                {
                    var command = new DebugCommand(model) { };
                    var result = driver.Key.ExecuteGeneric(driver.Value, command);
                    if (result.Status == DebugLib.CommResponse.Critical)
                    {
                        error = true;
                    }
                    LoggerService.Log.Info(model.cmd + model.dev_mac + " ret:" + result.Status.ToString()); 
                }

                if (error)
                {
                    MessageBox.Show("IP地址修改出现问题，扫描服务重新启动，请重新操作!");
                    LocalPLC.UserControl1.ucDebug.unitClientSoket();
                    LocalPLC.UserControl1.ucDebug.initClient();
                }
            }
            catch(Exception except)
            {
                LoggerService.Log.Fatal(model.cmd + model.dev_mac, except);
            }

            startTimer();
        }




        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }


            stopTimer();

            FormSimpleLoading loadingfrm = new FormSimpleLoading(this);
            //loadingfrm.StartPosition = FormStartPosition.CenterScreen;
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
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
                    loading.CloseWaitForm();
                    //MessageBox.Show(sbuilder.ToString());

                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    Console.WriteLine("超时");

                    loading.CloseWaitForm();
                    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.YellowGreen;
                    dataGridView1.SelectedRows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = Color.YellowGreen;

                }
                else
                {
                    Console.WriteLine("失败");

                    //MessageBox.Show("失败");
                }
            }
            catch (Exception)
            { /*可选处理异常*/ }
            finally {/* loading.CloseWaitForm();*/ }

            startTimer();
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;  //设置列标题不换行
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 10;

            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[(int)COLUMN_NAME.STATUS].Name = "Status";
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

            string[] row0 = { "0", model.ip, model.subnet_mask,
            model.gateway, model.dev_mac, model.dev_id, model.firm_ver, model.dhcp.ToString(), model.ack, false.ToString(),  false.ToString()};


            dataGridView1.Rows.Add(row0);

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    // This image control use to place over cell with the help of drawImage function.  
                    Image imgForGridCell = null;
                    // Check the column where we need to place the image.  
                    if (dataGridView1.Columns[e.ColumnIndex].Name.Contains("Status"))
                    {
                        // Check the data of cell of column ImageName  
                        // On the bases of cell data, we will get the specific image from ImageList control.  
                        if (e.Value.ToString().Equals("0"))
                        {
                        // Getting image from ImageList control "imageListOfMembers" and assiging it to image control "imgForGridCell"  
                        imgForGridCell = Properties.Resources.client;

                        }
                        else if(e.Value.ToString().Equals("PankajImg")) {
                            imgForGridCell = Properties.Resources.LocalPLC24P;
                        }
                        else if(e.Value.ToString().Equals("ManojImg")) {
                            imgForGridCell = Properties.Resources.loading3;
                        }
                        else if(e.Value.ToString().Equals("DigVijayImg")) {
                            imgForGridCell = Properties.Resources.loading3;
                        }
                        else if(e.Value.ToString().Equals("SumitImg")) {
                            imgForGridCell = Properties.Resources.loading3;
                        }
                        else if(e.Value.ToString().Equals("VarunImg")) {
                            imgForGridCell = Properties.Resources.loading3;
                        }
                        if (imgForGridCell != null)
                        {
                            SolidBrush gridBrush = new SolidBrush(dataGridView1.GridColor);
                            Pen gridLinePen = new Pen(gridBrush);
                            SolidBrush backColorBrush = new SolidBrush(e.CellStyle.BackColor);
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                            // Draw lines over cell  
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        // Draw the image over cell at specific location.  
                        Size size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                         var newImage = resizeImage(imgForGridCell, size);
                            e.Graphics.DrawImage(newImage, e.CellBounds.Location);
                        dataGridView1.Rows[e.RowIndex].Cells["Status"].ReadOnly = true; // make cell readonly so below text will not dispaly on double click over cell.  
                        }
                        e.Handled = true;
                    }
            }
        }
    }
}
