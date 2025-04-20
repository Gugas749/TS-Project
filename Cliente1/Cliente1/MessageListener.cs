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
        private static bool _listening = false;

        public static void Start(NetworkStream stream, Form1 parent)
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
                        string newMessage = await CheckForMessageFromServer(stream);

                        if (!string.IsNullOrEmpty(newMessage))
                        {
                            parent.lbServerResponse.Text = $"Resposta do Servidor: {newMessage}";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Listener Error]: {ex.Message}");
                    }

                    await Task.Delay(1000); // Wait 1 seconds before checking again
                }
            });
        }

        public static void Stop()
        {
            _listening = false;
        }

        private static async Task<string> CheckForMessageFromServer(NetworkStream stream)
        {
            ProtocolSI protocol = new ProtocolSI();
            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
            string text = "empty";
            if (bytesRead > 0)
            {
                text = protocol.GetStringFromData();
            }
            return text;
        }
    }

}
