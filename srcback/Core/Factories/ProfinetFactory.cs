using sharpcada.Core.Contracts;

namespace sharpcada.Core.Factories;

public class ProfinetFactory : ICoreFactory
{
    private readonly Func<IProfiNet> _creater;

    public ProfinetFactory(IServiceProvider provider)
    {
        _creater = () => provider.GetRequiredService<IProfiNet>();
    }

    public IProfiNet Create() => _creater();
}
