using Bank_Apis.Services.Branches;
using Bank_Apis.Services.Mono;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Bank_Apis.Controllers
{
    [Route("api/user/{branchId}/")]
    [ApiController]
    public class MonoController : ControllerBase
    {
        private readonly IMonoAccountInterface _monoActions;

        private readonly IBranchInterface _branchActions;

        public MonoController(IMonoAccountInterface monoActions, IBranchInterface branchActions)
        {
           _monoActions = monoActions;
            _branchActions = branchActions;
        }

        [HttpGet]
        [Route("AccountId")]
        [Authorize]
        public async Task<string> GetAccountId(string code, string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if(branch == null)
            {
                return null;
            }
            var accountId = await _monoActions.GetAccountId(code);

            if(accountId == null)
            {
                return null;
            }
            else
            {
                branch.AccountId = accountId;
                await _branchActions.UpdateBranch(branchId, branch);
                return accountId;
            }   
        }

        [HttpGet]
        [Route("AccountInfo")]
        [Authorize]
        public async Task<string> GetAccountInfos(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var accountInfo = await _monoActions.GetAccountInfo(branch.AccountId);
            return accountInfo;
        }

        [HttpGet]
        [Route("AccountStatement/")]
        [Authorize]
        public async Task<string> GetAccountStatement(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var accountStatement = await _monoActions.GetAccountStatement(branch.AccountId);
            return accountStatement;
        }

        [HttpGet]
        [Route("AccountStatement/(period)")]
        [Authorize]
        public async Task<string> GetAccountStatement(string branchId, int period)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var accountStatement = await _monoActions.GetAccountStatement(branch.AccountId, period);
            return accountStatement;
        }

        [HttpGet]
        [Route("Transactions/")]
        [Authorize]
        public async Task<string> GetTransactions(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var transactions = await _monoActions.GetTransactions(branch.AccountId);
            return transactions;
        }

        [HttpGet]
        [Route("AccountIdentity/")]
        [Authorize]
        public async Task<string> GetAccountIdentity(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var accountIdentity = await _monoActions.GetAccountIdentity(branch.AccountId);
            return accountIdentity;
        }


        [HttpGet]
        [Route("Income/")]
        [Authorize]
        public async Task<string> GetAccountIncome(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var Income = await _monoActions.GetIncome(branch.AccountId);
            return Income;
        }

        [HttpGet]
        [Route("/UnlinkAccount")]
        [Authorize]
        public async Task<string> UnlinkAccount(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var Income = await _monoActions.UnlinkAccount(branch.AccountId);
            return Income;
        }

        [HttpGet]
        [Route("/ManualSync")]
        [Authorize]
        public async Task<string> ManualSync(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var Data = await _monoActions.ManualSync(branch.AccountId);
            return Data;
        }

        [HttpGet]
        [Route("/ReAuth")]
        [Authorize]
        public async Task<string> ReAuth(string branchId)
        {
            var branch = _branchActions.GetBranch(branchId);

            if (branch == null)
            {
                return null;
            }
            var AuthCode = await _monoActions.ReAuth(branch.AccountId);
            return AuthCode;
        }



        [HttpGet]
        [Route("/Instutitions")]
        public async Task<string> InstutitionsList()
        {
            var instutitions = await _monoActions.InstutitionsList();
            return instutitions;
        }



    }
}
