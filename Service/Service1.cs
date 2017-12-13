using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public partial class Service1 : ServiceBase
    {
        static DateTime UpdateTime = DateTime.Now;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Этот поток будет ждать подключения пользователей
            var ConnectingThread = new Thread(() =>
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8005);
                var tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Bind(ipPoint);
                tempSocket.Listen(10);
                //Console.WriteLine("Сервер готов подключать пользователей...");

                while (true)
                {
                    var NewThread = new Thread(SocketEvents);
                    NewThread.IsBackground = true;
                    NewThread.Start(tempSocket.Accept());
                    //Console.WriteLine("К нам кто-то подключился!");
                }
            });
            ConnectingThread.IsBackground = true;
            ConnectingThread.Start();

            //Console.WriteLine("Сервер запущен и вроде как работает...");
            //while (true) ;
        }

        protected override void OnStop()
        {
        }

        static void SocketEvents(object o)
        {
            var socket = o as Socket;
            while (true)
            {
                byte[] data = new byte[4];
                int value = 0;

                try
                {
                    do
                    {
                        socket.Receive(data);
                        value = BitConverter.ToInt32(data, 0);
                    }
                    while (socket.Available > 0);

                    switch (value)
                    {
                        case 0:
                            var msgSingle = Encoding.Unicode.GetBytes(UpdateTime.ToString());
                            socket.Send(msgSingle);
                            break;
                        case 1:
                            UpdateTime = DateTime.Now;
                            break;
                    }
                }
                catch
                {
                    socket.Close();
                    //Console.WriteLine("Кто-то отключился!");
                    return;
                }
            }
        }
    }
}
