using System.Runtime.InteropServices;

namespace Bank_Apis.Services.Mono
{
    public interface IMonoAccountInterface
    {

        public Task<string> GetAccountId(string code, string UserId);
        public Task<string> GetAccountInfo(string accountId);

        public Task<string> GetAccountStatement(string accountId);
        public Task<string> GetAccountStatement(string accountId, int period);

        public Task<string> GetTransactions(string accountId);

        public Task<string> GetIncome(string accountId);

        public Task<string> UnlinkAccount(string accountId);

        public Task<string> GetAccountIdentity(string accountId);

        public Task<string> ManualSync(string accountId);

        public Task<string> ReAuth(string accountId);

        public Task<string> InstutitionsList();
    }
}
