using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient {
    internal class Program {
        public static void Main(string[] args) {
            UdpClient cli = new UdpClient(); // UDP Client 객체 생성

            string msg = "안녕하세요"; // 보낼 데이터 생성

            byte[] datagram = Encoding.UTF8.GetBytes(msg); // 바이트 인코딩

            cli.Send(datagram, datagram.Length, "127.0.0.1", 7777); // 서버에 전송
            Console.WriteLine("[Send] 127.0.0.1:7777 | {0} byte sended", datagram.Length); // 보낸 데이터 출력

            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0); // 이후 클라이언트로 들어오는 모든 데이터를 받음
            byte[] bytes = cli.Receive(ref epRemote); // 서버로부터 데이터를 받음
            Console.WriteLine("[Receive] {1} Received from {0}", epRemote.ToString(), bytes.Length); // 받은 데이터를 출력
            
            cli.Close();
        }
    }
}