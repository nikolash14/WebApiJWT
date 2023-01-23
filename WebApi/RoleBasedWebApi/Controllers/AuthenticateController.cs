using System.Net;
using ESSLogger;
using ESSModels.Interface;
using ESSModels.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAuthenticateService _authenticateSerive;
        private ILoggerUtility _loggerUtility;
        public AuthenticateController(IAuthenticateService authenticateService, ILoggerUtility loggerUtility)
        {
            _authenticateSerive = authenticateService;
            _loggerUtility = loggerUtility;
        }
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            try
            {
                var user = _authenticateSerive.Authenticate(model.UserName, model.Password);
                if (user == null)
                    return BadRequest(new { message = "Username or Password is incorrect." });
                _loggerUtility.WriteLog("Successfully Validated");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error while processing" });
            }
        }

    }
}
