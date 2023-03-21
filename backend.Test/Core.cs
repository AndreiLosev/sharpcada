using sharpcada.Core.Factories;
namespace backend.Test.Core;

public class CoreTest
{
    [Fact]
    public void Test1()
    {
        var devParamFactoy = new DeviceParameterFactory();
        Assert.Equal("hel1lo", "hello");       
    }
}
