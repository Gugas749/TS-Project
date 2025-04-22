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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cliente1
{
    public partial class Form1 : Form
    {
        TcpClient client = new TcpClient();
        ProtocolSI protocol = new ProtocolSI();
        NetworkStream stream;
        string serverPublicKey = "";
        public static Form1 THIS;

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
            lbServerResponse.Text = "Resposta do Servidor: Server Public Key received.";

            THIS = this;
            MessageListener.Start(stream, this);
        }

        private void butLoginAuth_Click(object sender, EventArgs e)
        {
            string key = KeyManager.GetPublicKey();
            var dataToSend = new
            {
                username = KeyManager.EncryptWithPublicKey(textBoxUsernameAuth.Text.ToString().Trim(), serverPublicKey),
                password = KeyManager.EncryptWithPublicKey(textBoxPasswordAuth.Text.ToString().Trim(), serverPublicKey),
                isLoggingIn = "YES",
                publicKey = key
            };
            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);

            // int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

            //if (bytesRead > 0)
            //{
            //  lbServerResponse.Text = $"Resposta do Servidor: {protocol.GetStringFromData()}";
            //}
        }

        private void butSendMSG_Click(object sender, EventArgs e)
        {
            string key = KeyManager.GetPublicKey();
            var dataToSend = new
            {
                mensage = KeyManager.EncryptWithPublicKey(txtBoxMSGToSend.Text.ToString().Trim(), serverPublicKey),
                destination = txtBoxDestination.Text.Trim(),
                isLoggingIn = "NO",
                publicKey = key
            };

            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);
        }

        //Background listenner

        public void MensageFromServer(Dictionary<string, string> receivedObj)
        {
            switch (receivedObj["type"])
            {
                case "server-response":
                    if (label1.InvokeRequired)
                    {
                        lbServerResponse.Invoke(new Action(() => lbServerResponse.Text = $"Resposta do Servidor: {receivedObj["response"]}"));
                    }
                    else
                    {
                        lbServerResponse.Text = $"Resposta do Servidor: {receivedObj["response"]}";
                    }
                    break;
                case "mensage":
                    string aux = KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["From"]));
                    string text = aux + ": " + KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["mensage"]));
                    if (listViewDirect.InvokeRequired)
                    {
                        listViewDirect.Invoke(new Action(() => listViewDirect.Items.Add(text)));
                    }
                    else
                    {
                        listViewDirect.Items.Add(text);
                    }
                    break;
            }
        }

        private void txtBoxDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
