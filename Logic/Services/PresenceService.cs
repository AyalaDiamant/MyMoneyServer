using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IPresenceService
    {
        List<PresenceDTO> GetPresences(int currentUserId, PresenceSearch presence);
        bool AddPresence(PresenceDTO presence, int currentUserId);
        bool UpdatePresence(PresenceDTO presence);
        bool DeletePresence(int id);


    }
    public class PresenceService : IPresenceService
    {
        private IDBService dBService;
        public PresenceService(IDBService dBService)
        {
            this.dBService = dBService;
        }
        public List<PresenceDTO> GetPresences(int currentUserId, PresenceSearch search)
        {
            List<PresenceDTO> presences = new List<PresenceDTO>();
            var presenceDb = dBService.entities.Presences.Where(x => x.UserId == currentUserId).ToList();
            //if (search.From != null && search.To != null)
            //{
            //    presenceDb = presenceDb.Where(x => x.Start >= search.From && x.Start <= search.To).ToList();
            //}
            if (search.From != null)
            {
                presenceDb = presenceDb.Where(x => x.Start >= search.From).ToList();
            }
            if (search.To != null)
            {
                presenceDb = presenceDb.Where(x => x.Start <= search.To).ToList();
            }
            presences = presenceDb.Select(x => new PresenceDTO()
            {
                Id = x.Id,
                Start = x.Start,
                Finish = x.Finish,
                Note = x.Note,
                UserId = currentUserId
            }).ToList();
            return presences;

        }
        public bool AddPresence(PresenceDTO presence, int currentUserId)
        {
            var pres = new Presence()
            {
                Start = presence.Start,
                Finish = presence.Finish,
                Note = presence.Note,
                UserId = currentUserId
            };
            dBService.entities.Presences.Add(pres);
            dBService.Save();
            return true;
        }

        public bool UpdatePresence(PresenceDTO presence)
        {
            var pres = dBService.entities.Presences.FirstOrDefault(x => x.Id == presence.Id);
            if (pres == null) return false;
            pres.Start = presence.Start;
            pres.Finish = presence.Finish;
            pres.Note = presence.Note;
            dBService.Save();
            return true;
        }

        public bool DeletePresence(int id)
        {
            var pres = dBService.entities.Presences.FirstOrDefault(x => x.Id == id);
            if (pres == null) return false;
            dBService.entities.Presences.Remove(dBService.entities.Presences.FirstOrDefault(x => x.Id == id));
            dBService.Save();
            return true;
        }


    }
}
