using Mono.Net.Sdk;
using Mono.Net.Sdk.Models.Auth;

namespace Bank_Apis.Services.Mono
{
    public class MonoClientSetup
    {
        public readonly MonoClient client;

        private readonly string secret_key = "live_sk_jdrysL9K5fH7fRwms7Nn";

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
