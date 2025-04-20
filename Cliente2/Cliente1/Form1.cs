using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EI.SI;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cliente1
{
    public partial class Form1 : Form
    {
        TcpClient client = new TcpClient();
        ProtocolSI protocol = new ProtocolSI();
        NetworkStream stream;
        string serverPublicKey = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect("127.0.0.1", 50001);
            stream = client.GetStream();

            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
            serverPublicKey = protocol.GetStringFromData();
            lbServerResponse.Text = serverPublicKey;
        }

        private void butLoginAuth_Click(object sender, EventArgs e)
        {
            string key = KeyManager.GetPublicKey();
            var dataToSend = new
            {
                username = KeyManager.EncryptWithPublicKey(textBoxUsernameAuth.Text.ToString().Trim(), serverPublicKey),
                password = KeyManager.EncryptWithPublicKey(textBoxPasswordAuth.Text.ToString().Trim(), serverPublicKey),
                publicKey = key
            };
            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);

            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

            if (bytesRead > 0)
            {
                lbServerResponse.Text = $"Resposta do Servidor: {protocol.GetStringFromData()}";
            }
        }

        private void butSendMSG_Click(object sender, EventArgs e)
        {
            var dataToSend = new
            {
                mensage = KeyManager.EncryptWithPublicKey(txtBoxMSGToSend.Text.ToString().Trim(), serverPublicKey),
                destination = txtBoxDestination.Text.Trim()
            };

            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);

            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

            if (bytesRead > 0)
            {
                lbServerResponse.Text = $"Resposta do Servidor: {protocol.GetStringFromData()}";
            }
        }

        //Background listenner
        private bool _isListening = true;

        private void StartListening(NetworkStream stream)
        {
            Task.Run(() =>
            {
                while (_isListening)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            var receivedObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

                            // 👇 Invoke UI update on the main thread (important if you're in WinForms/WPF)
                            this.Invoke(new Action(() =>
                            {
                                lbServerResponse.Text = $"Resposta do Servidor: {receivedObj["response"]}";
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        _isListening = false;
                    }
                }
            });
        }

    }
}
