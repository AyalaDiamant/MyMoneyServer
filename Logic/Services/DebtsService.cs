using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IDebtsService
    {
        List<DebtDTO> GetDebts(int currentUserId, debtSearchDetails debtSearchDetails);
        void AddDebt(DebtDTO debt, int currentUserId);
        bool UpdateDebt(DebtDTO debt);
        bool DeleteDebt(int debtId);
        List<IdName> GetUrgencyies();
    }
    public class DebtsService : IDebtsService
    {
        IDBService dBService;
        public DebtsService(IDBService dBService)
        {
            this.dBService = dBService;
        }

        public void AddDebt(DebtDTO debt, int currentUserId)
        {
            var newDebt = new Debt()
            {
                Description = debt.Description,
                IsActive = debt.IsActive,
                Payments = debt.Payments,
                UrgencyId = debt.Urgency.Id,
                UserId = currentUserId,
                Sum = debt.Sum,
                AreaId = debt.Areaid.Id
            };
            dBService.entities.Debts.Add(newDebt);
            dBService.Save();
        }

        public bool DeleteDebt(int debtId)
        {
            var debt = dBService.entities.Debts.FirstOrDefault(x => x.Id == debtId);
            if (debt != null)
            {
                dBService.entities.Debts.Remove(debt);
                dBService.Save();
                return true;
            }
            return false;
        }

        public List<DebtDTO> GetDebts(int currentUserId, debtSearchDetails debtSearchDetails)
        {
            List<DebtDTO> list = new List<DebtDTO>();
            if (dBService.entities.Debts.Any(x => x.UserId == currentUserId))
            {
                var query = dBService.entities.Debts.Where(x => x.UserId == currentUserId);
                if (debtSearchDetails != null)
                {
                    if (!string.IsNullOrEmpty(debtSearchDetails.Description))
                    {
                        query = query.Where(x => x.Description.Contains(debtSearchDetails.Description));
                    }
                    if (!string.IsNullOrEmpty(debtSearchDetails.Urgency))
                    {
                        query = query.Where(x => x.Urgency.Description.Contains(debtSearchDetails.Urgency));
                    }
                    if (!string.IsNullOrEmpty(debtSearchDetails.Sum))
                    {
                        query = query.Where(x => x.Sum.ToString().Contains(debtSearchDetails.Sum));
                    }
                    if (!string.IsNullOrEmpty(debtSearchDetails.Payments))
                    {
                        query = query.Where(x => x.Payments.ToString().Contains(debtSearchDetails.Payments));
                    }

                }

                list = query.Select(x => new DebtDTO()
                {
                    Description = x.Description,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    Payments = x.Payments,
                    Urgency = new IdName()
                    {
                        Id = x.Id,
                        Name = x.Urgency.Description
                    },
                    Areaid = new Area()
                    {
                        Id = x.Area.Id,
                        Description = x.Area.Description
                    },
                    UserId = x.UserId,
                    Sum = x.Sum,
                }).ToList();
            }
            return list;
        }

        public List<IdName> GetUrgencyies()
        {
            List<IdName> list = new List<IdName>();
            list = dBService.entities.UrgencyDebts.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description,
            }).ToList();
            return list;
        }

        public bool UpdateDebt(DebtDTO debt)
        {
            var dbDebt = dBService.entities.Debts.FirstOrDefault(x => x.Id == debt.Id);
            if (dbDebt != null)
            {
                dbDebt.Description = debt.Description;
                dbDebt.IsActive = debt.IsActive;
                dbDebt.Payments = debt.Payments;
                dbDebt.UrgencyId = debt.Urgency.Id;
                dbDebt.Sum = debt.Sum;
                dBService.Save();
                return true;
            }
            return false;
        }


    }
}


