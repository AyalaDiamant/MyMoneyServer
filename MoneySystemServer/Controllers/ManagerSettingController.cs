using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    public class ManagerSettingController : GlobalController
    {
        private IManagerSettingService managerSettingService;

        public ManagerSettingController(IManagerSettingService managerSettingService)
        {
            this.managerSettingService = managerSettingService;
        }
        public Result UpdateManagerSetting(ManagerSettingDTO managerSetting)
        {

            return Success(managerSettingService.UpdateManagerSetting(managerSetting, UserId.Value));
        }
    }
}
