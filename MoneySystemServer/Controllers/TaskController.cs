using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    [IsActive]
    public class TaskController : GlobalController
    {
        private ITaskService TaskService;
        public TaskController( ITaskService taskService)
        {
            this.TaskService = taskService;
        }
        [HttpPost]
        public GResult<List<TaskDTO>> GetTasks(TaskSearch taskSearch)
        {
            return Success(TaskService.GetTasks(UserId.Value,taskSearch));

        }

        [HttpPost]
        public Result AddTask(TaskDTO task)
        {
            TaskService.AddTask(task, UserId.Value);
            return Success();
        }
        [HttpPut]
        public Result UpdateTask(TaskDTO task)
        {
            TaskService.UpdateTask(task);
            return Success();
        }

        [HttpDelete("{id}")]
        public Result DeleteTask(int id)
        {
            var isTaskExist = TaskService.DeleteTask(id);
            if (!isTaskExist)
                return Fail();
            return Success();
        }

    }
}
