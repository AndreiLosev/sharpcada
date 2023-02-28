using sharpcada.Data.Repositories;
using sharpcada.Data.Entities;

namespace sharpcada.Services.DataAcquisition;

public class DataCollectionSettings : Contracts.IServices
{ 
    private readonly SettingsRepository _settingsRepository;

    private ICollection<Setting> _settings;

    public DataCollectionSettings(SettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
        _settings = new List<Setting>();
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
        _settings = await _settingsRepository.GetAsync();

    public async Task Save() =>
        await _settingsRepository.SaveAsync();

    private Setting? findSetting(string key)
    {
        return _settings
            .Where(s => s.Key == key)
            .FirstOrDefault();
    }

    private string findSettingOrDefault(string key, string defaultValue)
    {
        var setting = this.findSetting(key);

        return setting is Setting ? setting.Value : defaultValue;
    }

    private void setSetting(string key, string newValue)
    {
        var setting = this.findSetting(key);
        if (setting is null)
        {
            var newSetting = new Setting { Key = key, Value = newValue };
            _settings.Add(newSetting);
            _settingsRepository.Create(newSetting);
            return;
        }
            
        setting.Value = newValue;
        _settingsRepository.Update(setting);
    }
}
