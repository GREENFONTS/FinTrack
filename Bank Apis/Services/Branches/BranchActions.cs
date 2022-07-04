using Bank_Apis.Model;

namespace Bank_Apis.Services.Branches
{
    public class BranchActions : IBranchInterface
    {
            private readonly DatabaseContext _dbClient;
            private readonly IConfiguration _configuration;

        public BranchActions(IConfiguration iconfiguration, DatabaseContext _client)
         {
                _configuration = iconfiguration;
                _dbClient = _client;

         }

        public async Task<Branch> CreateBranchAsync(Branch branch)
        {
            var Branch = _dbClient.Branches.FirstOrDefault(x => x.BranchName == branch.BranchName & x.UserId == branch.UserId);
            if (Branch == null)
            {
                _dbClient.Branches.Add(branch);
                await _dbClient.SaveChangesAsync();

                return branch;
            }

            return null; 

           
        }

        public async Task<Branch> DeleteBranch(string Id)
        {
            var branch = _dbClient.Branches.FirstOrDefault(x => x.BranchId == Id);
            if(branch == null)
            {
                return null;
            }
            _dbClient.Branches.Remove(branch);
             await _dbClient.SaveChangesAsync();

            return branch;
          }

        public Branch GetBranch(string Id)
        {
            var branch = _dbClient.Branches.FirstOrDefault(x => x.BranchId == Id);

            return branch;
        }

        public IEnumerable<Branch> GetBranches(string userId)
        {
            var branches = _dbClient.Branches.Where(x => x.UserId == userId).ToList();

            return branches;
        }

        public async Task<Branch> UpdateBranch(string Id, Branch branch)
        {
            var currentBranch = _dbClient.Branches.SingleOrDefault(x => x.BranchId == Id);
            if(currentBranch == null)
            {
                return null;
            }
            else
            {
                _dbClient.Entry(currentBranch).CurrentValues.SetValues(branch);
                await _dbClient.SaveChangesAsync();
                return branch;
            }

        }
    }
}
