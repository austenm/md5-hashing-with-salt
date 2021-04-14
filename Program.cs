using System;
using System.Text;
using System.Collections.Generic;

namespace P2
{
    class Program
    {   
        public static string CreateMD5(string input, byte[] input2)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] pBytes = new UTF8Encoding().GetBytes(input);

                byte[] saltyBytes = new byte[pBytes.Length + input2.Length - 3];
                System.Buffer.BlockCopy(pBytes, 0, saltyBytes, 0, pBytes.Length);
                System.Buffer.BlockCopy(input2, 0, saltyBytes, pBytes.Length, input2.Length - 3);

                byte[] hashBytes = md5.ComputeHash(saltyBytes);
                string hashed5 = BitConverter.ToString(hashBytes, 0, 5);

                return hashed5;
            }
        }
        static void Main(string[] args)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] letts = characters.ToCharArray();
            char[] goodString = new char[10];
            Random random = new Random();

            Dictionary<string, string> bday = new Dictionary<string, string>();
            
            for (int j = 0; j < bday.Count + 1; j++)
            {
                for (int i = 0; i < goodString.Length; i++)
                    {
                        goodString[i] = letts[random.Next(letts.Length)];
                    }
                string strang = new string(goodString);
                string[] saltArray = Environment.GetCommandLineArgs();
                int saltConvert = Convert.ToUInt16(saltArray[1], 16);
                byte[] salt = BitConverter.GetBytes(saltConvert);

                string shibby = CreateMD5(strang, salt);
                string key = shibby.Replace("-", " ");
                try 
                {
                    bday.Add(key, strang);
                }
                catch
                {
                    Console.WriteLine("{0},{1}", strang, bday[key]);
                }
            }
        }
    }
}
