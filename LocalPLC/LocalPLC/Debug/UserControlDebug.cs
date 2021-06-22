using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using DebugLib;
using DebugLib.Protocols;
using JsonSerializerAndDeSerializer;
using System.Net.NetworkInformation;
using System.Net;

namespace LocalPLC.Debug
{
    public class UserControlDebug
    {
        public UserControlDebug()
        {
            Socket _socketServer;
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socketServer.Bind(new IPEndPoint(IPAddress.Any, DebugPort.PortHost));
            //create a server driver
            var udpServer = new DebugServer(new DebugTcpCodec()) { Address = 1 };
            udpServer.OutgoingData += ucDebugIP.OutgoingData;
            udpServer.IncommingData += ucDebugIP.IncommingData;
            udpServer.OutgoingDataToUi += ucDebugIP.OutgoingDataToUi;
            //listen for an incoming request
            _listener = _socketServer.GetUdpListener(udpServer);
            _listener.ServeCommand += null;
            _listener.Start();

            initClient();
        }

        ~UserControlDebug()
        {
            //unInit();
        }

        void initClient()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ipaddr = "";
            var iep1 = new IPEndPoint(IPAddress.Broadcast, DebugPort.PortDevice);
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    soketClientList.Add(socket2);

                    var iep2 = new IPEndPoint(ip, 21327);
                    socket2.Bind(iep2);
                    byte[] test2 = Encoding.Unicode.GetBytes("test");
                    socket2.SetSocketOption(SocketOptionLevel.Socket,
SocketOptionName.Broadcast, 1);
                    socket2.Connect(/*new IPEndPoint(IPAddress.Broadcast, 555)*/ iep1);
                    var _portClient = socket2.GetClient();
                    var _driver = new DebugClient(new  DebugTcpCodec()) { };
                    driverList.Add(_driver);
                    driverDic.Add(_driver, _portClient);
                    _driver.OutgoingData += null;
                    _driver.IncommingData += null;

                    var command = new DebugCommand("scan", "AB.CD.EF.GH") {  };
                    var result = _driver.ExecuteGeneric(_portClient, command);

                    //socket2.SendTo(test2, iep1);
                }
            }
        }
        public void unInit()
        {
            if (_listener != null)
            {
                _listener.Abort();
                _listener = null;
            }

            if (_socket != null)
            {
                _socket.Close();
                _socket.Dispose();
                _socket = null;
            }

            if(soketClientList.Count > 0)
            {
                foreach(var soket in soketClientList)
                {
                    if(soket != null)
                    {
                        soket.Close();
                        soket.Dispose();
                    }
                }
            }
        }

        #region
        Socket _socket = null;
        ICommServer _listener = null;
        public List<DebugClient> driverList = new List<DebugClient>();
        public List<Socket> soketClientList = new List<Socket>();

        public Dictionary<DebugClient, ICommClient> driverDic = new Dictionary<DebugClient, ICommClient>();


        public UserControlDebugIP ucDebugIP = new UserControlDebugIP();

        #endregion
    }
}
