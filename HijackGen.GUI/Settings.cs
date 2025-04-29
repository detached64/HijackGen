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
        public string DllPath
        {
            get => (string)this[nameof(DllPath)];
            set => this[nameof(DllPath)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string SaveDirInternal
        {
            get => (string)this[nameof(SaveDirInternal)];
            set => this[nameof(SaveDirInternal)] = value;
        }

        public string SaveDir
        {
            get
            {
                string dir = SaveDirInternal;
                if (string.IsNullOrWhiteSpace(dir))
                {
                    dir = Path.Combine(Path.GetDirectoryName(DllPath) ?? DefaultDir);
                }
                return dir;
            }
            set => SaveDirInternal = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string SelectedButtonName
        {
            get => (string)this[nameof(SelectedButtonName)];
            set => this[nameof(SelectedButtonName)] = value;
        }
    }
}
