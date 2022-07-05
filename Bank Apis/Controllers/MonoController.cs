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
            var accountId = await _monoActions.GetAccountId(code, branch.UserId);

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
        public async Task<string> GetAccountInfos(string userId)
        {
            var accountInfo = await _monoActions.GetAccountInfo(userId);
            return accountInfo;
        }

        [HttpGet]
        [Route("AccountStatement/")]
        [Authorize]
        public async Task<string> GetAccountStatement(string userId)
        {
            var accountStatement = await _monoActions.GetAccountStatement(userId);
            return accountStatement;
        }

        [HttpGet]
        [Route("AccountStatement/(period)")]
        [Authorize]
        public async Task<string> GetAccountStatement(string accountId, int period)
        {
            var accountStatement = await _monoActions.GetAccountStatement(accountId, period);
            return accountStatement;
        }

        [HttpGet]
        [Route("Transactions/")]
        [Authorize]
        public async Task<string> GetTransactions(string accountId)
        {
            Console.WriteLine(accountId);
            var transactions = await _monoActions.GetTransactions(accountId);
            return transactions;
        }

        [HttpGet]
        [Route("AccountIdentity/")]
        [Authorize]
        public async Task<string> GetAccountIdentity(string accountId)
        {
            var accountIdentity = await _monoActions.GetAccountIdentity(accountId);
            return accountIdentity;
        }


        [HttpGet]
        [Route("Income/")]
        [Authorize]
        public async Task<string> GetAccountIncome(string accountId)
        {
            var Income = await _monoActions.GetIncome(accountId);
            return Income;
        }

        [HttpGet]
        [Route("/UnlinkAccount")]
        [Authorize]
        public async Task<string> UnlinkAccount(string accountId)
        {
            var Income = await _monoActions.UnlinkAccount(accountId);
            return Income;
        }

        [HttpGet]
        [Route("/ManualSync")]
        [Authorize]
        public async Task<string> ManualSync(string accountId)
        {
            var Data = await _monoActions.ManualSync(accountId);
            return Data;
        }

        [HttpGet]
        [Route("/ReAuth")]
        [Authorize]
        public async Task<string> ReAuth(string accountId)
        {
            var AuthCode = await _monoActions.ReAuth(accountId);
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
