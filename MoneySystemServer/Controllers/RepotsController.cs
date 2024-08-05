using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    public class RepotsController : GlobalController
    {
        private IReportsServies repotsService;

        public RepotsController(IReportsServies ReportsService)
        {
            this.repotsService = ReportsService;
        }

       
        [HttpGet]
        public GResult<List<MovingReportsDTO>> GetMovingReports()
        {
            return Success(repotsService.GetMovingReports(UserId.Value));
        }
        //[HttpGet]
        //public GResult<List<DebtReportsDTO>> GetMovingReports()
        //{
        //    return Success(repotsService.GetMovingReports(UserId.Value));
        //}

        //האם לעשות את זה בקונטרולר
        [HttpGet]
        public GResult<List<HistoryDTO>> GetHistory()
        {
            return Success(repotsService.GetHistory(UserId.Value));
        }

        [HttpPost]
        public GResult<TithesDataDTO> GetSumOfTithe(SerchTitheDTO s)
        {
            return Success(repotsService.RepoTithe(UserId.Value, s));
        }

        [HttpGet]
        public GResult<List<int>> GetYears()
        {
            return Success(repotsService.YearsMoovings(UserId.Value));
        }

        //[HttpPost]
        //public Result AddHistory(int iD,AreaDTO area, AreaDTO area1)
        //{
        //    //    var isAreaExist = areaService.AddArea(area, UserId.Value);
        //    //    if (!isAreaExist)
        //    //    {
        //    //        return Fail(message: "area already exits");
        //    //    }
        //    return Success();
        //}

    }
}
