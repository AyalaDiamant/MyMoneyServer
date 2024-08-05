using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface ISettingsService
    {
        string AllowAllPermissions { get; set; }
    }

    public class SettingsService : ISettingsService
    {
        private static string allowAllPermissions;

        #region CTOR
        private MyMoneyBContext db;

        public SettingsService(MyMoneyBContext db)
        {
            this.db = db;
        }

        #endregion CTOR

        #region Functions
        public string AllowAllPermissions
        {
            get
            {
                return allowAllPermissions;
            }
            set
            {
                allowAllPermissions = value;
            }
        }


        #endregion Functions
    }
}
