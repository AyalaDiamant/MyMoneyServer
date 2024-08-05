using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IAreaServies
    {
        List<AreaDTO> GetAreas(int CurrentUserId, int? type);

        bool AddArea(AreaDTO area, int CurrentUserId);

        bool UpdateArea(AreaDTO area, int CurrentUserId);

        bool DeleteArea(int id, int CurrentUserId);

        List<IdName> GetAreaList(int userId, int type);
    }
    public class AreaServies : IAreaServies
    {
        private IDBService dbService;
        
        private IReportsServies reportsService;

        public AreaServies(IDBService dbService,IReportsServies reportsService)
        {
            this.dbService = dbService;
            this.reportsService = reportsService;
        }

        public List<AreaDTO> GetAreas(int CurrentUserId, int? type)
        {
            List<AreaDTO> list = new List<AreaDTO>();

            if (dbService.entities.User2Areas.Any(x => x.UserId == CurrentUserId))
            {
                var query = dbService.entities.User2Areas.Where(x => x.UserId == CurrentUserId).ToList();
                if (type > 0)
                {
                    query = query.Where(x => x.UserId == CurrentUserId && x.Type == (int)type).ToList();
                }
                
                list = query.Select(x => new AreaDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Type = x.Type,
                    Sum = x.Sum,
                    IsMaaser = x.IsMaaser,
                    IsActive = x.IsActive
                }).ToList();
            }
            return list;
        }

        public List<IdName> GetAreaList(int userId, int type)
        {
            List<IdName> list = new List<IdName>();
            list = dbService.entities.Areas.Where(x => x.Type == type).Select(x => new IdName()
            {
                Name = x.Description,
                Id = x.Id
            }).ToList();

            return list;
        }

        public bool AddArea(AreaDTO user2Area, int CurrentUserId)
        {
            //if (dbService.entities.User2Area.Any(x => x.UserId == CurrentUserId && x.Type == user2Subject.Type && x.Subject.Id != user2Subject.Id && x.Subject.Description == user2Subject.Description.Name))
            //{
            //    return false;
            //}
            //var subject = dbService.entities.User2Area.FirstOrDefault(x => x.Id == user2Subject.Description.Id);
            //if (user2Subject.Description.Id == 0)
            //{
            //    var newSubject = new Subject()
            //    {
            //        Description = user2Subject.Description.Name,
            //        Type = user2Subject.Type,
            //        IsGlobal = false
            //    };
            //    dbService.entities.Subjects.Add(newSubject);
            //    dbService.Save();
            //    user2Subject.Description.Id = newSubject.Id;
            //}
            var newUser2Subject = new User2Area()
            {
                UserId = CurrentUserId,
                Sum = user2Area.Sum,
                IsMaaser = user2Area.IsMaaser,
                IsActive = user2Area.IsActive,
                Description = user2Area.Description,
                Type = user2Area.Type
            };
            dbService.entities.User2Areas.Add(newUser2Subject);

            dbService.Save();
            SortByName(CurrentUserId, newUser2Subject.Type);
            return true;
        }

        public bool UpdateArea(AreaDTO area, int CurrentUserId)
        {
            if (dbService.entities.User2Areas.Any(x => x.UserId == CurrentUserId && x.Type == area.Type && x.Id != area.Id && x.Description == area.Description))
            {
                return false;
            }
            var dbUser2Area = dbService.entities.User2Areas.FirstOrDefault(x => x.Id == area.Id);

            if (dbUser2Area != null)
            {
                if (area.IsActive == false && dbUser2Area.IsActive == true)
                {

                  area.ActionOption = ActionOptions.IsNotActive;

                }
                else
                {
                area.ActionOption = ActionOptions.update;

                }

                reportsService.AddHistory(CurrentUserId, dbUser2Area, area);
                dbUser2Area.IsActive = area.IsActive;
                dbUser2Area.IsMaaser = area.IsMaaser;
                dbUser2Area.Description = area.Description;
                dbUser2Area.Sum = area.Sum;
                dbService.entities.SaveChanges();
                SortByName(CurrentUserId, area.Type);
                return true;
            }
            return false;
        }

        public bool DeleteArea(int id, int CurrentUserId)
        {
            var dbUser2area = dbService.entities.User2Areas.FirstOrDefault(x => x.UserId == CurrentUserId && x.Id == id);

            if (dbUser2area != null)
            {
                if (dbUser2area.Movings != null && dbUser2area.Movings.Count > 0)
                {
                    dbUser2area.IsActive = false;
                    dbService.Save();
                    return false;
                }
                else
                {

                    AreaDTO area = new AreaDTO();
                    area.ActionOption = ActionOptions.delete;
                    reportsService.AddHistory(CurrentUserId, dbUser2area, area);
                    dbService.entities.User2Areas.Remove(dbService.entities.User2Areas.FirstOrDefault(x => x.UserId == CurrentUserId && x.Id == id));
                    dbService.Save();

                    return true;
                }
            }
            return false;
        }

        public void SortByName(int CurrentUserId, int? type)
        {
            var list = dbService.entities.User2Areas.Where(x => x.UserId == CurrentUserId && x.Type == type).ToList();
            list = list.OrderBy(x => x.Description).ToList();
            dbService.Save();
        }

    }
}
