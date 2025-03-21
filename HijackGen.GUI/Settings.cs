using System.Configuration;
using System.Diagnostics;

namespace HijackGen.GUI
{
    internal class Settings : ApplicationSettingsBase
    {
        public static Settings Default { get; } = (Settings)Synchronized(new Settings());

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
        public string DefSaveDir
        {
            get => (string)this[nameof(DefSaveDir)];
            set => this[nameof(DefSaveDir)] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string HSaveDir
        {
            get => (string)this[nameof(HSaveDir)];
            set => this[nameof(HSaveDir)] = value;
        }
    }
}
