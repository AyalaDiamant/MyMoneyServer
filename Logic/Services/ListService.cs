using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IListService
    {
        ListsDTO GetAllLists(int userId,int managerId);
        List<IdName> GetList(IdNameDB item,int userId, int managerId);
        bool AddItem(IdNameDB idName,int userId,int managerId);
        bool DeleteItem(IdNameDB item, int managerId);
        bool UpdateItem(IdNameDB idName,int user, int managerId);
    }
    public class ListService : IListService
    {
        private IDBService dbService;
        //private User currentUser;
        //private int currentUserId;
        public ListService(IDBService dbService)
        {
            this.dbService = dbService;
            //this.currentUser = dbService.entities.Users.FirstOrDefault(x => x.Id == currentUserId);

        }

        public ListsDTO GetAllLists(int userId, int managerId)
        {
            var lists = new ListsDTO();
            lists.UserTypes = dbService.entities.UserTypes.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description
            }).ToList();
            lists.Cities = dbService.entities.Cities.Where(x=>x.ManagerId==managerId).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Name,
                ManagerId=x.ManagerId

            }).ToList();
            lists.Statuses = dbService.entities.Statuses.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description,
                ManagerId = x.ManagerId

            }).ToList();
            lists.Areas = dbService.entities.Areas.Where(x => x.ManagerId == managerId).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description,
                Type = x.Type,
                ManagerId = managerId
            }).ToList();
            lists.UrgencyDebts = dbService.entities.UrgencyDebts.Where(x => x.ManagerId == managerId).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description,
                ManagerId = managerId

            }).ToList();
            lists.Lenders = dbService.entities.Users.Where(x => x.UserTypeId == 2 && x.IsActive).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToList();
            lists.Managers = dbService.entities.Users.Where(x => x.UserTypeId == 4 && x.IsActive).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToList();

            lists.PayOption = dbService.entities.PayOptions.Where(x => x.ManagerId == userId).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description,
                ManagerId= x.ManagerId

            }).ToList();

            return lists;
        }

        public List<IdName> GetList(IdNameDB item, int userId,int managerId)
        {            
            //var currentUser = dbService.entities.Users.FirstOrDefault(x => x.Id == userId);
            List<IdName> list = new List<IdName>();
            if (item.TableCode == TableCode.UserTypes)
            {
                list = dbService.entities.UserTypes.Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                }).ToList();
            }
            else if (item.TableCode == TableCode.Cities)
            {
                list = dbService.entities.Cities.Where(x => x.ManagerId == managerId).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ManagerId = x.ManagerId
                }).ToList();
            }
            else if (item.TableCode == TableCode.Status)
            {
                list = dbService.entities.Statuses.Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                    ManagerId = x.ManagerId

                }).ToList();
            }
            else if (item.TableCode == TableCode.UrgencyDebt)
            {
                list = dbService.entities.UrgencyDebts.Where(x => x.ManagerId == managerId).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                    ManagerId = x.ManagerId

                }).ToList();
            }
            else if (item.TableCode == TableCode.Areas)
                list = dbService.entities.Areas.Where(x => x.ManagerId==managerId).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                    Type = x.Type,
                    ManagerId =x.ManagerId

                }).ToList();
            //else if (item.TableCode == TableCode.Users)
            //{
            //    list = dbService.entities.Users.Where(x => x.UserTypeId == item.Type && x.IsActive).Select(x => new IdName()
            //    {
            //        Id = x.Id,
            //        Name = x.FirstName + " " + x.LastName
            //    }).ToList();
            //}
            else if (item.TableCode == TableCode.Users)
            {
                list = dbService.entities.Users.Where(x => x.ManagerId == managerId ).Select(x => new IdName()
                {
                    Id = x.Id,
                    ManagerId=x.ManagerId,
                   
                    Name = x.FirstName + " " + x.LastName
                }).ToList();
            }
            else if (item.TableCode == TableCode.PayOption)
            {
                list = dbService.entities.PayOptions.Where(x => x.ManagerId == userId ).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                    ManagerId=x.ManagerId
                
                }).ToList();

            }

            return list;
        }

        public bool AddItem(IdNameDB idName, int userId, int managerId)
        {
            switch (idName.TableCode)
            {
                case TableCode.UserTypes:
                    {
                        return AddUserType(idName);
                    }
                case TableCode.Cities:
                    {
                        return AddCity(idName, userId,managerId);
                    }
                case TableCode.Areas:
                    {
                        return AddSubject(idName,userId,managerId);
                    }
                case TableCode.UrgencyDebt:
                    {
                        return AddUrgencyDebt(idName,userId,managerId);
                    }
                case TableCode.PayOption:
                    {
                        return AddPayOptions(idName, userId);
                    }
                default:
                    break;
            }
            return false;
        }

        public bool DeleteItem(IdNameDB item, int managerId)
        {
            switch (item.TableCode)
            {
                //case TableCode.UserTypes:
                //    {
                //        return DeleteUserType(idName.Id);
                //    }
                case TableCode.Cities:
                    {
                        return DeleteCity(item.Id);
                    }
                case TableCode.Areas:
                    {
                        return DeleteAreas(item.Id);
                    }
                case TableCode.UrgencyDebt:
                    {
                        return DeleteUrgencyDebt(item.Id);
                    }
                case TableCode.PayOption:
                    {
                        return DeletePayOption(item.Id);
                    }
                default:
                    break;
            }
            return false;
        }

        public bool UpdateItem(IdNameDB idName,int userId, int managerId)
        {
            switch (idName.TableCode)
            {
                //case TableCode.UserTypes:
                //    {
                //        return UpdateUserType(idName);
                //    }
                case TableCode.Cities:
                    {
                        return UpdateCity(idName);
                    }
                case TableCode.Areas:
                    {
                        return UpdateSubject(idName);
                    }
                case TableCode.UrgencyDebt:
                    {
                        return UpdateUrgencyDebt(idName);
                    }
                case TableCode.PayOption:
                    {
                        return UpdatePayOption(idName,userId);
                    }
                default:
                    break;
            }
            return false;

        }

        private bool UpdateCity(IdNameDB idName)
        {
            var dbCity = dbService.entities.Cities.FirstOrDefault(x => x.Id == idName.Id);
            if (dbCity == null) return false;
            dbCity.Name = idName.Name;
            dbService.Save();
            return true;
        }

        private bool UpdateUserType(IdName idName)
        {
            var dbUserType = dbService.entities.UserTypes.FirstOrDefault(x => x.Id == idName.Id);
            if (dbUserType == null) return false;
            dbUserType.Description = idName.Name;
            dbService.Save();
            return true;
        }

        private bool UpdateSubject(IdName idName)
        {
            var dbSubject = dbService.entities.Areas.FirstOrDefault(x => x.Id == idName.Id);
            if (dbSubject == null) return false;
            dbSubject.Description = idName.Name;
            dbService.Save();
            return true;
        }

        private bool UpdateUrgencyDebt(IdName idName)
        {
            var dBurgencyDebt = dbService.entities.UrgencyDebts.FirstOrDefault(x => x.Id == idName.Id);
            if (dBurgencyDebt != null)
            {
                dBurgencyDebt.Description = idName.Name;
                dbService.Save();
                return true;
            }
            return false;
        }
        private bool UpdateStatus(IdName idName)
        {
            var dBStatuses = dbService.entities.Statuses.FirstOrDefault(x => x.Id == idName.Id);
            if (dBStatuses != null)
            {
                dBStatuses.Description = idName.Name;
                dbService.Save();
                return true;
            }
            return false;
        }

        public bool UpdatePayOption(IdName payOpt, int CurrentUserId)
        {
            if (dbService.entities.PayOptions.Any(x => x.ManagerId == CurrentUserId && x.Id == payOpt.Id && x.Description == payOpt.Name))
            {
                return true;
            }
            var dbDescrip = dbService.entities.PayOptions.FirstOrDefault(x => x.ManagerId == CurrentUserId && x.Id == payOpt.Id);
            if (dbDescrip != null)
            {
                dbDescrip.Description = payOpt.Name;
                dbDescrip.IsActive = payOpt.IsActive;
                dbService.entities.SaveChanges();
                return true;
            }
            return false;
        }
        private bool DeleteUserType(int userTypeId)
        {
            var userType = dbService.entities.UserTypes.FirstOrDefault(x => x.Id == userTypeId);
            if (userType != null)
            {
                dbService.entities.UserTypes.Remove(dbService.entities.UserTypes.FirstOrDefault(x => x.Id == userTypeId));
                dbService.Save();
                return true;
            }
            return false;

        }

        private bool DeleteCity(int id)
        {
            var city = dbService.entities.Cities.FirstOrDefault(x => x.Id == id);
            if (city != null)
            {
                dbService.entities.Cities.Remove(dbService.entities.Cities.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool DeleteAreas(int id)
        {
            var ar = dbService.entities.Areas.FirstOrDefault(x => x.Id == id);
            if (ar != null)
            {
                dbService.entities.Areas.Remove(dbService.entities.Areas.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool DeleteUrgencyDebt(int id)
        {
            if (dbService.entities.UrgencyDebts.Any(x => x.Id == id))
            {
                dbService.entities.UrgencyDebts.Remove(dbService.entities.UrgencyDebts.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        public bool DeletePayOption(int id)
        {
          
            var dbPayOpt = dbService.entities.PayOptions.FirstOrDefault(x=> x.Id == id);
            if (dbPayOpt != null)
            {
                if (dbPayOpt.Movings != null && dbPayOpt.Movings.Count > 0)
                {
                    dbPayOpt.IsActive = false;
                    dbService.entities.SaveChanges();
                    return true;
                }
                else
                {
                    dbService.entities.PayOptions.Remove(dbService.entities.PayOptions.FirstOrDefault(x => x.Id == id));
                    dbService.entities.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        private bool DeleteStatus(int id)
        {
            if (dbService.entities.Statuses.Any(x => x.Id == id))
            {
                dbService.entities.Statuses.Remove(dbService.entities.Statuses.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool AddUserType(IdName idName)
        {
            if (dbService.entities.UserTypes.Any(x => x.Description == idName.Name )) return false;
            var newItem = new UserType()
            {
                Description = idName.Name
            };
            dbService.entities.UserTypes.Add(newItem);
            dbService.Save();
            return true;
        }

        private bool AddCity(IdNameDB idName, int currentUserId,int managerId)
        {
            if (dbService.entities.Cities.Any(x => x.Name == idName.Name && x.ManagerId==managerId)) return false;
             var currentUser = dbService.entities.Users.FirstOrDefault(x => x.Id == currentUserId);
            var newItem = new City()
            {
                Name = idName.Name,
                ManagerId = managerId
            };
            dbService.entities.Cities.Add(newItem);
            dbService.Save();
            return true;
        }

        private bool AddSubject(IdNameDB idName, int currentUserId, int managerId)
        {
            if (dbService.entities.Areas.Any(x => x.Description == idName.Name && x.ManagerId == managerId))
            {
                return false;
            }
            var newItem = new Area()
            {
                Description = idName.Name,
                Type = (int)idName.Type,
                ManagerId=managerId
            };
            dbService.entities.Areas.Add(newItem);
            dbService.Save();
            return true;
        }

        private bool AddUrgencyDebt(IdNameDB idName,int userId,int managerId)
        {
            if (dbService.entities.UrgencyDebts.Any(x => x.Description == idName.Name && x.ManagerId == managerId))
            {
                return false;
            }
            var newItem = new UrgencyDebt()
            {
                Description = idName.Name,
                ManagerId= managerId
            };
            dbService.entities.UrgencyDebts.Add(newItem);
            dbService.Save();
            return true;
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
    }
}
