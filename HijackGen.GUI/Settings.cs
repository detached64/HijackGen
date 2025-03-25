using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace HijackGen.GUI
{
    internal class Settings : ApplicationSettingsBase
    {
        public static Settings Default { get; } = (Settings)Synchronized(new Settings());

        public static readonly string DefaultDir = Environment.GetFolderPath(Environment.SpecialFolder.System);

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string DllPathInternal
        {
            get => (string)this[nameof(DllPathInternal)];
            set => this[nameof(DllPathInternal)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string SaveDirInternal
        {
            get => (string)this[nameof(SaveDirInternal)];
            set => this[nameof(SaveDirInternal)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("false")]
        public bool IsX64Internal
        {
            get => (bool)this[nameof(IsX64Internal)];
            set => this[nameof(IsX64Internal)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("false")]
        public bool GenDefX64Internal
        {
            get => (bool)this[nameof(GenDefX64Internal)];
            set => this[nameof(GenDefX64Internal)] = value;
        }

        public static string DllPath
        {
            get
            {
                return Default.DllPathInternal;
            }
            set
            {
                Default.DllPathInternal = value;
                Default.Save();
            }
        }

        public static string SaveDir
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Default.SaveDirInternal))
                {
                    Default.SaveDirInternal = Path.GetDirectoryName(DllPath);
                    Default.Save();
                    return Path.GetDirectoryName(DllPath);
                }
                else
                {
                    return Default.SaveDirInternal;
                }
            }
            set
            {
                Default.SaveDirInternal = value;
                Default.Save();
            }
        }

        public static bool IsX64
        {
            get
            {
                return Default.IsX64Internal;
            }
            set
            {
                Default.IsX64Internal = value;
                Default.Save();
            }
        }

        public static bool GenDefX64
        {
            get
            {
                return Default.GenDefX64Internal;
            }
            set
            {
                Default.GenDefX64Internal = value;
                Default.Save();
            }
        }
    }
}
