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

            bool hasLoggedIn = false;
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
                        var receivedObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(receivedData);
                        string publicKeyXml = receivedObj["publicKey"];

                        if (clientInfo.PublicKeyXml == null)
                        {
                            clientInfo.PublicKeyXml = publicKeyXml;
                            Console.WriteLine("Chave pública do cliente recebida:");
                            Console.WriteLine(clientInfo.PublicKeyXml);
                        }

                        if(hasLoggedIn)
                        {
                            Console.WriteLine($"Mensagem recebida: {receivedData}");

                            // Aqui podes futuramente validar assinatura, etc.

                            // Envia confirmação
                            byte[] ackPacket = protocol.Make(ProtocolSICmdType.DATA, "Recebido");
                            stream.Write(ackPacket, 0, ackPacket.Length);
                        }
                        else
                        {
                            string username = receivedObj["username"];
                            string password = receivedObj["password"];
                            CheckForUser(username, password, publicKeyXml);
                        }
                    }
                }
            }
        }

        static private void CheckForUser(string userEmailCrypted, string password, string encryptKey)
        {
            OleDbConnection Conn = null;
            connectToDB(ref Conn);
            string sSQL, idAux = "0";
            bool hasAccount = false, passwordIsCorrect = false;
            sSQL = "SELECT * FROM Clients";
            OleDbCommand comand = new OleDbCommand(sSQL, Conn);
            OleDbDataReader dataReader = comand.ExecuteReader();

            while (dataReader.Read())
            {
                if (userEmailCrypted.Equals(dataReader.GetData(1).ToString()))
                {
                    hasAccount = true;
                    if (password.Equals(dataReader.GetData(2).ToString()))
                    {
                        passwordIsCorrect = true;
                        idAux = dataReader.GetData(0).ToString();
                    }
                }
            }

            if(hasAccount)
            {
                Console.WriteLine("User has account.");
                if (passwordIsCorrect)
                {
                    Console.WriteLine($"User {idAux} has been authenticated with success.");
                }
            } else {
                Console.WriteLine("User dosent have a account.");
                CreateUser(userEmailCrypted, password, encryptKey);
            }
            Conn.Close();
        }

        static private void CreateUser(string userEmailCrypted, string inputedPassword, string encryptKey)
        {
            var logFilePath = "./Scripts/error-log.txt";
            var upgradeLogger = new FileUpgradeLog(logFilePath);

            string executablePath = Assembly.GetEntryAssembly().Location;
            string directory = Path.GetDirectoryName(executablePath);
            string folderLoc = directory + @"\Scripts";

            string connectionString = "Server=localhost;Database=proj-ts;Uid=root;Pwd=;";

            var variables = new Dictionary<string, string>
            {
                { "email", userEmailCrypted },
                { "password", inputedPassword },
                { "publicKey", encryptKey }
            };

            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(folderLoc, sqlFilePath => sqlFilePath.Contains("001-insert-user"))
                    .WithVariables(variables)
                    .LogTo(upgradeLogger)
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
            }
        }

        static public void connectToDB(ref OleDbConnection Conn)
        {
            try
            {
                Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\ts-project.accdb");
                Conn.Open();
            }
            catch
            {
                Console.WriteLine("DB Connection Fail");
            }
        }
    }
}
