using sharpcada.Data.Repositories;
using sharpcada.Data.Entities;

namespace sharpcada.Services.DataAcquisition;

public class DataCollectionSettings
{
    private readonly SettingsRepository _settingsRepository;

    private List<Setting> _settings;

    public DataCollectionSettings(SettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
        _settings = new();
    }

    public bool DataAcquisitionRun
    {
        get
        {
            var res = this.findSettingOrDefault("DataAcquisitionRun", "0");
            return res == "1";
        }
        set
        {
            var res = value ? "1" : "0";
            this.setSetting("DataAcquisitionRun", res);
        }
    }

    public uint DataAcquisitionPeriod
    {
        get
        {
            var res = this.findSettingOrDefault("DataAcquisitionPeriod", "10");
            return Convert.ToUInt32(res);
        }
        set
        {
            this.setSetting("DataAcquisitionPeriod", value.ToString());
        }
    }

    public uint MaxPeriodCheckSettings
    {
        get
        {
            var res = this.findSettingOrDefault("MaxPeriodCheckSettings", "600");
            return Convert.ToUInt32(res);
        }
        set
        {
            this.setSetting("MaxPeriodCheckSettings", value.ToString());
        }
    }

    public async Task Load() =>
        _settings = await _settingsRepository.AllAsync();

    public async Task Save() =>
        await _settingsRepository.Save();

    private (int, string?) findSetting(string key)
    {
        var index = 0;
        foreach (var setting in _settings)
        {
            if (setting.Key == key)
            {
                return (index, setting.Value);
            }
            index++;
        }

        return (index, null);
    }

    private string findSettingOrDefault(string key, string defaultValue)
    {
        var (_, value) = this.findSetting(key);

        return value ?? defaultValue;
    }

    private void setSetting(string key, string newValue)
    {
        var (index, value) = this.findSetting(key);
        if (value is null)
        {
            var newSetting = new Setting { Key = key, Value = newValue };
            _settings.Add(newSetting);
            _settingsRepository.Create(newSetting);
        }

        _settings[index].Value = newValue;
        _settingsRepository.Update(_settings[index]);
    }
}
