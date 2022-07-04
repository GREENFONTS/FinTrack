using Bank_Apis.Model;
using Bank_Apis.Services.Users;
using Bank_Apis.Utils;
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
        [Route("/register")]
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
        [Route("/login")]
        public async Task<KeyValuePair<string, object>[]> Login(string email, string password)
        {
            var user = await _userActions.GetUserViaEmail(email);

            if (user != null)
            {
                bool checkPassword = PasswordHash.VerifyHash(user.Password, password);
                
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

        
    }
}
