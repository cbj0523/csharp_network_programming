using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer {
    internal class Program {
        public static void Main(string[] args) {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Socket 객체 생성

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 7000);
            // 들어오는 모든 접속을 받아들이기 위해 ep 설정
            
            sock.Bind(ep);
            // 어떤 포트를 사용할지 객체에 바인드함

            sock.Listen(10);
            // backlog는 동시에 여러 클라이언트가 접속했을 때 최대로 대기할 수 있는 클라이언트의 수를 뜻함.

            Socket clientSock = sock.Accept();
            // socket에 요청이 들어온다면 Accept

            byte[] buff = new byte[8192];
            // 데이터를 송수신하기 위한 버퍼

            while ((!Console.KeyAvailable) & (Encoding.UTF8.GetString(buff, 0, clientSock.Receive(buff)) != "Q")) {
                int n = clientSock.Receive(buff); 
                // 서버에 들어오는 데이터를 받음

                string data = Encoding.UTF8.GetString(buff, 0, n);
                Console.WriteLine($"[Received] {DateTime.Now} | {data}");
                // 받은 데이터를 인코딩해서 출력 
                
                clientSock.Send(buff, 0, n, SocketFlags.None);
                // 받은 데이터를 다시 출력
            }

            clientSock.Close();
            sock.Close();
        }
    }
}