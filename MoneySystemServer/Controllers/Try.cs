using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;

namespace Api.Controllers
{
    public class Try : GlobalController
    {
        //private ManagerSettingService managerSettingService;
        //public Try(ManagerSettingService managerSettingService) 
        //{ 
        //    this.managerSettingService = managerSettingService;
        //}
        [HttpGet]
        public GResult<ManagerDesignDTO> GetSetting()
        {
            return new GResult<ManagerDesignDTO>();   
        }
    }
}
