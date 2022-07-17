using Bank_Apis.Model;
using Mono.Net.Sdk;
using Mono.Net.Sdk.Models.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public async Task<string> GetAccountIdentity(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);

            var response = await _client.Accounts.GetUserIdentity(accountId);
            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> GetAccountInfo(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);

            var response = await _client.Accounts.GetInformation(accountId);
               return JsonConvert.SerializeObject(response.Data.Account);
            
        }

        public async Task<string> GetAccountStatement(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);
            var response = await _client.Accounts.GetStatementsInJson(accountId);

            return JsonConvert.SerializeObject(response.Data.StatementList); 
        }

        public async Task<string> GetAccountStatement(string branchId, int period)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);
            var response = await _client.Accounts.GetStatementsInJson(accountId, period);
            return JsonConvert.SerializeObject(response.Data.StatementList);
        }

        public async Task<string> GetIncome(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);

            var response = await _client.Accounts.GetIncome(accountId);

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> GetTransactions(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);
            var debitRes = await _client.Accounts.GetTransactions(accountId, null, null, null, 200, "debit", true);
            var creditRes = await _client.Accounts.GetTransactions(accountId, null, null, null, 200, "credit", true);

            var debitTransactions = JArray.Parse(JsonConvert.SerializeObject(debitRes.Data.Transactions));
            var creditTransactions = JArray.Parse(JsonConvert.SerializeObject(creditRes.Data.Transactions));

            var allTransactions = new JArray(debitTransactions.Union(creditTransactions));

            return JsonConvert.SerializeObject(allTransactions);
        }

        public async Task<string> InstutitionsList()
        {
            var response = await _client.Misc.GetInstitutions();

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> ManualSync(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);
            var response = await _client.Auth.SyncDataManually(accountId); ;

            return JsonConvert.SerializeObject(response.Data);
        }

        public async Task<string> ReAuth(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);
            var response = await _client.Auth.ReAuthorizeCode(accountId); ;

            return JsonConvert.SerializeObject(response.Data);
        }



        public async Task<string> UnlinkAccount(string branchId)
        {
            var userId = Utils.Monohelper.UserId(_dbclient, branchId);
            _client = _clientSetup.GetMonoClient(_dbclient, userId);

            var accountId = Utils.Monohelper.AccountId(_dbclient, branchId);
            var response = await _client.Misc.UnlinkAccount(accountId);

            return JsonConvert.SerializeObject(response.Data);
        }
    }
}
