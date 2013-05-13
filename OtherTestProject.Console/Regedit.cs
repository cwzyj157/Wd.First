using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace OtherTestProject.Console
{
    public class Regedit
    {
        public static string GetValue(string key)
        {
            return GetValue(key, "TTKN");
        }
        public static string GetValue(string key, string subkeyPath, string defaultValue = default(string))
        {
            string value = string.Empty;
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey keySoftWare = localMachine.OpenSubKey("SOFTWARE\\Wow6432Node", true) ?? localMachine.OpenSubKey("SOFTWARE", true);
            System.Console.WriteLine(keySoftWare.Name);
            RegistryKey keySub = keySoftWare.OpenSubKey(subkeyPath, true);
            if (keySub != null)
            {
                var obj = keySub.GetValue(key, defaultValue);
                if (obj != null)
                    value = obj.ToString();
            }
            keySub.Close();
            localMachine.Close();
            return value;
        }
    }
}
