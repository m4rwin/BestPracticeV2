using System;

namespace NetworkAndWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string host = Network.GetHostName();
            Console.WriteLine("[Host: {0}]", host);
            foreach(IPAddress address in Network.GetHostAddress(host))
            {
                Console.WriteLine("-->{0} [{1}]", address, address.AddressFamily);
            }

            Console.WriteLine();
            Console.Write("Insert host name: ");
            host = Console.ReadLine();
            PingReply reply = Network.MyPing(host);
            Console.WriteLine("address: {0}", reply.Address.ToString());
            Console.WriteLine("dont fragment: {0}, TTL: {1}", reply.Options.DontFragment, reply.Options.Ttl);
            Console.WriteLine("roundtrip: {0} ms", reply.RoundtripTime);
            Console.WriteLine("status: {0}", reply.Status);

            Console.ReadLine();
            */

            Console.WriteLine("Press 1 for start server OR 2 for start client: ");
            string option = Console.ReadLine();

            if (option.Equals("1"))
            {
                string server = "127.0.0.11";
                int port = 3366;
                Console.WriteLine("[{0}:{1}] Starting Server...", server, port);
                TCPIP_Server.StartServer(server, port);
            }
            else if (option.Equals("2"))
            {
                Console.WriteLine("Connect to server: ");
                string server = Console.ReadLine();
                Console.WriteLine("Port: ");
                string port = Console.ReadLine();
                Console.WriteLine("Starting Client...");
                TCPIP_Client.StartClient(server, Int32.Parse(port));
            }
            else
            {
                Console.WriteLine("Wrong options, try it againg.");
            }
        }
    }
}
