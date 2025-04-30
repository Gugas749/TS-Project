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
using server1;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cliente1
{
    public partial class Form1 : Form
    {
        TcpClient client = new TcpClient();
        ProtocolSI protocol = new ProtocolSI();
        NetworkStream stream;
        string serverPublicKey = "", userEmail = "", userId ="";
        public static Form1 THIS;
        List<Mensage> msgList = new List<Mensage>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            THIS = this;
            enabelDisableFunc(1);
            listViewDirect.View = View.Details;
            listViewDirect.Columns.Clear();
            listViewDirect.Columns.Add("Mensagens", -2);
            listViewDirect.FullRowSelect = true;
            listViewDirect.HeaderStyle = ColumnHeaderStyle.None;

            listViewOnlineUsers.View = View.Details;
            listViewOnlineUsers.Columns.Clear();
            listViewOnlineUsers.Columns.Add("Users", -2);
            listViewOnlineUsers.FullRowSelect = true;
            listViewOnlineUsers.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void butLoginAuth_Click(object sender, EventArgs e)
        {
            connectToServer();

            string key = KeyManager.GetPublicKey();
            var dataToSend = new
            {
                username = KeyManager.EncryptWithPublicKey(textBoxUsernameAuth.Text.ToString().Trim(), serverPublicKey),
                password = KeyManager.EncryptWithPublicKey(textBoxPasswordAuth.Text.ToString().Trim(), serverPublicKey),
                request = "logIn",
                publicKey = key
            };
            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);
            userEmail = textBoxUsernameAuth.Text.ToString().Trim();
            enabelDisableFunc(2);
        }

        private void butSendMSG_Click(object sender, EventArgs e)
        {
            string key = KeyManager.GetPublicKey();
            byte[] dataBytes = Encoding.UTF8.GetBytes(txtBoxMSGToSend.Text.ToString().Trim());
            var dataToSend = new
            {
                mensage = KeyManager.EncryptWithPublicKey(txtBoxMSGToSend.Text.ToString().Trim(), serverPublicKey),
                destination = txtBoxDestination.Text.Trim(),
                request = "sendMSG",
                signature = KeyManager.SignData(dataBytes),
                publicKey = key
            };

            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);
            listViewDirect.Items.Add(userEmail +": "+ txtBoxMSGToSend.Text.ToString().Trim());

            Mensage msg = new Mensage();
            msg.ReceiverID = txtBoxDestination.Text.Trim();
            msg.SenderID = userId;
            msg.SenderEmail = userEmail;
            msg.MSG = txtBoxMSGToSend.Text.ToString().Trim();
            msgList.Add(msg);

            txtBoxMSGToSend.Text = "";
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

                                    butRequestUserList.Enabled = false;
                                    butSendMSG.Enabled = false;
                                    txtBoxDestination.Enabled = false;
                                    txtBoxMSGToSend.Enabled = false;
                                }));
                                break;
                            case 2:
                            case 3:
                                this.Invoke(new Action(() =>
                                {
                                    msgList = getListMSG(receivedObj["msgs"]);
                                    userId = receivedObj["userID"];
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

                                butRequestUserList.Enabled = false;
                                butSendMSG.Enabled = false;
                                txtBoxDestination.Enabled = false;
                                txtBoxMSGToSend.Enabled = false;
                                break;
                            case 2:
                            case 3:
                                msgList = getListMSG(receivedObj["msgs"]);
                                userId = receivedObj["userID"];
                                break;
                        }
                    }
                    break;
                case "mensage":
                    string aux = KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["From"]));
                    string text = KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["mensage"]));
                    if (listViewDirect.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            listViewDirect.Items.Add(aux + ": " + text);

                            Mensage msg = new Mensage();
                            msg.ReceiverID = userId;
                            msg.SenderID = receivedObj["FromID"];
                            msg.SenderEmail = aux;
                            msg.MSG = text;
                            msgList.Add(msg);
                        }));
                    }
                    else
                    {
                        listViewDirect.Items.Add(aux + ": " + text);

                        Mensage msg = new Mensage();
                        msg.ReceiverID = userId;
                        msg.SenderID = receivedObj["FromID"];
                        msg.SenderEmail = aux;
                        msg.MSG = text;
                        msgList.Add(msg);
                    }
                    break;
                case "updateOnUserList":
                    List<string> userList = getList(receivedObj["userList"]);

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

        private void butLogout_Click(object sender, EventArgs e)
        {
            connectToServer();
        }

        private void butRequestUserList_Click(object sender, EventArgs e)
        {
            string key = KeyManager.GetPublicKey();
            var dataToSend = new
            {
                request = "userList",
                publicKey = key
            };
            string json = JsonConvert.SerializeObject(dataToSend);

            byte[] dataPacket = protocol.Make(ProtocolSICmdType.DATA, json);
            stream.Write(dataPacket, 0, dataPacket.Length);
        }

        private void enabelDisableFunc(int selection)
        {
            butLoginAuth.Enabled = false;
            butSendMSG.Enabled = false;
            butRequestUserList.Enabled = false;
            butLogout.Enabled = false;

            txtBoxDestination.Enabled = false;
            txtBoxMSGToSend.Enabled = false;
            textBoxUsernameAuth.Enabled = false;
            textBoxPasswordAuth.Enabled = false;
            switch (selection)
            {
                case 0:
                    break;
                case 1:
                    textBoxUsernameAuth.Enabled = true;
                    textBoxPasswordAuth.Enabled = true;
                    butLoginAuth.Enabled = true;
                    butLogout.Enabled = true;
                    break;
                case 2:
                    butLogout.Enabled = true;
                    txtBoxDestination.Enabled = true;
                    txtBoxMSGToSend.Enabled = true;
                    butSendMSG.Enabled = true;
                    butRequestUserList.Enabled = true;
                    break;
            }
        }

        private void listViewOnlineUsers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewOnlineUsers.SelectedItems.Count > 0)
            {
                string input = listViewOnlineUsers.SelectedItems[0].Text;
                string idPart = input.Split('-')[0];
                txtBoxDestination.Text = idPart;
            }
        }

        private List<string> getList(string input)
        {
            List<string> result = new List<string>();

            string[] parts = input.Split('-', (char)StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length - 1; i += 2)
            {
                result.Add($"{parts[i]}-{parts[i + 1]}");
            }

            return result;
        }

        private void txtBoxMSGToSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                butSendMSG.PerformClick();
            }
        }

        private void txtBoxDestination_TextChanged(object sender, EventArgs e)
        {
            listViewDirect.Items.Clear();

            if(txtBoxDestination.Text.Trim().Length > 0)
            {
                foreach(Mensage mensage in msgList)
                {
                    if (txtBoxDestination.Text.Trim().Equals(mensage.SenderID) || txtBoxDestination.Text.Trim().Equals(mensage.ReceiverID))
                    {
                        listViewDirect.Items.Add(mensage.SenderEmail + ": " + mensage.MSG);
                    }
                }
            }
        }

        private List<Mensage> getListMSG(string input)
        {
            List<Mensage> result = new List<Mensage>();

            string[] parts = input.Split('-', (char)StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length - 1; i += 2)
            {
                Mensage mensage = new Mensage();
                mensage.SenderID = parts[i];
                mensage.ReceiverID = parts[i + 1];
                mensage.SenderEmail = parts[i + 2];
                string aux = parts[i + 3];
                mensage.MSG = CaesarCipher.Decrypt(aux, 10);
                result.Add(mensage);
                i += 2;
            }

            return result;
        }

        private void connectToServer()
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
                lbConnectionState.Text = "Estado da Conexão: Offline";
            }
        }
    }
}
