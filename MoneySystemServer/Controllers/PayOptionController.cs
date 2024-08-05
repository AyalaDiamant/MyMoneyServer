using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneySystemServer.Controllers
{
    public class PayOptionController : GlobalController
    {
        private IPayOptionService payOptionService;

        public PayOptionController( IPayOptionService payOptionService)
        {
            this.payOptionService = payOptionService;
        }
        [HttpGet]
        public GResult<List<IdName>> GetPayOptions()
        {
            return Success(payOptionService.GetPayOptions(UserId.Value));
        }
        [HttpPost]
        public Result AddPayOptions(IdName payOpt)
        {
            var isPayOptExist = payOptionService.AddPayOptions(payOpt, UserId.Value);
            if (!isPayOptExist)
            {
                return Fail(message: "a pay option with such description already exists.");
            }
            return Success();
        }
        [HttpPut]
        public Result UpdatePayOption(IdName payOpt)
        {
            var isPayOptExist = payOptionService.UpdatePayOption(payOpt, UserId.Value);
            if (isPayOptExist)
            {
                return Fail(message: "pay option with such description already exits");
            }
            return Success();
        }
        [HttpDelete("{id}")]
        public Result DeletePayOption(int id)
        {
            var isPayOptExist = payOptionService.DeletePayOption(id, UserId.Value);
            if (!isPayOptExist)
            {
                return Fail(message: "pay option is conected to a move or not found");
            }
            return Success();
        }

    }
}
