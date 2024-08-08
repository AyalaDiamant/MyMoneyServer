using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    public class ManagerDesignController : GlobalController
    {
        private IManagerDesignService managerDesignService;

        public ManagerDesignController(IManagerDesignService managerDesignService)
        {
            this.managerDesignService = managerDesignService;
        }

        [HttpGet]
        public GResult<ManagerDesignDTO> GetManagerDesign()
        {
            if (UserId.Value != null)
                return Success(managerDesignService.GetManagerDesign(UserId.Value));
            return Success(managerDesignService.GetManagerDesign(0));

        }

        [HttpPut]
        public Result UpdateManagerDesign([FromForm] ManagerDesignDTO file)
        {
            var request = Request;
            ManagerDesignDTO mDesign = null;
            mDesign = new ManagerDesignDTO()
            {
                Id = file.Id,
                ManagerId = UserId.Value,
                Title = file.Title,
                Slogan = file.Slogan,
                HeaderColor = file.HeaderColor,
                TextColor = file.TextColor,
                FileName = file.FileName,
                ImageContent = file.ImageContent,
            };
            if (request.Form != null && request.Form.Files != null && request.Form.Files.Count > 0 && request.Form.Files[0] != null && request.Form.Files[0].Length > 0)
            {

                byte[] data = null;

                using (var ms = new MemoryStream())
                {
                    request.Form.Files[0].CopyTo(ms);
                    data = ms.ToArray();
                }
                mDesign.ImageContent = data;

            }
            return Success(managerDesignService.UpdateManagerDesign(mDesign, UserId.Value));
        }
        //-------
        // שייך לקונטרולר אחר אבל עושה בעיות אז בינתיים זה פה

        [HttpGet]
        public GResult<List<ManagerSettingDTO>> GetManagerSetting()
        {
            return Success(managerDesignService.GetManagerSetting(UserId.Value));
        }
        [HttpPut]
        public Result UpdateManagerSetting(ManagerSettingDTO managerSetting)
        {

            return Success(managerDesignService.UpdateManagerSetting(managerSetting, UserId.Value));
        }
    }
}
