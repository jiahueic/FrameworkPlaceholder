using AGTIV.Framework.General;
using AGTIV.Framework.MVC.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Configurations
{
    public class AppSetting : IAppSetting
    {
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string DBConnString { get; private set; }
        public string WebApiUrl { get; private set; }
        public string WebUrl { get; private set; }

        //Agtiv email information
        public string AG_EmailDefaultCredential { get; private set; }
        public string AG_EmailFrom { get; private set; }
        public string AG_EmailHost { get; private set; }
        public string AG_EmailPassword { get; private set; }
        public string AG_EmailPort { get; private set; }
        public string AG_EmailUseDefaultCredential { get; private set; }
        public string AG_EmailUsername { get; private set; }
        public string AG_EmailUseSSL { get; private set; }
        public string AG_EncKey { get; private set; }

        public static string GetAppSetting(string keyName)
        {
            if (ConfigurationManager.AppSettings[keyName] != null)
            {
                return ConfigurationManager.AppSettings[keyName];
            }

            return null;
        }

        private string GetConnSetting(string keyName)
        {
            if (ConfigurationManager.ConnectionStrings[keyName] != null)
            {
                ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[keyName];
                return setting.ConnectionString;
            }

            return null;
        }

        public AppSetting()
        {
            string encryptedKey = GetAppSetting(ConstantHelper.AppSetting.AG_ENCKEY);
            string decryptedKey = !string.IsNullOrEmpty(encryptedKey) ? EncryptionHelper.Decrypt(encryptedKey) : string.Empty;


            //string encryptedDBConnString = GetConnSetting(ConstantHelper.ConnString.Default);

            //if (!string.IsNullOrEmpty(decryptedKey))
            //{
            //    DBConnString = !string.IsNullOrEmpty(encryptedDBConnString)
            //        ? EncryptionHelper.Decrypt(encryptedDBConnString, decryptedKey)
            //        : string.Empty;
            //}
            //else
            //{
            //    DBConnString = !string.IsNullOrEmpty(encryptedDBConnString)
            //        ? encryptedDBConnString
            //        : string.Empty;
            //}

            string encryptedEmailUsername = GetAppSetting(ConstantHelper.AppSetting.AG_EmailUsername);
            string encryptedEmailPassword = GetAppSetting(ConstantHelper.AppSetting.AG_EmaillPassword);

            AG_EmailUsername = !String.IsNullOrEmpty(encryptedEmailUsername) ? EncryptionHelper.Decrypt(encryptedEmailUsername, decryptedKey) : String.Empty;
            AG_EmailPassword = !String.IsNullOrEmpty(encryptedEmailPassword) ? EncryptionHelper.Decrypt(encryptedEmailPassword, decryptedKey) : String.Empty;

            ClientId = GetAppSetting(ConstantHelper.AppSetting.ClientId);
            ClientSecret = GetAppSetting(ConstantHelper.AppSetting.ClientSecret);
            DBConnString = GetConnSetting(ConstantHelper.ConnString.Default);
            WebApiUrl = GetAppSetting(ConstantHelper.AppSetting.WebApiUrl);
            WebUrl = GetAppSetting(ConstantHelper.AppSetting.WebUrl);

            AG_EmailDefaultCredential = GetAppSetting(ConstantHelper.AppSetting.AG_EmailDefaultCredential);
            AG_EmailFrom = GetAppSetting(ConstantHelper.AppSetting.AG_EmailFrom);
            AG_EmailHost = GetAppSetting(ConstantHelper.AppSetting.AG_EmailHost);
            //AG_EmailPassword = GetAppSetting(ConstantHelper.AppSetting.AG_EmaillPassword);
            AG_EmailPort = GetAppSetting(ConstantHelper.AppSetting.AG_EmailPort);
            AG_EmailUseDefaultCredential = GetAppSetting(ConstantHelper.AppSetting.AG_EmailUseDefaultCredential);
            //AG_EmailUsername = GetAppSetting(ConstantHelper.AppSetting.AG_EmailUsername);
            AG_EmailUseSSL = GetAppSetting(ConstantHelper.AppSetting.AG_EmailUseSSL);
            AG_EncKey = GetAppSetting(ConstantHelper.AppSetting.AG_ENCKEY);
        }
    }
}
