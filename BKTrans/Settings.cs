using IniParser;
using IniParser.Model;
using IniParser.Parser;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BKTrans
{
    public static class Settings
    {
        public static FileIniDataParser Parser = new FileIniDataParser();
        public static IniDataParser DataParser = new IniDataParser();

        public static string ReadSettings(string section, string key)
        {
            IniData data = Parser.ReadFile(App.SETTINGS_PATH);
            return data[section][key];
        }

        public static IniData ReadSettings()
        {
            IniData data = Parser.ReadFile(App.SETTINGS_PATH);
            return data;
        }

        public static void SaveSettings(string section, string key, string value)
        {
            try
            {
                IniData data = Parser.ReadFile(App.SETTINGS_PATH);
                data[section][key] = value;
                Parser.WriteFile(App.SETTINGS_PATH, data);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void SaveSettings(IniData data)
        {
            IniData main = Parser.ReadFile(App.SETTINGS_PATH);
            main.Merge(data);
            Parser.WriteFile(App.SETTINGS_PATH, main);
        }

        public static HotKey GetHotKeyFromSetting(KeyDataCollection datas)
        {

            int[] keyData = datas["Key"].Split(',').Select(Int32.Parse).ToArray();
            int[] modifierKeysData = datas["ModifierKeys"].Split(',').Select(Int32.Parse).ToArray();

            Key key = 0;
            ModifierKeys modifierKeys = 0;

            foreach (var item in keyData)
            {
                key |= (Key)item;
            }
            foreach (var item in modifierKeysData)
            {
                modifierKeys |= (ModifierKeys)item;
            }

            return new HotKey(key, modifierKeys);
        }

        public static void SaveHotKey(string section, HotKey hotKey)
        {
            string keyStringData = hotKey.Key.ToString().Replace(" ", "");
            string modifierKeyStringData = hotKey.ModifierKeys.ToString().Replace(" ", "");

            int[] keyData = keyStringData.Split(',').Select(x => GetValueNameKey(x)).ToArray();
            int[] modifierKeysData = modifierKeyStringData.Split(',').Select(x => GetValueNameModifierKey(x)).ToArray();

            SaveSettings(section, "Key", string.Join(",", keyData));
            SaveSettings(section, "ModifierKeys", string.Join(",", modifierKeysData));
        }

        public static int GetValueNameKey(string name)
        {
            return (int)Enum.Parse(typeof(Key), name);
        }

        public static int GetValueNameModifierKey(string name)
        {
            return (int)Enum.Parse(typeof(ModifierKeys), name);
        }
    }
}
