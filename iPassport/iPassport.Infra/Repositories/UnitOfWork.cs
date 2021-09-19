using iPassport.Application.Exceptions;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace iPassport.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly iPassportContext _contextPassport;
        private readonly PassportIdentityContext _contextIdentityPassport;
        private IDbContextTransaction _transactionPassport;
        private IDbContextTransaction _transactionIdentity;

        public UnitOfWork(PassportIdentityContext contextIdentityPassport, iPassportContext contextPassport)
        {
            _contextPassport = contextPassport;
            _contextIdentityPassport = contextIdentityPassport;
        }

        public void BeginTransactionPassport()
        {
            _transactionPassport = _contextPassport.Database.BeginTransaction();
        }
        public void CommitPassport()
        {
            try
            {
                _contextPassport.SaveChanges();
                _transactionPassport.Commit();
            }
            finally
            {
                _transactionPassport.Dispose();
            }
        }
        public void RollbackPassport()
        {
            if (_transactionPassport != null)
            {
                _transactionPassport.Rollback();
                try
                {
                    _contextPassport.ChangeTracker.Entries()
                       .Where(e => e.Entity != null && e.State == EntityState.Added).ToList()
                       .ForEach(e => e.State = EntityState.Detached);
                    _transactionPassport.Dispose();
                }
                catch (Exception e)
                {
                    throw new PersistenceException(e);
                }
            }
        }

        public void BeginTransactionIdentity()
        {
            _transactionIdentity = _contextIdentityPassport.Database.BeginTransaction();
        }                
        public void CommitIdentity()
        {
            try
            {
                _contextIdentityPassport.SaveChanges();
                _transactionIdentity.Commit();
            }
            finally
            {
                _transactionIdentity.Dispose();
            }
        }
        public void RollbackIdentity()
        {
            if (_transactionIdentity != null)
            {
                _transactionIdentity.Rollback();
                _contextIdentityPassport.ChangeTracker.Entries()
                   .Where(e => e.Entity != null && e.State == EntityState.Added).ToList()
                   .ForEach(e => e.State = EntityState.Detached);
                try
                {
                    _transactionIdentity.Dispose();
                }
                catch (Exception e)
                {
                    throw new PersistenceException(e);
                }
            }
        }
    }
}
