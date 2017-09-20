using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetworkAndWeb
{
    public class TCPIP_Server
    {
        public static void StartServer(string serverHost, int serverPort)
        {
            IPAddress localhost = IPAddress.Parse(serverHost);
            TcpListener listener = new TcpListener(localhost, serverPort);
            listener.Start();

            while (true)
            {
                Console.Write("Waiting for connection. ");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected: {0}", client.Connected);
                Thread thread = new Thread(new ParameterizedThreadStart(HandleClientThread));
                thread.Start(client);
            }
        }

        private static void HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            bool done = false;

            while (!done)
            {
                string recieved = ReadMessage(client);
                Console.WriteLine("Recieved: {0}", recieved);
                done = recieved.Equals("bye");

                if (done) { SendResponse(client, "BYE"); }
                else { SendResponse(client, "OK"); }
            }

            client.Close();
            Console.WriteLine("Connection closed.");
        }

        private static string ReadMessage(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 0;

            do
            {
                int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;

            } while (client.GetStream().DataAvailable);

            return Encoding.Unicode.GetString(buffer, 0, totalRead);
        }

        private static void SendResponse(TcpClient client, string message)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}
