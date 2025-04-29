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
        string serverPublicKey = "", userEmail = "";
        public static Form1 THIS;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            THIS = this;
            enabelDisableFunc(0);
            listViewDirect.View = View.Details;
            listViewDirect.Columns.Clear();
            listViewDirect.Columns.Add("Mensagens");
            listViewDirect.FullRowSelect = true;
            listViewDirect.HeaderStyle = ColumnHeaderStyle.None;

            listViewOnlineUsers.View = View.Details;
            listViewOnlineUsers.Columns.Clear();
            listViewOnlineUsers.Columns.Add("Users");
            listViewOnlineUsers.FullRowSelect = true;
            listViewOnlineUsers.HeaderStyle = ColumnHeaderStyle.None;
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
            userEmail = textBoxUsernameAuth.Text.ToString().Trim();
            enabelDisableFunc(2);

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
            listViewDirect.Items.Add(userEmail +": "+ txtBoxMSGToSend.Text.ToString().Trim());
        }

        public void MensageFromServer(Dictionary<string, string> receivedObj)
        {
            switch (receivedObj["type"])
            {
                case "server-response":
                    if (lbServerResponse.InvokeRequired)
                    {
                        lbServerResponse.Invoke(new Action(() => lbServerResponse.Text = $"Resposta do Servidor: {receivedObj["responseText"]}"));
                        switch (Convert.ToInt32(receivedObj["response"]))
                        {
                            case 0:
                            case 1:
                            case 4:
                                this.Invoke(new Action(() =>
                                {
                                    textBoxUsernameAuth.Enabled = true;
                                    textBoxPasswordAuth.Enabled = true;
                                    butLoginAuth.Enabled = true;

                                    butSendMSG.Enabled = false;
                                    txtBoxDestination.Enabled = false;
                                    txtBoxMSGToSend.Enabled = false;
                                }));
                                break;
                        }
                    }
                    else
                    {
                        lbServerResponse.Text = $"Resposta do Servidor: {receivedObj["responseText"]}";
                        switch (Convert.ToInt32(receivedObj["response"]))
                        {
                            case 0:
                            case 1:
                            case 4:
                                textBoxUsernameAuth.Enabled = true;
                                textBoxPasswordAuth.Enabled = true;
                                butLoginAuth.Enabled = true;

                                butSendMSG.Enabled = false;
                                txtBoxDestination.Enabled = false;
                                txtBoxMSGToSend.Enabled = false;
                                break;
                        }
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
                case "updateOnUserList":
                    string jsonArrayString = receivedObj["userList"];
                    List<string> userList = JsonConvert.DeserializeObject<List<string>>(jsonArrayString);

                    if (listViewOnlineUsers.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            foreach (string user in userList)
                            {   
                                listViewOnlineUsers.Items.Add(user);
                            }
                        }));
                    }
                    else
                    {
                        foreach (string user in userList)
                        {
                            listViewOnlineUsers.Items.Add(user);
                        }
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

        private void butConnectToServer_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                client.Connect("127.0.0.1", 50001);
                stream = client.GetStream();

                int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                serverPublicKey = protocol.GetStringFromData();
                lbServerResponse.Text = "Resposta do Servidor: Server Public Key received.";
                MessageListener.Start(stream, this);

                lbConnectionState.Text = "Estado da Conexão: Online";
                enabelDisableFunc(1);
            }
            catch
            {
                MessageBox.Show("Impossivel Connectar", "Não foi possivel estabelecer conexão ao servidor.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void enabelDisableFunc(int selection)
        {
            butLoginAuth.Enabled = false;
            butSendMSG.Enabled = false;

            txtBoxDestination.Enabled = false;
            txtBoxMSGToSend.Enabled = false;
            textBoxUsernameAuth.Enabled = false;
            textBoxPasswordAuth.Enabled = false;
            switch (selection)
            {
                case 0:
                    butConnectToServer.Enabled = true;
                    break;
                case 1:
                    textBoxUsernameAuth.Enabled = true;
                    textBoxPasswordAuth.Enabled = true;
                    butLoginAuth.Enabled = true;
                    break;
                case 2:
                    txtBoxDestination.Enabled = true;
                    txtBoxMSGToSend.Enabled = true;
                    butSendMSG.Enabled = true;
                    break;
            }
        }
    }
}
