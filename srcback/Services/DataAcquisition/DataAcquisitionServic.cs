namespace sharpcada.Services.DataAcquisition;

public class DataAcquisitionServic : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        


        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(120000, stoppingToken);
        }
    }
}

public enum DataAcquisitionServicState
{
    Stop,
}
