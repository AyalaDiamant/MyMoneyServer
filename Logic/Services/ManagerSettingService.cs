using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IManagerSettingService
    {
        bool AddManagerSetting(ManagerSettingDTO managerSetting, int CurrentUserId);
        bool UpdateManagerSetting(ManagerSettingDTO managerSetting, int CurrentUserId);

    }
    public class ManagerSettingService : IManagerSettingService
    {
        private IDBService dbService;
        public ManagerSettingService(IDBService dbService)
        {
            this.dbService = dbService;
        }

        //-----------
        public bool AddManagerSetting(ManagerSettingDTO managerSetting, int CurrentUserId)
        {
            var newManagerSetting = new ManagerSetting();
            newManagerSetting.ManagerId = CurrentUserId;
            newManagerSetting.ContactMen = managerSetting.ContactMen;
            newManagerSetting.Email = managerSetting.Email;
            newManagerSetting.Adress = managerSetting.Adress;
            newManagerSetting.OrganizationName = managerSetting.OrganizationName;
            newManagerSetting.Phon = managerSetting.Phon;
            newManagerSetting.OrganizationType = managerSetting.OrganizationType;

            dbService.entities.ManagerSettings.Add(newManagerSetting);
            dbService.Save();

            return true;
        }
        public bool UpdateManagerSetting(ManagerSettingDTO managerSetting, int CurrentUserId)
        {
            var dbUpdateManagerSetting = dbService.entities.ManagerSettings.FirstOrDefault(x => x.Id == managerSetting.Id);
            if (dbUpdateManagerSetting == null)
            {
                return (AddManagerSetting(managerSetting, CurrentUserId));
            }
            else
            {
                dbUpdateManagerSetting.ManagerId = CurrentUserId;
                dbUpdateManagerSetting.ContactMen = managerSetting.ContactMen;
                dbUpdateManagerSetting.Email = managerSetting.Email;
                dbUpdateManagerSetting.Adress = managerSetting.Adress;
                dbUpdateManagerSetting.OrganizationName = managerSetting.OrganizationName;
                dbUpdateManagerSetting.Phon = managerSetting.Phon;
                dbUpdateManagerSetting.OrganizationType = managerSetting.OrganizationType;
                dbService.Save();
            }
            return true;
        }
    }
}
