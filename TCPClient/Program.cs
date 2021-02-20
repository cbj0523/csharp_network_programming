using System;
using System.Net.Sockets;
using System.Text;

namespace TCPClient {
    internal class Program {
        public static void Main(string[] args) {
            TcpClient tc = new TcpClient("127.0.0.1", 7000); // 해당 host와 포트로 tcp 연결 구성
            string msg = "hello world"; // 전달할 내용
            byte[] buff = Encoding.ASCII.GetBytes(msg); // msg를 버퍼화

            NetworkStream stream = tc.GetStream(); // 네트워크 스트림 리턴. 이를 통해서 송수신 하게 됨. 
            
            stream.Write(buff, 0, buff.Length); // byte들을 서버로 보냄. 데이터 송수신은 보통 바이트로 진행되는데, 이를 위해 위에서 msg를 바이트화 한 것임.

            byte[] outbuf = new byte[1024]; // 서버에서 들어오는 데이터를 받기 위해 새로운 버퍼 생성
            int nbytes = stream.Read(outbuf, 0, outbuf.Length); // 서버로부터 버퍼를 읽어옴
            string output = Encoding.ASCII.GetString(outbuf, 0, nbytes); // 읽은 버퍼를 다시 문자화

            stream.Close(); // 통신 종료
            tc.Close();
            
            Console.WriteLine($"{nbytes} bytes: {output}"); // output 출력
        }
    }
}