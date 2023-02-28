using sharpcada.Data.Repositories;
using sharpcada.Data.Entities;

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

    public async Task<ICollection<Device>> GetDevicesWithChanells(ICollection<long>? devicesIds = null)
    {
        var devices = devicesIds switch
        {
            null => await _devicesRepository.GetAsync(),
            _ => await _devicesRepository.GetAsync(devicesIds),
        };

        await _networkChannelRepository.LoadFor(devices);

        return devices;
    }
}
