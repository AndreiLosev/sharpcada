using sharpcada.Core.Contracts;

namespace sharpcada.Core.NetworkProtocol;

public class Profinet : IProfiNet
{
    public void Connect(string idAddres, ushort prot)
    {
        Console.WriteLine($"{idAddres}:{prot}");
    }
}
