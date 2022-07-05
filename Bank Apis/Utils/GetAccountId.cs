using Bank_Apis.Model;

namespace Bank_Apis.Utils
{
    public static class GetAccountIdClass
    {
        public static string AccountId(DatabaseContext _dbclient, string userId)
        {
            var branch = _dbclient.Branches.FirstOrDefault(x => x.UserId == userId);
            if(branch == null)
            {
                return null;
            }
            return branch.AccountId;
        }
    }
}
