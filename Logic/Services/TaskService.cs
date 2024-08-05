using Logic.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface ITaskService
    {
        List<TaskDTO> GetTasks(int CurrentUserId, TaskSearch taskSearch);

        // TaskDTO GetTask(int id);
        bool AddTask(TaskDTO task, int currentUserId);
        bool UpdateTask(TaskDTO task);
        bool DeleteTask(int id);



    }
    public class TaskService : ITaskService
    {
        private IDBService dbService;

        public TaskService(IDBService dbService)
        {
            this.dbService = dbService;
        }

        public List<TaskDTO> GetTasks(int CurrentUserId, TaskSearch taskSearch)
        {
            List<TaskDTO> tasks = new List<TaskDTO>();
            if (dbService.entities.Tasks.Any(x => x.UserId == CurrentUserId))
            {
                var query = dbService.entities.Tasks.Where(x => x.UserId == CurrentUserId);
                if (taskSearch != null)
                {
                    if (!string.IsNullOrEmpty(taskSearch.Description))
                    {
                        query = query.Where(x => x.Description.Contains(taskSearch.Description));
                    }
                    if (!string.IsNullOrEmpty(taskSearch.Comments))
                    {
                        query = query.Where(x => x.Comment.Contains(taskSearch.Comments));
                    }
                    if (taskSearch.CreateDate != default(DateTime))
                    {
                        query = query.Where(x => x.CreateDate.Date == taskSearch.CreateDate.Date);
                    }
                    // זה של פנינה לא למחוק
                    //if (taskSearch.DoDate != null)
                    //{
                    //    query = query.Where(x => x.DoDate == taskSearch.DoDate.Date);
                    //}
                    if (!string.IsNullOrEmpty(taskSearch.Urgency))
                    {
                        query = query.Where(x => x.Urgency.Description.Contains(taskSearch.Urgency));
                    }
                    if (!string.IsNullOrEmpty(taskSearch.Status))
                    {
                        query = query.Where(x => x.Status.Description.Contains(taskSearch.Status));
                    }
                }
                tasks = query.Select(x => new TaskDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    CreateDate = x.CreateDate,
                    Comments = x.Comment,
                    Status = new IdName
                    {
                        Id = x.Id,
                        Name = x.Status.Description
                    },
                    Urgency = new IdName
                    {
                        Id = x.Id,
                        Name = x.Urgency.Description
                    },
                    DoDate = x.DoDate

                }).ToList();
            }
            return tasks;
        }

        public bool AddTask(TaskDTO task, int currentUserId)
        {
            var newTask = new Task()
            {
                Description = task.Description,
                Comment = task.Comments,
                StatusId = task.Status.Id,
                UserId = currentUserId,
                CreateDate = DateTime.Now,
                UrgencyId = task.Urgency.Id,
                DoDate = task.DoDate
            };
            dbService.entities.Tasks.Add(newTask);
            dbService.Save();

            return true;

        }
        public bool UpdateTask(TaskDTO task)
        {
            var dbTask = dbService.entities.Tasks.FirstOrDefault(x => x.Id == task.Id);
            if (dbTask != null)
            {
                dbTask.Description = task.Description;
                dbTask.Comment = task.Comments;
                dbTask.StatusId = task.Status.Id;
                dbTask.DoDate = task.DoDate;
                dbTask.UrgencyId = task.Urgency.Id;

                dbService.Save();
                return true;
            }
            return false;
        }

        public bool DeleteTask(int id)
        {
            var dbTask = dbService.entities.Tasks.FirstOrDefault(x => x.Id == id);
            if (dbTask != null)
            {
                dbService.entities.Tasks.Remove(dbService.entities.Tasks.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;

        }
    }
}
