using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EI.SI;
using Newtonsoft.Json;

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
                    // Replace this with your message check logic
                    var receivedObj = await CheckForMessageFromServer(stream);

                    if (receivedObj != null)
                    {
                        parent.MensageFromServer(receivedObj);
                    }

                    await Task.Delay(100); // Wait 0.1 seconds before checking again
                }
            });
        }

        public static void Stop()
        {
            _listening = false;
        }

        private static async Task<Dictionary<string, string>> CheckForMessageFromServer(NetworkStream stream)
        {
            ProtocolSI protocol = new ProtocolSI();
            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
            if (bytesRead > 0)
            {
                var receivedObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(protocol.GetStringFromData());
                return receivedObj;
            }
            else
            {
                return null;
            }
        }
    }

}
