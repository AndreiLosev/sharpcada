using EntityDeviceParameter = sharpcada.Data.Entities.DeviceParameter;
using EntityModbusDev = sharpcada.Data.Entities.ModbusDevice;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;
using sharpcada.Core.Enams;

namespace sharpcada.Core.Factories;

public class DeviceParameterFactory : ICoreFactory
{
    public DeviceParameter? Create(EntityDeviceParameter entity)
    {
        if (entity.DevParameterNetChannels is null)
        {
            return null;
        }

        var writeDevParameterNetChannels = entity.DevParameterNetChannels
            .Where(d => d.ChannelType is NetworkChannelType.Write)
            .First();


        if (writeDevParameterNetChannels is null)
        {
            return null;
        }

        var device = entity.Device;

        if (device is not EntityModbusDev)
        {
            throw new System.Exception();
        }


        return new DeviceParameter(
            entity,
            (float value) => new ForNetworkChunnel(
                value,
                entity.Type,
                ((EntityModbusDev)device).ByteOrder,
                writeDevParameterNetChannels));
    }

    public Dictionary<long, DeviceParameter> CreateDictionary(
        ICollection<EntityDeviceParameter> entities)
    {
        var result = new Dictionary<long, DeviceParameter>();

        foreach (var entity in entities)
        {
            var devParamOrNull = this.Create(entity);
            if (devParamOrNull is DeviceParameter devParam)
            {
                result.Add(entity.Id, devParam);
            }
        }

        return result;
    }
}
