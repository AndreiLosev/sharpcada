using sharpcada.Core.DTO;

namespace sharpcada.Core.Contracts;

public interface INetworkChannel<T> where T : class
{
    public Task<ForDeviceParametr[]> ReadAsync(T client);
    public Task WriteAsync(T client, ForNetworkChunnel[] forChannel);
    public bool IsRead();
    public bool IsWrite();
}
