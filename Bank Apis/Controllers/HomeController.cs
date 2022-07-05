using Bank_Apis.Model;
using Bank_Apis.Services.Users;
using Bank_Apis.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var users = _userActions.GetUsers();
            return users;
        }

        [HttpPost]
        [Route("register")]
        public async Task<KeyValuePair<string, object>[]> Register(User _user)
        {
            _user.Id = Guid.NewGuid().ToString();
            _user.Password = PasswordHash.HashPassword(_user.Password);
            _user.IsEmailVerified = false;
           
            var user = await _userActions.CreateUserAsync(_user);
            if(user == null)
            {
                var res = new[] {
                    new KeyValuePair<string, object>("user", null),
                    new KeyValuePair<string, object>("state", "Email/UserName already exists")};
                return res;
            }
            return new[] {
                    new KeyValuePair<string, object>("user", user),
                    new KeyValuePair<string, object>("state", "Success")}; ;
        }

        [HttpPost]
        [Route("login")]
        public async Task<KeyValuePair<string, object>[]> Login(LoginModel _loginData)
        {
            var user = await _userActions.GetUserViaEmail(_loginData.Email);

            if (user != null)
            {
                bool checkPassword = PasswordHash.VerifyHash(user.Password, _loginData.Password);
                
                if (checkPassword)
                {
                    var token = _userActions.GetToken(user.Email, user.Id);
                    var res = new[] {
                    new KeyValuePair<string, object>("token", token),
                    new KeyValuePair<string, object>("user", user),
                    new KeyValuePair<string, object>("state", "Success")};

                    return res;
                }
                else
                {
                    var res = new[] {
                    new KeyValuePair<string, object>("token", null),
                    new KeyValuePair<string, object>("user", null),
                    new KeyValuePair<string, object>("state", "Password is Incorrect")};

                    return res;
                }
            }
            return new[] {
                    new KeyValuePair<string, object>("token", null),
                    new KeyValuePair<string, object>("user", null),
                    new KeyValuePair<string, object>("state", "User not Found")}; ;
        }

        [HttpPut("Id")]
        [Authorize]
        public async Task<User> UpdateUser(string Id, User user)
        {
            var UpdatedUser = await _userActions.UpdateUser(Id, user);
            return UpdatedUser;
        }

        [HttpPost]
        [Route("/AddServiceKeys")]
        [Authorize]
        public async Task<KeyValuePair<string, object>[]> AddServiceKeys(ServiceKeys _servicekey)
        {
            var serviceKey = await _userActions.AddAccountKeys(_servicekey);
            if (serviceKey == null)
            {
                var res = new[] {
                    new KeyValuePair<string, object>("serviceKey", null),
                    new KeyValuePair<string, object>("state", "User does not exists")};
                return res;
            }
            return new[] {
                    new KeyValuePair<string, object>("servicekey", serviceKey),
                    new KeyValuePair<string, object>("state", "Success")}; ;
        }

    }
}
