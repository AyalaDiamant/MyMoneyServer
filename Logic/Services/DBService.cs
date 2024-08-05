using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IDBService
    {
        MyMoneyBContext entities { get; }

        int Save();
        void DisposeInternal();
    }

    public class DBService : IDBService
    {
        private ConcurrentDictionary<int, MyMoneyBContext> threads;
        private IDbContextFactory<MyMoneyBContext> dbContextFactory;
        private IConfiguration configuration;

        public DBService(IDbContextFactory<MyMoneyBContext> dbContextFactory, IConfiguration configuration)
        {
            threads = new ConcurrentDictionary<int, MyMoneyBContext>();
            this.dbContextFactory = dbContextFactory;
            this.configuration = configuration;
        }

        public MyMoneyBContext entities
        {
            get
            {
                return threads.GetOrAdd(Thread.CurrentThread.ManagedThreadId, x =>
                {
                    var ef = dbContextFactory.CreateDbContext();
                    return ef;
                });
            }
        }

        public int Save()
        {
            return entities.SaveChanges();
        }

        public void DisposeInternal()
        {
            if (threads.TryRemove(Thread.CurrentThread.ManagedThreadId, out MyMoneyBContext entities1))
            {
                entities1.Dispose();
            }
        }
    }
}
