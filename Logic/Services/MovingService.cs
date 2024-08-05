using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IMovingService
    {
        List<MovingDTO> GetMovings(Search search, int CurrentUserId);
        //    object SumOfTithe();

        bool AddMove(MovingDTO move, int CurrentUserId);
        bool UpdateMove(MovingDTO move, int CurrentUserId);
        bool DeleteMove(int id, int CurrentUserId);


        Filters GetFilters(int type, int CurrentUserId);

    }
    public class MovingService : IMovingService
    {
        private IDBService dbService;
        private IPayOptionService payOptionService;
        private IAreaServies subjectService;


        public MovingService(IDBService dbService, IPayOptionService payOptionService, IAreaServies subjectService)
        {
            this.dbService = dbService;
            this.payOptionService = payOptionService;
            this.subjectService = subjectService;

        }
        public List<MovingDTO> GetMovings(Search search, int CurrentUserId)
        {

            List<MovingDTO> list = new List<MovingDTO>();
            bool isCurrentMonth = false;
            var query = dbService.entities.Movings.Where(x => x.User2Area.UserId == CurrentUserId).ToList();
            if (search.Type > 0)
            {
                query = query.Where(x => x.User2Area.Type == search.Type).ToList();
            }

            if (search.IsToFullMaaser)
            {
                return SetList(query);
            }

            if (search.From != null && search.To != null)
            {
                DateTime from = (DateTime)search.From;
                DateTime to = (DateTime)search.To;
                query = query.Where(x => x.Date >= from && x.Date <= to).ToList();

                isCurrentMonth = from.Month == to.Month;
            }
            else
            {
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                isCurrentMonth = true;

                query = query.Where(x => x.Date.Month == month && x.Date.Year == year).ToList();
            }

            if (search.User2AreaId != null && search.User2AreaId > 0)
            {
                query = query.Where(x => x.User2AreaId == search.User2AreaId).ToList();
            }

            if (search.PayOptionId != null && search.PayOptionId > 0)
            {
                query = query.Where(x => x.PayOptionId == search.PayOptionId).ToList();
            }

            list = SetList(query);


            if (search.IsToFullMaaser)
            {
                return SetList(query);
            }

            query = query.Where(x => x.Date >= search.From && x.Date <= search.To).ToList();

            isCurrentMonth = ((DateTime)search.From).Month == ((DateTime)search.To).Month;

            if (search.PayOptionId != null && search.PayOptionId > 0)
            {
                query = query.Where(x => x.PayOptionId == search.PayOptionId).ToList();
            }

            list = SetList(query);

            if (isCurrentMonth)
            {
                list = CheckDeviation(CurrentUserId, list);
            }

            return list;
        }

        private List<MovingDTO> CheckDeviation(int currentUserId, List<MovingDTO> list)
        {
            //var subjectsOfUser = dbService.entities.Subjects.Where(x => x.User2Subject.Any(u => u.UserId == currentUserId && x.Type == (int)MoovingType.Expenses)).ToList();
            var subjectsOfUser = dbService.entities.User2Areas.Where(x => x.UserId == currentUserId && x.Type == (int)MoovingType.Expenses).ToList();

            foreach (var u2s in subjectsOfUser)
            {
                int sum = list.Where(x => x.UserArea.Id == u2s.Id).Sum(x => x.Sum);
                if (sum > u2s.Sum)
                {
                    foreach (var item in list)
                    {
                        if (item.UserArea.Id == u2s.Id)
                        {
                            item.IsDeviation = true;
                        }
                    }
                }
            }
            return list;
        }
        private List<MovingDTO> SetList(List<Moving> query)
        {
            return query.Select(x => new MovingDTO()
            {
                Id = x.Id,
                Common = x.Common,
                Date = x.Date,
                PayOption = new IdName()
                {
                    Id = x.PayOptionId,
                    Name = x.PayOption.Description
                },
                UserArea = new IdName()
                {
                    Id = x.User2AreaId,
                    Name = x.User2Area.Description,
                    Type = x.User2Area.Type,
                },
                Sum = x.Sum
            }).OrderBy(x => x.Date).ToList();
        }
        public bool AddMove(MovingDTO move, int CurrentUserId)
        {
            //if (!move.Duplicate && dbService.entities.Movings.Any(x => x.UserId == CurrentUserId && x.User2SubjectId == move.UserToSubject.Id && x.Date == move.Date && x.Sum == move.Sum && x.Common == move.Common && x.PayOptionId == move.PayOption.Id && x.Description == move.Description))
            //{
            //    return false;
            //}
            var newMove = new Moving()
            {
                Common = move.Common,
                Date = move.Date,
                PayOptionId = move.PayOption.Id,
                User2AreaId = move.UserArea.Id,
                Sum = move.Sum,
                // UserId = CurrentUserId
            };
            dbService.entities.Movings.Add(newMove);
            dbService.Save();
            //newMove.PayOptionId = move.PayOption.Id;
            //newMove.SubjectId = move.Subject.Id;
            dbService.Save();
            return true;
        }
        public bool UpdateMove(MovingDTO move, int CurrentUserId)
        {
            //if (!move.Duplicate && dbService.entities.Movings.Any(x => x.UserId == CurrentUserId && x.Id != move.Id && x.User2SubjectId == move.UserToSubject.Id && x.Date == move.Date && x.Sum == move.Sum && x.Common == move.Common && x.PayOptionId == move.PayOption.Id && x.Description == move.Description))
            //{
            //    return false;
            //}
            var dbMove = dbService.entities.Movings.FirstOrDefault(x => x.User2Area.UserId == CurrentUserId && x.Id == move.Id);
            dbMove.Common = move.Common;
            dbMove.Date = move.Date;
            dbMove.PayOptionId = move.PayOption.Id;
            dbMove.User2AreaId = move.UserArea.Id;
            dbMove.Sum = move.Sum;
            //dbMove.UserId = CurrentUserId;

            dbService.Save();
            return true;
        }
        public bool DeleteMove(int id, int CurrentUserId)
        {
            var dbMove = dbService.entities.Movings.FirstOrDefault(x => x.User2Area.UserId == CurrentUserId && x.Id == id);
            if (dbMove != null)
            {
                dbService.entities.Movings.Remove(dbService.entities.Movings.FirstOrDefault(x => x.User2Area.UserId == CurrentUserId && x.Id == id));
                dbService.entities.SaveChanges();
                return true;
            }
            return false;

        }
        public Filters GetFilters(int type, int CurrentUserId)
        {
            var filters = new Filters()
            {
                PayOptions = payOptionService.GetPayOptions(CurrentUserId),
                Areas = subjectService.GetAreas(CurrentUserId, type)
            };
            return filters;
        }


     







        //       public object  SumOfTithe()
        //  {


        //   var query = from moving in dbService.entities.Movings
        //   join user2Area in dbService.entities.User2Areas
        //   on moving.User2AreaId equals user2Area.Id
        //   where user2Area.IsMaaser ==true
        //    select new
        //    {
        //  Moving = moving,
        ////  User2Area = user2Area
        //   };

        // Execute the query to retrieve the results
        //   var result = query.ToList();
        //   return result;
        //   }




        //public bool SetMoveToUser2Sub()
        //{
        //    var listMove = dbService.entities.Movings.ToList();
        //    var subs = dbService.entities.Subjects.ToList();

        //    foreach (var item in subs)
        //    {
        //        var u2s = new User2Subject()
        //        {

        //        };
        //        dbService.entities.User2Subject.Add(u2s);
        //        save

        //        var ms = listMove.Where(x => x.SubjectId == item.Id);
        //        foreach (var m in ms)
        //        {
        //            m.userToSubjectId = u2s.Id;
        //            save
        //        }

        //    }




        //    return true;
    }

}
