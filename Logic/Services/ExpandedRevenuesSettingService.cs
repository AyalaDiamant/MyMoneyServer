using Logic.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IExpandedRevenuesService {
        List<PresenceSettingsDTO> GetDaysRevenuesExpanded();
        List<AmountSettingsDTO> GetProductsRevenuesExpanded();
        bool AddPresenceRevenue(PresenceSettingsDTO newRevenue);
        bool AddAmountRevenue(AmountSettingsDTO newRevenue);
    }

    public class ExpandedRevenuesSettingService : IExpandedRevenuesService
    {
        private IDBService dbService;

        public ExpandedRevenuesSettingService( IDBService dbService )
        {
            this.dbService = dbService;
        }

        public List<PresenceSettingsDTO> GetDaysRevenuesExpanded() {

            List<PresenceSettingsDTO> list = new List<PresenceSettingsDTO>();

            list=dbService.entities.PresenceSettings.Select(x=>new PresenceSettingsDTO()
            {
                Day=x.Day,
                Hours=x.Hours,
            }).ToList();

            return list;
        }

        public bool AddPresenceRevenue(PresenceSettingsDTO newRevenue)
        {
            PresenceSetting revenue = new PresenceSetting()
            {
                Day = newRevenue.Day,
                Hours = newRevenue.Hours,
            };

            dbService.entities.PresenceSettings.Add(revenue);
            dbService.Save();
            return true;
        }

        public List<AmountSettingsDTO> GetProductsRevenuesExpanded() {

            List<AmountSettingsDTO> list = new List<AmountSettingsDTO>();

            list = dbService.entities.AmountSettings.Select(x => new AmountSettingsDTO()
            {
                Day = x.Day,
                Product = x.Product,
                ProductType = x.ProductType,
                ProductValue = x.ProductValue,
                ProductQuantity = x.ProductQuantity,
            }).ToList();

            return list;
        }

        public bool AddAmountRevenue(AmountSettingsDTO newRevenue)
        {
            AmountSetting amount = new AmountSetting()
            {
                Day = newRevenue.Day,
                Product = newRevenue.Product,
                ProductQuantity = newRevenue.ProductQuantity,
                ProductType = newRevenue.ProductType,
                ProductValue = newRevenue.ProductValue,
            };
            dbService.entities.AmountSettings.Add(amount);
            dbService.Save();
            return true;
        }  

    }

}
