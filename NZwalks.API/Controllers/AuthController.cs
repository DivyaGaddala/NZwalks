using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repository;


namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> usermanager;

        public ITokenRepository tokenrepository;

        //POST:/api/Auth/Register

        public AuthController(UserManager<IdentityUser> usermanager , ITokenRepository tokenrepository)
        {
            this.usermanager = usermanager;
            this.tokenrepository = tokenrepository;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestdto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestdto.UserName,
                Email = registerRequestdto.UserName
            };

          var identityResult=  await usermanager.CreateAsync(identityUser,registerRequestdto.Password);

            if (identityResult.Succeeded)
            {
                //add role to this user

                if (registerRequestdto.roles != null && registerRequestdto.roles.Any())
                {
                   identityResult= await usermanager.AddToRolesAsync(identityUser, registerRequestdto.roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("user has registered successfully! Please Login");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }



        //POST:/api/Auth/Login
        [HttpPost]
        [Route("Loginj")]

       public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginrequestdto)
        {
            //use the user manager class to find the username
           var user=await usermanager.FindByEmailAsync(loginrequestdto.username);

            //if user is not null
            if(user!=null)
            {
                //then go forward and check the pwd also using usserManager class
              var checkpswdresult=  await usermanager.CheckPasswordAsync(user, loginrequestdto.password);

                if(checkpswdresult)
                {
                    //Get roles for this user
                    var roles = await usermanager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //Create Token
                        var jwttoken = tokenrepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            Jwttoken = jwttoken
                        };
                        return Ok(response);

                    }
                }
            }

            return BadRequest("Username or password is incorrect");
        }

    }
}
