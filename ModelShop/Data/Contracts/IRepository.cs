namespace ModelShop.Data.Contracts
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        void Save();
    }
}
