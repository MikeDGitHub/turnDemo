using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace turnDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = "d2ecd40663e247bab3a5fc797cd89008";
            var uId = 72057594038028896;
            var appId = 999;
            var u = string.Concat(appId, '_', uId);
            var realm = "com.beetle.face";
            var key = hmac(u, realm, token);
            set_turn_key(appId, uId, key);
            set_turn_password(appId, uId, token);
            Console.ReadLine();
        }

        private static string hmac(string username, string realm, string password)
        {
            var key = MD5Encrypt(string.Concat(username, ":", realm, ":", password));
            Console.WriteLine(key);
            return key;
        }


        private static void set_turn_key(int appid, long uid, string key)
        {
            var u = string.Concat(appid, '_', uid);
            var k = string.Concat("turn/user/", u, "/key");
            Console.WriteLine(string.Concat(k, "---", key));
        }

        private static void set_turn_password(int appid, long uid, string password)
        {
            var u = string.Concat(appid, '_', uid);
            var k = string.Concat("turn/user/", u, "/password");
            Console.WriteLine(string.Concat(k, "---", password));
        }

        private static string MD5Encrypt(string strText)
        {
            byte[] result = Encoding.Default.GetBytes(strText);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
    }
}
