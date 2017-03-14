using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TestControl.Net.Interfaces;

namespace TestControl.Extension.Example
{
    public class CommandExtension //: ITestControlExtension
    {
        private static bool clientStarted;
        public void LoadExtension(IntPtr ptr, uint processId)
        {
            if (!clientStarted)
            {
                clientStarted = true;
                var ctThread = new Thread(StartClient);
                ctThread.Start();

            }
        }


        public static void StartClient()
        {
            
            var bytes = new byte[1024];

            try
            {

#pragma warning disable 612,618
                var ipHostInfo = Dns.Resolve("localhost");
#pragma warning restore 612,618
                var ipAddress = ipHostInfo.AddressList[0];
                var localEndPoint = new IPEndPoint(ipAddress, 7070);

                var listener = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();
                    var data = string.Empty;

                    // An incoming connection needs to be processed.
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOP>") > -1)
                        {
                            //run command
                            //handler.Send()
                        }
                    }
 

//                    handler.Send(msg);
//                    handler.Shutdown(SocketShutdown.Both);
//                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

}
