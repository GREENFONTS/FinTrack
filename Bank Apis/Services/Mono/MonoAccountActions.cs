using Bank_Apis.Model;
using Mono.Net.Sdk;
using Mono.Net.Sdk.Models.Auth;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Bank_Apis.Services.Mono
{
    public class MonoAccountActions : IMonoAccountInterface
    {
        private readonly DatabaseContext _dbclient;
        private MonoClient _client;

        private readonly MonoClientSetup _clientSetup = new();


        public MonoAccountActions(DatabaseContext client)
        {
            _dbclient = client;
        }
        //62c2b9c2ec4b9a0160c52fa3

        public async Task<string> GetAccountId(string code, string UserId)
        {
            //Get the account Id
            _client = _clientSetup.GetMonoClient(_dbclient, UserId);

            var response = await _client.Auth.GetAccountId(new AuthAccountRequest
            {
                Code = code
            });
            string accountId = response.Data.AccountId;          
            
            return accountId;
        }

        public async Task<string> GetAccountIdentity(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);

            var response = await _client.Accounts.GetUserIdentity(accountId);
            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> GetAccountInfo(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);

            var response = await _client.Accounts.GetInformation(accountId);
               return JsonConvert.SerializeObject(response.Data.Account);
            
        }

        public async Task<string> GetAccountStatement(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);
            var response = await _client.Accounts.GetStatementsInJson(accountId);

            return JsonConvert.SerializeObject(response.Data.StatementList); 
        }

        public async Task<string> GetAccountStatement(string userId, int period)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);
            var response = await _client.Accounts.GetStatementsInJson(accountId, period);
            return JsonConvert.SerializeObject(response.Data.StatementList);
        }

        public async Task<string> GetIncome(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);

            var response = await _client.Accounts.GetIncome(accountId);

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> GetTransactions(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);
            var response = await _client.Accounts.GetTransactions(accountId);
            return JsonConvert.SerializeObject(response.Data.Transactions);
        }

        public async Task<string> InstutitionsList()
        {
            var response = await _client.Misc.GetInstitutions();

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> ManualSync(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);
            var response = await _client.Auth.SyncDataManually(accountId); ;

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> ReAuth(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);
            var response = await _client.Auth.ReAuthorizeCode(accountId); ;

            return JsonConvert.SerializeObject(response.Data);
        }



        public async Task<string> UnlinkAccount(string userId)
        {
            _client = _clientSetup.GetMonoClient(_dbclient, userId);
            var accountId = Utils.GetAccountIdClass.AccountId(_dbclient, userId);
            var response = await _client.Misc.UnlinkAccount(accountId);

            return JsonConvert.SerializeObject(response.Data);
        }
    }
}
