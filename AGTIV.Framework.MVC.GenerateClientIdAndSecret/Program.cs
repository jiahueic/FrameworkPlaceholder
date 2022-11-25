using AGTIV.Framework.MVC.Data.Context;
using AGTIV.Framework.MVC.Entities.Authentication;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AGTIV.Framework.MVC.GenerateClientIdAndSecret
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select one to generate key: ");
            Console.WriteLine("1 -> Client Id ");
            Console.WriteLine("2 -> Client Secret ");

            string answer = "";

            answer = Console.ReadLine();

            if (answer.Equals("1"))
            {
                Console.WriteLine("Remember to add/update your client id in your client app. ");

                var clientId = Guid.NewGuid();
                using (var md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(clientId.ToString()));
                    var guid = new Guid(hash).ToString("N");
                    Console.WriteLine("Generated Client Id: " + guid);
                    Console.ReadLine();
                }

            }
            else if (answer.Equals("2"))
            {
                string appName = "";
                Console.WriteLine("Please enter client app name: ");

                appName = Console.ReadLine();

                RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();

                byte[] buffer = new byte[20];
                cryptoRandomDataGenerator.GetBytes(buffer);
                string uniq = Convert.ToBase64String(buffer);

                using (var context = new AppDbContext())
                {

                    var appSecret = new AppSecret()
                    {
                        AppName = appName,
                        ClientSecret = uniq,
                        CreatedOn = DateTime.UtcNow
                    };

                    context.AppSecret.Add(appSecret);
                    context.SaveChanges();
                }

                Console.WriteLine($"Generated Client Secret for '{appName}': {uniq}");
                Console.ReadLine();
            }
        }
    }
}
