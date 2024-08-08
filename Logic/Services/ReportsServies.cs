using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{


    public interface IReportsServies
    {
        List<MovingReportsDTO> GetMovingReports(int current);
        List<HistoryDTO> GetHistory(int current);
        bool AddHistory(int Id, User2Area oldDetails, AreaDTO newDetails);
        public TithesDataDTO RepoTithe(int userId, SerchTitheDTO s);
        public List<int> YearsMoovings(int userId);

    }



    public class ReportsService : IReportsServies
    {
        private IDBService dbService;

        public ReportsService(IDBService dbService)
        {
            this.dbService = dbService;

        }

        public List<MovingReportsDTO> GetMovingReports(int current)
        {

            var reports = new List<MovingReportsDTO>();

            var oldMove = dbService.entities.Movings.Where(x => x.User2Area.UserId == current).OrderBy(x => x.Date).FirstOrDefault();
            if (oldMove != null)
            {
                DateTime firstDate = oldMove.Date;

                DateTime oldMonth = new DateTime(firstDate.Year, firstDate.Month, 1);
                DateTime lastMonth = DateTime.Now;
                DateTime finalMonth = new DateTime(lastMonth.Year, lastMonth.Month, 1);


                for (DateTime date = oldMonth; date < lastMonth; date = date.AddMonths(1))
                {
                    int? sumOfExpenses = 0;
                    int? sumOfRevenues = 0;

                    var monings = dbService.entities.Movings.Where(x => x.User2Area.UserId == current && x.Date.Year == date.Year && x.Date.Month == date.Month).ToList();
                    if (monings != null)
                    {
                        var expenses = monings.Where(l => l.User2Area.Type == 2);
                        var revenues = monings.Where(l => l.User2Area.Type == 1);

                        // var expenses = monings.Where(l => l.Subject.Type == 2);
                        // var revenues = monings.Where(l => l.Subject.Type == 1);

                        if (expenses != null)
                        {
                            sumOfExpenses = expenses.Sum(l => l.Sum);
                        }
                        if (revenues != null)
                        {
                            sumOfRevenues = revenues.Sum(l => l.Sum);
                        }
                    }

                    reports.Add(new MovingReportsDTO()
                    {
                        Month = date.Month.ToString(),
                        Year = date.Year.ToString(),
                        Expenses = sumOfExpenses,
                        Revenues = sumOfRevenues
                    });

                    // logic here

                }
            }

            return reports;
        }


        public List<HistoryDTO> GetHistory(int current)
        {
            List<HistoryDTO> historys = new List<HistoryDTO>();
            historys = dbService.entities.Histories.Where(x => x.UserId == current).Select(x => new HistoryDTO()
            {
                Id = x.Id,
                UserId = x.UserId,
                DateofChange = x.DateofChange,
                OldDomain = x.OldDomain,
                NewDomain = x.NewDomain,
                OldAmount = x.OldAmount,
                NewAmount = x.NewAmount,
                //ActionOption = x.ActionOption
            }).ToList();


            return historys;
        }


        public bool AddHistory(int Id, User2Area oldDetails, AreaDTO newDetails)
        {
            History newHistory = new History();
            int noAmount = 0;
            newHistory.UserId = Id;
            newHistory.DateofChange = DateTime.Now;
            newHistory.OldDomain = oldDetails.Description;
            newHistory.NewDomain = newDetails.Description;
            if (newDetails.ActionOption == ActionOptions.delete)
            {
                newHistory.NewDomain = "התחום נמחק";
            }
            newHistory.OldAmount = oldDetails.Sum;
            newHistory.NewAmount = newDetails.Sum;
            //newHistory.ActionOption = newDetails.ActionOption;
            if (newDetails.ActionOption == ActionOptions.delete|| newDetails.ActionOption == ActionOptions.IsNotActive)
            {
                newHistory.NewAmount = noAmount;
            }
           

            dbService.entities.Histories.Add(newHistory);
            try
            {
                dbService.entities.SaveChanges();

                return true;

            }
            catch (Exception)
            {
                return false;

            }

        }
        public TithesDataDTO RepoTithe(int userId, SerchTitheDTO s)
        {
            if (s.AllDate)
            {
                DateTime x = dbService.entities.Movings.Where(x => x.User2Area.UserId == userId).OrderBy(m => m.Date).ToList()[0].Date;
                s.FromDate = x;
                s.ToDate = DateTime.Now;
            }
            TithesDataDTO result = new TithesDataDTO();
            result.TitheList = new List<TitheDTO>();
            var revenuesList = dbService.entities.Movings.Where(x => x.User2Area.UserId == userId
            && x.User2Area.Type == 1 && x.User2Area.IsMaaser == true && s.FromDate <= x.Date && s.ToDate >= x.Date).ToList();
            var expensesList = dbService.entities.Movings.Where(x => x.User2Area.UserId == userId &&
            x.User2Area.Type == 2 && x.User2Area.IsMaaser == true && s.FromDate <= x.Date && s.ToDate >= x.Date).ToList();
            //     s.FromDate = dbService.entities.Movings.Where(x => x.User2Area.UserId == userId && x.Date >= s.FromDate&&x.User2Area.IsMaaser==true).OrderBy(d => d.Date).ToList()[0].Date;

            for (DateTime date = s.FromDate; date <= s.ToDate; date = date.AddMonths(1))
            {
                TitheDTO t = new TitheDTO();
                t.SumOfRevenues = revenuesList.FindAll(x => x.Date.Month == date.Month && x.Date.Year == date.Year).Sum(x => x.Sum);
                t.SumOfExpenses = expensesList.FindAll(x => x.Date.Month == date.Month && x.Date.Year == date.Year).Sum(x => x.Sum);
                t.DateTithe = new DateTime(date.Year, date.Month, 1);
                if (t.SumOfExpenses > 0 || t.SumOfRevenues > 0)
                    result.TitheList.Add(t);
                else
                    result.SkipMonth = true;
            }

            return result;

        }

        public List<int> YearsMoovings(int userId)
        {
            return dbService.entities.Movings.Where(x => x.User2Area.UserId == userId && x.User2Area.IsMaaser == true).OrderBy(x => x.Date).Select(x => x.Date.Year).Distinct().ToList();
        }
    }

}

