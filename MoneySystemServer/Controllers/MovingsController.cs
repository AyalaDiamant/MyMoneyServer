using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    //[Authorize]
    public class MovingsController : GlobalController
    {
        private IMovingService movingService;
        public MovingsController( IMovingService movingService)
        {
            this.movingService = movingService;
        }

        [HttpPost]
        public GResult<List<MovingDTO>> GetMovings(Search search)
        {
            //if (!user.IsActive)
            //{
            //    return Fail(new List<MovingDTO>(), "user.Is!Active");
            //}
            return Success(movingService.GetMovings(search, UserId.Value));
        }

        [HttpPost]
        public Result AddMove(MovingDTO move)
        {
            var isMoveExist = movingService.AddMove(move, UserId.Value);
            if (!isMoveExist)
            {
                return Fail(message: "move exits already");
            }
            return Success();
        }

        [HttpPut]
        public Result UpdateMove(MovingDTO move)
        {
            var isMoveExist = movingService.UpdateMove(move, UserId.Value);
            if (!isMoveExist)
            {
                return Fail(message: "move exits already");
            }
            return Success();
        }

        [HttpDelete("{id}")]
        public Result DeleteMove(int id)
        {
            var isMoveExist = movingService.DeleteMove(id, UserId.Value);
            if (!isMoveExist)
            {
                return Fail(message: "move not found");
            }
            return Success();
        }
        [HttpGet]
        public GResult<Filters> GetFilters(int type)
        {
            return Success(movingService.GetFilters(type, UserId.Value));
        }

    }
}
