using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EI.SI;

namespace Server
{
    internal class Program
    {
        static TcpListener server = new TcpListener(IPAddress.Any, 50001);

        static void Main(string[] args)
        {
            server = new TcpListener(IPAddress.Any, 50001);
            server.Start();
            Console.WriteLine("Server started. Waiting for client...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Thread thread = new Thread(() => ThreadServer(client));
                thread.Start();
            }
        }

        static private void ThreadServer(TcpClient client)
        {
            Console.WriteLine("Client Connectado");

            NetworkStream stream = client.GetStream();
            ProtocolSI protocol = new ProtocolSI();

            // Criação do objeto para guardar info do cliente
            ClientInfo clientInfo = new ClientInfo();

            while (true)
            {
                int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

                if (bytesRead > 0)
                {
                    if (protocol.GetCmdType() == ProtocolSICmdType.DATA)
                    {
                        string receivedData = protocol.GetStringFromData();

                        // Se ainda não temos chave pública, assumimos que esta é a primeira mensagem
                        if (clientInfo.PublicKeyXml == null)
                        {
                            clientInfo.PublicKeyXml = receivedData;
                            Console.WriteLine("Chave pública do cliente recebida:");
                            Console.WriteLine(clientInfo.PublicKeyXml);

                            // Envia confirmação
                            byte[] ack = protocol.Make(ProtocolSICmdType.ACK);
                            stream.Write(ack, 0, ack.Length);
                        }
                        else
                        {
                            Console.WriteLine($"Mensagem recebida: {receivedData}");

                            // Aqui podes futuramente validar assinatura, etc.

                            // Envia confirmação
                            byte[] ackPacket = protocol.Make(ProtocolSICmdType.DATA, "Recebido");
                            stream.Write(ackPacket, 0, ackPacket.Length);
                        }
                    }
                }
            }
        }

    }
}
