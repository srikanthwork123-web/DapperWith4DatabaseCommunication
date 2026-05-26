using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWith4DatabaseCommunication.Controllers
{
    //if you are not applying [Authorize] attribute here,any one can access my apis.
    //eventhogh if you are implemented token based authentication,if you forget to mention [authorize] attribute here,any one can access your apis.
    //here we are not mentioned [Authorize] attribute due to that any one can access this api,without token
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        [HttpPost]
        [Route("RolesCreation")]
        public async Task<IActionResult> RolesCreation([FromBody] RolesDTO rolesDTOObj)
        {        //Here we are creating the roles by using this api method.
            //we are storing the roles information by using this api.
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _rolesService.RolesCreation(rolesDTOObj);
                    return Ok(res);
                }
        }
    }
}
