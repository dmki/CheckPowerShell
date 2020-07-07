using System;
using Microsoft.Win32;

namespace CheckPowerShell
{
        public static class RegHelper
    {
        private const string HKCU = @"HKEY_CURRENT_USER\";
        private const string HKLM = @"HKEY_LOCAL_MACHINE\";
        //todo: replace with your registry branch
        public static string SettingsPath = "";

        public static string GetSettingString(string settingName, string defaultValue = "", RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            var result = Convert.ToString(Registry.GetValue(registryRoot + SettingsPath, settingName, string.Empty));
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        public static string GetSettingStringEx(string keyPath, string settingName, string defaultValue = "", RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            //if (!keyPath.StartsWith(@"\")) keyPath = @"\" + keyPath;
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            var result = Convert.ToString(Registry.GetValue(registryRoot + keyPath, settingName, string.Empty));
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        public static string[] GetSettingMultiLine(string settingName, RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            //var result = GetSettingString(settingName, root);
            //if (string.IsNullOrEmpty(result)) return null;
            //return result.Split(new char[';']);
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            return (string[]) Registry.GetValue(registryRoot + SettingsPath, settingName, string.Empty);
        }
        public static Int32 GetSettingInt(string settingName, int defaultValue = 0, RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            return Convert.ToInt32(Registry.GetValue(registryRoot + SettingsPath, settingName, defaultValue));
        }

        public static Boolean GetSettingBool(string settingName, bool defaultValue = false,  RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            return Convert.ToBoolean(Registry.GetValue(registryRoot + SettingsPath, settingName, defaultValue));
        }

        public static long GetSettingInt64(string settingName, RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            return Convert.ToInt64(Registry.GetValue(registryRoot + SettingsPath, settingName, 0));
        }

        public static void SaveSetting(string settingName, object value,
                                RegistryRootType root = RegistryRootType.HKEY_CURRENT_USER)
        {
            string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            Registry.SetValue(registryRoot + SettingsPath, settingName, value);
        }

        public static bool BranchExists(string path, RegistryRootType root = RegistryRootType.HKEY_LOCAL_MACHINE)
        {
            //string registryRoot = (root == RegistryRootType.HKEY_CURRENT_USER) ? HKCU : HKLM;
            //var keyPath = registryRoot + path;
            var hive = root == RegistryRootType.HKEY_LOCAL_MACHINE ? RegistryHive.LocalMachine : RegistryHive.CurrentUser;
            RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64);
            var key = baseKey.OpenSubKey(path);
            return (key != null);
        }
    }

    public enum RegistryRootType
    {
        HKEY_LOCAL_MACHINE,
        HKEY_CURRENT_USER
    }
}