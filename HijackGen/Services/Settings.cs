using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace HijackGen.Services
{
    internal class Settings : ApplicationSettingsBase
    {
        public static Settings Default { get; } = (Settings)Synchronized(new Settings());

        public static readonly string DefaultDir = Environment.GetFolderPath(Environment.SpecialFolder.System);

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string DllPath
        {
            get => (string)this[nameof(DllPath)];
            set => this[nameof(DllPath)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string SaveDir
        {
            get
            {
                string dir = (string)this[nameof(SaveDir)];
                if (string.IsNullOrWhiteSpace(dir))
                {
                    dir = Path.Combine(Path.GetDirectoryName(DllPath) ?? DefaultDir);
                }
                return dir;
            }
            set => this[nameof(SaveDir)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string SelectedFormatName
        {
            get => (string)this[nameof(SelectedFormatName)];
            set => this[nameof(SelectedFormatName)] = value;
        }
    }
}
