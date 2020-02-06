using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace Service
{
    public partial class Service1 : ServiceBase
    {
        static DateTime _updateTime = DateTime.Now;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var connectingThread = new Thread(() =>
            {
                var ipPoint = new IPEndPoint(IPAddress.Any, 8005);
                var tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Bind(ipPoint);
                tempSocket.Listen(10);

                while (true)
                {
                    var newThread = new Thread(SocketEvents) { IsBackground = true };
                    newThread.Start(tempSocket.Accept());
                }

                // ReSharper disable once FunctionNeverReturns
            }) { IsBackground = true };
            connectingThread.Start();
        }

        protected override void OnStop()
        {
        }

        static void SocketEvents(object o)
        {
            var socket = o as Socket;
            while (true)
            {
                var data = new byte[4];

                try
                {
                    int value;
                    do
                    {
                        socket?.Receive(data);
                        value = BitConverter.ToInt32(data, 0);
                    }
                    while (socket != null && socket.Available > 0);

                    switch (value)
                    {
                        case 0:
                        {
                            var msgSingle = Encoding.Unicode.GetBytes(_updateTime.ToString(CultureInfo.InvariantCulture));
                            socket?.Send(msgSingle);
                            break;
                        }
                        case 1:
                            _updateTime = DateTime.Now;
                            break;
                    }
                }
                catch
                {
                    socket?.Close();
                    return;
                }
            }
        }
    }
}
