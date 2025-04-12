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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect("127.0.0.1", 50001);
            stream = client.GetStream();
        }

        private void butLoginAuth_Click(object sender, EventArgs e)
        {
            string key = KeyManager.GetPublicKey();
            var dataToSend = new
            {
                username = KeyManager.EncryptWithPublicKey(textBoxUsernameAuth.Text.ToString().Trim(), key),
                password = KeyManager.EncryptWithPublicKey(textBoxPasswordAuth.Text.ToString().Trim(), key),
                publicKey = key
            };
            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);
            label1.Text = "Message sent.";

            int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

            if (bytesRead > 0)
            {
                label1.Text = $"Received: {protocol.GetStringFromData()}";
            }
        }
    }
}
