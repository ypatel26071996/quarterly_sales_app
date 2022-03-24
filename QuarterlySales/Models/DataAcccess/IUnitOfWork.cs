using QuarterlySales.Models.DomainModels;

namespace QuarterlySales.Models
{
    public interface IUnitOfWork
    {
        Repository<Employee> Employees { get; }
        Repository<Sales> Sales { get; }

        Repository<User> Users { get; }

        void Save();
    }
}
