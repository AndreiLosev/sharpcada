using sharpcada.Core.Contracts;
using sharpcada.Core;
using EntityDevice = sharpcada.Data.Entities.Device;

namespace srcback.Core.Factories;

class DeviceFactory<T> : ICoreFactory<Device<T>, EntityDevice>
{
    public Device<T> Create(EntityDevice entity)
    {

    }
}
