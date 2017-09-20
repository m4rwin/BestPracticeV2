using System;
using System.Net;
using System.Net.NetworkInformation;

namespace NetworkAndWeb
{
    public class Network
    {
        public static IPAddress[] GetHostAddress(string host)
        {
            return Dns.GetHostAddresses(host);
        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static PingReply MyPing(string host)
        {
            Ping p = new Ping();
            return p.Send(host);
        }

        public static void GetHost()
        {
            Console.Write("Zadej www adresu: ");
            string host = Console.ReadLine();

            IPAddress[] addresses = Dns.GetHostAddresses(host);
            Console.WriteLine("Pocet adres: {0}", addresses.Length);
            foreach (IPAddress address in addresses)
            {
                Console.WriteLine(address);
            }
            Console.WriteLine();
        }
    }
}
