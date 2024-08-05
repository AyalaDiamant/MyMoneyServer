using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    public class DebtsController : GlobalController
    {
        private IDebtsService debtsService;

        public DebtsController(IDebtsService debtsService)
        {
            this.debtsService = debtsService;
        }

        [HttpPost]
        public GResult<List<DebtDTO>> GetDebts(debtSearchDetails debtSearchDetails)
        {
            return Success(debtsService.GetDebts(UserId.Value, debtSearchDetails));
        }

        [HttpPost]
        public Result AddDebt(DebtDTO debt)
        {
            debtsService.AddDebt(debt, UserId.Value);
            return Success();
        }

        [HttpPut]
        public Result UpdateDebt(DebtDTO debt)
        {
            debtsService.UpdateDebt(debt);
            return Success();
        }

        [HttpDelete("{debtId}")]
        public Result DeleteDebt(int debtId)
        {
            var isDebtExist = debtsService.DeleteDebt(debtId);
            if (!isDebtExist)
                return Fail(message: "Unable to delete, probably already deleted");
            return Success();
        }

        [HttpGet]
        public GResult<List<IdName>> GetUrgencyies()
        {
            return Success(debtsService.GetUrgencyies());
        }
    }
}
