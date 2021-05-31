using System;
using Infrastructure;

namespace Service
{
    public interface IUnitOfWork : IDisposable
    {
        AppDataBaseContext Context { get; }
        void Save();
        void Dispose(bool disposing);
        new void Dispose();
    }

    public class UnitOfWork : IUnitOfWork
    {

        public AppDataBaseContext Context { get; }

        public UnitOfWork(AppDataBaseContext context)
        {
            Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    Context.Dispose();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
