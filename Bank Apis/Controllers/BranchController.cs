using Bank_Apis.Model;
using Bank_Apis.Services.Branches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bank_Apis.Controllers
{
    [Route("api/Branch/")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchInterface _branchActions;

        public BranchController(IBranchInterface userActions)
        {
            _branchActions = userActions;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Branch> GetBranches(string userId)
        {
            var branches = _branchActions.GetBranches(userId);
            return branches;
        }

        [HttpPost]
        [Route("/api/create")]
        [Authorize]
        public async Task<KeyValuePair<string, object>[]> Register(Branch _branch)
        {

            _branch.BranchId = Guid.NewGuid().ToString();
            var branch = await _branchActions.CreateBranchAsync(_branch);

            if(branch != null)
            {
                return new[] {
                    new KeyValuePair<string, object>("Created Branch", branch),
                    new KeyValuePair<string, object>("state", "Success")}; ;
            }
            
            return new[] {
                    new KeyValuePair<string, object>("Created Branch", null),
                    new KeyValuePair<string, object>("state", "Branch Already Exists")};
        }

        [HttpGet("Id")]
        [Authorize]
        public Branch GetBranch(string Id)
        {
            var branch = _branchActions.GetBranch(Id);
            return branch;
        }

        [HttpPut("Id")]
        [Authorize]
        public async Task<Branch> UpdateBranch(string Id, Branch branch)
        {
            var UpdatedBranch = await _branchActions.UpdateBranch(Id, branch);
            return UpdatedBranch;
        }

        [HttpDelete("Id")]
        [Authorize]
        public async Task<Branch> DeleteBranch(string Id)
        {
            var DeletedBranch = await _branchActions.DeleteBranch(Id);
            return DeletedBranch;
        }



    }
}

   
   

       
