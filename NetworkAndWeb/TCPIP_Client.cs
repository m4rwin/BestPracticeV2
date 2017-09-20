using System;
using System.Net.Sockets;
using System.Text;

namespace NetworkAndWeb
{
    public class TCPIP_Client
    {
        public static void StartClient(string server, int port)
        {
            Console.WriteLine("Send bye for close connection.");
            TcpClient client = new TcpClient(server, port);
            bool done = false;

            while (!done)
            {
                Console.WriteLine("Type message:");
                string msg = Console.ReadLine();
                SendMessage(client, msg);
                string response = ReadResponse(client);
                Console.WriteLine("Response: {0}", response);
                done = response.Equals("BYE");
            }
        }

        private static void SendMessage(TcpClient client, string message)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        private static string ReadResponse(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 0;

            do
            {
                int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;
            }
            while(client.GetStream().DataAvailable);
            return Encoding.Unicode.GetString(buffer, 0, buffer.Length);
        }
    }
}
