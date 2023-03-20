using EntityDevice = sharpcada.Data.Entities.Device;

namespace sharpcada.Core.Devices;

public abstract class Device<T> where T : class
{
    protected string _name;
    protected string _ipAddres;
    protected Dictionary<long, Contracts.INetworkChannel<T>> _writeNetworkChannels;
    protected Dictionary<long, Contracts.INetworkChannel<T>> _readNetworkChannels;
    protected Dictionary<long, DeviceParameter> _parameters;
    protected T _client;

    public Device(
        EntityDevice device,
        Dictionary<long, Contracts.INetworkChannel<T>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameters,
        T client)
    {
        _name = device.Name;
        _ipAddres = device.IpAddres;
        _client = client;
        _parameters = deviceParameters;
        _writeNetworkChannels = networkChannels
            .Where(c => c.Value.IsWrite())
            .ToDictionary(c => c.Key, c => c.Value);
        _readNetworkChannels = networkChannels
            .Where(c => c.Value.IsRead())
            .ToDictionary(c => c.Key, c => c.Value);

    }

    public async Task Read()
    {
        foreach (var channel in _readNetworkChannels)
        {
            var readResult = new DTO.ForDeviceParametr[] {};

            try
            {
                readResult = await channel.Value.ReadAsync(_client);
            }
            catch (System.Exception e)
            {
                //TODO handle execption
                Console.WriteLine(e);
            }
            
            foreach (var item in readResult)
            {
                _parameters[item.ParamAddres].SetValue(item.Value);
            }
        }
    }

    public Dictionary<long, DTO.DeviceParameterView> GetParameters() =>
        _parameters.ToDictionary(p => p.Key, p => p.Value.GetView());

    public async Task Write(Dictionary<long, float> values)
    {
        var preporation = values
            .Select(v => _parameters[v.Key].prepareForWriteing(v.Value))
            .GroupBy(v => v.ChanelAddres)
            .Select(v => (key: v.Key, value: v.ToArray()));

        foreach (var item in preporation)
        {
            await this._writeNetworkChannels[item.key].WriteAsync(_client, item.value);
        }
    }
}
