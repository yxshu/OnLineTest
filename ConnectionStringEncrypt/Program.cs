using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnLineTest.DBUtility;

namespace ConnectionStringEncrypt
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("======+++=================请输入要加密的火星文：=====================");
                string key = Console.ReadLine();
                string Mkey = DESEncrypt.Encrypt(key);
                Console.WriteLine("=======================密文如下：=====================");
                Console.WriteLine(Mkey);
                Console.WriteLine(string.Format("解密后的密文如下：{0}", DESEncrypt.Decrypt(Mkey)));

            } while (true);
        }
    }
}
