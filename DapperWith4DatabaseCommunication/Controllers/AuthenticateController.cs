using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Interfaces;
using DapperWith4DatabaseCommunication.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DapperWith4DatabaseCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        //by using IConfiguration interface we can read/Fetch any object/key information from appsettings.json file
        private readonly IConfiguration _configuration;
        public AuthenticateController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("UserSignInWithoutToken")]//Siginin/Login both are same.
        //Here i am verifyibg the username and password ,once it will valid i am returning the uservalid messge.
        public async Task<IActionResult> UserSignInWithoutToken([FromBody] LoginDTO loginDTOObj)
        {
            var res = await _authenticateService.UserSignIn(loginDTOObj);
            return Ok(res);
        }
        [HttpPost]
        [Route("UserSignIn1")]//Siginin/Login both are same.
        //Here i am verifyibg the username and password ,once it will valid i am generting the token.
        public async Task<IActionResult> UserSignIn([FromBody] LoginDTO loginDTOObj)
        {
            UserLoginResponse loginResponseObj = new UserLoginResponse();
            //Below process is recomended to validate the apis filed for validation.realtime use this process.
            var validationMessages = ValidationMessages.UserSignIn(loginDTOObj);
            if (validationMessages.Length > 0)
            {
                
                return StatusCode(StatusCodes.Status400BadRequest,Convert.ToString(validationMessages));
            }
            //if (!ModelState.IsValid)
            //{//this way not rememendedway to implemt validations
            //    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            //}
            else
            {
                var res = await _authenticateService.UserSignIn(loginDTOObj);
                if(res.StatusCode == ConstantMessages.SuccessStatusCode)
                {//if user details are validated successfully and the response conatins success status code 200,we will fetch the userroles information by using username.
                    var userRolesObj = await _authenticateService.GetUserRolesInformation(loginDTOObj);
                    var claims = new[] {//here prepare the claims array and stored into your required information to prepare the token.
                        new Claim("UserName",userRolesObj.UserName),//here map your username 
                        new Claim("EmailId", userRolesObj.EmailId),//here map your emailid  
                        new Claim("PhoneNumber", userRolesObj.PhoneNumber),//here map your PhoneNumber 
                        new Claim("Address", userRolesObj.Address),//here map your Address 
                        new Claim("IsActive", Convert.ToString(userRolesObj.IsActive)),//here map your IsActive 
                        new Claim("Roles", userRolesObj.RoleName)//here map your RoleName 
                    };
                    //this symentric security key comming from Microsoft.IdentityModel.Tokens  namespace
                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    //this SigningCredentials comming from Microsoft.IdentityModel.Tokens  namespace
                    //HmacSha256 is the encryption alogrotham.
                    //create object for SigningCredentials and pass key and algoritham.
                    SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    //this JwtSecurityToken comming from System.IdentityModel.Tokens.Jwt namespace                                
                    JwtSecurityToken token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,//pass the claims object here.
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpriyTime"])),//SET THE TIME FOR  YOUR TOKEN 
                        signingCredentials: signIn);
                    loginResponseObj.UserMessage = res.StatusMessage;
                    loginResponseObj.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);//pass the token object here
                    return Ok(loginResponseObj);

                }
                else
                {
                    loginResponseObj.UserMessage = res.StatusMessage;
                    loginResponseObj.AccessToken = "Userisinvalid,due to that we are not provided token,please enter correct credentials";
                }
                return Ok();
            }
        }
    }
}
/* 1.what  type of authentication you have implemeted in your project?
 * A)Jwt token based authentication implemented in my project.
 * =>By using Jwt Token based authentication we can provide the security to my  api's.
 * =>Without token you can't access my api's.Token is require to access my api's.
 * 2)How to implement token based authentication in your project?
 * A)In my project I have implemented Jwt token based authentication.
 * =>To implement token based authentication first install "Microsoft.AspNetCore.Authentication.JwtBearer" package in your project.
 * =>Next create one authenticatecontroller and in that write usersigin  Api method and pass LoginDto  class.
 * =>Next  we need to validate username and password is exist or not  in the database.
 * =>once user is  exist in db we return successmessage to api method alone with status code 200,after that based on username we can fetch required userinformation like (username,roles,email..)data we can fetch.
 * =>Next we need to store this userinfomration in the claims[] array.
 * =>Next  in appsettings.json we need to write one Jwt json object,which contain (key,issuer,audience,Subject an token expiry time keys)these are used in our token prepartion code.
 ======================================just for understanding=======================    
"Jwt": { //To prepare the token the below key values information is required.
    "Key": "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx",
    "Issuer": "JWTAuthenticationServer",
    "Audience": "JWTServicePostmanClient",
    "Subject": "JWTServiceAccessToken",
    "TokenExpriyTime": 4
  },
=====================================================================================
=>we have one predefined class called SymmetricSecurityKey we need to create object for that one  and pass the key.which  is read from appsetting.json file
=>this  SymmetricSecurityKey   object need to pass argument for SigningCredentials predefined class object and pass HmacSha256 SecurityAlgorithm.to encrypt the data purpose we need this HmacSha256 SecurityAlgorithm.
=>After that create object for JwtSecurityToken predefined class and pass Issuer,Audience,claims,TokenExpriyTime,signingCredentials.
=>next create object for JwtSecurityTokenHandler class it contains WriteToken() method you need  to pass JwtSecurityToken object. this will prepare  new token.
=>token is encrypt format,it contains all userinformation in encrypt format.this token we are returning from api.
=>this token used in Angular/react/mobile applications for each and every request they will send access our apis.
=>without token you can't get the api information.
=>next put[Authorize] attribute  for required controllers above .if you are not apply this [Authorize] attribute,everyone can access our api's
=>to validate the token in program.cs we need to write code in   builder.Services.AddAuthentication section we have one TokenValidationParameters class is there,
here we need to check whatever the token you passed that contains issuer,audience,jwt key is there or not,it should match with token prepartion time Whatever we are provided key,issuer,audience.
=>once this issuer,audeience,jwt key matched then only you can access the data from api.
=>if these are not matched it will return 401 unauthorized messgae from api.
===========================================================
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
===============================================================

 * 
 */