namespace ModelShop.Data.Contracts
{
    public interface IRepository<T> where T: class
    {
        ICollection<T> GetAll();

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<ICollection<T>> GetAllAsync();
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);

        void Save();
    }
}
