using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EI.SI;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using DbUp;
using DbUp.Engine.Output;
using System.Reflection;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DbUp.Helpers;
using MySqlConnector;
using server1;
using Cliente1;

namespace Server
{
    internal class Program
    {
        static TcpListener server = new TcpListener(IPAddress.Any, 50001);
        static List<ClientInfo> clients = new List<ClientInfo>();

        static void Main(string[] args)
        {
            server = new TcpListener(IPAddress.Any, 50001);
            server.Start();
            Console.WriteLine("Server started. Waiting for clients...");

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

            bool connected = true;
            NetworkStream stream = client.GetStream();
            ProtocolSI protocol = new ProtocolSI();

            // Criação do objeto para guardar info do cliente
            ClientInfo clientInfo = new ClientInfo();

            //Enviamos a public key do server para o client
            byte[] ackPacket = protocol.Make(ProtocolSICmdType.DATA, KeyManager.GetPublicKey());
            stream.Write(ackPacket, 0, ackPacket.Length);

            while (connected)
            {
                try
                {
                    int bytesRead = stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);

                    if (bytesRead > 0)
                    {
                        if (protocol.GetCmdType() == ProtocolSICmdType.DATA)
                        {
                            string receivedData = protocol.GetStringFromData();
                            var receivedObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(receivedData);
                            string publicKeyXml = receivedObj["publicKey"];

                            if (clientInfo.PublicKeyXml == null)
                            {
                                clientInfo.PublicKeyXml = publicKeyXml;
                                clientInfo.Stream = stream;
                                Console.WriteLine("Chave pública do cliente recebida.");
                            }

                            if (!receivedObj["isLoggingIn"].Equals("YES"))
                            {
                                Console.WriteLine($"Mensagem recebida do User {clientInfo.UserID}.");
                                Console.WriteLine($"Mensagem para ser enviada para o User {receivedObj["destination"]}.");

                                foreach (ClientInfo userData in clients)
                                {
                                    if (userData.UserID == Convert.ToInt32(receivedObj["destination"]))
                                    {
                                        Console.WriteLine($"User {receivedObj["destination"]} connectado.\nEnviando mensagem.");
                                        string aux = KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["mensage"]));
                                        var dataToSend = new
                                        {
                                            type = "mensage",
                                            From = KeyManager.EncryptWithPublicKey(clientInfo.Email, userData.PublicKeyXml),
                                            mensage = KeyManager.EncryptWithPublicKey(aux, userData.PublicKeyXml)
                                        };

                                        string json = JsonConvert.SerializeObject(dataToSend);

                                        ackPacket = protocol.Make(ProtocolSICmdType.DATA, json);
                                        userData.Stream.Write(ackPacket, 0, ackPacket.Length);
                                    }
                                    else
                                    {
                                        var dataToSend = new
                                        {
                                            type = "server-response",
                                            response = "The receiver Client is not logged in. (Offline)"
                                        };

                                        string json = JsonConvert.SerializeObject(dataToSend);

                                        ackPacket = protocol.Make(ProtocolSICmdType.DATA, json);
                                        stream.Write(ackPacket, 0, ackPacket.Length);
                                    }
                                }
                            }
                            else
                            {
                                string username = KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["username"]));
                                string password = KeyManager.DecryptWithPrivateKey(Convert.FromBase64String(receivedObj["password"]));
                                string responseText = "";
                                int response = CheckForUser(username, password, publicKeyXml, ref clientInfo);
                                switch (response)
                                {
                                    case 0: // error
                                        responseText = "An ERRROR occured during the authentication.";
                                        break;
                                    case 1: // incorrect password
                                        responseText = "The inserted password is incorrect.";
                                        break;
                                    case 2: // authenticated
                                        responseText = "User Authenticated with success.";
                                        break;
                                    case 3: // user created
                                        responseText = "User has been created with success.";
                                        break;
                                }

                                var dataToSend = new
                                {
                                    type = "server-response",
                                    response = responseText
                                };

                                clients.Add(clientInfo);

                                string json = JsonConvert.SerializeObject(dataToSend);

                                ackPacket = protocol.Make(ProtocolSICmdType.DATA, json);
                                stream.Write(ackPacket, 0, ackPacket.Length);
                            }
                        }
                    }
                }
                catch (IOException ioEx)
                {
                    //Cliente fechou
                    Console.WriteLine($"Client {clientInfo.UserID} connection closed.");
                    connected = false;
                }
            }
        }   

        static private int CheckForUser(string userEmailCrypted, string password, string encryptKey, ref ClientInfo clientInfo)
        {
            int response = 0;
            var connectionString = "Server=localhost;Database=proj-ts;Uid=root;Pwd=;";
            string query = "SELECT * FROM users WHERE email = @userEmail";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userEmail", userEmailCrypted);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string userID = reader["IDuser"].ToString();
                                string email = reader["email"].ToString();
                                string passwordFromDb = reader["pass"].ToString();

                                clientInfo.UserID = Convert.ToInt32(userID);
                                clientInfo.Email = email;

                                Console.WriteLine("User has account.");
                                if (passwordFromDb.Equals(CaesarCipher.Encrypt(password, 10)))
                                {
                                    Console.WriteLine($"User {userID} has been authenticated with success.");
                                    response = 2;
                                }
                                else
                                {
                                    Console.WriteLine("User has inserted the wrong password.");
                                    response = 1;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No user found with that email.");
                            Console.WriteLine("User dosent have a account.");
                            response = CreateUser(userEmailCrypted, password, encryptKey);
                        }
                    }
                }
            }

            return response;
        }

        static private int CreateUser(string userEmailCrypted, string inputedPassword, string encryptKey)
        {
            int response = 0;
            string logFilePath = "./Scripts/error-log.txt";
            FileUpgradeLog upgradeLogger = new FileUpgradeLog(logFilePath);

            string executablePath = Assembly.GetEntryAssembly().Location;
            string directory = Path.GetDirectoryName(executablePath);
            string folderLoc = directory + @"\Scripts";

            string connectionString = "Server=localhost;Database=proj-ts;Uid=root;Pwd=;";

            var variables = new Dictionary<string, string>
            {
                { "email", "'" + userEmailCrypted + "'" },
                { "password", "'" + CaesarCipher.Encrypt(inputedPassword, 10) + "'" }
            };

            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(folderLoc, sqlFilePath => sqlFilePath.Contains("001-insert-user"))
                    .WithVariables(variables)
                    .LogToConsole()
                    .LogTo(upgradeLogger)
                    .JournalTo(new NullJournal())
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while creating user. Error Logged to Error-Log.txt");
                upgradeLogger.LogError($"[{DateTime.Now}] Migration failed: {result.Error}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User created with success.");
                Console.ResetColor();
                response = 3;
            }

            return response;
        }
    }
}
