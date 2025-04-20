using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EI.SI;

namespace Cliente1
{
    public class MessageListener
    {
        ProtocolSI protocol = new ProtocolSI();
        NetworkStream stream;
        private static bool _listening = false;

        public static void Start()
        {
            if (_listening) return;
            _listening = true;

            Task.Run(async () =>
            {
                while (_listening)
                {
                    try
                    {
                        // Replace this with your message check logic
                        string newMessage = await CheckForMessageFromServer();

                        if (!string.IsNullOrEmpty(newMessage))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"[New message from server]: {newMessage}");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Listener Error]: {ex.Message}");
                    }

                    await Task.Delay(5000); // Wait 5 seconds before checking again
                }
            });
        }

        public static void Stop()
        {
            _listening = false;
        }

        private static async Task<string> CheckForMessageFromServer()
        {
            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

            if (bytesRead > 0)
            {
                lbServerResponse.Text = $"Resposta do Servidor: {protocol.GetStringFromData()}";
            }
            return protocol.GetStringFromData();
        }
    }

}
