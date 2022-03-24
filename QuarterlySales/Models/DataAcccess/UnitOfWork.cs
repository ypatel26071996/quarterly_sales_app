using QuarterlySales.Models.DomainModels;

namespace QuarterlySales.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private SalesContext context { get; set; }
        public UnitOfWork(SalesContext ctx) => context = ctx;

        private Repository<Employee> employees;
        public Repository<Employee> Employees {
            get {
                if (employees == null) employees = new Repository<Employee>(context);
                return employees;
            }
        }

        private Repository<Sales> sales;
        public Repository<Sales> Sales {
            get {
                if (sales == null) sales = new Repository<Sales>(context);
                return sales;
            }
        }

        private Repository<User> users;
        public Repository<User> Users
        {
            get
            {
                if (users == null) users = new Repository<User>(context);
                return users;
            }
        }

        
        public void Save() => context.SaveChanges();

        void IUnitOfWork.Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
