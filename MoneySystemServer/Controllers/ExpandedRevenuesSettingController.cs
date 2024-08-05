using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;


namespace Api.Controllers
{
    public class ExpandedRevenuesSettingController : GlobalController
    {
        private IExpandedRevenuesService expandedRevenuesService;

        public ExpandedRevenuesSettingController(IExpandedRevenuesService expandedRevenuesService)
        {
            this.expandedRevenuesService = expandedRevenuesService;
        }

        [HttpGet]
        public GResult<List<PresenceSettingsDTO>> GetDaysRevenuesExpanded() 
        { 
            return Success(expandedRevenuesService.GetDaysRevenuesExpanded());
        }

        [HttpPost]
        public Result AddPresenceRevenue(PresenceSettingsDTO newRevenue)
        {
            var isDayExist = expandedRevenuesService.AddPresenceRevenue(newRevenue);

            return Success();
        }

        [HttpGet]
        public GResult<List<AmountSettingsDTO>> GetProductsRevenuesExpanded()
        {
            return Success(expandedRevenuesService.GetProductsRevenuesExpanded());
        }

        [HttpPost]
        public Result AddAmountRevenue(AmountSettingsDTO newRevenue) 
        {
            var isProductExist = expandedRevenuesService.AddAmountRevenue(newRevenue);
            return Success();
        }

    }
}
