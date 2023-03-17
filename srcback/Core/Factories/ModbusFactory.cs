using sharpcada.Core.Contracts;

namespace sharpcada.Core.Factories;

public class ModbusFactory : ICoreFactory
{
    private readonly Func<IModbus> _creater;

    public ModbusFactory(IServiceProvider provider)
    {
        _creater = () => provider.GetRequiredService<IModbus>();
    }

    public IModbus Create() => _creater();
}
