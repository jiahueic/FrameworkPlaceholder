using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.Business.Workflows;
using AGTIV.Framework.MVC.Data.Context;
using AGTIV.Framework.MVC.Data.Repositories;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AppDbContext _context;
        private IRepository _repository;
        private IWorkflowRepository _workflowRepository;

        public UnitOfWork(
            AppDbContext context,
            IRepository repository,
            IWorkflowRepository workflowRepository)
        {
            _context = context;
            _repository = new Repository(_context);
            _workflowRepository = workflowRepository;
        }

        public IRepository Repository
        {
            get { return _repository; }
        }

        public IWorkflowRepository WorkflowRepository
        {
            get { return _workflowRepository; }
        }

        public int Save()
        {
            return _context.ExtendedSaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
