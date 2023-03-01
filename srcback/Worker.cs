namespace sharpcada;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private int _counter;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _counter = 0;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _counter++;
            _logger.LogInformation("Worker running at: {time}", _counter);
            await Task.Delay(3000, stoppingToken);
        }
    }

    public void setCounter(int value)
    {
        _counter = value;
    }
}
