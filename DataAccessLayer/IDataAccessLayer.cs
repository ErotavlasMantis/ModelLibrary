namespace DataAccessLayer

{
    public interface IDataAccessLayer<T>
        {
            public void Add(T entity);

            public void Update(T entity);

            public void Delete(object ID);

            public T GetByID(object ID);

            public IEnumerable<T> GetAll();

            public void Save();
        }
}
