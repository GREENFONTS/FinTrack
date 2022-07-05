using Bank_Apis.Model;

namespace Bank_Apis.Services.Users
{
    public interface IUserAuthenticationInterface
    {
        public Task<User> CreateUserAsync(User user);

        public IEnumerable<User> GetUsers();

        public Task<User> GetUser(string Id);

        public Task<User> UpdateUser(string Id, User user);

        public Task<User> GetUserViaEmail(string email);

        public string GetToken(string email, string Id);

        public Task<ServiceKeys> AddAccountKeys(ServiceKeys serviceKeys);

    }
}
