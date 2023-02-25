using Microsoft.AspNetCore.Mvc;
using sharpcada.Data.Repositories;

namespace sharpcada.Api.Settings.Controllers;

public class TestController : BaseControllers
{
    [HttpGet, Route("/get")]
    public async Task<IActionResult> Get(DeviceRepositories deviceRepositories)
    {
        var test = await deviceRepositories.AllAsync();
        await Task.Delay(1);
        return new JsonResult(test);
    }


}
