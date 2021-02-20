using System;
using System.Net;
using System.Net.Sockets;

namespace UDPServer {
    internal class Program {
        public static void Main(string[] args) {
            UdpClient srv = new UdpClient(7777);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (true) {
                byte[] datagram = srv.Receive(ref remoteEP);
                Console.WriteLine("[Receive] {1} byte received from {0}", remoteEP.ToString(), datagram.Length);

                srv.Send(datagram, datagram.Length, remoteEP);
                Console.WriteLine("[Send] {1} byte sended to {0}", remoteEP.ToString(), datagram.Length);
            }
        }
    }
}