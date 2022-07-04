using Mono.Net.Sdk;
using Mono.Net.Sdk.Models.Auth;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Bank_Apis.Services.Mono
{
    public class MonoAccountActions : IMonoAccountInterface
    {
        private readonly MonoClient _client;
        private readonly MonoClientSetup _clientSetup = new MonoClientSetup();

        public MonoAccountActions()
        {
            _client = _clientSetup.GetMonoClient();
        }
        //62c2b9c2ec4b9a0160c52fa3

        public async Task<string> GetAccountId(string code)
        {
            //Get the account Id
            var response = await _client.Auth.GetAccountId(new AuthAccountRequest
            {
                Code = code
            });
            string accountId = response.Data.AccountId;          
            
            return accountId;
        }

        public async Task<string> GetAccountIdentity(string accountId)
        {
           var response = await _client.Accounts.GetUserIdentity(accountId);
            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> GetAccountInfo(string accountId)
        {
            
            var response = await _client.Accounts.GetInformation(accountId);
               return JsonConvert.SerializeObject(response.Data.Account);
            
        }

        public async Task<string> GetAccountStatement(string accountId)
        {
           var response = await _client.Accounts.GetStatementsInJson(accountId);

            return JsonConvert.SerializeObject(response.Data.StatementList); 
        }

        public async Task<string> GetAccountStatement(string accountId, int period)
        {
            var response = await _client.Accounts.GetStatementsInJson(accountId, period);
            return JsonConvert.SerializeObject(response.Data.StatementList);
        }

        public async Task<string> GetIncome(string accountId)
        {
            var response = await _client.Accounts.GetIncome(accountId);

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> GetTransactions(string accountId)
        {
            var response = await _client.Accounts.GetTransactions(accountId);

            return JsonConvert.SerializeObject(response.Data.Transactions);
        }

        public async Task<string> InstutitionsList()
        {
            var response = await _client.Misc.GetInstitutions();

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> ManualSync(string accountId)
        {
            var response = await _client.Auth.SyncDataManually(accountId); ;

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> ReAuth(string accountId)
        {
            var response = await _client.Auth.ReAuthorizeCode(accountId); ;

            return JsonConvert.SerializeObject(response.Data);
        }



        public async Task<string> UnlinkAccount(string accountId)
        {
            var response = await _client.Misc.UnlinkAccount(accountId);

            return JsonConvert.SerializeObject(response.Data);
        }
    }
}
