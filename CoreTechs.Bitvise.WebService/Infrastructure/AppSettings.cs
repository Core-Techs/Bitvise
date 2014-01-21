using System;
using System.Configuration;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace CoreTechs.Bitvise.WebService.Infrastructure
{
    public class AppSettings
    {
        public static string ServiceUrl
        {
            get { return GetSettingOrDefault("http://localhost:8085"); }
        }

        public static string SFTPRootPath { get { return GetRequiredSetting(); } }

        public static string GetRequiredSetting([CallerMemberName]string name = null)
        {
            var setting = ConfigurationManager.AppSettings[name];

            if (setting == null)
                throw new SettingsPropertyNotFoundException(string.Format("The setting with name '{0}' was not found.",
                                                                          name));
            return setting;
        }

        public static T GetRequiredSetting<T>(Func<string, T> parser, [CallerMemberName] string name = null)
        {
            // setting must be defined in config
            var setting = GetRequiredSetting(name);

            try
            {
                return parser(setting);
            }
            catch (Exception ex)
            {
                throw new FormatException(
                    string.Format("The AppSetting '{0}' was not formatted correctly. The specified value was '{1}'",
                        name,
                        setting), ex);
            }
        }

        public static string GetSettingOrDefault(string @default = null, [CallerMemberName] string name = null)
        {
            return ConfigurationManager.AppSettings[name] ?? @default;
        }

        public static T GetSettingOrDefault<T>(Func<string, T> parser, T @default = default(T), [CallerMemberName] string name = null)
        {
            try
            {
                return parser(ConfigurationManager.AppSettings[name]);
            }
            catch
            {
                return @default;
            }
        }

        public static TEnum GetAppSettingAsEnumOrDefault<TEnum>([CallerMemberName]string name = null, bool ignoreCase = true, TEnum @default = default (TEnum)) where TEnum : struct
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException(string.Format("'{0}' is not an enum type", type.FullName));

            return GetSettingOrDefault(s => (TEnum)Enum.Parse(type, s, ignoreCase), @default, name);
        }
    }
}