using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    public class ListController : GlobalController
    {

        private IListService listService;

        public ListController(IListService listService)
        {
            this.listService = listService;
        }
        [IsManager]
        public GResult<ListsDTO> GetAllLists()
        {
            return Success(listService.GetAllLists(UserId.Value,ManagerId.Value));
        }
        [IsPermission]
        [HttpPost]
        public GResult<List<IdName>> GetList(IdNameDB item)
        {
            return Success(listService.GetList(item, UserId.Value,ManagerId.Value));
        }

        [HttpPost]
        [IsManager]
        public Result AddItem(IdNameDB idName)
        {
            var isSuccess = listService.AddItem(idName, UserId.Value,ManagerId.Value);
            if (isSuccess)
            {
                return Success();
            }
            return Fail(message: "ארעה שגיאה");
        }

        [HttpPost]
        [IsManager]
        public Result DeleteItem(IdNameDB idName)
        {
            var isSuccess = listService.DeleteItem(idName,ManagerId.Value);
            if (isSuccess )
            {
                return Success("נמחק בהצלחה");
            }

            return Fail(message: "אפשרות תשלום הוגדרה כלא פעילה אין אפשרות למחוק תחומים  מסיבה שיש תנועות עם אפשרות זו.");
        }

        [HttpPut]
        [IsManager]
        public Result UpdateItem(IdNameDB idName)
        {
            var isSuccess = listService.UpdateItem(idName, UserId.Value,ManagerId.Value);
            if (isSuccess)
            {
                return Success();
            }
            return Fail(message: "אין אפשרות לעדכן תחומים עם תחום זה");
        }
    }
}
