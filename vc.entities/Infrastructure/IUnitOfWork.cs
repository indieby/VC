namespace vc.data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}