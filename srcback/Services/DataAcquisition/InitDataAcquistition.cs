using sharpcada.Data.Repositories;

namespace sharpcada.Services.DataAcquisition;

public class InitDataAcquistition : Contracts.IServices
{
    private readonly DevicesRepository _devicesRepository;
    private readonly NetworkChannelRepository _networkChannelRepository;

    public InitDataAcquistition(
        DevicesRepository devicesRepository,
        NetworkChannelRepository networkChannelRepository)
    {
        _devicesRepository = devicesRepository;
        _networkChannelRepository = networkChannelRepository;
    }
}
