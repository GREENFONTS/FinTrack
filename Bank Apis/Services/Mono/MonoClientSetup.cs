using Bank_Apis.Model;
using Mono.Net.Sdk;
using Mono.Net.Sdk.Models.Auth;

namespace Bank_Apis.Services.Mono
{
    public class MonoClientSetup
    {
        public MonoClient GetMonoClient(DatabaseContext _client, string UserId)
        {
            var Key = _client.ServiceKeys.FirstOrDefault(x => x.UserId == UserId)!;
            Console.WriteLine(Key.MonoSecretKey);
            var client = new MonoClient(Key.MonoSecretKey);
            return client;
        }

       
    }
}
