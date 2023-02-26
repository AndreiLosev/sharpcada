using Microsoft.AspNetCore.Mvc;
// using sharpcada.Data.Entities;
using sharpcada.Data.Repositories;

namespace sharpcada.Api.Settings.Controllers;

public class TestController : BaseControllers
{
    private DeviceRepository _test;

    public TestController(DeviceRepository test)
    {
        _test = test;
    }

    [HttpGet, Route("/get")]
    public async Task<IActionResult> Get()
    {
        var res = await _test.AllAsync();

        Console.WriteLine($"count: {res.Count}");

        // var res = test.Name;
        await Task.Delay(1);
        return new JsonResult(res);
    }


}
