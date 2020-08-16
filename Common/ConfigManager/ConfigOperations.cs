using System;
using System.Configuration;

namespace Common.ConfigManager
{
    public class ConfigOperations : IConfigOperations
    {
        public string Get(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
