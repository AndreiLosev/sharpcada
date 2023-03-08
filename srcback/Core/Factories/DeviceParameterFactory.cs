using EntityDeviceParameter = sharpcada.Data.Entities.DeviceParameter;
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

        (long singleAddres, long multipleAddres, ushort indexNumber, byte bitIndexNumber) param =
            (singleAddres: 0L, multipleAddres: 0L, indexNumber: 0, bitIndexNumber: 0);

        var single = entity.DevParameterNetChannels
            .Where(d => d.ChannelType is NetworkChannelType.WriteSingle)
            .First();

        if (single is not null)
        {
            param.singleAddres = single.NetworkChannelId;
        }

        var multiple = entity.DevParameterNetChannels
            .Where(d => d.ChannelType is NetworkChannelType.WriteMultiple)
            .First();

        if (multiple is not null)
        {
            param.multipleAddres = multiple.NetworkChannelId;
            param.indexNumber = multiple.IndexNumber;
            param.bitIndexNumber = multiple.BitIndexNumber;
        }

        return new DeviceParameter(
            entity,
            (float value) => new ForNetworkChunnel(value, entity.Type, param));
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
