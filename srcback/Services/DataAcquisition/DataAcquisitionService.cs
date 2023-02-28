using sharpcada.Exception;

namespace sharpcada.Services.DataAcquisition;

public class DataAcquisitionService : BackgroundService
{
    private DataAcquisitionServeiceState _state;
    private DataCollectionSettings _settings;
    private InitDataAcquistition _initService;

    public DataAcquisitionService(
        DataCollectionSettings settings,
        InitDataAcquistition initService)
    {
        _state = DataAcquisitionServeiceState.Stop;
        _settings = settings;
        _initService = initService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1);
        while (true)
        {
            var _ = _state switch
            {
                DataAcquisitionServeiceState.Stop => await this.StopHandler(),
                // DataAcquisitionServeiceState.Init => 2,
                // DataAcquisitionServeiceState.Run => 3,
                // DataAcquisitionServeiceState.Shutdown => 4,
                _ => throw new DataAcquisitionServiceUndefinedStateException(_state),
            };
        }
    }

    private async Task<byte> StopHandler()
    {   
        var delay = TimeSpan.FromSeconds(_settings.ConfigPeriodCheckSecond);
        await Task.Delay(delay);
        await _settings.Load();

        if (_settings.DataAcquisitionRun)
        {
            _state = DataAcquisitionServeiceState.Init;
        }

        return 0;
    }

    private async Task<byte> InitHandler()
    {
        await Task.Delay(1);

        return 0;
    }
}

public enum DataAcquisitionServeiceState
{
    Stop,
    Init,
    Run,
    Shutdown,
}
