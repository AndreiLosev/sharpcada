using Microsoft.AspNetCore.Mvc;
using sharpcada.Data.Repositories;
using sharpcada.Data.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace sharpcada.Api.Settings.Controllers;

public class TestController : BaseControllers
{
    private DevicesRepository _test;
    public NetworkChannelRepository _ncr;

    public TestController(
            DevicesRepository test,
            NetworkChannelRepository ncr)
    {
        _test = test;
        _ncr = ncr;
    }

    [HttpGet, Route("/get")]
    public async Task<IActionResult> Get()
    {
        var dev = await _test.GetAsync();
        await _ncr.LoadFor(dev);

        var mc = dev
            .Select(d => d.NetworkChannels)
            .FirstOrDefault();
        ;

        if (mc is null)
        {
            return new JsonResult(new { Name = "local host 111" });
        }

        foreach (var mc1 in mc)
        {
            if (mc1 is ModbusChannel mc2)
            {
                Console.WriteLine(mc2.FunctionCode);
            }
        }

        var opt = new JsonSerializerOptions(JsonSerializerOptions.Default);
        opt.ReferenceHandler = ReferenceHandler.Preserve;
        opt.WriteIndented = true;

        return new JsonResult(mc, opt);
    }


}
