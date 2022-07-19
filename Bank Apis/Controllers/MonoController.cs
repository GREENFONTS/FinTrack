using Bank_Apis.Services.Branches;
using Bank_Apis.Services.Mono;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Bank_Apis.Controllers
{
    [Route("api/user/branch/")]
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
        [Route("AccountId/{branchId}")]
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
        [Route("AccountInfo/{branchId}")]
        [Authorize]
        public async Task<string> GetAccountInfos(string branchId)
        {
            var accountInfo = await _monoActions.GetAccountInfo(branchId);
            return accountInfo;
        }

        [HttpGet]
        [Route("AccountStatement/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetAccountStatement(string branchId)
        {
            var accountStatement = await _monoActions.GetAccountStatement(branchId);
            if (accountStatement == null)
            {
                ModelState.AddModelError("404", "Request Failed");
                return NotFound(ModelState);
            }
            return Ok(new
            {
                accountStatement
            }

                    );
        }

        [HttpGet]
        [Route("AccountStatement/{branchId}/(period)")]
        [Authorize]
        public async Task<IActionResult> GetAccountStatement(string branchId, int period)
        {
            var accountStatement = await _monoActions.GetAccountStatement(branchId, period);
            if(accountStatement == null)
            {
                ModelState.AddModelError("404", "Request Failed");
                return NotFound(ModelState);
            }
            return Ok(new
            {
               accountStatement
            }

                    );
        }

        [HttpGet]
        [Route("Transactions/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetTransactions(string branchId)
        {
            var transactions = await _monoActions.GetTransactions(branchId);
            if (transactions == null)
            {
                ModelState.AddModelError("404", "Request Failed");
                return NotFound(ModelState);
            }
            return Ok(new
            {
                transactions
            }

                    );
        }

        [HttpGet]
        [Route("AllTransactions/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllTransactions(string userId)
        {
            var transactions = await _monoActions.GetAllTransactions(userId);
            if (transactions == null)
            {
                ModelState.AddModelError("404", "Request Failed");
                return NotFound(ModelState);
            }
            return Ok(new
            {
                transactions
            }

                    );
        }

        [HttpGet]
        [Route("AccountIdentity/{branchId}")]
        [Authorize]
        public async Task<string> GetAccountIdentity(string branchId)
        {
            var accountIdentity = await _monoActions.GetAccountIdentity(branchId);
            return accountIdentity;
        }


        [HttpGet]
        [Route("Income/{branchId}")]
        [Authorize]
        public async Task<string> GetAccountIncome(string branchId)
        {
            var Income = await _monoActions.GetIncome(branchId);
            return Income;
        }

        [HttpGet]
        [Route("UnlinkAccount/{branchId}")]
        [Authorize]
        public async Task<IActionResult> UnlinkAccount(string branchId)
        {
            var res = await _monoActions.UnlinkAccount(branchId);
            if(res == null)
            {
                ModelState.AddModelError("404", "Request Failed");
                return NotFound(ModelState);
            }
            return Ok(
                new {
                res});

        }

        [HttpGet]
        [Route("/ManualSync/{branchId}")]
        [Authorize]
        public async Task<string> ManualSync(string branchId)
        {
            var Data = await _monoActions.ManualSync(branchId);
            return Data;
        }

        [HttpGet]
        [Route("/ReAuth/{branchId}")]
        [Authorize]
        public async Task<string> ReAuth(string branchId)
        {
            var AuthCode = await _monoActions.ReAuth(branchId);
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
