using System;
using System.Security.Cryptography;
using System.Text;

namespace WWB.IOT.AMQP
{
    public class Sign
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public static Sign Create(string accessKey, string accessSecret, string clientId, string iotInstanceId, string consumerGroupId)
        {
            var timestamp = GetCurrentMilliseconds();

            var userName = $"{clientId}|authMode=aksign,signMethod=hmacmd5,consumerGroupId={consumerGroupId},iotInstanceId={iotInstanceId},authId={accessKey},timestamp={timestamp}|";

            var param = $"authId={accessKey}&timestamp={timestamp}";

            var password = DoSign(param, accessSecret, "HmacMD5");

            return new Sign() {UserName = userName, Password = password};
        }

        private static long GetCurrentMilliseconds()
        {
            DateTime dt1970 = new DateTime(1970, 1, 1);
            DateTime current = DateTime.Now;
            return (long) (current - dt1970).TotalMilliseconds;
        }

        private static string DoSign(string param, string accessSecret, string signMethod)
        {
            //signMethod = HmacMD5
            byte[] key = Encoding.UTF8.GetBytes(accessSecret);
            byte[] signContent = Encoding.UTF8.GetBytes(param);
            var hmac = new HMACMD5(key);
            byte[] hashBytes = hmac.ComputeHash(signContent);
            return Convert.ToBase64String(hashBytes);
        }
    }
}