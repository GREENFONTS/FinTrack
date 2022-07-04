using Mono.Net.Sdk;
using Mono.Net.Sdk.Models.Auth;

namespace Bank_Apis.Services.Mono
{
    public class MonoClientSetup
    {
        public readonly MonoClient client;

        private readonly string secret_key = "your mono secret key";

        public MonoClientSetup()
        {
            client = new MonoClient(secret_key);
        }

        public MonoClient GetMonoClient()
        {
            return client;
        }

       
    }
}
