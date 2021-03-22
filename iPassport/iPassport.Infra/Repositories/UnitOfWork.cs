using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;

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
                    _transactionPassport.Dispose();
                }
                catch (Exception)
                {
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
                try
                {
                    _transactionIdentity.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
