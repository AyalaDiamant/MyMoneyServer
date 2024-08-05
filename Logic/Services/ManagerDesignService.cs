using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IManagerDesignService
    {
        ManagerDesignDTO GetManagerDesign(int ManagerId);
        bool AddManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId);
        bool UpdateManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId);
        ManagerDesignDTO GetFile(int id);
    }
    public class ManagerDesignService : IManagerDesignService
    {
        private IDBService dbService;
        public ManagerDesignService(IDBService dbService)
        {
            this.dbService = dbService;
        }
        public ManagerDesignDTO GetManagerDesign(int managerId)
        {
            var mDesign = new ManagerDesignDTO();
            ManagerDesign dbmDesign = null;

            if (managerId == null)
            {
                // במידה ואין ID, שלוף את השורה הראשונה בטבלה
                dbmDesign = dbService.entities.ManagerDesigns.FirstOrDefault();
            }
            var managerUser = dbService.entities.Users.FirstOrDefault(x => x.Id == managerId);
            if (managerUser.UserTypeId != 1 && managerUser.UserTypeId != 4)
                dbmDesign = dbService.entities.ManagerDesigns.FirstOrDefault(x => x.ManagerId == managerUser.ManagerId);
            else if (managerUser.UserTypeId == 1 || managerUser.UserTypeId == 4)
                dbmDesign = dbService.entities.ManagerDesigns.FirstOrDefault(x => x.ManagerId == managerId);
            if (dbmDesign != null)
            {
                mDesign.Id = dbmDesign.Id;
                mDesign.Title = dbmDesign.Title;
                mDesign.Slogan = dbmDesign.Slogan;
                mDesign.HeaderColor = dbmDesign.HeaderColor;
                mDesign.ImageContent = dbmDesign.ImageContent;
                mDesign.TextColor = dbmDesign.TextColor;
                mDesign.FileName = dbmDesign.FileName;
                //mDesign.Src = "File/ShowFileDesign/" + dbmDesign.Id;
                mDesign.Src = "File/ShowFileDesign/" + dbmDesign.Id + "!" + DateTime.Now.Millisecond;


            }
            else if (dbmDesign == null) {
            }
            return mDesign;
        }
        public bool AddManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId)
        {
            var newManagerDesign = new ManagerDesign();
            newManagerDesign.ManagerId = CurrentUserId;
            newManagerDesign.HeaderColor = (managerDesign.HeaderColor);
            newManagerDesign.ImageContent = (managerDesign.ImageContent);
            newManagerDesign.Title = (managerDesign.Title);
            newManagerDesign.Slogan = (managerDesign.Slogan);
            newManagerDesign.TextColor = (managerDesign.TextColor);
            newManagerDesign.FileName = (managerDesign.FileName);

            dbService.entities.ManagerDesigns.Add(newManagerDesign);

            dbService.Save();
            return true;
        }

        public bool UpdateManagerDesign(ManagerDesignDTO managerDesign, int CurrentUserId)
        {
            var dbUpdateManagerDesign = dbService.entities.ManagerDesigns.FirstOrDefault(x => x.Id == managerDesign.Id);
            if (dbUpdateManagerDesign == null)
            {
                return (AddManagerDesign(managerDesign, CurrentUserId));
            }
            else
            {
                dbUpdateManagerDesign.ManagerId = CurrentUserId;
                dbUpdateManagerDesign.HeaderColor = (managerDesign.HeaderColor);
                dbUpdateManagerDesign.ImageContent = (managerDesign.ImageContent);
                dbUpdateManagerDesign.Title = (managerDesign.Title);
                dbUpdateManagerDesign.Slogan = (managerDesign.Slogan);
                dbUpdateManagerDesign.TextColor = (managerDesign.TextColor);
                dbUpdateManagerDesign.FileName = (managerDesign.FileName);
                dbService.Save();
            }
            return true;
        }

        public ManagerDesignDTO GetFile(int id)
        {
            var mdFile = new ManagerDesignDTO();
            var dbFile = dbService.entities.ManagerDesigns.FirstOrDefault(x => x.Id == id);
            if (dbFile != null)
            {
                mdFile.ImageContent = dbFile.ImageContent;
                mdFile.FileName = dbFile.FileName;
            }
            return mdFile;
        }
    }
}
