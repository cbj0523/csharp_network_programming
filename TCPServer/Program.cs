using System.Net;
using System.Net.Sockets;

namespace TCPServer {
    internal class Program {
        public static void Main(string[] args) {
            TcpListener listener = new TcpListener(IPAddress.Any, 7000); // 7000 포트 listen
            listener.Start(); 

            byte[] buff = new byte[1024]; // 들어오는 데이터를 받은 buff

            while (true) {
                TcpClient tc = listener.AcceptTcpClient(); // TCP Connection을 받아들여 새로운 tcp client 객체를 생성

                NetworkStream stream = tc.GetStream(); // 위 객체에서 NetworkStream을 가져옴

                int nbytes; // 클라이언트가 연결을 끊을 때까지 통신
                while ((nbytes = stream.Read(buff, 0, buff.Length)) > 0) {
                    stream.Write(buff, 0, nbytes); // 데이터 송신
                }

                stream.Close();
                tc.Close();
            }
        }
    }
}