using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    public class PresenceController : GlobalController
    {
        private IPresenceService presenceService;
        public PresenceController( IPresenceService presenceService)
        {
            this.presenceService = presenceService;
        }
        [HttpPost]
        public GResult<List<PresenceDTO>> GetPresences(PresenceSearch presence)
        {
            return Success(presenceService.GetPresences(UserId.Value, presence));
        }
        [HttpPost]
        public Result AddPresence(PresenceDTO presence)
        {
            return Success(presenceService.AddPresence(presence, UserId.Value));
        }
        [HttpPut]
        public Result UpdatePresence(PresenceDTO presence)
        {
            bool isSuccess = presenceService.UpdatePresence(presence);
            if (!isSuccess)
                return Fail("presence not exist");
            return Success();
        }

        [HttpDelete("{id}")]
        public Result DeletePresence(int id)
        {
            bool isSuccess = presenceService.DeletePresence(id);
            if (!isSuccess)
                return Fail("presence not exist");
            return Success();

        }

    }
}
