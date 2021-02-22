using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient {
    internal class Program {
        public static void Main(string[] args) {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            // 소켓 객체 생성. 첫 번째 파라미터는 IP를 사용함, 두 번째 파라미터는 Stream 소켓을 사용한다는 것이고, 
            // 세 번째 파라미터는 TCP 프로토콜을 사용한다는 것이다.

            var ep = new IPEndPoint(IPAddress.Parse("192.168.219.106"), 7000);
            // 소켓에 연결하기 위해 IPEndPoint 객체를 생성한다.
            sock.Connect(ep);
            // 소켓 연결

            string cmd = string.Empty;
            byte [] receiverBuff = new byte[8192];
            
            Console.WriteLine("Connected... Enter Q to Exit");

            while ((cmd = Console.ReadLine()) != "Q") {
                byte [] buff = Encoding.UTF8.GetBytes(cmd);

                sock.Send(buff, SocketFlags.None);
                // 소켓에 데이터를 보내기 위해 Send 메서드 사용

                int n = sock.Receive(receiverBuff);
                // 데이터를 수신하기 위해 Receive 메서드 사용

                string data = Encoding.UTF8.GetString(receiverBuff, 0, n);

                Console.WriteLine(data);
            }

            sock.Close();
        }
    }
}