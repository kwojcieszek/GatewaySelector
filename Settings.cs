using System;
using System.IO;

namespace GatewaySelector
{
    public class Settings
    {
        private readonly string SettingFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\GatewaySelector";

        public void Save(string data)
        {
            File.WriteAllText(SettingFile, data);
        }

        public string Read()
        {
            if (File.Exists(SettingFile))
                return File.ReadAllText(SettingFile);
            else
                return null;
        }
    }
}
