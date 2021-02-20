using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TAPAsyncServer {
    internal class Program {
        public static void Main(string[] args) {
            AsyncEchoServer().Wait();
        }

        async static Task AsyncEchoServer() {
            TcpListener listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();
            while (true) {
                TcpClient tc = await listener.AcceptTcpClientAsync().ConfigureAwait(false);

                Task.Factory.StartNew(AsyncTcpProcess, tc);
            }
        }

        async static void AsyncTcpProcess(object o) {
            TcpClient tc = (TcpClient) o;
            int MAX_SIZE = 1024;
            NetworkStream stream = tc.GetStream();

            var buff = new byte[MAX_SIZE];
            var nbytes = await stream.ReadAsync(buff, 0, buff.Length).ConfigureAwait(false);
            if (nbytes > 0) {
                string msg = Encoding.ASCII.GetString(buff, 0, nbytes);
                Console.WriteLine($"{msg} at {DateTime.Now}");

                await stream.WriteAsync(buff, 0, nbytes).ConfigureAwait(false);
            }

            stream.Close();
            tc.Close();
        }
    }
}