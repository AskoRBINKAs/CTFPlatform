using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatform.Controllers;

[ApiController]
[Route("api/settings")]
[Authorize(Roles = "Admin")]
public class SettingsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllSettings()
    {
        return Ok(ContestSettings.GetAllSettings());
    }

    [HttpGet("ctf")]
    public IActionResult GetCTFSettings()
    {
        return Ok(ContestSettings.GetAllSettings()[0]);
    }

    [HttpPost("ctf/{param:required}")]
    public IActionResult EditCTFSettings(string param, string value)
    {
        switch (param)
        {
            case "started":
                ContestSettings.CTF.Started = bool.Parse(value);
                break;
            case "dynscore":
                ContestSettings.CTF.DynamicScoreEnabled = bool.Parse(value);
                break;
            case "teamsenabled":
                ContestSettings.CTF.TeamsEnabled = bool.Parse(value);
                break;
            case "flagsaccepting":
                ContestSettings.CTF.FlagAcceptingEnabled = bool.Parse(value);
                break;
            case "teamsize":
                ContestSettings.CTF.TeamSize = int.Parse(value);
                break;
            default:
                return NotFound("Parameter not found");
        }

        return Ok("success");
    }

    [HttpGet("userssettings")]
    public IActionResult GetUsersSettings()
    {
        return Ok(ContestSettings.GetAllSettings()[1]);
    }

    [HttpPost("userssettings/{param:required}")]
    public IActionResult EditUsersSettings(string param, string value)
    {
        switch (param)
        {
            case "registrationallowed":
                ContestSettings.UserConfiguration.RegistrationAllowed = bool.Parse(value);
                break;
            case "accessalloweddefault":
                ContestSettings.UserConfiguration.AccessAllowedDefault = bool.Parse(value);
                break;
            default:
                return NotFound("Parameter not found");
        }

        return Ok("success");
    }
}