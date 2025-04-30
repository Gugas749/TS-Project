using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cliente1
{
    internal class KeyManager
    {
        private static RSACryptoServiceProvider rsaProvider;
        private static string publicKey;
        private static string privateKey;

        static KeyManager()
        {
            // Geração do par de chaves (2048 bits)
            rsaProvider = new RSACryptoServiceProvider(2048);
            publicKey = rsaProvider.ToXmlString(false); // chave pública
            privateKey = rsaProvider.ToXmlString(true); // chave privada
        }

        // Devolve a chave pública como string
        public static string GetPublicKey()
        {
            return publicKey;
        }

        // Assinatura digital com a chave privada
        public static byte[] SignData(byte[] dataToSign)
        {
            rsaProvider.FromXmlString(privateKey);
            return rsaProvider.SignData(dataToSign, CryptoConfig.MapNameToOID("SHA256"));
        }

        // Verificação da assinatura com a chave pública
        public static bool VerifySignature(byte[] originalData, byte[] signature, string otherPublicKey)
        {
            rsaProvider.FromXmlString(otherPublicKey);
            return rsaProvider.VerifyData(originalData, CryptoConfig.MapNameToOID("SHA256"), signature);
        }

        // Cifra dados com a chave pública (usado pelo servidor)
        public static byte[] EncryptWithPublicKey(string data, string otherPublicKey)
        {
            var tempRsa = new RSACryptoServiceProvider();
            tempRsa.FromXmlString(otherPublicKey);
            return tempRsa.Encrypt(Encoding.UTF8.GetBytes(data), false);
        }

        // Decifra dados com a chave privada (usado pelo cliente)
        public static string DecryptWithPrivateKey(byte[] encryptedData)
        {
            rsaProvider.FromXmlString(privateKey);
            byte[] decryptedBytes = rsaProvider.Decrypt(encryptedData, false);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
