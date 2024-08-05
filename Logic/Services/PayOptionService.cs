using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IPayOptionService
    {
        List<IdName> GetPayOptions(int CurrentUserId);
        bool AddPayOptions(IdName payOpt, int CurrentUserId);
        bool UpdatePayOption(IdName description, int CurrentUserId);
        bool DeletePayOption(int id, int CurrentUserId);


    }
    public class PayOptionService : IPayOptionService
    {
        private IDBService dbService;
        public PayOptionService(IDBService dbService)
        {
            this.dbService = dbService;
        }
        public List<IdName> GetPayOptions(int CurrentUserId)
        {
            List<IdName> list = new List<IdName>();
            var mngrId = dbService.entities.Users.Where(u => u.Id == CurrentUserId).FirstOrDefault().ManagerId;
            if (dbService.entities.PayOptions.Any(x => x.ManagerId == CurrentUserId||x.ManagerId ==mngrId))
            {
                list = dbService.entities.PayOptions.Where(x => x.ManagerId == CurrentUserId || x.ManagerId == mngrId).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                    IsActive = x.IsActive
                }).OrderBy(x => x.Name).ToList();
            }
            return list;
        }
        public bool AddPayOptions(IdName payOpt, int CurrentUserId)
        {
            if (!dbService.entities.PayOptions.Any(x => x.ManagerId == CurrentUserId && x.Description == payOpt.Name))
            {
                var newPayOpt = new PayOption()
                {
                    Description = payOpt.Name,
                    ManagerId = CurrentUserId,
                    IsActive = true
                };
                dbService.entities.PayOptions.Add(newPayOpt);
                dbService.entities.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdatePayOption(IdName payOpt, int CurrentUserId)
        {
            if (dbService.entities.PayOptions.Any(x => x.ManagerId == CurrentUserId && x.Id != payOpt.Id && x.Description == payOpt.Name))
            {
                return true;
            }
            var dbDescrip = dbService.entities.PayOptions.FirstOrDefault(x => x.ManagerId == CurrentUserId && x.Id == payOpt.Id);
            if (dbDescrip != null)
            {
                dbDescrip.Description = payOpt.Name;
                dbDescrip.IsActive = payOpt.IsActive;
                dbService.entities.SaveChanges();
                return false;
            }
            return true;
        }
        public bool DeletePayOption(int id, int CurrentUserId)
        {
            var dbPayOpt = dbService.entities.PayOptions.FirstOrDefault(x => x.ManagerId == CurrentUserId && x.Id == id);
            if (dbPayOpt != null)
            {
                if (dbPayOpt.Movings != null && dbPayOpt.Movings.Count > 0)
                {
                    dbPayOpt.IsActive = false;
                    dbService.entities.SaveChanges();
                    return false;
                }
                else
                {
                    dbService.entities.PayOptions.Remove(dbService.entities.PayOptions.FirstOrDefault(x => x.ManagerId == CurrentUserId && x.Id == id));
                    dbService.entities.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
