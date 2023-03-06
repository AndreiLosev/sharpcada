using EntityDeviceParameter = sharpcada.Data.Entities.DeviceParameter;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;

namespace sharpcada.Core.Factories;

public class DeviceParameterFactory : ICoreFactory
{
    public DeviceParameter Create(EntityDeviceParameter entity)
    {
        var fn = (float[] values) => new ForNetworkChunnel(
            value,
            entity.DevParameterNetChannels);

        return new DeviceParameter(
            entity,
            (float value) => new ForNetworkChunnel(value, entity.Id));
    }
}
