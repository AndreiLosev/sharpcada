namespace sharpcada.Core.Contracts;

public interface INetworkChannel<T> where T : class
{
    public Task<byte[]> ReadAsync(T client);
    public Task WriteAsync(T client);
    public bool IsRead();
    public bool IsWrite();
}
