using Bank_Apis.Model;
using Bank_Apis.Utils;
using Microsoft.EntityFrameworkCore;


namespace Bank_Apis.Services.Users
{
    public class UserAuthenticationActions : IUserAuthenticationInterface
    {
        private readonly DatabaseContext _dbClient;
        private readonly IConfiguration _configuration;

        public UserAuthenticationActions(IConfiguration iconfiguration, DatabaseContext _client)
        {
            _configuration = iconfiguration;
            _dbClient = _client;

        }

        public IEnumerable<User> GetUsers()
        {
            var users = _dbClient.Users.ToList();
            return users;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var User = await _dbClient.Users.FirstOrDefaultAsync(x => x.Email == user.Email || x.UserName == user.UserName);
            if(User == null)
            {
                _dbClient.Users.Add(user);
                await _dbClient.SaveChangesAsync();

                return user;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<User> GetUser(string Id)
        {
            var user = await _dbClient.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }

        public async Task<User> GetUserViaEmail(string email)
        {
            var user = await _dbClient.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        
        public string GetToken(string email, string Id)
        {
            var classInstance = new GenerateToken(_configuration);

            var token = classInstance.GetToken(email, Id);

            return token;
        }




    }
}
