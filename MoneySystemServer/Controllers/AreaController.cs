using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;
using MoneySystemServer.Controllers;

namespace Api.Controllers
{



    [IsActive]
    public class AreaController : GlobalController
    {
        private IAreaServies areaService;
        public AreaController( IAreaServies areaService)
        {
            this.areaService = areaService;
        }

        [HttpGet("{type}")]
        public GResult<List<AreaDTO>> GetAreas(int type)
        {
            return Success(areaService.GetAreas(UserId.Value, type));
        }

        [HttpGet("{type}")]
        public GResult<List<IdName>> GetAreaList(int type)
        {
            return Success(areaService.GetAreaList(UserId.Value, type));
        }

        [HttpPost]
        public Result AddArea(AreaDTO area)
        {
            var isAreaExist = areaService.AddArea(area, UserId.Value);
            if (!isAreaExist)
            {
                return Fail(message: "area already exits");
            }
            return Success();
        }

        [HttpPut]
        public Result UpdateArea(AreaDTO area)
        {
            var isAreaExist = areaService.UpdateArea(area, UserId.Value);
            if (!isAreaExist)
            {
                return Fail(message: "area already exits");
            }
            return Success();
        }

        [HttpDelete("{id}")]
        public Result DeleteArea(int id)
        {
            var isAreaExist = areaService.DeleteArea(id, UserId.Value);
            if (!isAreaExist)
            {
                return Fail(message: "area is conected to a move or not found");
            }
            return Success();
        }
    }
}


