using Bank_Apis.Model;
using Bank_Apis.Services.Users;
using Bank_Apis.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bank_Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IUserAuthenticationInterface _userActions;

        public HomeController(IUserAuthenticationInterface userActions)
        {
            _userActions = userActions;
        }

       
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(User _user)
        {
            _user.Id = Guid.NewGuid().ToString();
            _user.Password = PasswordHash.HashPassword(_user.Password);
            _user.IsEmailVerified = false;
           
            var user = await _userActions.CreateUserAsync(_user);
            if(user == null)
            {
                ModelState.AddModelError("404", "Username or Email already Exists");
                return NotFound(ModelState);

               
            }
            var token = _userActions.GetToken(user.Email, user.Id);
            return Ok(
                    new
                    {
                        user,
                        token,
                        status = 200
                    }
                    );
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel _loginData)
        {
            var user = await _userActions.GetUserViaEmail(_loginData.Email);

            if (user != null)
            {
                bool checkPassword = PasswordHash.VerifyHash(user.Password, _loginData.Password);
                
                if (checkPassword)
                {
                    var token = _userActions.GetToken(user.Email, user.Id);
                    var monoKey = _userActions.GetServiceKey(user.Id);
                    return Ok(new
                    {
                        token, 
                        user,
                        monoKey
                    }

                    );
                }
                else
                {
                    ModelState.AddModelError("404", "Password is Incorrect");
                    return NotFound(ModelState);
                }
            }

            ModelState.AddModelError("404", "User not Found");
            return NotFound(ModelState);
        }

        [HttpPost("/user/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string Id, User user)
        {
            var UpdatedUser = await _userActions.UpdateUser(Id, user);
            if(UpdatedUser == null)
            {
                ModelState.AddModelError("404", "User not Found");
                return NotFound(ModelState);
            }
            return Ok(new {
                UpdatedUser
            });
        }

        [HttpPost]
        [Route("AddServiceKeys")]
        [Authorize]
        public async Task<IActionResult> AddServiceKeys(ServiceKeys _servicekey)
        {
            var serviceKey = await _userActions.AddAccountKeys(_servicekey);
            if (serviceKey == null)
            {
                ModelState.AddModelError("404", "User does not exists");
                return NotFound(ModelState);
              
            }
            return Ok(new
            {
                serviceKey.MonoPrivateKey
            });
        }

        [HttpGet]
        [Route("verifyToken")]
        public User VerifyToken(string token)
        {
            var res = _userActions.VerifyToken(token);
            return res;
        }

    }
}
