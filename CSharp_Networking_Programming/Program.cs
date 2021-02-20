using System;
using System.Net;

namespace CSharp_Networking_Programming
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry("www.google.com");
            
            Console.WriteLine(hostEntry.HostName);

            foreach (IPAddress ip in hostEntry.AddressList) {
                Console.WriteLine(ip);
            }

            foreach (string allies in hostEntry.Aliases) {
                Console.WriteLine(allies);
            }

            string hostname = Dns.GetHostName();
            
            foreach (IPAddress ip in Dns.GetHostEntry(hostname).AddressList) {
                Console.WriteLine(ip);
            }
        }
    }
}
