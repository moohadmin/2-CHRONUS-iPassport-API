namespace iPassport.Domain.Repositories
{
    public interface IUnitOfWork
    {
        void BeginTransactionPassport();
        void CommitPassport();
        void RollbackPassport();

        void BeginTransactionIdentity();
        void CommitIdentity();
        void RollbackIdentity();
    }
}
